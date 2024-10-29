
namespace Newton_Projektuppgift01_FitTrack.Model
{
    public class StrengthWorkout : Workout
    {
        // EGENSKAPER ↓
        // Unik parameter för denna klass
        public int Repetition { get; set; }

        // KONSTRUKTOR ↓
        public StrengthWorkout(DateTime Date, string Type, TimeSpan Duration, int CaloriesBurned, string Notes, int Repetition) : base(Date, Type, Duration, CaloriesBurned, Notes)
        {
            this.Repetition = Repetition;
        }

        // METODER ↓
        // Beräkna brända kalorier
        public override int CalculateCaloriesBurned()
        {
            // Returnera en generell uträkning baserat på medelpersonen
            return Repetition * 10;
        }

        // Överskuggande metod som klonar en träning och möjliggör tillfällig redigering
        public override Workout Clone()
        {
            // Skapa en ny träning baserat på träningstypens parametrar
            return new StrengthWorkout(Date, Type, Duration, CaloriesBurned, Notes, Repetition);
        }
    }
}
