
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
            return Repetition * 10;
        }

        // Överskuggande metod som klonar en träning och möjliggör tillfällig redigering
        public override Workout Clone()
        {
            return new StrengthWorkout(Date, Type, Duration, CaloriesBurned, Notes, Repetition);
        }
    }
}
