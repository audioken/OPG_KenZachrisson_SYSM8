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
        public ObservableCollection<Workout> WorkoutList { get; set; } // Håller koll på alla träningspass
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
        // Öppnar fönster för att kunna lägga till ett träningspass
        public void AddWorkout()
        {
            // Öppnar "AddWorkoutWindow"
            AddWorkoutWindow addWorkoutWindow = new AddWorkoutWindow();
            addWorkoutWindow.Show();

            // KOD HÄR för att stänga detta fönster?
        }

        // Tar bort det valda träningspasset
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

        // Öppna fönster för att hämta mer information av det valda träningspasset
        public void OpenWorkoutDetails()
        {
            // Om något är valt i listan
            if (SelectedWorkout != null)
            {
                // Tillfällig lagring av vald träning i managerklassen
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

        // Öppna fönster för användarens profilinställningar
        public void OpenUserDetails()
        {
            // Öppnar "UserDetailsWindow"
            UserDetailsWindow userDetailsWindow = new UserDetailsWindow();
            userDetailsWindow.Show();

            // KOD HÄR för att stänga detta fönster?
        }

        // Öppnar en popup med information om företaget och appen
        public void AppInfo()
        {
            // KOD HÄR för att öppna en ny ruta eller ett fönster med företagets och appens info
        }

        // Logga ut och återgå till MainWindow
        public void SignOut()
        {
            // Öppnar "MainWindow"
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();

            // KOD HÄR för att stänga detta fönster
        }
    }
}
