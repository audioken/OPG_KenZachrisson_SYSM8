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
        // Möjliggör stängning av detta fönster
        public Window _workoutWindow { get; set; }

        // Spårar användarnamn och får sitt värde i konstruktorn
        public User User { get; } // Read-only

        // Den valda träningen från listan
        public Workout SelectedWorkout { get; set; }

        // Listan med träningspass
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

        // Tillfällig lista för att möjliggöra filtrering
        public ObservableCollection<Workout> FilteredWorkoutList { get; set; }

        // Sökfiltret med döljbar stödtext
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
                    PHSearchFilterVisibility = Visibility.Visible;
                }
                else
                {
                    PHSearchFilterVisibility = Visibility.Collapsed;
                }
            }
        }
        private Visibility pHSearchFilterVisibility;
        public Visibility PHSearchFilterVisibility
        {
            get { return pHSearchFilterVisibility; }
            set
            {
                pHSearchFilterVisibility = value;
                OnPropertyChanged();
            }
        }

        // Varaktighetsfiltret
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

            // Endast för att kunna vem som är inloggad
            User = Manager.Instance.CurrentUser;

            // Instansierar den temporära listan
            FilteredWorkoutList = new ObservableCollection<Workout>();

            // Kolla om admin är inloggad
            if (Manager.Instance.CurrentUser is AdminUser admin)
            {
                // Hämta alla användares träningspass
                WorkoutList = admin.ManageAllWorkouts();
            }
            else
            {
                // Hämta endast användarens egna träningspass
                WorkoutList = Manager.Instance.CurrentUser.UserWorkouts;
            }

            // Sätter startvärdet för slidern så alla pass visas
            DurationFilter = 0;

            // Uppdatera vyn
            ApplySearchFilter();
        }

        // METODER ↓
        // Öppnar fönster för att kunna lägga till ett träningspass
        private void AddWorkout()
        {
            // Öppnar AddWorkoutWindow
            OpenAddWorkoutWindow();

            // Stäng WorkoutWindow
            _workoutWindow.Close();
        }

        // Tar bort det valda träningspasset
        private void RemoveWorkout()
        {
            if (SelectedWorkout != null)
            {
                // Håller koll på om träningen raderats
                bool wasWorkoutRemoved = false;

                // Kolla igenom alla användare
                foreach (var user in Manager.Instance.AllUsers)
                {
                    // Leta efter träningen
                    if (user.UserWorkouts.Contains(SelectedWorkout))
                    {
                        // Ta bort från användarens träningar
                        user.UserWorkouts.Remove(SelectedWorkout);

                        // Träningen raderades
                        wasWorkoutRemoved = true;

                        // Avbryter iterering
                        break;
                    }
                }

                // Om träningen är raderad
                if (wasWorkoutRemoved)
                {
                    // Ta bort från träningslista
                    WorkoutList.Remove(SelectedWorkout);

                    // Uppdatera träningslista
                    OnPropertyChanged(nameof(WorkoutList));

                    // Uppdatera vyn
                    ApplySearchFilter();
                }
            }
            else { MessageBox.Show("Du måste välja något i listan!", "Missing input!", MessageBoxButton.OK, MessageBoxImage.Warning); }
        }

        // Öppna fönstret för detaljerad information om valt träningspass
        private void OpenWorkoutDetails()
        {
            // Om något är valt i listan
            if (SelectedWorkout != null)
            {
                // Tillfällig lagring av vald träning i managerklassen
                Manager.Instance.CurrentWorkout = SelectedWorkout;

                // Öppna WorkoutDetailsWindow
                OpenWorkoutDetailsWindow();

                // Stäng WorkoutWindow
                _workoutWindow.Close();
            }
            else { MessageBox.Show("Du måste välja något i listan!", "Missing input!", MessageBoxButton.OK, MessageBoxImage.Warning); }
        }

        // Öppna fönster för användarens profilinställningar
        private void OpenUserDetails()
        {
            // Öppnar UserDetailsWindow
            OpenUserDetailsWindow();

            // Stänger WorkoutWindow
            _workoutWindow.Close();
        }

        // Öppnar en popup med information om företaget och appen
        private void AppInfo()
        {
            CompanyInfoWindow companyInfoWindow = new CompanyInfoWindow();
            companyInfoWindow.Show();
        }

        // Logga ut och återgå till MainWindow
        private void SignOut()
        {
            // Rensar spårning av inloggad användare
            Manager.Instance.CurrentUser = null;

            // Öppnar MainWindow
            OpenMainWindow();

            // Stäng WorkoutWindow
            _workoutWindow.Close();
        }

        // Sökfilter med textruta för träningstyp och kommentarer
        private void ApplySearchFilter()
        {
            // Rensa den tillfälliga listan för träningspass
            FilteredWorkoutList.Clear();

            // Hämta relevanta träningar från originallistan
            foreach (var workout in WorkoutList)
            {
                // Hämta och visa alla träningar vid tom inmatning
                if (string.IsNullOrEmpty(SearchFilter))
                {
                    FilteredWorkoutList.Add(workout);
                }
                // Hämta och visa de träningar som inkluderar inmatad söktext
                else if (workout.Type.Contains(SearchFilter) || workout.Notes.Contains(SearchFilter))
                {
                    FilteredWorkoutList.Add(workout);
                }
            }
        }

        // Sökfilter med slider för varaktighet
        private void ApplyDurationFilter()
        {
            // Rensa den tillfälliga listan för träningspass
            FilteredWorkoutList.Clear();

            // Hämta relevanta träningar från originallistan
            foreach (var workout in WorkoutList)
            {
                // Hämta och visa de träningar som överstiger eller är samma som sökvärdet
                if (workout.Duration.TotalMinutes >= DurationFilter)
                {
                    FilteredWorkoutList.Add(workout);
                }
            }
        }

        // Öppnar olika fönster
        private void OpenUserDetailsWindow()
        {
            UserDetailsWindow userDetailsWindow = new UserDetailsWindow();
            userDetailsWindow.Show();
        }
        private void OpenWorkoutDetailsWindow()
        {
            WorkoutDetailsWindow workoutDetailsWindow = new WorkoutDetailsWindow();
            workoutDetailsWindow.Show();
        }
        private void OpenMainWindow()
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
        }
        private void OpenAddWorkoutWindow()
        {
            AddWorkoutWindow addWorkoutWindow = new AddWorkoutWindow();
            addWorkoutWindow.Show();
        }
    }
}
