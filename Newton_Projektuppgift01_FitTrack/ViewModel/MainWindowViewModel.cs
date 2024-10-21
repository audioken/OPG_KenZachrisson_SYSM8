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
        public string UsernameInput { get; set; } = "user"; // Tillfälligt för att logga in snabbare
        public string PasswordInput { get; set; } = "password"; // Tillfälligt för att logga in snabbare

        // Relay-kommand som anropar olika metoder för inloggning och registrering vid klick
        public RelayCommand SignInCommand => new RelayCommand(execute => SignIn());
        public RelayCommand RegisterCommand => new RelayCommand(execute => Register());
        public RelayCommand ForgottPasswordCommand => new RelayCommand(execute => ForgotPassword()); // För VG

        // KONSTRUKTOR ↓
        public MainWindowViewModel()
        {
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
                    if (UsernameInput == user.Username && PasswordInput == user.Password)
                    {
                        if (user is AdminUser admin)
                        {
                            Manager.Instance.CurrentUser = admin;
                            //admin.ManageAllWorkouts(); // Skapar problem när admin ska ta bort pass från andra
                            admin.SignIn();
                        }
                        else
                        {
                            // Lagrar nuvarande användare
                            Manager.Instance.CurrentUser = user;

                            // Anropar metod för att skriva ut inloggningsinformation
                            user.SignIn();
                        }

                        // Öppna "WorkoutWindow"
                        WorkoutWindow workoutWindow = new WorkoutWindow();
                        workoutWindow.Show();

                        // Stäng "MainWindow"
                        Application.Current.MainWindow.Close();

                        // Testutskrift
                        accountFound = true; // Undviker att skriva ut felmeddelandet efter foreach-loopen
                        break;
                    }
                }
                if (!accountFound) { MessageBox.Show("Användarnamn och/eller lösenord är fel.."); }
            }
            else { MessageBox.Show("Du måste fylla i all information!"); }
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
                    if (UsernameInput == user.Username)
                    {
                        // Kod för återställning av lösenord. Behöver man gör ett nytt window?
                        // user.ResetPassword(securityAnswer);
                    }
                }
            }
            else { MessageBox.Show("Du måste skriva in ett giltigt användarnamn!"); }
        }
    }
}
