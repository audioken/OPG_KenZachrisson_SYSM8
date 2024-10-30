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

        // Användarnamn med döljbar stödtext
        private string usernameInput;
        public string UsernameInput
        {
            get { return usernameInput; }
            set
            {
                usernameInput = value;
                OnPropertyChanged();

                // Visar stödtext om inmatningsfältet är tomt
                if (string.IsNullOrEmpty(usernameInput))
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

        // Lösenord med döljbar stödtext
        private string passwordInput;
        public string PasswordInput
        {
            get { return passwordInput; }
            set
            {
                passwordInput = value;
                OnPropertyChanged();

                // Visar stödtext om inmatningsfältet är tomt
                if (string.IsNullOrEmpty(passwordInput))
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

        // Bekräfta lösenord med döljbar stödtext
        private string confirmPasswordInput;
        public string ConfirmPasswordInput
        {
            get { return confirmPasswordInput; }
            set
            {
                confirmPasswordInput = value;
                OnPropertyChanged();

                // Visar stödtext om inmatningsfältet är tomt
                if (string.IsNullOrEmpty(confirmPasswordInput))
                {
                    PHConfirmPasswordVisibility = Visibility.Visible;
                }
                // Döljer stödtexten om inmatningsfältet har värde
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

        // Lista med säkerhetsfrågor som får sina värden i konstruktorn
        public ObservableCollection<string> SecurityQuestions { get; } // Read-only

        // Den valda säkerhetsfrågan
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

        // Säkerhetssvar med döljbar stödtext
        private string securityAnswerInput;
        public string SecurityAnswerInput
        {
            get { return securityAnswerInput; }
            set
            {
                securityAnswerInput = value;
                OnPropertyChanged();

                // Visar stödtext om inmatningsfältet är tomt
                if (string.IsNullOrEmpty(securityAnswerInput))
                {
                    PHSecurityAnswerVisibility = Visibility.Visible;
                }
                // Döljer stödtexten om inmatningsfältet har värde
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

        // Lista med länder som får sina värden i konstruktorn
        public ObservableCollection<string> Countries { get; } // Read-only

        // Det valda landet
        public string SelectedCountry { get; set; }

        // Relaykommandon som representerar knappklick
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
        private void RegisterNewUser()
        {
            // Kontrollerar så alla inputs har inmatning
            if (!string.IsNullOrEmpty(UsernameInput) && !string.IsNullOrEmpty(PasswordInput) &&
                !string.IsNullOrEmpty(ConfirmPasswordInput) && !string.IsNullOrEmpty(SelectedCountry) &&
                !string.IsNullOrEmpty(SecurityAnswerInput) && !string.IsNullOrEmpty(selectedSecurityQuestion))
            {
                if (UsernameInput.Length >= 3)
                {
                    // Kollar om användarnamnet hittas
                    bool isUserNameAvailable = true;

                    // Kollar igenom alla användare om användarnamnet redan finns
                    foreach (User user in Manager.Instance.AllUsers)
                    {
                        if (user.Username == UsernameInput)
                        {
                            // Användarnamnet var upptaget
                            isUserNameAvailable = false;

                            break;
                        }
                    }

                    // Om användarnamnet är tillgängligt
                    if (isUserNameAvailable)
                    {
                        // Mall för lösenordskontroll av specialtecken
                        string specialCharacters = "!@#$%^&*()-_=+[{]};:’\"|\\,<.>/?";

                        // Kontrollerar så lösenordet innehåller minst 8 tecken, en siffra och ett specialtecken
                        bool hasSpecial = PasswordInput.Any(c => specialCharacters.Contains(c));
                        bool hasLength = PasswordInput.Length > 7;
                        bool hasDigit = PasswordInput.Any(char.IsDigit);

                        // Om lösenordet är starkt nog
                        if (hasSpecial && hasLength && hasDigit)
                        {
                            // Om lösenord och bekräftat lösenord matchar
                            if (PasswordInput == ConfirmPasswordInput)
                            {
                                // Skapa användare baserat på inmatad information
                                User newUser = new User(UsernameInput, PasswordInput, SelectedCountry, SelectedSecurityQuestion, SecurityAnswerInput);

                                // Lägg till ny användare i listan för alla användare
                                Manager.Instance.AddUser(newUser);

                                // Öppna MainWindow
                                OpenMainWindow();

                                // Stäng RegisterWindow
                                _registerWindow.Close();

                                MessageBox.Show($"Tack {UsernameInput}! Din användarprofil har skapats. Var god logga in..", "Success!", MessageBoxButton.OK, MessageBoxImage.Information);
                            }
                            else { MessageBox.Show("Lösenorden matchar inte!", "No match!", MessageBoxButton.OK, MessageBoxImage.Warning); }
                        }
                        else { MessageBox.Show("Minst en åtta tecken, en siffra och ett specieltecken!", "Wrong input!", MessageBoxButton.OK, MessageBoxImage.Warning); }
                    }
                    else { MessageBox.Show("Användarnamnet finns redan!", "Occupied!", MessageBoxButton.OK, MessageBoxImage.Warning); }
                }
                else { MessageBox.Show("Användarnamnet måste ha minst tre tecken!", "Wrong input!", MessageBoxButton.OK, MessageBoxImage.Warning); }
            }
            else { MessageBox.Show("Du måste fylla i all information!", "Missing input!", MessageBoxButton.OK, MessageBoxImage.Warning); }
        }

        // Avbryt registrering och återgå till MainWindow
        private void Cancel()
        {
            // Öppna MainWindow
            OpenMainWindow();

            // Stäng RegisterWindow
            _registerWindow.Close();
        }

        // Öppnar MainWindow
        private void OpenMainWindow()
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
        }
    }
}
