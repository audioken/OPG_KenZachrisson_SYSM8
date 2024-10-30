using System.Collections.ObjectModel;

namespace Newton_Projektuppgift01_FitTrack.Model
{
    public class Manager
    {
        // SINGLETON-IMPLEMENTERING ↓
        // Säkerställer att endast en instans av Manager existerar under applikationens livstid
        private static Manager _instance;

        // Publik egenskap som ger åtkomst till den enda instansen av Manager
        // Använder en lazy initialization för att skapa instansen första gången den efterfrågas
        public static Manager Instance => _instance ??= new Manager();

        // FÄLT ↓
        // Startanvändare
        private User user;
        private AdminUser admin;

        // EGENSKAPER ↓
        // Lista som ska lagra alla användare
        // Read-only då endast Manager ska kunna ändra listan
        public ObservableCollection<User> AllUsers { get; private set; }

        // Spårar inloggad användare
        public User CurrentUser { get; set; }

        // Spårar aktuell träning
        public Workout CurrentWorkout { get; set; }

        // Används temporärt vid kopiering av träningspass
        public Workout CopiedWorkout { get; set; }

        // KONSTRUKTOR ↓
        private Manager()
        {
            // Instansierar listan för alla användare
            AllUsers = new ObservableCollection<User>();

            // Skapar startanvändare för testning
            user = new User("user", "password", "Sweden", "The name of your favourite pet?", "Fido");
            admin = new AdminUser("admin", "password", "Sweden", "The name of your favourite pet?", "Fido");

            // Lägg till startanvändare i listan
            AddUser(user);
            AddUser(admin);

            // Förinlagda träningar för profilen "user"
            Workout w1 = new StrengthWorkout(new DateTime(2024, 10, 10, 14, 0, 0), "Strength Workout", TimeSpan.FromMinutes(30), 0, "Tyngdlyftning", 20);
            Workout w2 = new CardioWorkout(new DateTime(2024, 10, 12, 10, 30, 0), "Cardio Workout", TimeSpan.FromMinutes(45), 0, "Rodd", 12);
            Workout w3 = new CardioWorkout(new DateTime(2024, 10, 15, 7, 45, 0), "Cardio Workout", TimeSpan.FromMinutes(75), 0, "Lång runda", 15);
            Workout w4 = new CardioWorkout(new DateTime(2024, 10, 19, 11, 20, 0), "Cardio Workout", TimeSpan.FromMinutes(15), 0, "Spinning", 5);

            // Lägger till träningarna i användarens träningslista
            user.UserWorkouts.Add(w1);
            user.UserWorkouts.Add(w2);
            user.UserWorkouts.Add(w3);
            user.UserWorkouts.Add(w4);
        }

        // Lägger till ny användare i listan för alla användare
        public void AddUser(User user)
        {
            AllUsers.Add(user);
        }
    }
}
