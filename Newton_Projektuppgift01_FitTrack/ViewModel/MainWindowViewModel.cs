using Newton_Projektuppgift01_FitTrack.Model;
using Newton_Projektuppgift01_FitTrack.MVVM;
using Newton_Projektuppgift01_FitTrack.View;
using System.Windows;

namespace Newton_Projektuppgift01_FitTrack.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        // EGENSKAPER ↓
        // Sätter titeln på applikationen
        public string LabelTitle { get; set; } = "FitTrack";

        // Spårar inloggningsuppgifter för kontroll
        public string UsernameInput { get; set; }
        public string PasswordInput { get; set; }

        // Relay-kommand som anropar olika metoder för inloggning och registrering vid klick
        public RelayCommand SignInCommand => new RelayCommand(execute => SignIn());
        public RelayCommand RegisterCommand => new RelayCommand(execute => Register());
        public RelayCommand ForgottPasswordCommand => new RelayCommand(execute => ForgotPassword()); // För VG

        // KONSTRUKTOR ↓
        public MainWindowViewModel()
        {
            // Skapar en tillfällig User för testning
            Manager.Instance.AllUsers.Add(new User("user", "password", "nocountry"));
        }

        // METODER ↓
        // Kontroll för inloggning
        public void SignIn()
        {
            // Kontroll om kontot finns
            bool accountFound = false;

            // Kolla så text är inmatad
            if (!string.IsNullOrEmpty(UsernameInput) && !string.IsNullOrEmpty(PasswordInput))
            {
                // Kollar igenom användarlistan i Managerklassen
                foreach (User user in Manager.Instance.AllUsers)
                {
                    // Kontrollerar så det matchar en användarprofil
                    if (UsernameInput == user.UserName && PasswordInput == user.Password)
                    {
                        // Lagrar nuvarande användare
                        Manager.Instance.CurrentUser = user;

                        // Anropar metod för att skriva ut inloggningsinformation
                        user.SignIn();

                        // Öppna "WorkoutWindow"
                        WorkoutWindow workoutWindow = new WorkoutWindow();
                        workoutWindow.Show();

                        // Testutskrift
                        accountFound = true; // Undviker att skriva ut felmeddelandet efter foreach-loopen
                        break;
                    }
                }

                // Felmeddelande om inget konto matchar
                if (accountFound == false)
                {
                    MessageBox.Show("Användarnamn och/eller lösenord är fel..");
                }
            }
            // Felmeddelande om textboxarna är tomma
            else
            {
                MessageBox.Show("Inget har skrivits in!");
            }
        }

        // Öppnar fönster för registrering av användare
        public void Register()
        {
            // Öppnar RegisterWindow
            RegisterWindow registerWindow = new RegisterWindow();
            registerWindow.Show();

            // Stänger ner MainWindow
            Application.Current.MainWindow.Close();
        }

        // För VG 
        public void ForgotPassword()
        {
            // Kolla så text är inmatad för användarnamn
            if (!string.IsNullOrEmpty(UsernameInput))
            {
                // Kollar igenom användarlistan i Managerklassen
                foreach (User user in Manager.Instance.AllUsers)
                {
                    // Kontrollerar så det matchar en användarprofil
                    if (UsernameInput == user.UserName)
                    {
                        // Kod för återställning av lösenord. Behöver man gör ett nytt window?
                        // user.ResetPassword(securityAnswer);
                    }
                }
            }
            // Felmeddelande om textboxen för användarnamn är tom
            else
            {
                MessageBox.Show("Du måste skriva in ett giltigt användarnamn!");
            }
        }
    }
}
