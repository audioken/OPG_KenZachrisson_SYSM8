using Newton_Projektuppgift01_FitTrack.Model;
using Newton_Projektuppgift01_FitTrack.MVVM;
using Newton_Projektuppgift01_FitTrack.View;
using System.Collections.ObjectModel;
using System.Windows;

namespace Newton_Projektuppgift01_FitTrack.ViewModel
{
    public class WorkoutWindowViewModel : ViewModelBase
    {
        // EGENSKAPER ↓

        public Window _workoutWindow { get; set; }

        public User User { get; set; } // Håller koll på inloggad användare
        public AdminUser AdminUser { get; set; } // Håller koll på inloggad admin

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

        public ObservableCollection<Workout> FilteredWorkoutList { get; set; } = new ObservableCollection<Workout>();

        public Workout Workout { get; set; }
        public Workout SelectedWorkout { get; set; }

        private string searchFilter;
        public string SearchFilter
        {
            get { return searchFilter; }
            set
            {
                searchFilter = value;
                OnPropertyChanged();
                ApplySearchFilter();

                if (string.IsNullOrEmpty(searchFilter))
                {
                    PHSearchVisibility = "Visible";
                }
                else
                {
                    PHSearchVisibility = "Collapsed";
                }
            }
        }

        private int durationFilter;
        public int DurationFilter
        {
            get { return durationFilter; }
            set
            {
                durationFilter = value;
                OnPropertyChanged();
                ApplyDurationFilter();
            }
        }

        private string pHSearchVisibility;
        public string PHSearchVisibility
        {
            get { return pHSearchVisibility; }
            set
            {
                pHSearchVisibility = value;
                OnPropertyChanged();


            }
        }


        // Relay-kommando som öppnar olika fönster vid klick
        public RelayCommand UserDetailsCommand => new RelayCommand(execute => OpenUserDetails());
        public RelayCommand AddWorkoutCommand => new RelayCommand(execute => AddWorkout());
        public RelayCommand OpenDetailsCommand => new RelayCommand(execute => OpenWorkoutDetails());
        public RelayCommand RemoveWorkoutCommand => new RelayCommand(execute => RemoveWorkout());
        public RelayCommand AppInfoCommand => new RelayCommand(execute => AppInfo());
        public RelayCommand SignOutCommand => new RelayCommand(execute => SignOut());

        // KONSTRUKTOR ↓
        public WorkoutWindowViewModel(Window _workoutWindow)
        {
            this._workoutWindow = _workoutWindow;

            // Håller koll på nuvarande användare
            User = Manager.Instance.CurrentUser;

            // Visa träningspassen efter användare
            if (User is AdminUser admin)
            {
                // Hämta alla användares träningspass
                WorkoutList = admin.ManageAllWorkouts();
            }
            else
            {
                // Hämta endast användarens egna träningspass
                WorkoutList = User.UserWorkouts;
            }

            // Sätter startvärdet för slidern så alla pass visas
            DurationFilter = 0;

            // Uppdatera vyn
            ApplySearchFilter();
        }

        // METODER ↓
        // Öppnar fönster för att kunna lägga till ett träningspass
        public void AddWorkout()
        {
            // Öppnar AddWorkoutWindow
            OpenAddWorkoutWindow();

            // Stäng WorkoutWindow
            _workoutWindow.Close();
        }

        // Tar bort det valda träningspasset
        public void RemoveWorkout()
        {
            if (SelectedWorkout != null)
            {
                // Uppdatera alla användares träningslistor som innehåller detta träningspass
                foreach (var user in Manager.Instance.AllUsers)
                {
                    // Ta bort träningspass från användarens lista av träningspass
                    if (user.UserWorkouts.Contains(SelectedWorkout))
                    {
                        user.UserWorkouts.Remove(SelectedWorkout);
                        WorkoutList.Remove(SelectedWorkout);

                        OnPropertyChanged(nameof(user.UserWorkouts));

                        // Kör filtret för att uppdatera vyn
                        ApplySearchFilter();

                        // Behöver inte iterera igenom fler användare
                        break;
                    }
                }

                // Uppdatera listan
                OnPropertyChanged(nameof(WorkoutList));
            }
            else
            {
                MessageBox.Show("Du måste välja något i listan!");
            }
        }

        // Öppna fönster för att hämta mer information av det valda träningspasset
        public void OpenWorkoutDetails()
        {
            // Om något är valt i listan
            if (SelectedWorkout != null)
            {
                // Tillfällig lagring av vald träning i managerklassen
                Manager.Instance.CurrentWorkout = SelectedWorkout;

                // Öppnar WorkoutDetailsWindow
                OpenWorkoutDetailsWindow();

                // Stänger WorkoutWindow
                _workoutWindow.Close();
            }
            else
            {
                MessageBox.Show("Du måste välja något i listan!");
            }
        }

        // Öppna fönster för användarens profilinställningar
        public void OpenUserDetails()
        {
            // Öppnar UserDetailsWindow
            OpenUserDetailsWindow();

            // Stänger WorkoutWindow
            _workoutWindow.Close();
        }

        // Öppnar en popup med information om företaget och appen
        public void AppInfo()
        {
            // KOD HÄR för att öppna en ny ruta eller ett fönster med företagets och appens info
        }

        // Logga ut och återgå till MainWindow
        public void SignOut()
        {
            // Öppnar MainWindow
            OpenMainWindow();

            // Stäng WorkoutWindow
            _workoutWindow.Close();
        }

        // Sökfilter med textruta för träningstyp och kommentarer
        public void ApplySearchFilter()
        {
            // Rensa den tillfälliga listan för träningspass
            FilteredWorkoutList.Clear();

            // Gå igenom alla träningar som finns i "WorkoutList"
            foreach (var workout in WorkoutList)
            {
                // Om textrutan för sökfilter är tomt - visa alla träningar från orignallistan
                // Matchar sökordet - lägg endast till de matchande träningarna från originallistan
                if (string.IsNullOrEmpty(SearchFilter))
                {
                    FilteredWorkoutList.Add(workout);
                }
                else if (workout.Type.Contains(SearchFilter) || workout.Notes.Contains(SearchFilter))
                {
                    FilteredWorkoutList.Add(workout);
                }
            }
        }

        // Sökfilter med slider för varaktighet
        public void ApplyDurationFilter()
        {
            FilteredWorkoutList.Clear();

            foreach (var workout in WorkoutList)
            {
                if (workout.Duration.TotalMinutes >= DurationFilter)
                {
                    FilteredWorkoutList.Add(workout);
                }
            }
        }

        // Öppnar olika fönster
        public void OpenUserDetailsWindow()
        {
            UserDetailsWindow userDetailsWindow = new UserDetailsWindow();
            userDetailsWindow.Show();
        }
        public void OpenWorkoutDetailsWindow()
        {
            WorkoutDetailsWindow workoutDetailsWindow = new WorkoutDetailsWindow();
            workoutDetailsWindow.Show();
        }
        public void OpenMainWindow()
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
        }
        public void OpenAddWorkoutWindow()
        {
            AddWorkoutWindow addWorkoutWindow = new AddWorkoutWindow();
            addWorkoutWindow.Show();
        }
    }
}
