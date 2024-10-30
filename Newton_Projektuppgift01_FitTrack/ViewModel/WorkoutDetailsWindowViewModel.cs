using Newton_Projektuppgift01_FitTrack.Model;
using Newton_Projektuppgift01_FitTrack.MVVM;
using Newton_Projektuppgift01_FitTrack.View;
using System.Collections.ObjectModel;
using System.Windows;

namespace Newton_Projektuppgift01_FitTrack.ViewModel
{
    public class WorkoutDetailsWindowViewModel : ViewModelBase
    {
        // EGENSKAPER ↓
        // Möjliggör stängning av detta fönster
        public Window _workoutDetailsWindow { get; set; }

        // Aktiverar eller avaktiverar redigering
        private bool isEditEnabled;
        public bool IsEditEnabled
        {
            get { return isEditEnabled; }
            set
            {
                isEditEnabled = value;
                OnPropertyChanged();
            }
        }

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
                if (selectedWorkoutType != value)
                {
                    selectedWorkoutType = value;
                    OnPropertyChanged();

                    // Skapar en ny träning baserad på vald träningstyp
                    CreateSelectedWorkout();

                    // Döljer eller visar extraparametern, som är specifik för träningstypen, i UI
                    UpdateVisibilityBasedOnWorkout();
                }
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

                // Uppdaterar värdena för WorkoutEditable varje gång slider ändras
                UpdateWorkoutEditable();

                // Räkna ut antal brända kalorier och reflektera i UI
                CaloriesBurned = WorkoutEditable.CalculateCaloriesBurned();
                OnPropertyChanged(nameof(CaloriesBurned));
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

                // Uppdaterar värdena för WorkoutEditable varje gång slider ändras
                UpdateWorkoutEditable();

                // Räkna ut antal brända kalorier och reflektera i UI
                CaloriesBurned = WorkoutEditable.CalculateCaloriesBurned();
                OnPropertyChanged(nameof(CaloriesBurned));
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

        // Döljer eller visar Restore-knappen
        private Visibility restoreVisibility;
        public Visibility RestoreVisibility
        {
            get { return restoreVisibility; }
            set
            {
                restoreVisibility = value;
                OnPropertyChanged();
            }
        }

        // Döljer eller visar Edit-knappen
        private Visibility editVisibility;
        public Visibility EditVisibility
        {
            get { return editVisibility; }
            set
            {
                editVisibility = value;
                OnPropertyChanged();
            }
        }

        // Blir en klon av vald träning och får sitt initiella värde i konstruktorn
        public Workout WorkoutEditable { get; private set; } // Read-only för klassens metoder

        // Listor för olika typer av inmatning som får sina värden i konstruktorn
        public ObservableCollection<string> WorkoutTypes { get; } // Read-only
        public ObservableCollection<int> AvailableDateHours { get; } // Read-only
        public ObservableCollection<int> AvailableDateMinutes { get; } // Read-only

        // Relaykommandon som representerar knappklick
        public RelayCommand EditCommand => new RelayCommand(execute => EditWorkout());
        public RelayCommand RestoreCommand => new RelayCommand(execute => RestoreWorkout());
        public RelayCommand SaveCommand => new RelayCommand(execute => SaveWorkout());
        public RelayCommand CopyCommand => new RelayCommand(execute => CopyWorkout());
        public RelayCommand CancelCommand => new RelayCommand(execute => Cancel());

        // KONSTRUKTOR ↓
        public WorkoutDetailsWindowViewModel(Window _workoutDetailsWindow)
        {
            this._workoutDetailsWindow = _workoutDetailsWindow;

            // Instansierar lista med träningstyper
            WorkoutTypes = new ObservableCollection<string> { "Cardio Workout", "Strength Workout" };

            // Instansierar lista med timmar
            AvailableDateHours = new ObservableCollection<int>
            {
                06, 07, 08, 09, 10, 11, 12, 13, 14, 15, 16, 17,
                18, 19, 20, 21, 22, 23, 24, 01, 02, 03, 04, 05
            };

            // Instansierar lista med minuter
            AvailableDateMinutes = new ObservableCollection<int>
            {
                00, 01, 02, 03, 04, 05, 06, 07, 08, 09,
                10, 11, 12, 13, 14, 15, 16, 17, 18, 19,
                20, 21, 22, 23, 24, 25, 26, 27, 28, 29,
                30, 31, 32, 33, 34, 35, 36, 37, 38, 39,
                40, 41, 42, 43, 44, 45, 46, 47, 48, 49,
                50, 51, 52, 52, 54, 55, 56, 57, 58, 59
            };

            // Beräknar brända kalorier för träningen
            Manager.Instance.CurrentWorkout.CaloriesBurned = Manager.Instance.CurrentWorkout.CalculateCaloriesBurned();

            // Klonar vald träning för att möjliggöra redigering och återställning av ändringar
            WorkoutEditable = Manager.Instance.CurrentWorkout.Clone();

            // Hämtar alla värden från den aktuella träningen så de reflekteras i UI
            FetchWorkout();

            // Avaktivera redigering initiellt
            IsEditEnabled = false;

            // Döljer restore-knappen initiellt
            RestoreVisibility = Visibility.Collapsed;
        }

        // METODER ↓
        // Aktivera redigering av träning
        private void EditWorkout()
        {
            // Låser upp alla fält
            IsEditEnabled = true;

            // Döljer Edit-knapp och visar Restore-knapp
            EditVisibility = Visibility.Collapsed;
            RestoreVisibility = Visibility.Visible;
        }

        // Avbryter redigering och återställer värden
        private void RestoreWorkout()
        {
            // Klonar originalet för att återställa värdena
            WorkoutEditable = Manager.Instance.CurrentWorkout.Clone();

            // Hämta alla värden på nytt
            FetchWorkout();

            // Avaktiverar redigering
            IsEditEnabled = false;

            // Döljer Restore-knapp och visar Edit-knapp
            EditVisibility = Visibility.Visible;
            RestoreVisibility = Visibility.Collapsed;

        }

        // Sparar ändringar
        private void SaveWorkout()
        {
            // Uppdaterar värdena för WorkoutEditable för att säkerställa korrekt värden
            UpdateWorkoutEditable();

            // Hitta index för originalet av träningen som ändrats
            int indexOfWorkout = Manager.Instance.CurrentUser.UserWorkouts.IndexOf(Manager.Instance.CurrentWorkout);

            // Kontrollera så det är en vanlig användare samt att index för träningen finns
            if (Manager.Instance.CurrentUser is User && indexOfWorkout >= 0)
            {
                // Ersätt originalet med uppdaterad träning för användaren
                Manager.Instance.CurrentUser.UserWorkouts[indexOfWorkout] = WorkoutEditable;
            }
            // Kontrollera om det är en admin som är inloggad
            else if (Manager.Instance.CurrentUser is AdminUser)
            {
                // Kolla igenom alla användare i listan AllUsers
                foreach (User user in Manager.Instance.AllUsers)
                {
                    // Om en användare har den aktuella träningen som ska uppdateras
                    if (user.UserWorkouts.Contains(Manager.Instance.CurrentWorkout))
                    {
                        // Hitta index
                        int indexUserWorkout = user.UserWorkouts.IndexOf(Manager.Instance.CurrentWorkout);

                        // Ersätt med den redigerade klonen
                        user.UserWorkouts[indexUserWorkout] = WorkoutEditable;

                        break;
                    }
                }
            }
            else { MessageBox.Show("Något gick fel! Träningen kunde inte sparas..", "Error!", MessageBoxButton.OK, MessageBoxImage.Error); }

            // Öppna WorkoutWindow
            OpenWorkoutWindow();

            // Stäng WorkoutDetailsWindow
            _workoutDetailsWindow.Close();
        }

        // Kopiera träning
        private void CopyWorkout()
        {
            // Hämta värdena och lagra i Managerklassen för enkel åtkomst
            Manager.Instance.CopiedWorkout = WorkoutEditable;

            MessageBox.Show("Träningen är kopierad!", "Success!", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        // Avbryt redigering och återgå till WorkoutWindow
        private void Cancel()
        {
            // Öppna WorkoutWindow
            OpenWorkoutWindow();

            // Stäng WorkoutDetails
            _workoutDetailsWindow.Close();
        }

        // Öppna WorkoutWindow
        private void OpenWorkoutWindow()
        {
            WorkoutWindow workoutWindow = new WorkoutWindow();
            workoutWindow.Show();
        }

        // Infoga relevanta parametrar från det kopierade träningspasset
        private void FetchWorkout()
        {
            // Hämta datum och tid
            SelectedDate = WorkoutEditable.Date;
            OnPropertyChanged(nameof(SelectedDate));
            SelectedDateHour = WorkoutEditable.Date.Hour;
            OnPropertyChanged(nameof(SelectedDateHour));
            SelectedDateMinute = WorkoutEditable.Date.Minute;
            OnPropertyChanged(nameof(SelectedDateMinute));

            // Hämta träningstyp
            SelectedWorkoutType = WorkoutEditable.Type;
            OnPropertyChanged(nameof(SelectedWorkoutType));

            // Hämta varaktighet i minuter
            SelectedDurationSlider = ConvertTimeSpanToMinutes();
            OnPropertyChanged(nameof(SelectedDurationSlider));

            // Hämta brända kalorier
            CaloriesBurned = WorkoutEditable.CaloriesBurned;
            OnPropertyChanged(nameof(CaloriesBurned));

            // Hämta kommentar
            NotesInput = WorkoutEditable.Notes;
            OnPropertyChanged(nameof(NotesInput));

            // Kontrollera vilken träningstyp som är kopierad för att tilldela rätt extravärde
            if (WorkoutEditable is StrengthWorkout strengthWorkout)
            {
                // Hämta repetitioner
                SelectedRepetitionSlider = strengthWorkout.Repetition;
                OnPropertyChanged(nameof(SelectedRepetitionSlider));
            }
            else if (WorkoutEditable is CardioWorkout cardioWorkout)
            {
                // Hämta distans
                SelectedDistanceSlider = cardioWorkout.Distance;
                OnPropertyChanged(nameof(SelectedDistanceSlider));
            }
        }

        // Skapar träning baserat på träningstyp och inkluderar relevanta parametrar
        private void CreateSelectedWorkout()
        {
            // Kolla först vad som faktist är valt
            if (SelectedWorkoutType == "Strength Workout")
            {
                // Skapa ny träning om vald träningstyp inte matchar den aktuella träningstypen
                if (!(WorkoutEditable is StrengthWorkout))
                {
                    WorkoutEditable = new StrengthWorkout(FullDateTime, SelectedWorkoutType, DurationInput, CaloriesBurned, NotesInput, SelectedRepetitionSlider);

                    // Återställ slider för Cardio Workout
                    SelectedDistanceSlider = 0;
                }
            }
            else if (SelectedWorkoutType == "Cardio Workout")
            {
                // Skapa ny träning om vald träningstyp inte matchar den aktuella träningstypen
                if (!(WorkoutEditable is CardioWorkout))
                {
                    WorkoutEditable = new CardioWorkout(FullDateTime, SelectedWorkoutType, DurationInput, CaloriesBurned, NotesInput, SelectedDistanceSlider);

                    // Återställ slider för Strength Workout
                    SelectedRepetitionSlider = 0;
                }
            }
        }

        // Uppdaterar WorkoutEditable till de senaste värdena
        private void UpdateWorkoutEditable()
        {
            if (WorkoutEditable is StrengthWorkout strengthWorkout)
            {
                strengthWorkout.Date = FullDateTime;
                strengthWorkout.Type = SelectedWorkoutType;
                strengthWorkout.Duration = DurationInput;
                strengthWorkout.Notes = NotesInput;
                strengthWorkout.Repetition = SelectedRepetitionSlider;
                strengthWorkout.CaloriesBurned = strengthWorkout.CalculateCaloriesBurned();
            }
            else if (WorkoutEditable is CardioWorkout cardioWorkout)
            {
                cardioWorkout.Date = FullDateTime;
                cardioWorkout.Type = SelectedWorkoutType;
                cardioWorkout.Duration = DurationInput;
                cardioWorkout.Notes = NotesInput;
                cardioWorkout.Distance = SelectedDistanceSlider;
                cardioWorkout.CaloriesBurned = cardioWorkout.CalculateCaloriesBurned();
            }

            OnPropertyChanged(nameof(CaloriesBurned));
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
            return (int)WorkoutEditable.Duration.TotalMinutes;
        }
    }
}
