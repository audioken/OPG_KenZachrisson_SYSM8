﻿using Newton_Projektuppgift01_FitTrack.Model;
using Newton_Projektuppgift01_FitTrack.MVVM;
using Newton_Projektuppgift01_FitTrack.View;
using System.Collections.ObjectModel;
using System.Windows;

namespace Newton_Projektuppgift01_FitTrack.ViewModel
{
    public class WorkoutWindowViewModel : ViewModelBase
    {
        // EGENSKAPER ↓
        // Möjliggör stängning av detta fönster
        public Window _workoutWindow { get; set; }

        // Spårar användarnamn och får sitt värde i konstruktorn
        public User User { get; } // Read-only

        // Den valda träningen från listan
        public Workout? SelectedWorkout { get; set; }

        // Listan med träningspass
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

        // Tillfällig lista för att möjliggöra filtrering
        public ObservableCollection<Workout> FilteredWorkoutList { get; set; }

        // Sökfiltret med döljbar stödtext
        private string searchFilter;
        public string SearchFilter
        {
            get { return searchFilter; }
            set
            {
                searchFilter = value;
                OnPropertyChanged();

                // Filtrera i listan med träningar
                ApplyCombinedFilter();

                // Visar stödtext om inmatningsfältet är tomt
                if (string.IsNullOrEmpty(searchFilter))
                {
                    PHSearchFilterVisibility = Visibility.Visible;
                }
                // Döljer stödtexten om inmatningsfältet har värde
                else
                {
                    PHSearchFilterVisibility = Visibility.Collapsed;
                }
            }
        }
        private Visibility pHSearchFilterVisibility;
        public Visibility PHSearchFilterVisibility
        {
            get { return pHSearchFilterVisibility; }
            set
            {
                pHSearchFilterVisibility = value;
                OnPropertyChanged();
            }
        }

        // Varaktighetsfiltret
        private int durationFilter;
        public int DurationFilter
        {
            get { return durationFilter; }
            set
            {
                durationFilter = value;
                OnPropertyChanged();

                // Filtrera i listan med träningar
                ApplyCombinedFilter();
            }
        }

        // Döljer eller visar Clear Filter-knappen
        private Visibility clearFilterVisibility;
        public Visibility ClearFilterVisibility
        {
            get { return clearFilterVisibility; }
            set
            {
                clearFilterVisibility = value;
                OnPropertyChanged();
            }
        }

        // Bestämmer om sökfiltret är i fokus
        private bool isSearchFilterFocused;
        public bool IsSearchFilterFocused
        {
            get { return isSearchFilterFocused; }
            set
            {
                isSearchFilterFocused = value;
                OnPropertyChanged();
            }
        }


        // Relay-kommando som öppnar olika fönster vid klick
        public RelayCommand AppInfoCommand => new RelayCommand(execute => AppInfo());
        public RelayCommand UserDetailsCommand => new RelayCommand(execute => OpenUserDetails());
        public RelayCommand SignOutCommand => new RelayCommand(execute => SignOut());
        public RelayCommand AddWorkoutCommand => new RelayCommand(execute => AddWorkout());
        public RelayCommand RemoveWorkoutCommand => new RelayCommand(execute => RemoveWorkout());
        public RelayCommand ClearFilterCommand => new RelayCommand(execute => ClearFilter());
        public RelayCommand WorkoutDetailsCommand => new RelayCommand(execute => OpenWorkoutDetails());

        // KONSTRUKTOR ↓
        public WorkoutWindowViewModel(Window _workoutWindow)
        {
            this._workoutWindow = _workoutWindow;

            // Instansierar värden för att undvika nullvarningar
            FilteredWorkoutList = new ObservableCollection<Workout>();
            workoutList = new ObservableCollection<Workout>();
            User = new User("No one", "No password", "No country");

            // Sätter fokuset på sökfiltret
            IsSearchFilterFocused = true;

            // Sätter startvärdet för båda filter
            DurationFilter = 0;
            SearchFilter = "";

            // Undviker nullvarning
            searchFilter = "";

            // Nullkontroll
            if (Manager.Instance.CurrentUser != null)
            {
                // Spårar användaren för att kunna visa i fönstret
                User = Manager.Instance.CurrentUser;
            }
            else
            {
                MessageBox.Show("Kunde inte hämta någon användare!", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);

                // Logga ut användaren
                SignOut();
            }

            // Testa kodblock som säkerhetsåtergärd
            try
            {
                // Kolla om admin är inloggad
                if (Manager.Instance.CurrentUser is AdminUser admin)
                {
                    // Hämta alla användares träningspass
                    WorkoutList = admin.ManageAllWorkouts();
                }
                else
                {
                    // Nullkontroll
                    if (Manager.Instance.CurrentUser != null)
                    {
                        // Hämta endast användarens egna träningspass
                        WorkoutList = Manager.Instance.CurrentUser.UserWorkouts;
                    }
                    else
                    {
                        MessageBox.Show("Kunde inte hämta någon användare!", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);

                        // Logga ut användaren
                        SignOut();
                    }
                }
            }
            // Om ett oväntat fel sker
            catch (Exception ex)
            {
                MessageBox.Show($"Ett fel uppstod vid hämtning av träningspass: {ex.Message}", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);

                // Logga ut användaren
                SignOut();
            }

            // Uppdatera vyn
            ApplyCombinedFilter();
        }

        // METODER ↓
        // Öppnar en popup med information om företaget och appen
        private void AppInfo()
        {
            CompanyInfoWindow companyInfoWindow = new CompanyInfoWindow();
            companyInfoWindow.Show();
        }

        // Öppna fönster för användarens profilinställningar
        private void OpenUserDetails()
        {
            // Öppnar UserDetailsWindow
            OpenUserDetailsWindow();

            // Stänger WorkoutWindow
            _workoutWindow.Close();
        }

        // Logga ut och återgå till MainWindow
        private void SignOut()
        {
            // Rensar all spårning
            Manager.Instance.CurrentUser = null;
            Manager.Instance.CurrentWorkout = null;
            Manager.Instance.CopiedWorkout = null;

            // Öppnar MainWindow
            OpenMainWindow();

            // Stäng WorkoutWindow
            _workoutWindow.Close();
        }

        // Öppnar fönster för att kunna lägga till ett träningspass
        private void AddWorkout()
        {
            // Öppnar AddWorkoutWindow
            OpenAddWorkoutWindow();

            // Stäng WorkoutWindow
            _workoutWindow.Close();
        }

        // Tar bort det valda träningspasset
        private void RemoveWorkout()
        {
            if (SelectedWorkout != null)
            {
                // Håller koll på om träningen raderats
                bool wasWorkoutRemoved = false;

                // Testa kodblock som säkerhetsåtergärd
                try
                {
                    // Kolla igenom alla användare
                    foreach (var user in Manager.Instance.AllUsers)
                    {
                        // Leta efter träningen
                        if (user.UserWorkouts.Contains(SelectedWorkout))
                        {
                            // Ta bort från användarens träningar
                            user.UserWorkouts.Remove(SelectedWorkout);

                            // Träningen raderades
                            wasWorkoutRemoved = true;

                            // Avbryter iterering
                            break;
                        }
                    }

                    // Om träningen är raderad
                    if (wasWorkoutRemoved)
                    {
                        // Ta bort från träningslista
                        WorkoutList.Remove(SelectedWorkout);

                        // Uppdatera träningslista
                        OnPropertyChanged(nameof(WorkoutList));

                        // Uppdatera vyn
                        ApplyCombinedFilter();
                    }
                }
                // Om ett oväntat fel sker
                catch (Exception ex)
                {
                    MessageBox.Show($"Ett fel uppstod vid borttagning av träningspass: {ex.Message}", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);

                    // Logga ut användaren
                    SignOut();
                }
            }
            else { MessageBox.Show("Du måste välja något i listan!", "Missing input!", MessageBoxButton.OK, MessageBoxImage.Warning); }
        }

        // Rensa filter
        private void ClearFilter()
        {
            SearchFilter = "";
            DurationFilter = 0;
        }

        // Öppna fönstret för detaljerad information om valt träningspass
        private void OpenWorkoutDetails()
        {
            // Om något är valt i listan
            if (SelectedWorkout != null)
            {
                // Tillfällig lagring av vald träning i managerklassen
                Manager.Instance.CurrentWorkout = SelectedWorkout;

                // Öppna WorkoutDetailsWindow
                OpenWorkoutDetailsWindow();

                // Stäng WorkoutWindow
                _workoutWindow.Close();
            }
            else { MessageBox.Show("Du måste välja något i listan!", "Missing input!", MessageBoxButton.OK, MessageBoxImage.Warning); }
        }

        // Kombinerar filterresultatet från SearchFilter och DurationFilter
        private void ApplyCombinedFilter()
        {
            // Kontrollera null
            if (WorkoutList == null || FilteredWorkoutList == null)
            {
                return;
            }

            // Rensa den tillfälliga listan för träningspass
            FilteredWorkoutList.Clear();

            // Hämta relevanta träningar från originallistan
            foreach (var workout in WorkoutList)
            {
                // Kontrollera om DurationFilter är aktiverat
                bool isDurationActive = workout.Duration.TotalMinutes >= DurationFilter;

                // Kontrollera om SearchFilter är aktiverat
                bool isSearchActive = string.IsNullOrEmpty(SearchFilter) || workout.Type.Contains(SearchFilter) || workout.Notes.Contains(SearchFilter);

                // Lägg till träningen om båda filterna är aktiva
                if (isDurationActive && isSearchActive)
                {
                    FilteredWorkoutList.Add(workout);
                }
            }

            // Döljer eller visar Clear Filter-knappen beroende på om det finns inmatning eller ej
            if (DurationFilter > 0 || SearchFilter != "")
            {
                ClearFilterVisibility = Visibility.Visible;
            }
            else
            {
                ClearFilterVisibility = Visibility.Collapsed;
            }
        }

        // Öppnar olika fönster
        private void OpenUserDetailsWindow()
        {
            UserDetailsWindow userDetailsWindow = new UserDetailsWindow();
            userDetailsWindow.Show();
        }
        private void OpenWorkoutDetailsWindow()
        {
            WorkoutDetailsWindow workoutDetailsWindow = new WorkoutDetailsWindow();
            workoutDetailsWindow.Show();
        }
        private void OpenMainWindow()
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
        }
        private void OpenAddWorkoutWindow()
        {
            AddWorkoutWindow addWorkoutWindow = new AddWorkoutWindow();
            addWorkoutWindow.Show();
        }
    }
}
