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
        public string TwoFAInput { get; set; } = "123456";
        public string TwoFACode { get; set; } = "123456";
        public string SecurityAnswerInput { get; set; }

        // Visas när användaren klickar på knappen "Forgot Password"
        private string securityAnswerVisibility;
        public string SecurityAnswerVisibility
        {
            get { return securityAnswerVisibility; }
            set
            {
                securityAnswerVisibility = value;
                OnPropertyChanged();
            }
        }

        // Visas när användaren klickar på knappen "Forgot Password"
        private string generateNewPasswordVisibility;
        public string GenerateNewPasswordVisibility
        {
            get { return generateNewPasswordVisibility; }
            set
            {
                generateNewPasswordVisibility = value;
                OnPropertyChanged();
            }
        }

        // Relay-kommand som anropar olika metoder för inloggning och registrering vid klick
        public RelayCommand SignInCommand => new RelayCommand(execute => SignIn());
        public RelayCommand RegisterCommand => new RelayCommand(execute => Register());
        public RelayCommand ForgotPasswordCommand => new RelayCommand(execute => ForgotPassword());
        public RelayCommand GenerateNewPasswordCommand => new RelayCommand(execute => GenerateNewPassword());
        public RelayCommand SendTwoFACommand => new RelayCommand(execute => SendTwoFA());

        // KONSTRUKTOR ↓
        public MainWindowViewModel()
        {
            // Döljer knapp och textbox som dyker upp först när användaren klickar på Forgot Password
            SecurityAnswerVisibility = "Collapsed";
            GenerateNewPasswordVisibility = "Collapsed";
        }

        // METODER ↓
        // Kontroll för inloggning
        public void SignIn()
        {
            // Kontroll om kontot finns
            bool accountFound = false;

            // Kolla så text är inmatad
            if (!string.IsNullOrEmpty(UsernameInput) && !string.IsNullOrEmpty(PasswordInput) && !string.IsNullOrEmpty(TwoFAInput))
            {
                // Kollar igenom användarlistan i Managerklassen
                foreach (User user in Manager.Instance.AllUsers)
                {
                    // Kontrollerar så det matchar en användarprofil
                    if (UsernameInput == user.Username && PasswordInput == user.Password)
                    {
                        // Undviker att skriva ut felmeddelandet efter foreach-loopen
                        accountFound = true;

                        // Kontrollerar så 2FA stämmer
                        if (TwoFAInput == TwoFACode)
                        {
                            if (user is AdminUser admin)
                            {
                                Manager.Instance.CurrentUser = admin;
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

                            break;
                        }
                        else { MessageBox.Show("2FA-koden är fel.."); }
                    }
                }
                if (!accountFound) { MessageBox.Show("Användarnamn och/eller lösenord är fel.."); }
            }
            else { MessageBox.Show("Du måste fylla i användarnamn, lösenord och 2FA-kod.."); }
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

        // Möjliggör återställning av lösenord
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
                        // Visa knapp och textbox som behövs för återställning av lösenord
                        SecurityAnswerVisibility = "Visible";
                        GenerateNewPasswordVisibility = "Visible";

                        // Se till att det är rätt användares profil som ändras
                        Manager.Instance.CurrentUser = user;
                    }
                }
            }
            else { MessageBox.Show("Du måste skriva in ett giltigt användarnamn!"); }
        }

        // Generera ett nytt lösenord
        public void GenerateNewPassword()
        {
            // Skickar användarens svar på säkerhetsfrågan till metoden för återställning i User-objektet
            Manager.Instance.CurrentUser.ResetPassword(SecurityAnswerInput);
        }

        // Generera en slumpad 2FA-kod som lagras för att kontrollera så att användarens inmatade kod är korrekt
        public void SendTwoFA()
        {
            // Skapa ett objekt för slumpade nummer
            Random random = new Random();

            // Generea ett sexsiffrigt tal och lagra i egenskapen "TwoFACode"
            TwoFACode = random.Next(100000, 1000000).ToString();

            MessageBox.Show($"Din 2FA-kod är {TwoFACode}");
        }
    }
}
