using Newton_Projektuppgift01_FitTrack.Model;
using Newton_Projektuppgift01_FitTrack.MVVM;
using Newton_Projektuppgift01_FitTrack.View;
using System.Collections.ObjectModel;
using System.Windows;

namespace Newton_Projektuppgift01_FitTrack.ViewModel
{
    public class WorkoutDetailsWindowViewModel : ViewModelBase
    {
        // EGENSKAPER ↓
        public Window _workoutDetailsWindow { get; set; }

        public User User { get; set; }

        // Används för att lagra den valda träningen
        public Workout Workout { get; set; }

        // Ska klona Workout för att möjligöra redigering
        public Workout WorkoutEditable { get; set; }

        // Kontroll för om DataGrid går att redigera eller ej
        private bool isDataGridReadOnly;
        public bool IsDataGridReadOnly
        {
            get { return isDataGridReadOnly; }
            set
            {
                isDataGridReadOnly = value;
                OnPropertyChanged();
            }
        }

        private Visibility distanceVisibility;
        public Visibility DistanceVisibility
        {
            get { return distanceVisibility; }
            set
            {
                distanceVisibility = value;
                OnPropertyChanged();
            }
        }

        private Visibility repetitionVisibility;
        public Visibility RepetitionVisibility
        {
            get { return repetitionVisibility; }
            set
            {
                repetitionVisibility = value;
                OnPropertyChanged();
            }
        }

        // Tillfällig lista för att möjliggöra visning av träning i DataGrid
        private ObservableCollection<Workout> workoutList;
        public ObservableCollection<Workout> WorkoutList
        {
            get { return workoutList; }
            set
            {
                workoutList = value;
                OnPropertyChanged();
            }
        }

        // Relay-kommandon
        public RelayCommand EditWorkoutCommand => new RelayCommand(execute => EditWorkout());
        public RelayCommand AbortEditCommand => new RelayCommand(execute => AbortEdit());
        public RelayCommand SaveWorkoutCommand => new RelayCommand(execute => SaveWorkout());
        public RelayCommand CopyWorkoutCommand => new RelayCommand(execute => CopyWorkout());
        public RelayCommand CancelCommand => new RelayCommand(execute => Cancel());

        // KONSTRUKTOR ↓
        public WorkoutDetailsWindowViewModel(Window _workoutDetailsWindow)
        {
            this._workoutDetailsWindow = _workoutDetailsWindow;

            // Referens till inloggad användare
            User = Manager.Instance.CurrentUser;

            // Referens till den valda träningen
            Workout = Manager.Instance.CurrentWorkout;

            if (Workout is StrengthWorkout)
            {
                DistanceVisibility = Visibility.Collapsed;
                RepetitionVisibility = Visibility.Visible;
            }
            else if (Workout is CardioWorkout)
            {
                DistanceVisibility = Visibility.Visible;
                RepetitionVisibility = Visibility.Collapsed;
            }

            // Gör en beräkning av brända kalorier med den valda träningens metod
            Workout.CaloriesBurned = Workout.CalculateCaloriesBurned();

            // Klonar träning som backup för återställning vid klick på cancel
            WorkoutEditable = Workout.Clone();

            // Lägger in träningen i lista för att kunna visas i DataGrid
            WorkoutList = new ObservableCollection<Workout> { WorkoutEditable };

            // Avaktiverar DataGrid för att initiellt förhindra redigering
            IsDataGridReadOnly = true;



        }

        // METODER ↓
        // Möjliggör redigering av valt träningspass
        public void EditWorkout()
        {
            // Aktiverar redigering av DataGrid som visar Workout
            IsDataGridReadOnly = false;
        }

        // Avbryter redigering och återställer värdena
        public void AbortEdit()
        {
            // Klonar originalet för att återställa värdena
            WorkoutEditable = Workout.Clone();

            // Lägger in den klonade träningen i listan
            WorkoutList = new ObservableCollection<Workout> { WorkoutEditable };

            // Stänger av möjligheten att redigera träning i DataGrid
            IsDataGridReadOnly = true;
        }

        // Sparar ändringar
        public void SaveWorkout()
        {
            // Hitta index för originalet av träningen som ändras
            int indexOfWorkout = Manager.Instance.CurrentUser.UserWorkouts.IndexOf(Workout);

            // Kontrollera så det är en användare som är inloggad samt att index finns
            if (User is User && indexOfWorkout >= 0)
            {
                // Ersätt originalet med uppdaterad träning för den inloggade användaren
                Manager.Instance.CurrentUser.UserWorkouts[indexOfWorkout] = WorkoutEditable;
            }
            // Kontrollera om det är en Admin som är inloggad
            else if (User is AdminUser)
            {
                // Kolla igenom alla användare i listan AllUsers
                foreach (User user in Manager.Instance.AllUsers)
                {
                    // Om en användare har den aktuella träningen som ska uppdateras
                    if (user.UserWorkouts.Contains(Workout))
                    {
                        // Kolla dess index
                        int indexUser = user.UserWorkouts.IndexOf(Workout);

                        // Ersätt med den redigerade klonen
                        user.UserWorkouts[indexUser] = WorkoutEditable;

                        break;
                    }
                }
            }
            else
            {
                // Annars lägg till träning
                Manager.Instance.CurrentUser.UserWorkouts.Add(WorkoutEditable);
            }

            // Tror inte denna behövs?
            Manager.Instance.CurrentWorkout = WorkoutEditable;

            // Avvaktivera möjlighet till redigering i DataGrid
            IsDataGridReadOnly = true;

            // Öppna WorkoutWindow
            OpenWorkoutWindow();

            // Stäng WorkoutDetailsWindow
            _workoutDetailsWindow.Close();
        }

        public void CopyWorkout()
        {
            Manager.Instance.CopiedWorkout = WorkoutEditable;
            MessageBox.Show("Kopierat!");
        }

        public void Cancel()
        {
            // Öppna WorkoutWindow
            OpenWorkoutWindow();

            // Stäng WorkoutDetails
            _workoutDetailsWindow.Close();
        }

        // Öppna WorkoutWindow
        public void OpenWorkoutWindow()
        {
            WorkoutWindow workoutWindow = new WorkoutWindow();
            workoutWindow.Show();
        }
    }
}
