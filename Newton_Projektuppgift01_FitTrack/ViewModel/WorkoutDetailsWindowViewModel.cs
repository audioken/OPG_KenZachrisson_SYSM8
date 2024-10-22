﻿using Newton_Projektuppgift01_FitTrack.Model;
using Newton_Projektuppgift01_FitTrack.MVVM;
using Newton_Projektuppgift01_FitTrack.View;
using System.Collections.ObjectModel;
using System.Windows;

namespace Newton_Projektuppgift01_FitTrack.ViewModel
{
    public class WorkoutDetailsWindowViewModel : ViewModelBase
    {
        // EGENSKAPER ↓
        public Window _workoutDetailsWindow { get; set; }

        public User User { get; set; }

        // Används för att lagra den valda träningen
        public Workout Workout { get; set; }

        // Ska klona Workout för att möjligöra redigering
        public Workout WorkoutEditable { get; set; }

        // Kontroll för om DataGrid går att redigera eller ej
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

        // Tillfällig lista för att möjliggöra visning av träning i DataGrid
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

        // Relay-kommandon
        public RelayCommand EditWorkoutCommand => new RelayCommand(execute => EditWorkout());
        public RelayCommand AbortEditCommand => new RelayCommand(execute => AbortEdit());
        public RelayCommand SaveWorkoutCommand => new RelayCommand(execute => SaveWorkout());
        public RelayCommand CopyWorkoutCommand => new RelayCommand(execute => CopyWorkout());
        public RelayCommand CancelCommand => new RelayCommand(execute => Cancel());

        // KONSTRUKTOR ↓
        public WorkoutDetailsWindowViewModel(Window _workoutDetailsWindow)
        {
            this._workoutDetailsWindow = _workoutDetailsWindow;

            // Referens till inloggad användare
            User = Manager.Instance.CurrentUser;

            // Referens till den valda träningen
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
        public void AbortEdit()
        {
            // Klonar originalet för att återställa värdena
            WorkoutEditable = Workout.Clone();

            // Lägger in den klonade träningen i listan
            WorkoutList = new ObservableCollection<Workout> { WorkoutEditable };

            // Stänger av möjligheten att redigera träning i DataGrid
            IsEditDataGridEnabled = false;
        }

        // Sparar ändringar
        public void SaveWorkout()
        {
            // Hitta index för originalet av träningen som ändras
            int indexCurrentUser = Manager.Instance.CurrentUser.UserWorkouts.IndexOf(Workout);
            int indexManager = Manager.Instance.AllWorkouts.IndexOf(Workout);

            if (User is User && indexCurrentUser >= 0 && indexManager >= 0)
            {
                // Ersätt originalet med uppdaterad träning för den inloggade användaren
                Manager.Instance.CurrentUser.UserWorkouts[indexCurrentUser] = WorkoutEditable;

                // Ersätt också originalet i admins lista av alla träningar
                Manager.Instance.AllWorkouts[indexManager] = WorkoutEditable;
            }
            else if (User is AdminUser)
            {
                foreach (User user in Manager.Instance.AllUsers)
                {
                    if (user.UserWorkouts.Contains(Workout))
                    {
                        int indexUser = user.UserWorkouts.IndexOf(Workout);
                        user.UserWorkouts[indexUser] = WorkoutEditable;
                    }
                }

                Manager.Instance.AllWorkouts[indexManager] = WorkoutEditable;
            }
            else
            {
                // Annars lägg till träning
                Manager.Instance.CurrentUser.UserWorkouts.Add(WorkoutEditable);
                Manager.Instance.AllWorkouts.Add(WorkoutEditable);
            }

            // Tror inte denna behövs?
            Manager.Instance.CurrentWorkout = WorkoutEditable;

            // Avvaktivera möjlighet till redigering i DataGrid
            IsEditDataGridEnabled = false;

            // Öppna WorkoutWindow
            OpenWorkoutWindow();

            // Stäng WorkoutDetailsWindow
            _workoutDetailsWindow.Close();
        }

        public void CopyWorkout()
        {
            Manager.Instance.CopiedWorkout = WorkoutEditable;
            MessageBox.Show("Kopierat!");
        }

        public void Cancel()
        {
            // Öppna WorkoutWindow
            OpenWorkoutWindow();

            // Stäng WorkoutDetails
            _workoutDetailsWindow.Close();
        }

        // Öppna WorkoutWindow
        public void OpenWorkoutWindow()
        {
            WorkoutWindow workoutWindow = new WorkoutWindow();
            workoutWindow.Show();
        }


    }
}
