namespace Newton_Projektuppgift01_FitTrack.ViewModel
{
    public class AddWorkWindowViewModel
    {
        public string WorkoutTypeComboBox { get; set; }
        public TimeSpan DurationInput { get; set; }
        public int CaloriesBurnedInput { get; set; }
        public string NotesInput { get; set; }

        public AddWorkWindowViewModel(string WorkoutTypeComboBox, TimeSpan DurationInput, int CaloriesBurnedInput, string NotesInput)
        {
            this.WorkoutTypeComboBox = WorkoutTypeComboBox;
            this.DurationInput = DurationInput;
            this.CaloriesBurnedInput = CaloriesBurnedInput;
            this.NotesInput = NotesInput;
        }

        public void SaveWorkout()
        {
            // Kod för att spara sitt träningsupplägg
        }
    }
}
