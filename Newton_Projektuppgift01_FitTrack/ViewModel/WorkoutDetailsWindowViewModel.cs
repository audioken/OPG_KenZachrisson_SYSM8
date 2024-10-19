using Newton_Projektuppgift01_FitTrack.Model;
using Newton_Projektuppgift01_FitTrack.MVVM;

namespace Newton_Projektuppgift01_FitTrack.ViewModel
{
    public class WorkoutDetailsWindowViewModel : ViewModelBase
    {
        //public User User { get; set; }
        public Workout Workout { get; set; }

        public WorkoutDetailsWindowViewModel()
        {
            //User = Manager.Instance.CurrentUser;        
            Workout = Manager.Instance.CurrentWorkout;
        }

        public void EditWorkout()
        {
            // Kod för att redigera workout
        }

        public void SaveWorkout()
        {
            // Kod för att spara ändringar av workout
        }
    }
}
