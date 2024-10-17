using System.Collections.ObjectModel;
using System.Windows;

namespace Newton_Projektuppgift01_FitTrack.Model
{
    public class Manager
    {
        //EGENSKAPER ↓
        // Singleton-implementering av Manger för att möjliggöra åtkomst över hela projektet
        private static Manager _instance;
        public static Manager Instance => _instance ??= new Manager();

        // Lista som lagrar alla användare
        public ObservableCollection<User> AllUsers { get; private set; }

        // KONSTRUKTOR ↓
        private Manager()
        {
            // Instansierar listan "AllUsers"
            AllUsers = new ObservableCollection<User>();
        }

        // METODER ↓
        // Metod för testning så datan kommer fram
        public void PrintAllUsers()
        {
            foreach (User user in AllUsers)
            {
                MessageBox.Show($"{user.UserName} {user.Password} {user.Country}");
            }
        }
    }
}
