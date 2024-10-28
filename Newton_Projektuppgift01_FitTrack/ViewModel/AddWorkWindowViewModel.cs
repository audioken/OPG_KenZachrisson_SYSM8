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
        private int selectedDurationSlider;
        public int SelectedDurationSlider
        {
            get { return selectedDurationSlider; }
            set
            {
                selectedDurationSlider = value;
                OnPropertyChanged();

                DurationInput = new TimeSpan(GetHours(), GetMinutes(), 0);
            }
        }
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

        // Distance
        private int selectedDistanceSlider;
        public int SelectedDistanceSlider
        {
            get { return selectedDistanceSlider; }
            set
            {
                selectedDistanceSlider = value;
                OnPropertyChanged();
            }
        }
        private Visibility distanceSliderVisibility;
        public Visibility DistanceSliderVisibility
        {
            get { return distanceSliderVisibility; }
            set
            {
                distanceSliderVisibility = value;
                OnPropertyChanged();
            }
        }

        // Repetition
        private int selectedRepetitionSlider;
        public int SelectedRepetitionSlider
        {
            get { return selectedRepetitionSlider; }
            set
            {
                selectedRepetitionSlider = value;
                OnPropertyChanged();
            }
        }
        private Visibility repetitionSliderVisibility;
        public Visibility RepetitionSliderVisibility
        {
            get { return repetitionSliderVisibility; }
            set
            {
                repetitionSliderVisibility = value;
                OnPropertyChanged();
            }
        }

        // CaloriesBurned (Funderar på att skippa denna helt för inmatning eftersom kalorier räknas ut när man kollar detaljer)
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
        private string notesInput;
        public string NotesInput
        {
            get { return notesInput; }
            set
            {
                notesInput = value;
                OnPropertyChanged();

                if (string.IsNullOrEmpty(notesInput))
                {
                    PHNotesVisibility = Visibility.Visible;
                }
                else
                {
                    PHNotesVisibility = Visibility.Collapsed;
                }
            }
        }
        private Visibility pHNotesVisibility;
        public Visibility PHNotesVisibility
        {
            get { return pHNotesVisibility; }
            set
            {
                pHNotesVisibility = value;
                OnPropertyChanged();
            }
        }


        // Listor som gör inmatningen enklare för användaren
        public ObservableCollection<string> WorkoutTypes { get; set; }
        public ObservableCollection<int> AvailableDateHours { get; set; }
        public ObservableCollection<int> AvailableDateMinutes { get; set; }
        public ObservableCollection<int> DurationHours { get; set; }
        public ObservableCollection<int> DurationMinutes { get; set; }

        // Relay-kommandon
        public RelayCommand SaveCommand => new RelayCommand(execute => SaveWorkout());
        public RelayCommand PasteCommand => new RelayCommand(execute => PasteWorkout());
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

            // Tilldelar värden för alla inputs för att det ska finnas något förifyllt
            //DateTime now = DateTime.Now; // Hämtar aktuellt datum
            SelectedDate = DateTime.Now; // Tilldelar aktuellt datum
            SelectedDateHour = 10;
            SelectedDateMinute = 00;
            WorkoutTypeComboBox = "Strength Workout";
            SelectedDurationSlider = 0;
            SelectedRepetitionSlider = 0;
            SelectedDistanceSlider = 0;
            NotesInput = "";
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
                        workout = new StrengthWorkout(FullDateTime, WorkoutTypeComboBox, DurationInput, 0, NotesInput, SelectedRepetitionSlider);

                    }
                    else if (WorkoutTypeComboBox == "Cardio Workout")
                    {
                        // Instansierar ny konditionsträning
                        workout = new CardioWorkout(FullDateTime, WorkoutTypeComboBox, DurationInput, 0, NotesInput, SelectedDistanceSlider);
                    }

                    // Lägg till träningen i användarens träningslista
                    Manager.Instance.CurrentUser.UserWorkouts.Add(workout);

                    MessageBox.Show($"Du har lagt till följande träning:\n{FullDateTime} {WorkoutTypeComboBox} {SelectedDurationSlider} {CaloriesBurnedInput} {NotesInput}");

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

            //SelectedDurationHours = Manager.Instance.CopiedWorkout.Duration.Hours;
            //OnPropertyChanged(nameof(SelectedDurationHours));

            //SelectedDurationMinutes = Manager.Instance.CopiedWorkout.Duration.Minutes;
            //OnPropertyChanged(nameof(SelectedDurationMinutes));

            // Kontrollera vilken träningstyp som är kopierad för att tilldela rätt extravärde
            if (Manager.Instance.CopiedWorkout is StrengthWorkout copiedStrengthWorkout)
            {
                SelectedRepetitionSlider = copiedStrengthWorkout.Repetition;
                OnPropertyChanged(nameof(SelectedRepetitionSlider));
            }
            else if (Manager.Instance.CopiedWorkout is CardioWorkout copiedCardioWorkout)
            {
                SelectedDistanceSlider = copiedCardioWorkout.Distance;
                OnPropertyChanged(nameof(SelectedDistanceSlider));
            }

            CaloriesBurnedInput = Manager.Instance.CopiedWorkout.CaloriesBurned;
            OnPropertyChanged(nameof(CaloriesBurnedInput));

            NotesInput = Manager.Instance.CopiedWorkout.Notes;
            OnPropertyChanged(nameof(NotesInput));
        }

        // Kontrollerar vilken knapp och vilken inputruta som syns beroende på träningstyp
        private void UpdateVisibility()
        {
            if (WorkoutTypeComboBox == "Cardio Workout")
            {
                DistanceSliderVisibility = Visibility.Visible;
                RepetitionSliderVisibility = Visibility.Collapsed;
            }
            else if (WorkoutTypeComboBox == "Strength Workout")
            {
                DistanceSliderVisibility = Visibility.Collapsed;
                RepetitionSliderVisibility = Visibility.Visible;
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

        public int GetHours()
        {
            return SelectedDurationSlider / 60;
        }

        public int GetMinutes()
        {
            return SelectedDurationSlider % 60;
        }

        // Öppnar WorkoutWindow
        public void OpenWorkoutWindow()
        {
            WorkoutWindow workoutWindow = new WorkoutWindow();
            workoutWindow.Show();
        }
    }
}
