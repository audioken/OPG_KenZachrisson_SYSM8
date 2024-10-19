using Newton_Projektuppgift01_FitTrack.Model;
using Newton_Projektuppgift01_FitTrack.MVVM;
using System.Collections.ObjectModel;

namespace Newton_Projektuppgift01_FitTrack.ViewModel
{
    public class WorkoutDetailsWindowViewModel : ViewModelBase
    {
        // EGENSKAPER ↓
        //public User User { get; set; }
        public Workout Workout { get; set; }

        public Workout WorkoutEditable { get; set; }

        private bool isEditDataGridEnabled;
        public bool IsEditDataGridEnabled
        {
            get { return isEditDataGridEnabled; }
            set
            {
                isEditDataGridEnabled = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Workout> workoutList;
        public ObservableCollection<Workout> WorkoutList
        {
            get { return workoutList; }
            set
            {
                workoutList = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand EditWorkoutCommand => new RelayCommand(execute => EditWorkout());
        public RelayCommand CancelEditCommand => new RelayCommand(execute => CancelEdit());
        public RelayCommand SaveEditCommand => new RelayCommand(execute => SaveWorkout());

        // KONSTRUKTOR ↓
        public WorkoutDetailsWindowViewModel()
        {
            // Hämtar den valda träningen
            Workout = Manager.Instance.CurrentWorkout;

            // Klonar träning som backup för återställning vid klick på cancel
            WorkoutEditable = Workout.Clone();

            // Lägger in träningen i lista för att kunna visas i DataGrid
            WorkoutList = new ObservableCollection<Workout> { WorkoutEditable };

            // Avaktiverar DataGrid för att initiellt förhindra redigering
            IsEditDataGridEnabled = false;
        }

        // METODER ↓
        // Möjliggör redigering av valt träningspass
        public void EditWorkout()
        {
            // Aktiverar redigering av DataGrid som visar Workout
            IsEditDataGridEnabled = true;
        }

        // Avbryter redigering och återställer värdena
        public void CancelEdit()
        {
            // Återgår till originalvärden
            WorkoutEditable = Workout.Clone();

            // Instansierar listan på nytt med originalvärden
            WorkoutList = new ObservableCollection<Workout> { WorkoutEditable };

            // Uppdaterar listan
            OnPropertyChanged(nameof(WorkoutList));

            // Stänger av möjligheten att redigera träning i DataGrid
            IsEditDataGridEnabled = false;
        }

        // Sparar ändringar
        public void SaveWorkout()
        {
            // Kod för att spara ändringar av workout
        }
    }
}
