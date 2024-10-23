using Newton_Projektuppgift01_FitTrack.Model;
using Newton_Projektuppgift01_FitTrack.MVVM;
using Newton_Projektuppgift01_FitTrack.View;
using System.Collections.ObjectModel;
using System.Windows;

namespace Newton_Projektuppgift01_FitTrack.ViewModel
{
    public class AddWorkWindowViewModel : ViewModelBase
    {
        // EGENSKAPER ↓

        public Window _addWorkoutWindow { get; set; }

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
        //public string WorkoutTypeComboBox { get; set; }

        private string workoutTypeComboBox;
        public string WorkoutTypeComboBox
        {
            get { return workoutTypeComboBox; }
            set
            {
                workoutTypeComboBox = value;
                OnPropertyChanged();
                UpdateVisibility();
            }
        }

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

        // Distance
        private string distanceVisibility;
        public string DistanceVisibility
        {
            get { return distanceVisibility; }
            set
            {
                distanceVisibility = value;
                OnPropertyChanged();
            }
        }

        private int distanceInput;
        public int DistanceInput
        {
            get { return distanceInput; }
            set
            {
                distanceInput = value;
                OnPropertyChanged();
            }
        }


        // Repetition
        private string repetitionVisibility;
        public string RepetitionVisibility
        {
            get { return repetitionVisibility; }
            set
            {
                repetitionVisibility = value;
                OnPropertyChanged();
            }
        }

        private int repetitionInput;
        public int RepetitionInput
        {
            get { return repetitionInput; }
            set
            {
                repetitionInput = value;
                OnPropertyChanged();
            }
        }

        // CaloriesBurned
        public int CaloriesBurnedInput { get; set; }

        private int calculateCaloriesBurned;
        public int CalculateCaloriesBurned
        {
            get { return calculateCaloriesBurned; }
            set
            {
                calculateCaloriesBurned = value;
                OnPropertyChanged();
            }
        }

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
        public RelayCommand CancelCommand => new RelayCommand(execute => Cancel());

        // KONSTRUKTOR ↓
        public AddWorkWindowViewModel(Window addWorkoutWindow)
        {
            _addWorkoutWindow = addWorkoutWindow;

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
            RepetitionInput = 0;
            DistanceInput = 0;
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
                    // Deklarer variabel som träningen ska instansieras från
                    Workout workout = null;

                    // Kolla sen vad det är för typ av träning för att instansiera rätt träningsklass
                    if (WorkoutTypeComboBox == "Strength Workout")
                    {
                        // Instansierar ny styrketräning
                        workout = new StrengthWorkout(FullDateTime, WorkoutTypeComboBox, DurationInput, CaloriesBurnedInput, NotesInput, RepetitionInput);

                    }
                    else if (WorkoutTypeComboBox == "Cardio Workout")
                    {
                        // Instansierar ny konditionsträning
                        workout = new CardioWorkout(FullDateTime, WorkoutTypeComboBox, DurationInput, CaloriesBurnedInput, NotesInput, DistanceInput);
                    }

                    // Lägg till träningen i användarens träningslista
                    Manager.Instance.CurrentUser.UserWorkouts.Add(workout);

                    MessageBox.Show($"Du har lagt till följande träning:\n{FullDateTime} {WorkoutTypeComboBox} {DurationInput} {CaloriesBurnedInput} {NotesInput}");

                    // Öppna WorkoutWindow
                    OpenWorkoutWindow();

                    // Stäng AddWorkoutWindow
                    _addWorkoutWindow.Close();

                    // Oklart om man behöver använda dessa uppdateringar
                    OnPropertyChanged(nameof(Manager.Instance.CurrentUser.UserWorkouts));
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

        private void UpdateVisibility()
        {
            if (WorkoutTypeComboBox == "Cardio Workout")
            {
                DistanceVisibility = "Visible";
                RepetitionVisibility = "Collapsed";
            }
            else if (WorkoutTypeComboBox == "Strength Workout")
            {
                DistanceVisibility = "Collapsed";
                RepetitionVisibility = "Visible";
            }
        }

        // Gå tillbaka till tidigare fönster
        public void Cancel()
        {
            // Öppna WorkoutWindow
            OpenWorkoutWindow();

            // Stäng AddWorkoutWindow
            _addWorkoutWindow.Close();
        }

        // Öppnar WorkoutWindow
        public void OpenWorkoutWindow()
        {
            WorkoutWindow workoutWindow = new WorkoutWindow();
            workoutWindow.Show();
        }
    }
}
