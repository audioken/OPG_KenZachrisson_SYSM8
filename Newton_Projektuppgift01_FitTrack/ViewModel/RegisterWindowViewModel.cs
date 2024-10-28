using Newton_Projektuppgift01_FitTrack.Model;
using Newton_Projektuppgift01_FitTrack.MVVM;
using System.Collections.ObjectModel;
using System.Windows;

namespace Newton_Projektuppgift01_FitTrack.ViewModel
{
    public class RegisterWindowViewModel : ViewModelBase
    {
        // EGENSKAPER ↓
        // Möjliggör stängning av detta fönster
        public Window _registerWindow { get; set; }

        // Användarnamn
        private string usernameInput;
        public string UsernameInput
        {
            get { return usernameInput; }
            set
            {
                usernameInput = value;
                OnPropertyChanged();

                if (string.IsNullOrEmpty(usernameInput))
                {
                    PHUsernameVisibility = Visibility.Visible;
                }
                else
                {
                    PHUsernameVisibility = Visibility.Collapsed;
                }
            }
        }
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

        // Lösenord
        private string passwordInput;
        public string PasswordInput
        {
            get { return passwordInput; }
            set
            {
                passwordInput = value;
                OnPropertyChanged();

                if (string.IsNullOrEmpty(passwordInput))
                {
                    PHPasswordVisibility = Visibility.Visible;
                }
                else
                {
                    PHPasswordVisibility = Visibility.Collapsed;
                }
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

        // Bekräfta lösenord
        private string confirmPasswordInput;
        public string ConfirmPasswordInput
        {
            get { return confirmPasswordInput; }
            set
            {
                confirmPasswordInput = value;
                OnPropertyChanged();

                if (string.IsNullOrEmpty(confirmPasswordInput))
                {
                    PHConfirmPasswordVisibility = Visibility.Visible;
                }
                else
                {
                    PHConfirmPasswordVisibility = Visibility.Collapsed;
                }
            }
        }
        private Visibility pHConfirmPasswordVisibility;
        public Visibility PHConfirmPasswordVisibility
        {
            get { return pHConfirmPasswordVisibility; }
            set
            {
                pHConfirmPasswordVisibility = value;
                OnPropertyChanged();
            }
        }

        // Säkerhetsfrågor
        public ObservableCollection<string> SecurityQuestions { get; set; }
        private string selectedSecurityQuestion;
        public string SelectedSecurityQuestion
        {
            get { return selectedSecurityQuestion; }
            set
            {
                selectedSecurityQuestion = value;
                OnPropertyChanged();
            }
        }

        // Säkerhetssvar
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
                    PHSecurityAnswerVisibility = Visibility.Visible;
                }
                else
                {
                    PHSecurityAnswerVisibility = Visibility.Collapsed;
                }
            }
        }
        private Visibility pHSecurityAnswerVisibility;
        public Visibility PHSecurityAnswerVisibility
        {
            get { return pHSecurityAnswerVisibility; }
            set
            {
                pHSecurityAnswerVisibility = value;
                OnPropertyChanged();
            }
        }

        // Länder
        public ObservableCollection<string> Countries { get; set; }
        public string CountryComboBox { get; set; }

        // Relaykommandon vid klick på knappar
        public RelayCommand RegisterNewUserCommand => new RelayCommand(execute => RegisterNewUser());
        public RelayCommand CancelCommand => new RelayCommand(execute => Cancel());

        // KONSTRUKTOR ↓
        public RegisterWindowViewModel(Window _registerWindow)
        {
            this._registerWindow = _registerWindow;

            // Instansierar alla länder
            Countries = new ObservableCollection<string>
            {
                "Sweden",
                "Denmark",
                "Norway",
                "Finland"
            };

            // Instansierar alla säkerhetsfrågor
            SecurityQuestions = new ObservableCollection<string>
            {
                "The name of your favourite pet?",
                "What was the name of your first car?",
                "What was the name of your childhood best friend?",
                "What city were your mother born in?"
            };
        }

        // METOD ↓
        // Registrera ny användare baserad på inmatad information
        public void RegisterNewUser()
        {
            // Kontrollerar så alla inputs har inmatning
            if (!string.IsNullOrEmpty(UsernameInput) && !string.IsNullOrEmpty(PasswordInput) &&
                !string.IsNullOrEmpty(ConfirmPasswordInput) && !string.IsNullOrEmpty(CountryComboBox) &&
                !string.IsNullOrEmpty(SecurityAnswerInput) && !string.IsNullOrEmpty(selectedSecurityQuestion))
            {
                if (UsernameInput.Length >= 3)
                {
                    // Kontroll för tillgängligt användarnamn
                    bool isUserNameAvailable = true;

                    // Kollar om användarnamnet redan finns
                    foreach (User user in Manager.Instance.AllUsers)
                    {
                        if (user.Username == UsernameInput)
                        {
                            isUserNameAvailable = false; // Samma användarnamn finns redan
                            break;
                        }
                    }

                    // Om användarnamnet är ledigt
                    if (isUserNameAvailable)
                    {
                        // Kontrollerar så lösenordet är starkt nog
                        string specialCharacters = "!@#$%^&*()-_=+[{]};:’\"|\\,<.>/?"; // Mall för lösenordskontroll av specialtecken
                        bool hasSpecial = PasswordInput.Any(c => specialCharacters.Contains(c)); // Innehåller det minst ett specialtecken?
                        bool hasLength = PasswordInput.Length > 7; // Innehåller det minst åtta tecken?
                        bool hasDigit = PasswordInput.Any(char.IsDigit); // Innehåller det minst en siffra?

                        // Om lösenordet är starkt nog
                        if (hasSpecial && hasLength && hasDigit)
                        {
                            // Om lösenord och bekräfta lösenord matchar
                            if (PasswordInput == ConfirmPasswordInput)
                            {
                                // Skapa ny användare baserat på den inmatade informationen
                                User newUser = new User(UsernameInput, PasswordInput, CountryComboBox, SelectedSecurityQuestion, SecurityAnswerInput);

                                // Lägg till ny användare i listan för alla användare
                                Manager.Instance.AddUser(newUser);

                                MessageBox.Show($"Tack {UsernameInput}! Din användarprofil har skapats. Var god logga in..");

                                // Öppna MainWindow
                                OpenMainWindow();

                                // Stäng RegisterWindow
                                _registerWindow.Close();
                            }
                            else { MessageBox.Show("Lösenorden matchar inte!"); }
                        }
                        else { MessageBox.Show("Minst en åtta tecken, en siffra och ett specieltecken!"); }
                    }
                    else { MessageBox.Show("Användarnamnet finns redan!"); }
                }
                else { MessageBox.Show("Användarnamnet måste ha minst tre tecken!"); }
            }
            else { MessageBox.Show("Du måste fylla i all information!"); }
        }

        public void Cancel()
        {
            // Öppna MainWindow
            OpenMainWindow();

            // Stäng RegisterWindow
            _registerWindow.Close();
        }

        // Öppnar MainWindow
        public void OpenMainWindow()
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
        }
    }
}
