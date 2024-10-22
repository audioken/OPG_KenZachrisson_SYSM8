using Newton_Projektuppgift01_FitTrack.Model;
using Newton_Projektuppgift01_FitTrack.MVVM;
using System.Collections.ObjectModel;
using System.Windows;

namespace Newton_Projektuppgift01_FitTrack.ViewModel
{
    public class AddWorkWindowViewModel : ViewModelBase
    {
        // EGENSKAPER ↓
        // Håller koll på inloggad användare
        public User User { get; set; }

        // Date
        private DateTime selectedDate;
        public DateTime SelectedDate
        {
            get { return selectedDate; }
            set
            {
                selectedDate = value;
                OnPropertyChanged(nameof(FullDateTime));
            }
        }

        private int selectedDateHour;
        public int SelectedDateHour
        {
            get { return selectedDateHour; }
            set
            {
                selectedDateHour = value;
                OnPropertyChanged(nameof(FullDateTime));
            }
        }

        private int selectedDateMinute;
        public int SelectedDateMinute
        {
            get { return selectedDateMinute; }
            set
            {
                selectedDateMinute = value;
                OnPropertyChanged(nameof(FullDateTime));
            }
        }

        private DateTime fullDateTime;
        public DateTime FullDateTime
        {
            get { return new DateTime(SelectedDate.Year, SelectedDate.Month, SelectedDate.Day, SelectedDateHour, SelectedDateMinute, 0); }
        }

        // Type
        public string WorkoutTypeComboBox { get; set; }

        // Duration
        private TimeSpan durationInput;
        public TimeSpan DurationInput
        {
            get { return durationInput; }
            set
            {
                durationInput = value;
                OnPropertyChanged();
            }
        }

        private int selectedDurationHours;
        public int SelectedDurationHours
        {
            get { return selectedDurationHours; }
            set
            {
                selectedDurationHours = value;
                DurationInput = new TimeSpan(selectedDurationHours, selectedDurationMinutes, 0);
                OnPropertyChanged();
            }
        }

        private int selectedDurationMinutes;
        public int SelectedDurationMinutes
        {
            get { return selectedDurationMinutes; }
            set
            {
                selectedDurationMinutes = value;
                DurationInput = new TimeSpan(selectedDurationHours, selectedDurationMinutes, 0);
                OnPropertyChanged();
            }
        }

        // CaloriesBurned
        public int CaloriesBurnedInput { get; set; }

        // Notes
        public string NotesInput { get; set; }

        // Listor som gör inmatningen enklare för användaren
        public ObservableCollection<string> WorkoutTypes { get; set; }
        public ObservableCollection<int> AvailableDateHours { get; set; }
        public ObservableCollection<int> AvailableDateMinutes { get; set; }
        public ObservableCollection<int> DurationHours { get; set; }
        public ObservableCollection<int> DurationMinutes { get; set; }

        // Relay-kommandon
        public RelayCommand SaveCommand => new RelayCommand(execute => SaveWorkout());
        public RelayCommand PasteWorkoutCommand => new RelayCommand(execute => PasteWorkout());

        // KONSTRUKTOR ↓
        public AddWorkWindowViewModel()
        {
            // Hämtar nuvarande användare
            User = Manager.Instance.CurrentUser; // Behövs kanske inte? Jag sparar ju direkt till Managerklassen i Save

            // Instansierar listor med värden
            WorkoutTypes = new ObservableCollection<string> { "Cardio Workout", "Strength Workout" };
            AvailableDateHours = new ObservableCollection<int> { 9, 10, 11, 12, 13, 14, 15, 16, 17, 18 };
            AvailableDateMinutes = new ObservableCollection<int> { 00, 15, 30, 45 };
            DurationHours = new ObservableCollection<int> { 0, 1, 2, 3 };
            DurationMinutes = new ObservableCollection<int> { 0, 5, 10, 15, 20, 25, 30, 35, 40, 45, 50, 55 };

            // Instansierar värden för alla inputs för att det ska finnas något förifyllt
            DateTime now = DateTime.Now; // Hämtar aktuellt datum
            SelectedDate = now; // Tilldelar aktuellt datum
            SelectedDateHour = 10;
            SelectedDateMinute = 00;
            WorkoutTypeComboBox = "Strength Workout";
            SelectedDurationHours = 1;
            SelectedDurationMinutes = 30;
            CaloriesBurnedInput = 200;
            NotesInput = "Weightlifting";
        }

        // METODER ↓
        // Spara träningspasset
        public void SaveWorkout()
        {
            // Kolla så kalorier inte är negativt
            if (CaloriesBurnedInput >= 0)
            {
                // Kolla också så det finns en kommentar
                if (!string.IsNullOrEmpty(NotesInput))
                {
                    // Kolla sen vad det är för typ av träning för att instansiera rätt träningsklass
                    if (WorkoutTypeComboBox == "Strength Workout")
                    {
                        // Skapar en ny styrketräning
                        Workout strengthWorkout = new StrengthWorkout(FullDateTime, WorkoutTypeComboBox, DurationInput, CaloriesBurnedInput, NotesInput, 0);

                        // Lägger den nya styrketräningen i användarens och managers lista
                        Manager.Instance.CurrentUser.UserWorkouts.Add(strengthWorkout);
                        Manager.Instance.AllWorkouts.Add(strengthWorkout);

                    }
                    else if (WorkoutTypeComboBox == "Cardio Workout")
                    {
                        // Skapar en ny konditionsträning
                        Workout cardioWorkout = new CardioWorkout(FullDateTime, WorkoutTypeComboBox, DurationInput, CaloriesBurnedInput, NotesInput, 0);

                        // Lägger den nya konditionsträningen i användarens och managers lista
                        Manager.Instance.CurrentUser.UserWorkouts.Add(cardioWorkout);
                        Manager.Instance.AllWorkouts.Add(cardioWorkout);
                    }

                    // Oklart om man behöver använda dessa uppdateringar
                    OnPropertyChanged(nameof(Manager.Instance.AllWorkouts));
                    OnPropertyChanged(nameof(Manager.Instance.CurrentUser.UserWorkouts));

                    MessageBox.Show($"Du har lagt till följande träning:\n{FullDateTime} {WorkoutTypeComboBox} {DurationInput} {CaloriesBurnedInput} {NotesInput}");
                }
                else { MessageBox.Show("Du måste skriva en kommentar.."); }
            }
            else { MessageBox.Show("Antal brända kalorier måste minst vara 0.."); }
        }

        // Infoga alla parametrar från träningspasset som kopierades i WorkoutDetailsWindow
        public void PasteWorkout()
        {
            SelectedDate = Manager.Instance.CopiedWorkout.Date;
            OnPropertyChanged(nameof(SelectedDate));

            SelectedDateHour = Manager.Instance.CopiedWorkout.Date.Hour;
            OnPropertyChanged(nameof(SelectedDateHour));

            SelectedDateMinute = Manager.Instance.CopiedWorkout.Date.Minute;
            OnPropertyChanged(nameof(SelectedDateMinute));

            WorkoutTypeComboBox = Manager.Instance.CopiedWorkout.Type;
            OnPropertyChanged(nameof(WorkoutTypeComboBox));

            SelectedDurationHours = Manager.Instance.CopiedWorkout.Duration.Hours;
            OnPropertyChanged(nameof(SelectedDurationHours));

            SelectedDurationMinutes = Manager.Instance.CopiedWorkout.Duration.Minutes;
            OnPropertyChanged(nameof(SelectedDurationMinutes));

            CaloriesBurnedInput = Manager.Instance.CopiedWorkout.CaloriesBurned;
            OnPropertyChanged(nameof(CaloriesBurnedInput));

            NotesInput = Manager.Instance.CopiedWorkout.Notes;
            OnPropertyChanged(nameof(NotesInput));
        }
    }
}
