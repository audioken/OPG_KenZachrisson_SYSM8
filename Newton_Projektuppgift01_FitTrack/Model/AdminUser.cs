using System.Collections.ObjectModel;
using System.Windows;

namespace Newton_Projektuppgift01_FitTrack.Model
{
    public class AdminUser : User
    {
        // KONSTRUKTOR ↓
        public AdminUser(string Username, string Password, string Country, string SecurityQuestion, string SecurityAnswer) : base(Username, Password, Country, SecurityQuestion, SecurityAnswer) { }

        // METODER ↓
        // Hämtar alla användares träningspass och samlar i en lista
        public ObservableCollection<Workout> ManageAllWorkouts()
        {
            // Testa kodblock som säkerhetsåtergärd
            try
            {
                // Instansiera en ny träningslista
                ObservableCollection<Workout> WorkoutList = new ObservableCollection<Workout>();

                // Gå igenom alla användare
                foreach (User user in Manager.Instance.AllUsers)
                {
                    // Lägg till varje träning från användaren i listan
                    foreach (Workout workout in user.UserWorkouts)
                    {
                        WorkoutList.Add(workout);
                    }
                }

                // Returnera listan med alla träningspass
                return WorkoutList;
            }
            // Om ett oväntat fel sker
            catch (Exception ex)
            {
                MessageBox.Show($"Ett fel uppstod vid hantering av träningspass: {ex.Message}", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);

                // Returnera en tom lista om ett fel uppstår
                return new ObservableCollection<Workout>();
            }
        }
    }
}
