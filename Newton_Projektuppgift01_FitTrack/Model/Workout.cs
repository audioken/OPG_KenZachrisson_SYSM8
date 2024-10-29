namespace Newton_Projektuppgift01_FitTrack.Model
{
    // Abstrakt klass som tvingar härledda klasser att implementera dess medlemmar
    public abstract class Workout
    {
        // EGENSKAPER ↓
        // Grundparametrar som ett träningspass består av
        public DateTime Date { get; set; }
        public string Type { get; set; }
        public TimeSpan Duration { get; set; }
        public int CaloriesBurned { get; set; }
        public string Notes { get; set; }
        public User User { get; set; }

        // KONSTRUKTOR ↓
        public Workout(DateTime Date, string Type, TimeSpan Duration, int CaloriesBurned, string Notes)
        {
            this.Date = Date;
            this.Type = Type;
            this.Duration = Duration;
            this.CaloriesBurned = CaloriesBurned;
            this.Notes = Notes;
        }

        // METODER ↓
        // Uträkning av brända kalorier i härledda klasser
        public abstract int CalculateCaloriesBurned();

        // Kloning av träningspass i härledda klasser
        public abstract Workout Clone();
    }
}
