using Newton_Projektuppgift01_FitTrack.Model;
using Newton_Projektuppgift01_FitTrack.MVVM;
using System.Collections.ObjectModel;

namespace Newton_Projektuppgift01_FitTrack.ViewModel
{
    public class WorkoutDetailsWindowViewModel : ViewModelBase
    {
        //public User User { get; set; }
        public Workout Workout { get; set; }

        private bool isEnabled;
        public bool IsEnabled
        {
            get { return isEnabled; }
            set
            {
                isEnabled = value;
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

        public WorkoutDetailsWindowViewModel()
        {
            //User = Manager.Instance.CurrentUser;        
            Workout = Manager.Instance.CurrentWorkout;
            WorkoutList = new ObservableCollection<Workout> { Workout };
            IsEnabled = false;
        }

        public void EditWorkout()
        {
            // Aktiverar redigering av DataGrid som visar Workout
            IsEnabled = true;
        }

        public void SaveWorkout()
        {
            // Kod för att spara ändringar av workout
        }
    }
}
