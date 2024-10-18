using Newton_Projektuppgift01_FitTrack.MVVM;
using System.Collections.ObjectModel;

namespace Newton_Projektuppgift01_FitTrack.ViewModel
{
    public class AddWorkWindowViewModel : ViewModelBase
    {
        public string WorkoutTypeComboBox { get; set; }

        private TimeSpan durationInput;

        public TimeSpan DurationInput
        {
            get { return durationInput; }
            set
            {
                durationInput = value;
            }
        }

        public int CaloriesBurnedInput { get; set; }

        public string NotesInput { get; set; }

        public ObservableCollection<string> WorkoutTypes { get; set; }
        public ObservableCollection<string> AvailableTimes { get; set; }
        public ObservableCollection<int> DurationMinutes { get; set; }
        public ObservableCollection<int> DurationSeconds { get; set; }
        public ObservableCollection<int> DurationHours { get; set; }

        public AddWorkWindowViewModel()
        {
            // Instansierar listor med värden
            WorkoutTypes = new ObservableCollection<string> { "Cardio Workout", "Strength Workout" };
            AvailableTimes = new ObservableCollection<string> { "09:00", "09:30", "10:00", "10:30", "11:00", "11:30", "12:00" };
            DurationMinutes = new ObservableCollection<int> { 5, 10, 15, 20, 25, 30, 35, 40, 45, 50, 55 };

            DurationInput = TimeSpan.FromMinutes(0);
        }

        public void SaveWorkout()
        {
            // Kod för att spara sitt träningsupplägg
        }
    }
}
