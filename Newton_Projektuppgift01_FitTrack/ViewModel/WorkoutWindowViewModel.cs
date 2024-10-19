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
        public User User { get; set; } // Håller koll på inloggad användare
        public ObservableCollection<Workout> WorkoutList { get; set; } // Håller koll på alla workouts
        public Workout Workout { get; set; }
        public Workout SelectedWorkout { get; set; }

        // Relay-kommando som öppnar olika fönster vid klick
        public RelayCommand UserDetailsCommand => new RelayCommand(execute => OpenUserDetails());
        public RelayCommand AddWorkoutCommand => new RelayCommand(execute => AddWorkout());
        public RelayCommand OpenDetailsCommand => new RelayCommand(execute => OpenWorkoutDetails());
        public RelayCommand RemoveWorkoutCommand => new RelayCommand(execute => RemoveWorkout());
        public RelayCommand AppInfoCommand => new RelayCommand(execute => AppInfo());
        public RelayCommand SignOutCommand => new RelayCommand(execute => SignOut());

        // KONSTRUKTOR ↓
        public WorkoutWindowViewModel()
        {
            // Håller koll på nuvarande användare
            User = Manager.Instance.CurrentUser;

            // Hämtar användarens lista för träningspass
            WorkoutList = User.UserWorkouts;
        }

        // METODER ↓
        public void AddWorkout()
        {
            // Öppnar "AddWorkoutWindow"
            AddWorkoutWindow addWorkoutWindow = new AddWorkoutWindow();
            addWorkoutWindow.Show();

            // KOD HÄR för att stänga detta fönster?
        }

        public void RemoveWorkout()
        {
            if (SelectedWorkout != null)
            {
                // Ta bort träningspass
                WorkoutList.Remove(SelectedWorkout);
            }
            else
            {
                MessageBox.Show("Du måste välja något i listan!");
            }
        }

        public void OpenWorkoutDetails()
        {
            if (SelectedWorkout != null)
            {
                Manager.Instance.CurrentWorkout = SelectedWorkout;

                // Öppnar "WorkoutDetailsWindow"
                WorkoutDetailsWindow detailsWindow = new WorkoutDetailsWindow();
                detailsWindow.Show();

                // KOD HÄR för att stänga detta fönster?
            }
            else
            {
                MessageBox.Show("Du måste välja något i listan!");
            }
        }

        public void OpenUserDetails()
        {
            // Öppnar "UserDetailsWindow"
            UserDetailsWindow userDetailsWindow = new UserDetailsWindow();
            userDetailsWindow.Show();

            // KOD HÄR för att stänga detta fönster?
        }

        public void AppInfo()
        {
            // KOD HÄR för att öppna en nu ruta eller ett fönster med företagets och appens info
        }

        public void SignOut()
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();

            // KOD HÄR för att stänga detta fönster
        }
    }
}
