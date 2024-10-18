using Newton_Projektuppgift01_FitTrack.Model;

namespace Newton_Projektuppgift01_FitTrack.ViewModel
{
    public class WorkoutWindowViewModel
    {
        public User User { get; set; }
        public List<Workout> WorkoutList { get; set; }

        //public RelayCommand UserDetailsCommand => new RelayCommand(execute => OpenDetails());

        public WorkoutWindowViewModel()
        {
            // Håller koll på nuvarande användare
            User = Manager.Instance.CurrentUser;
        }

        //public WorkoutWindowViewModel(User User, List<Workout> WorkoutList)
        //{
        //    this.User = User;
        //    this.WorkoutList = WorkoutList;
        //}

        public void AddWorkout()
        {
            // Öppna -> AddWorkoutWindow
        }

        public void RemoveWorkout()
        {
            // Kod för att ta bort workout
        }

        public void OpenDetails(Workout workout)
        {
            // Öppna -> WorkoutDetailsWindow
        }
    }
}
