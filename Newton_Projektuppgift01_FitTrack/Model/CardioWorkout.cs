
namespace Newton_Projektuppgift01_FitTrack.Model
{
    public class CardioWorkout : Workout
    {
        // EGENSKAPER ↓
        // Unik parameter för denna klass
        public int Distance { get; set; }

        // KONSTRUKTOR ↓
        public CardioWorkout(DateTime Date, string Type, TimeSpan Duration, int CaloriesBurned, string Notes, int Distance) : base(Date, Type, Duration, CaloriesBurned, Notes)
        {
            this.Distance = Distance;
        }

        // METODER ↓
        // Beräkna brända kalorier
        public override int CalculateCaloriesBurned()
        {
            // Returnera en generell uträkning baserat på medelpersonen
            return Distance * 70;
        }

        // Överskuggande metod som klonar en träning och möjliggör tillfällig redigering
        public override Workout Clone()
        {
            // Skapa en ny träning baserat på träningstypens parametrar
            return new CardioWorkout(Date, Type, Duration, CaloriesBurned, Notes, Distance);
        }
    }
}
