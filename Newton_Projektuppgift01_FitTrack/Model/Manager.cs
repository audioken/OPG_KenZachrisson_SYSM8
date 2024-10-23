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
            user = new User("user", "password", "Sweden", "What's your favourite pet?", "Fido");
            admin = new AdminUser("admin", "password", "Sweden");

            AllUsers = new ObservableCollection<User>();

            AddUser(user);
            AddUser(admin);

            // Parametrar för förinlagda exempelträningar
            DateTime dateTime = DateTime.Now;
            TimeSpan timeSpan = TimeSpan.FromMinutes(30);

            // Förinlagda träningar för profilen "user"
            Workout userWorkout1 = new StrengthWorkout(dateTime, "Strength Workout", timeSpan, 200, "Tynglyftning", 5);
            Workout userWorkout2 = new CardioWorkout(dateTime, "Cardio Workout", timeSpan, 300, "Running", 5000);

            // Lägger till träningarna i användarens träningslista
            user.UserWorkouts.Add(userWorkout1);
            user.UserWorkouts.Add(userWorkout2);
        }

        public void AddUser(User user)
        {
            AllUsers.Add(user);
        }
    }
}
