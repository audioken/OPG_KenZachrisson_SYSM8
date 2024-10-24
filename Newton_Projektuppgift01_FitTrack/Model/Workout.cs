﻿namespace Newton_Projektuppgift01_FitTrack.Model
{
    public abstract class Workout
    {
        public DateTime Date { get; set; }
        public string Type { get; set; }
        public TimeSpan Duration { get; set; }
        public int CaloriesBurned { get; set; }
        public string Notes { get; set; }
        public User User { get; internal set; }

        public Workout(DateTime Date, string Type, TimeSpan Duration, int CaloriesBurned, string Notes)
        {
            this.Date = Date;
            this.Type = Type;
            this.Duration = Duration;
            this.CaloriesBurned = CaloriesBurned;
            this.Notes = Notes;
        }

        public abstract int CalculateCaloriesBurned();

        // Tvingar härledda klasser att ha en metod för kloning
        public abstract Workout Clone();
    }
}
