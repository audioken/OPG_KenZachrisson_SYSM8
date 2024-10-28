using System.Collections.ObjectModel;

namespace Newton_Projektuppgift01_FitTrack.Model
{
    public class Manager
    {
        // FÄLT ↓
        private User user;
        private AdminUser admin;

        //EGENSKAPER ↓
        // Singleton-implementering av Manager för att möjliggöra åtkomst över hela projektet
        private static Manager _instance;
        public static Manager Instance => _instance ??= new Manager();

        // Deklarerar listor som lagrar alla användare och träningar
        public ObservableCollection<User> AllUsers { get; private set; } // Readonly

        // Håller koll på inloggad användare
        public User CurrentUser { get; set; }
        public Workout CurrentWorkout { get; set; }
        public Workout CopiedWorkout { get; set; }

        // KONSTRUKTOR ↓
        private Manager()
        {
            // Startanvändare redan inlagda för testning
            user = new User("user", "password", "Sweden", "The name of your favourite pet?", "Fido");
            admin = new AdminUser("admin", "password", "Sweden");

            AllUsers = new ObservableCollection<User>();

            AddUser(user);
            AddUser(admin);

            // Parametrar för förinlagda exempelträningar
            DateTime dateTime = DateTime.Now;
            TimeSpan timeSpan = TimeSpan.FromMinutes(30);

            // Förinlagda träningar för profilen "user"
            Workout w1 = new StrengthWorkout(dateTime, "Strength Workout", timeSpan, 200, "Tynglyftning", 20);
            Workout w2 = new CardioWorkout(dateTime, "Cardio Workout", timeSpan, 300, "Row", 12);
            Workout w3 = new CardioWorkout(dateTime, "Cardio Workout", timeSpan, 300, "Marathon", 42);
            Workout w4 = new CardioWorkout(dateTime, "Cardio Workout", timeSpan, 300, "Running", 10);

            // Lägger till träningarna i användarens träningslista
            user.UserWorkouts.Add(w1);
            user.UserWorkouts.Add(w2);
            user.UserWorkouts.Add(w3);
            user.UserWorkouts.Add(w4);
        }

        public void AddUser(User user)
        {
            AllUsers.Add(user);
        }
    }
}
