
namespace Newton_Projektuppgift01_FitTrack.Model
{
    public class CardioWorkout : Workout
    {
        public int Distance { get; set; }

        public CardioWorkout(DateTime Date, string Type, TimeSpan Duration, int CaloriesBurned, string Notes, int Distance) : base(Date, Type, Duration, CaloriesBurned, Notes)
        {
            this.Distance = Distance;
        }

        public override int CalculateCaloriesBurned()
        {
            return 0; // Bara tillfäligt, behöver kod här
        }

        // Överskuggande metod som "kopierar" en träning och möjliggör redigering
        public override Workout Clone()
        {
            return new CardioWorkout(Date, Type, Duration, CaloriesBurned, Notes, Distance);
        }
    }
}
