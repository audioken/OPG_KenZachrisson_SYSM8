
namespace Newton_Projektuppgift01_FitTrack.Model
{
    public class CardioWorkout : Workout
    {
        public int Distance { get; set; }

        public CardioWorkout(DateTime Date, string Type, TimeSpan Duration, int CaloriesBurned, string Notes, int Distance) : base(Date, Type, Duration, CaloriesBurned, Notes)
        {
            this.Distance = Distance;
        }

        // BEHÖVER FÖRSTÅ HUR VI SKA ANVÄNDA DENNA?
        public override int CalculateCaloriesBurned()
        {
            return Distance * 70;
        }

        // Överskuggande metod som klonar en träning och möjliggör tillfällig redigering
        public override Workout Clone()
        {
            return (Workout)this.MemberwiseClone();
        }
    }
}
