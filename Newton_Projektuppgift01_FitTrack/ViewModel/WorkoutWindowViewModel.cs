using Newton_Projektuppgift01_FitTrack.Model;
using Newton_Projektuppgift01_FitTrack.MVVM;
using Newton_Projektuppgift01_FitTrack.View;
using System.Collections.ObjectModel;

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

        // KONSTRUKTOR ↓
        public WorkoutWindowViewModel()
        {
            // Håller koll på nuvarande användare
            User = Manager.Instance.CurrentUser;

            // Tillfällig träning för test
            DateTime dateTime = DateTime.Now;
            TimeSpan timeSpan = TimeSpan.FromSeconds(1);
            Workout = new StrengthWorkout(dateTime, "Strength Workout", timeSpan, 200, "Tough session", 5);
            WorkoutList = new ObservableCollection<Workout> { Workout };
        }

        // METODER ↓
        public void AddWorkout()
        {
            AddWorkoutWindow addWorkoutWindow = new AddWorkoutWindow();
            addWorkoutWindow.Show();

            // KOD HÄR för att stänga detta fönster?
        }

        public void RemoveWorkout()
        {
            WorkoutList.Remove(SelectedWorkout);
        }

        public void OpenWorkoutDetails()
        {
            WorkoutDetailsWindow detailsWindow = new WorkoutDetailsWindow();
            detailsWindow.Show();
        }

        public void OpenUserDetails()
        {
            // Öppnar "UserDetailsWindow"
            UserDetailsWindow userDetailsWindow = new UserDetailsWindow();
            userDetailsWindow.Show();

            // KOD HÄR för att stänga detta fönster?
        }
    }
}
