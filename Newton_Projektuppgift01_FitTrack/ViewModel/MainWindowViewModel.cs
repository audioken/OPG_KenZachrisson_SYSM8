using Newton_Projektuppgift01_FitTrack.Model;
using Newton_Projektuppgift01_FitTrack.MVVM;
using Newton_Projektuppgift01_FitTrack.View;
using System.Windows;

namespace Newton_Projektuppgift01_FitTrack.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        // EGENSKAPER ↓
        // Logotyp för applikationen
        public string LabelTitle { get; set; }
        public string LabelTitle2 { get; set; }

        // Inmatning av användarnamn
        private string usernameInput;
        public string UsernameInput
        {
            get { return usernameInput; }
            set
            {
                usernameInput = value;
                OnPropertyChanged();

                // Visar stödtext om inmatningsfältet är tomt
                if (string.IsNullOrEmpty(UsernameInput))
                {
                    PHUsernameVisibility = Visibility.Visible;
                }
                // Döljer stödtexten om inmatningsfältet har värde
                else
                {
                    PHUsernameVisibility = Visibility.Collapsed;
                }
            }
        }

        // Inmatning av lösenord
        private string passwordInput;
        public string PasswordInput
        {
            get { return passwordInput; }
            set
            {
                passwordInput = value;
                OnPropertyChanged();

                // Visar stödtext om inmatningsfältet är tomt
                if (string.IsNullOrEmpty(PasswordInput))
                {
                    PHPasswordVisibility = Visibility.Visible;
                }
                // Döljer stödtexten om inmatningsfältet har värde
                else
                {
                    PHPasswordVisibility = Visibility.Collapsed;
                }
            }
        }

        // Inmatning av 2FA-kod
        private string twoFAInput;
        public string TwoFAInput
        {
            get { return twoFAInput; }
            set
            {
                twoFAInput = value;
                OnPropertyChanged();

                // Visar stödtext om inmatningsfältet är tomt
                if (string.IsNullOrEmpty(TwoFAInput))
                {
                    PHTwoFAVisibility = Visibility.Visible;
                }
                // Döljer stödtexten om inmatningsfältet har värde
                else
                {
                    PHTwoFAVisibility = Visibility.Collapsed;
                }
            }
        }

        // Slumpad 2FA-kod
        public string TwoFACode { get; set; }

        // Visar användarens säkerhetsfråga
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

        // Inmatning av svar på säkerhetsfråga
        private string securityAnswerInput;
        public string SecurityAnswerInput
        {
            get { return securityAnswerInput; }
            set
            {
                securityAnswerInput = value;
                OnPropertyChanged();

                // Visar knappar beroende på inmatning
                if (string.IsNullOrEmpty(securityAnswerInput))
                {
                    // Utan inmatning visas Cancel
                    GenerateNewPasswordVisibility = Visibility.Collapsed;
                    CancelNewPasswordVisibility = Visibility.Visible;
                }
                else
                {
                    // Med inmatning visas Generate New Password
                    GenerateNewPasswordVisibility = Visibility.Visible;
                    CancelNewPasswordVisibility = Visibility.Collapsed;
                }
            }
        }

        // Döljer eller visar säkerhetsfråga och inmatning av svar
        private Visibility securityVisibility;
        public Visibility SecurityVisibility
        {
            get { return securityVisibility; }
            set
            {
                securityVisibility = value;
                OnPropertyChanged();
            }
        }

        // Döljer eller visar knappen för generering av nytt lösenord
        private Visibility generateNewPasswordVisibility;
        public Visibility GenerateNewPasswordVisibility
        {
            get { return generateNewPasswordVisibility; }
            set
            {
                generateNewPasswordVisibility = value;
                OnPropertyChanged();
            }
        }

        // Döljer eller visar 2FA inmatningen
        private Visibility twoFAVisibility;
        public Visibility TwoFAVisibility
        {
            get { return twoFAVisibility; }
            set
            {
                twoFAVisibility = value;
                OnPropertyChanged();
            }
        }

        // Döljer eller visar stödtext/placeholders för olika inmatningar
        private Visibility pHUsernameVisibility;
        public Visibility PHUsernameVisibility
        {
            get { return pHUsernameVisibility; }
            set
            {
                pHUsernameVisibility = value;
                OnPropertyChanged();
            }
        }
        private Visibility pHPasswordVisibility;
        public Visibility PHPasswordVisibility
        {
            get { return pHPasswordVisibility; }
            set
            {
                pHPasswordVisibility = value;
                OnPropertyChanged();
            }
        }
        private Visibility pHTwoFAVisibility;
        public Visibility PHTwoFAVisibility
        {
            get { return pHTwoFAVisibility; }
            set
            {
                pHTwoFAVisibility = value;
                OnPropertyChanged();
            }
        }

        // Döljer eller visar knappar för inloggning och registrering 
        private Visibility signInVisibility;
        public Visibility SignInVisibility
        {
            get { return signInVisibility; }
            set
            {
                signInVisibility = value;
                OnPropertyChanged();
            }
        }

        // Döljer eller visar knappen som avbryter säkerhetsfrågan
        private Visibility cancelNewPasswordVisibility;
        public Visibility CancelNewPasswordVisibility
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
        public RelayCommand SendTwoFACommand => new RelayCommand(execute => GenerateAndSendTwoFA());
        public RelayCommand CancelNewPasswordCommand => new RelayCommand(execute => HideSecurityQuestion());

        // KONSTRUKTOR ↓
        public MainWindowViewModel()
        {
            // Sätter texten för applikationens namn
            LabelTitle = "Fit";
            LabelTitle2 = "Track";

            // Tillfälligt för snabbare inlogg vid testning
            UsernameInput = "user";
            PasswordInput = "password";
            TwoFAInput = "123456";
            TwoFACode = "123456";

            // Dölj säkerhetsfrågan initiellt
            HideSecurityQuestion();
        }

        // METODER ↓
        // Kontroll för inloggning
        public void SignIn()
        {
            // Kollar om kontot hittas
            bool accountFound = false;

            // Kolla så text är inmatad
            if (!string.IsNullOrEmpty(UsernameInput) && !string.IsNullOrEmpty(PasswordInput) && !string.IsNullOrEmpty(TwoFAInput))
            {
                // Kollar igenom användarlistan i Managerklassen
                foreach (User user in Manager.Instance.AllUsers)
                {
                    // Kontrollerar om det matchar en användarprofil
                    if (UsernameInput == user.Username && PasswordInput == user.Password)
                    {
                        // Kontot hittades
                        accountFound = true;

                        // Kontrollerar så 2FA stämmer
                        if (TwoFAInput == TwoFACode)
                        {
                            // Loggar in admin eller användare
                            if (user is AdminUser admin)
                            {
                                // Skapar en referens till den inloggade
                                Manager.Instance.CurrentUser = admin;

                                // Anropar metod för att skriva ut inloggningsinformation
                                admin.SignIn();
                            }
                            else
                            {
                                Manager.Instance.CurrentUser = user;
                                user.SignIn();
                            }

                            // Öppna WorkoutWindow
                            OpenWorkoutWindow();

                            // Stäng MainWindow
                            Application.Current.MainWindow.Close();

                            // Behöver inte iterera mer
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

            // Stänger MainWindow
            Application.Current.MainWindow.Close();
        }

        // Möjliggör återställning av lösenord
        public void ForgotPassword()
        {
            // Kollar om användaren hittas
            bool didUsernameExist = false;

            // Kolla så text är inmatad för användarnamn
            if (!string.IsNullOrEmpty(UsernameInput))
            {
                // Kollar igenom användarlistan i Managerklassen
                foreach (User user in Manager.Instance.AllUsers)
                {
                    // Om det matchar en användare
                    if (UsernameInput == user.Username)
                    {
                        // Hämta säkerhetsfråga
                        SecurityQuestion = user.SecurityQuestion;

                        // Visa säkerhetsfråga
                        ShowSecurityQuestion();

                        // Hämta användare
                        Manager.Instance.CurrentUser = user;

                        // Användaren hittades
                        didUsernameExist = true;

                        // Avbryt iterering
                        break;
                    }
                }
                if (!didUsernameExist) { MessageBox.Show("Användarnamnet finns tyvärr inte!"); }
            }
            else { MessageBox.Show("Du måste skriva in ett giltigt användarnamn!"); }
        }

        // Dölj säkerhetsfråga
        public void HideSecurityQuestion()
        {
            // Kontroll för stödtext
            if (string.IsNullOrEmpty(TwoFAInput))
            {
                PHTwoFAVisibility = Visibility.Visible;
            }
            else
            {
                PHTwoFAVisibility = Visibility.Collapsed;
            }

            // Visa 2FA samt knappar för inloggning och registrering
            TwoFAVisibility = Visibility.Visible;
            SignInVisibility = Visibility.Visible;

            // Dölj alla element relaterade till säkerhetsfrågan
            SecurityVisibility = Visibility.Collapsed;
            GenerateNewPasswordVisibility = Visibility.Collapsed;
            CancelNewPasswordVisibility = Visibility.Collapsed;
        }

        // Visa säkerhetsfråga
        public void ShowSecurityQuestion()
        {
            // Visa alla element relaterade till säkerhetsfrågan
            SecurityVisibility = Visibility.Visible;
            CancelNewPasswordVisibility = Visibility.Visible;

            // Dölj 2FA samt knappar för inloggning och registrering
            TwoFAVisibility = Visibility.Collapsed;
            PHTwoFAVisibility = Visibility.Collapsed;
            SignInVisibility = Visibility.Collapsed;
        }

        // Generera ett nytt lösenord
        public void GenerateNewPassword()
        {
            // Genererar ett nytt lösenord, samt lagrar det, om svaret på säkerhetsfrågan är rätt
            bool isPasswordChanged = Manager.Instance.CurrentUser.ResetPassword(SecurityAnswerInput);

            // Återgå till startlayout om lösenordet ändrats
            if (isPasswordChanged)
            {
                // Återställ alla inmatningar för lösenord
                PasswordInput = "";
                TwoFAInput = "";
                SecurityAnswerInput = "";

                // Dölj säkerhetsfråga
                HideSecurityQuestion();
            }
        }

        // Generera och skicka en slumpad 2FA-kod som "SMS" till användaren
        public void GenerateAndSendTwoFA()
        {
            // Skapa ett objekt för slumpade nummer
            Random random = new Random();

            // Generera en sexsiffrig kod och lagra den för kontroll vid inlogg
            TwoFACode = random.Next(100000, 1000000).ToString();

            MessageBox.Show($"Din 2FA-kod är {TwoFACode}");
        }

        // Öppna WorkoutWindow
        public void OpenWorkoutWindow()
        {
            WorkoutWindow workoutWindow = new WorkoutWindow();
            workoutWindow.Show();
        }
    }
}
