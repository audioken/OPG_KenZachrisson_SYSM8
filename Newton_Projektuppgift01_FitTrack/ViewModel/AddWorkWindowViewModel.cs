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
        // Möjliggör stängning av detta fönster
        public Window _addWorkoutWindow { get; set; }

        // Välj datum
        private DateTime selectedDate;
        public DateTime SelectedDate
        {
            get { return selectedDate; }
            set
            {
                selectedDate = value;

                // Uppdaterar värdet för datum och tid
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

                // Uppdaterar värdet för datum och tid
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

                // Uppdaterar värdet för datum och tid
                OnPropertyChanged(nameof(FullDateTime));
            }
        }

        // Lägger ihop datum och tid
        private DateTime fullDateTime;
        public DateTime FullDateTime
        {
            // Returnerar en ny DateTime baserad valt datum och tid
            get
            {
                return new DateTime(

                SelectedDate.Year,
                SelectedDate.Month,
                SelectedDate.Day,
                SelectedDateHour,
                SelectedDateMinute,
                0
                );
            } // Read-only
        }

        // Välj träningstyp
        private string selectedWorkoutType;
        public string SelectedWorkoutType
        {
            get { return selectedWorkoutType; }
            set
            {
                selectedWorkoutType = value;
                OnPropertyChanged();

                // Visar inmatning för Repetition eller Distans beroende på vald träningstyp
                UpdateVisibilityBasedOnWorkout();
            }
        }

        // Välj varaktighet
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
        private int selectedDurationSlider;
        public int SelectedDurationSlider
        {
            get { return selectedDurationSlider; }
            set
            {
                selectedDurationSlider = value;
                OnPropertyChanged();

                // Skapar en ny TimeSpan som räknar ut sina värden via metoder som hanterar vald varaktighet
                DurationInput = new TimeSpan(GetHours(), GetMinutes(), 0);
            }
        }

        // Välj distans för Cardio Workout
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

        // Välj repetitioner för Strength Workout
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

        // Räknar ut brända kalorier i realtid
        private int caloriesBurned;
        public int CaloriesBurned
        {
            get { return caloriesBurned; }
            set
            {
                caloriesBurned = value;
                OnPropertyChanged();
            }
        }

        // Skriv in kommentar
        private string notesInput;
        public string NotesInput
        {
            get { return notesInput; }
            set
            {
                notesInput = value;
                OnPropertyChanged();

                // Visar stödtext om inmatningsfältet är tomt
                if (string.IsNullOrEmpty(notesInput))
                {
                    PHNotesVisibility = Visibility.Visible;
                }
                // Döljer stödtexten om inmatningsfältet har värde
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

        // Listor för olika typer av inmatning som får sina värden i konstruktorn
        public ObservableCollection<string> WorkoutTypes { get; } // Read-only
        public ObservableCollection<int> AvailableDateHours { get; } // Read-only
        public ObservableCollection<int> AvailableDateMinutes { get; } // Read-only

        // Relaykommandon som representerar knappklick
        public RelayCommand SaveCommand => new RelayCommand(execute => SaveWorkout());
        public RelayCommand PasteCommand => new RelayCommand(execute => PasteWorkout());
        public RelayCommand CancelCommand => new RelayCommand(execute => Cancel());

        // KONSTRUKTOR ↓
        public AddWorkWindowViewModel(Window addWorkoutWindow)
        {
            _addWorkoutWindow = addWorkoutWindow;

            // Instansierar listor med värden
            WorkoutTypes = new ObservableCollection<string> { "Cardio Workout", "Strength Workout" };

            AvailableDateHours = new ObservableCollection<int>
            {
                06, 07, 08, 09, 10, 11, 12, 13, 14, 15, 16, 17,
                18, 19, 20, 21, 22, 23, 24, 01, 02, 03, 04, 05
            };

            AvailableDateMinutes = new ObservableCollection<int>
            {
                00, 01, 02, 03, 04, 05, 06, 07, 08, 09,
                10, 11, 12, 13, 14, 15, 16, 17, 18, 19,
                20, 21, 22, 23, 24, 25, 26, 27, 28, 29,
                30, 31, 32, 33, 34, 35, 36, 37, 38, 39,
                40, 41, 42, 43, 44, 45, 46, 47, 48, 49,
                50, 51, 52, 52, 54, 55, 56, 57, 58, 59
            };

            // Standardmall för ny träning
            SelectedDate = DateTime.Now;
            SelectedDateHour = 10;
            SelectedDateMinute = 30;
            SelectedWorkoutType = "Strength Workout";
            SelectedDurationSlider = 0;
            SelectedRepetitionSlider = 0;
            SelectedDistanceSlider = 0;
            NotesInput = "";
        }

        // METODER ↓
        // Spara träningspasset
        private void SaveWorkout()
        {
            // Kolla så det finns en kommentar
            if (!string.IsNullOrEmpty(NotesInput))
            {
                // Deklarerar en variabel som träningen ska instansieras från
                Workout newWorkout = null;

                // Kolla typ av träning för att instansiera rätt träningstyp
                if (SelectedWorkoutType == "Strength Workout")
                {
                    // Instansierar ny styrketräning
                    newWorkout = new StrengthWorkout(FullDateTime, SelectedWorkoutType, DurationInput, 0, NotesInput, SelectedRepetitionSlider);

                }
                else if (SelectedWorkoutType == "Cardio Workout")
                {
                    // Instansierar ny konditionsträning
                    newWorkout = new CardioWorkout(FullDateTime, SelectedWorkoutType, DurationInput, 0, NotesInput, SelectedDistanceSlider);
                }

                // Lägg till träningen i användarens träningslista
                Manager.Instance.CurrentUser.UserWorkouts.Add(newWorkout);

                MessageBox.Show($"Vald träningstyp: {SelectedWorkoutType}\nTidpunkt: {FullDateTime}\nVaraktighet: {SelectedDurationSlider} min\nÖvriga kommenterar: {NotesInput}");

                // Öppna WorkoutWindow
                OpenWorkoutWindow();

                // Stäng AddWorkoutWindow
                _addWorkoutWindow.Close();
            }
            else { MessageBox.Show("Du måste skriva en kommentar.."); }
        }

        // Infoga relevanta parametrar från det kopierade träningspasset
        private void PasteWorkout()
        {
            // Kolla så det finns en kopierad träning
            if (Manager.Instance.CopiedWorkout != null)
            {
                // Hämta datum och tid
                SelectedDate = Manager.Instance.CopiedWorkout.Date;
                OnPropertyChanged(nameof(SelectedDate));
                SelectedDateHour = Manager.Instance.CopiedWorkout.Date.Hour;
                OnPropertyChanged(nameof(SelectedDateHour));
                SelectedDateMinute = Manager.Instance.CopiedWorkout.Date.Minute;
                OnPropertyChanged(nameof(SelectedDateMinute));

                // Hämta träningstyp
                SelectedWorkoutType = Manager.Instance.CopiedWorkout.Type;
                OnPropertyChanged(nameof(SelectedWorkoutType));

                // Hämta varaktighet i minuter
                SelectedDurationSlider = ConvertTimeSpanToMinutes();
                OnPropertyChanged(nameof(SelectedDurationSlider));

                // Kontrollera vilken träningstyp som är kopierad för att tilldela rätt extravärde
                if (Manager.Instance.CopiedWorkout is StrengthWorkout strengthWorkout)
                {
                    // Hämta repetitioner
                    SelectedRepetitionSlider = strengthWorkout.Repetition;
                    OnPropertyChanged(nameof(SelectedRepetitionSlider));
                }
                else if (Manager.Instance.CopiedWorkout is CardioWorkout cardioWorkout)
                {
                    // Hämta distans
                    SelectedDistanceSlider = cardioWorkout.Distance;
                    OnPropertyChanged(nameof(SelectedDistanceSlider));
                }

                // Hämta kommentar
                NotesInput = Manager.Instance.CopiedWorkout.Notes;
                OnPropertyChanged(nameof(NotesInput));
            }
            else { MessageBox.Show("Det finns ingen kopierad träning.."); }
        }

        // Avbryt tillägg av ny träning och öppna WorkoutWindow
        private void Cancel()
        {
            // Öppna WorkoutWindow
            OpenWorkoutWindow();

            // Stäng AddWorkoutWindow
            _addWorkoutWindow.Close();
        }

        // Kontrollerar vilken knapp och vilken inputruta som syns beroende på träningstyp
        private void UpdateVisibilityBasedOnWorkout()
        {
            if (SelectedWorkoutType == "Cardio Workout")
            {
                DistanceSliderVisibility = Visibility.Visible;
                RepetitionSliderVisibility = Visibility.Collapsed;
            }
            else if (SelectedWorkoutType == "Strength Workout")
            {
                DistanceSliderVisibility = Visibility.Collapsed;
                RepetitionSliderVisibility = Visibility.Visible;
            }
        }

        // Konverterar konstant DurationSliders minutvärde till TimeSpan
        private int GetHours()
        {
            return SelectedDurationSlider / 60;
        }
        private int GetMinutes()
        {
            return SelectedDurationSlider % 60;
        }

        // Konverterar TimeSpan till minuter när man klistrar in en träning
        private int ConvertTimeSpanToMinutes()
        {
            return (int)Manager.Instance.CopiedWorkout.Duration.TotalMinutes;
        }

        // Öppnar WorkoutWindow
        private void OpenWorkoutWindow()
        {
            WorkoutWindow workoutWindow = new WorkoutWindow();
            workoutWindow.Show();
        }
    }
}
