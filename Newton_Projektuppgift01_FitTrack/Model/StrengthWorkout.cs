
namespace Newton_Projektuppgift01_FitTrack.Model
{
    public class StrengthWorkout : Workout
    {
        public int Repetition { get; set; }

        public StrengthWorkout(DateTime Date, string Type, TimeSpan Duration, int CaloriesBurned, string Notes, int Repetition) : base(Date, Type, Duration, CaloriesBurned, Notes)
        {
            this.Repetition = Repetition;
        }

        public override int CalculateCaloriesBurned()
        {
            return 0; // Bara tillfäligt, behöver kod här
        }
    }
}
