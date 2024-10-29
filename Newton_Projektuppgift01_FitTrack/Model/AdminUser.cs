using System.Collections.ObjectModel;

namespace Newton_Projektuppgift01_FitTrack.Model
{
    public class AdminUser : User
    {
        // KONSTRUKTOR ↓
        public AdminUser(string Username, string Password, string Country) : base(Username, Password, Country) { }

        // METODER ↓
        // Hämtar alla användares träningspass och samlar i en lista
        public ObservableCollection<Workout> ManageAllWorkouts()
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
    }
}
