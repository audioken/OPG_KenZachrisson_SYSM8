using Newton_Projektuppgift01_FitTrack.Model;
using Newton_Projektuppgift01_FitTrack.MVVM;
using System.Collections.ObjectModel;
using System.Windows;

namespace Newton_Projektuppgift01_FitTrack.ViewModel
{
    public class AddWorkWindowViewModel : ViewModelBase
    {
        // Håller koll på inloggad användare
        public User User { get; set; }

        // Datum och Tid
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

        // Typ av träning
        public string WorkoutTypeComboBox { get; set; }

        // Varaktighet av träning
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

        // Brända kalorier
        public int CaloriesBurnedInput { get; set; }

        // Anteckningar
        public string NotesInput { get; set; }

        // Listor
        public ObservableCollection<string> WorkoutTypes { get; set; }
        public ObservableCollection<int> AvailableDateHours { get; set; }
        public ObservableCollection<int> AvailableDateMinutes { get; set; }
        public ObservableCollection<int> DurationHours { get; set; }
        public ObservableCollection<int> DurationMinutes { get; set; }

        // Relay-kommandon
        public RelayCommand SaveCommand => new RelayCommand(execute => SaveWorkout());

        // Konstruktor
        public AddWorkWindowViewModel()
        {
            // Instansierar listor med värden
            WorkoutTypes = new ObservableCollection<string> { "Cardio Workout", "Strength Workout" };
            AvailableDateHours = new ObservableCollection<int> { 9, 10, 11, 12, 13, 14, 15, 16, 17, 18 };
            AvailableDateMinutes = new ObservableCollection<int> { 00, 15, 30, 45 };
            DurationHours = new ObservableCollection<int> { 0, 1, 2, 3 };
            DurationMinutes = new ObservableCollection<int> { 0, 5, 10, 15, 20, 25, 30, 35, 40, 45, 50, 55 };

            SelectedDate = new DateTime(2024, 10, 25, 10, 00, 00);
            SelectedDateHour = 10;
            SelectedDateMinute = 00;

            WorkoutTypeComboBox = "Strength Workout";

            SelectedDurationHours = 1;
            SelectedDurationMinutes = 30;

            CaloriesBurnedInput = 200;

            NotesInput = "Weightlifting";


            User = Manager.Instance.CurrentUser;
        }

        // Metoder
        public void SaveWorkout()
        {
            // KOD HÄR för kontroll av inputs
            User.UserWorkouts.Add(new StrengthWorkout(FullDateTime, WorkoutTypeComboBox, DurationInput, CaloriesBurnedInput, NotesInput, 5));

            // TESTNING
            MessageBox.Show($"{FullDateTime} {WorkoutTypeComboBox} {DurationInput} {CaloriesBurnedInput} {NotesInput}");
        }
    }
}
