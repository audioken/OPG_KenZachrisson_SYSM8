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
        //public string UsernameInput { get; set; }

        private string usernameInput;
        public string UsernameInput
        {
            get { return usernameInput; }
            set
            {
                usernameInput = value;
                OnPropertyChanged();

                if (string.IsNullOrEmpty(UsernameInput))
                {
                    PHUsernameVisibility = "Visible";
                }
                else
                {
                    PHUsernameVisibility = "Collapsed";
                }
            }
        }

        private string passwordInput;
        public string PasswordInput
        {
            get { return passwordInput; }
            set
            {
                passwordInput = value;
                OnPropertyChanged();

                if (string.IsNullOrEmpty(PasswordInput))
                {
                    PHPasswordVisibility = "Visible";
                }
                else
                {
                    PHPasswordVisibility = "Collapsed";
                }
            }
        }

        private string twoFAInput;
        public string TwoFAInput
        {
            get { return twoFAInput; }
            set
            {
                twoFAInput = value;
                OnPropertyChanged();

                if (string.IsNullOrEmpty(TwoFAInput))
                {
                    PHTwoFAVisibility = "Visible";
                }
                else
                {
                    PHTwoFAVisibility = "Collapsed";
                }
            }
        }

        public string TwoFACode { get; set; }

        private string securityQuestion;
        public string SecurityQuestion
        {
            get { return securityQuestion; }
            set
            {
                securityQuestion = value;
                OnPropertyChanged();
            }
        }

        private string securityAnswerInput;
        public string SecurityAnswerInput
        {
            get { return securityAnswerInput; }
            set
            {
                securityAnswerInput = value;
                OnPropertyChanged();

                if (string.IsNullOrEmpty(securityAnswerInput))
                {
                    GenerateNewPasswordVisibility = "Collapsed";
                    CancelNewPasswordVisibility = "Visible";
                }
                else
                {
                    GenerateNewPasswordVisibility = "Visible";
                    CancelNewPasswordVisibility = "Collapsed";
                }
            }
        }

        // Visas när användaren klickar på knappen "Forgot Password"
        private string securityVisibility;
        public string SecurityVisibility
        {
            get { return securityVisibility; }
            set
            {
                securityVisibility = value;
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

        // Dölj medan användaren försöker återställa lösenordet
        private string twoFAVisibility;
        public string TwoFAVisibility
        {
            get { return twoFAVisibility; }
            set
            {
                twoFAVisibility = value;
                OnPropertyChanged();
            }
        }

        private string pHUsernameVisibility;
        public string PHUsernameVisibility
        {
            get { return pHUsernameVisibility; }
            set
            {
                pHUsernameVisibility = value;
                OnPropertyChanged();
            }
        }

        private string pHPasswordVisibility;
        public string PHPasswordVisibility
        {
            get { return pHPasswordVisibility; }
            set
            {
                pHPasswordVisibility = value;
                OnPropertyChanged();
            }
        }

        private string pHTwoFAVisibility;
        public string PHTwoFAVisibility
        {
            get { return pHTwoFAVisibility; }
            set
            {
                pHTwoFAVisibility = value;
                OnPropertyChanged();
            }
        }

        private string signInVisibility;
        public string SignInVisibility
        {
            get { return signInVisibility; }
            set
            {
                signInVisibility = value;
                OnPropertyChanged();
            }
        }

        private string cancelNewPasswordVisibility;
        public string CancelNewPasswordVisibility
        {
            get { return cancelNewPasswordVisibility; }
            set
            {
                cancelNewPasswordVisibility = value;
                OnPropertyChanged();
            }
        }

        // Relay-kommand som anropar olika metoder för inloggning och registrering vid klick
        public RelayCommand SignInCommand => new RelayCommand(execute => SignIn());
        public RelayCommand RegisterCommand => new RelayCommand(execute => Register());
        public RelayCommand ForgotPasswordCommand => new RelayCommand(execute => ForgotPassword());
        public RelayCommand GenerateNewPasswordCommand => new RelayCommand(execute => GenerateNewPassword());
        public RelayCommand SendTwoFACommand => new RelayCommand(execute => SendTwoFA());
        public RelayCommand CancelNewPasswordCommand => new RelayCommand(execute => Cancel());

        // KONSTRUKTOR ↓
        public MainWindowViewModel()
        {
            // Tillfälligt för snabbare inlogg vid testning
            UsernameInput = "";
            PasswordInput = "";
            TwoFAInput = "";
            TwoFACode = "123456";

            // Döljer label, knapp och textbox som dyker upp först när användaren klickar på Forgot Password
            SecurityVisibility = "Collapsed";
            GenerateNewPasswordVisibility = "Collapsed";
            CancelNewPasswordVisibility = "Collapsed";
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
            bool didUsernameExist = false;

            // Kolla så text är inmatad för användarnamn
            if (!string.IsNullOrEmpty(UsernameInput))
            {
                // Kollar igenom användarlistan i Managerklassen
                foreach (User user in Manager.Instance.AllUsers)
                {
                    // Kontrollerar så det matchar en användarprofil
                    if (UsernameInput == user.Username)
                    {
                        // Hämta användarens säkerhetsfråga
                        SecurityQuestion = user.SecurityQuestion;

                        // Visa knapp och textbox som behövs för återställning av lösenord
                        SecurityVisibility = "Visible";
                        TwoFAVisibility = "Collapsed";
                        PHTwoFAVisibility = "Collapsed";
                        SignInVisibility = "Collapsed";
                        CancelNewPasswordVisibility = "Visible";

                        // Se till att det är rätt användares profil som ändras
                        Manager.Instance.CurrentUser = user;

                        didUsernameExist = true;

                        break;
                    }
                }
                if (!didUsernameExist) { MessageBox.Show("Användarnamnet finns tyvärr inte!"); }
            }
            else { MessageBox.Show("Du måste skriva in ett giltigt användarnamn!"); }
        }

        // Generera ett nytt lösenord
        public void GenerateNewPassword()
        {
            // Skickar användarens svar på säkerhetsfrågan till metoden för återställning i User-objektet
            bool isPasswordChanged = Manager.Instance.CurrentUser.ResetPassword(SecurityAnswerInput);

            if (isPasswordChanged)
            {
                SecurityAnswerInput = "";
                TwoFAInput = "";

                TwoFAVisibility = "Visible";
                PHTwoFAVisibility = "Visible";
                SecurityVisibility = "Collapsed";
                GenerateNewPasswordVisibility = "Collapsed";
                SignInVisibility = "Visible";
                CancelNewPasswordVisibility = "Collapsed";
            }
            //else
            //{
            //    TwoFAVisibility = "Collapsed";
            //    PHTwoFAVisibility = "Collapsed";
            //    SecurityVisibility = "Visible";
            //    GenerateNewPasswordVisibility = "Visible";
            //    SignInVisibility = "Collapsed";
            //    CancelNewPasswordVisibility = "Visible";
            //}
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

        public void Cancel()
        {
            TwoFAVisibility = "Visible";

            if (string.IsNullOrEmpty(TwoFAInput))
            {
                PHTwoFAVisibility = "Visible";
            }
            else
            {
                PHTwoFAVisibility = "Collapsed";
            }

            SecurityVisibility = "Collapsed";
            GenerateNewPasswordVisibility = "Collapsed";
            SignInVisibility = "Visible";
            CancelNewPasswordVisibility = "Collapsed";
        }
    }
}
