using System.Collections.ObjectModel;
using System.Windows;

namespace Newton_Projektuppgift01_FitTrack.Model
{
    public class Manager
    {
        //EGENSKAPER ↓
        // Singleton-implementering av Manager för att möjliggöra åtkomst över hela projektet
        private static Manager _instance;
        public static Manager Instance => _instance ??= new Manager();

        // Deklarerar listor som lagrar alla användare och träningar
        public ObservableCollection<User> AllUsers { get; private set; } // Readonly
        public ObservableCollection<Workout> AllWorkouts { get; private set; } // Readonly

        // Håller koll på inloggad användare
        public User CurrentUser { get; set; }
        public Workout CurrentWorkout { get; set; }

        // KONSTRUKTOR ↓
        private Manager()
        {
            // Start användare redan inlagda för testning
            User user = new User("user", "password", "Sweden");
            User admin = new AdminUser("admin", "password", "Sweden"); // BEHÖVER FIXAS SÅ MAN KAN VARA ADMIN

            // Parametrar för förinlagda exempelträningar
            DateTime dateTime = DateTime.Now;
            TimeSpan timeSpan = TimeSpan.FromMinutes(30);

            // Förinlagda träningar för profilen "user"
            Workout userWorkout1 = new StrengthWorkout(dateTime, "Strength Workout", timeSpan, 200, "Tynglyftning", 5);
            Workout userWorkout2 = new CardioWorkout(dateTime, "Cardio Workout", timeSpan, 300, "Running", 5000);

            // Lägger till träningarna i "user"
            user.UserWorkouts = new ObservableCollection<Workout> { userWorkout1, userWorkout2 };

            // Instansierar lista för alla användare samt lägger till de två test-användarna
            AllUsers = new ObservableCollection<User> { user, admin };

            // Instansierar lista för alla träningar
            AllWorkouts = new ObservableCollection<Workout> { userWorkout1, userWorkout2 };
        }

        // METODER ↓
        // Metod för testning så datan kommer fram
        public void PrintAllUsers()
        {
            foreach (User user in AllUsers)
            {
                MessageBox.Show($"{user.Username} {user.Password} {user.Country}");
            }
        }
    }
}
