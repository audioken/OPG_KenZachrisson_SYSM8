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
        public ObservableCollection<User> AllUsers { get; private set; } = new ObservableCollection<User>(); // Readonly
        public ObservableCollection<Workout> AllWorkouts { get; private set; } = new ObservableCollection<Workout>(); // Readonly

        // Håller koll på inloggad användare
        public User CurrentUser { get; set; }
        public Workout CurrentWorkout { get; set; }
        public Workout CopiedWorkout { get; set; }

        // KONSTRUKTOR ↓
        private Manager()
        {
            // Startanvändare redan inlagda för testning
            user = new User("user", "password", "Sweden", "What's your favourite pet?", "Fido");
            admin = new AdminUser("admin", "password", "Sweden"); // BEHÖVER FIXAS SÅ MAN KAN VARA ADMIN

            // Parametrar för förinlagda exempelträningar
            DateTime dateTime = DateTime.Now;
            TimeSpan timeSpan = TimeSpan.FromMinutes(30);

            // Förinlagda träningar för profilen "user"
            Workout userWorkout1 = new StrengthWorkout(dateTime, "Strength Workout", timeSpan, 200, "Tynglyftning", 5);
            Workout userWorkout2 = new CardioWorkout(dateTime, "Cardio Workout", timeSpan, 300, "Running", 5000);

            // Lägger till träningarna i managerklassens lista över alla träningar
            AllWorkouts = new ObservableCollection<Workout> { userWorkout1, userWorkout2 };

            // Lägger till träningarna i användarens träningslista
            user.UserWorkouts = new ObservableCollection<Workout> { userWorkout1, userWorkout2 };

            // Instansierar lista för alla användare samt lägger till de två test-användarna
            AllUsers = new ObservableCollection<User> { user, admin };
        }
    }
}
