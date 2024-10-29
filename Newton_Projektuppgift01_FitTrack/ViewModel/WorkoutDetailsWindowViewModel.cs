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
        // Möjliggör stängning av detta fönster
        public Window _workoutDetailsWindow { get; set; }

        // Ska klona vald träning
        public Workout WorkoutEditable { get; set; }

        // Aktivererar eller avaktiverar redigering
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

        // Lista som visar det valda träningspasset
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

        // Relaykommandon som representerar knappklick
        public RelayCommand EditWorkoutCommand => new RelayCommand(execute => EditWorkout());
        public RelayCommand AbortEditCommand => new RelayCommand(execute => AbortEdit());
        public RelayCommand SaveWorkoutCommand => new RelayCommand(execute => SaveWorkout());
        public RelayCommand CopyWorkoutCommand => new RelayCommand(execute => CopyWorkout());
        public RelayCommand CancelCommand => new RelayCommand(execute => Cancel());

        // KONSTRUKTOR ↓
        public WorkoutDetailsWindowViewModel(Window _workoutDetailsWindow)
        {
            this._workoutDetailsWindow = _workoutDetailsWindow;

            // Beräknar brända kalorier för träningen
            Manager.Instance.CurrentWorkout.CaloriesBurned = Manager.Instance.CurrentWorkout.CalculateCaloriesBurned();

            // Klonar vald träning för att möjliggöra redigering och återställning ändringar
            WorkoutEditable = Manager.Instance.CurrentWorkout.Clone();

            // Lägger in träningen i lista för att kunna visas i DataGrid
            WorkoutList = new ObservableCollection<Workout> { WorkoutEditable };

            // Avaktiverar redigering initiellt
            IsDataGridReadOnly = true;
        }

        // METODER ↓
        // Aktivera redigering av träning
        public void EditWorkout()
        {
            // Ta bort skrivskydded från DataGrid
            IsDataGridReadOnly = false;
        }

        // Avbryter redigering och återställer värden
        public void AbortEdit()
        {
            // Klonar originalet för att återställa värdena
            WorkoutEditable = Manager.Instance.CurrentWorkout.Clone();

            // Lägger in den klonade träningen och ersätter listan
            WorkoutList = new ObservableCollection<Workout> { WorkoutEditable };

            // Avaktiverar redigering
            IsDataGridReadOnly = true;
        }

        // Sparar ändringar
        public void SaveWorkout()
        {
            // Hitta index för originalet av träningen som ändrats
            int indexOfWorkout = Manager.Instance.CurrentUser.UserWorkouts.IndexOf(Manager.Instance.CurrentWorkout);

            // Kontrollera så det är en vanlig användare samt att index för träningen finns
            if (Manager.Instance.CurrentUser is User && indexOfWorkout >= 0)
            {
                // Ersätt originalet med uppdaterad träning för användaren
                Manager.Instance.CurrentUser.UserWorkouts[indexOfWorkout] = WorkoutEditable;
            }
            // Kontrollera om det är en admin som är inloggad
            else if (Manager.Instance.CurrentUser is AdminUser)
            {
                // Kolla igenom alla användare i listan AllUsers
                foreach (User user in Manager.Instance.AllUsers)
                {
                    // Om en användare har den aktuella träningen som ska uppdateras
                    if (user.UserWorkouts.Contains(Manager.Instance.CurrentWorkout))
                    {
                        // Hitta index
                        int indexUserWorkout = user.UserWorkouts.IndexOf(Manager.Instance.CurrentWorkout);

                        // Ersätt med den redigerade klonen
                        user.UserWorkouts[indexUserWorkout] = WorkoutEditable;

                        break;
                    }
                }
            }
            else { MessageBox.Show("Något gick fel! Träningen kunde inte sparas.."); }

            // Avvaktivera möjlighet till redigering i DataGrid
            IsDataGridReadOnly = true;

            // Öppna WorkoutWindow
            OpenWorkoutWindow();

            // Stäng WorkoutDetailsWindow
            _workoutDetailsWindow.Close();
        }

        // Kopiera träning
        public void CopyWorkout()
        {
            // Hämta värdena och lagra i Managerklassen för enkel åtkomst
            Manager.Instance.CopiedWorkout = WorkoutEditable;

            MessageBox.Show("Kopierat!");
        }

        // Avbryt redigering och återgå till WorkoutWindow
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
