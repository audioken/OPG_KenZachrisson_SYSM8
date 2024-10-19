
namespace Newton_Projektuppgift01_FitTrack.Model
{
    public class StrengthWorkout : Workout
    {
        public int Repetition { get; set; }

        public StrengthWorkout(DateTime Date, string Type, TimeSpan Duration, int CaloriesBurned, string Notes, int Repetition) : base(Date, Type, Duration, CaloriesBurned, Notes)
        {
            this.Repetition = Repetition;
        }

        // BEHÖVER FÖRSTÅ HUR VI SKA ANVÄNDA DENNA?
        public override int CalculateCaloriesBurned()
        {
            return Repetition * CaloriesBurned;
        }

        // Överskuggande metod som "kopierar" en träning och möjliggör redigering
        public override Workout Clone()
        {
            return new StrengthWorkout(Date, Type, Duration, CaloriesBurned, Notes, Repetition);
        }
    }
}
