using Newton_Projektuppgift01_FitTrack.Model;

namespace Newton_Projektuppgift01_FitTrack.ViewModel
{
    public class WorkoutDetailsWindowViewModel
    {
        public Workout Workout { get; set; }

        public WorkoutDetailsWindowViewModel(Workout Workout)
        {
            this.Workout = Workout;
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
