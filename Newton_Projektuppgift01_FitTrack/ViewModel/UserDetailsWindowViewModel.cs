using Newton_Projektuppgift01_FitTrack.Model;
using Newton_Projektuppgift01_FitTrack.MVVM;
using Newton_Projektuppgift01_FitTrack.View;
using System.Collections.ObjectModel;
using System.Windows;

namespace Newton_Projektuppgift01_FitTrack.ViewModel
{
    public class UserDetailsWindowViewModel : ViewModelBase
    {
        // EGENSKAPER ↓
        // Möjliggör stängning av detta fönster
        public Window _userDetailsWindow { get; set; }

        // Användarnamn med döljbar stödtext
        private string newUsernameInput;
        public string NewUsernameInput
        {
            get { return newUsernameInput; }
            set
            {
                newUsernameInput = value;
                OnPropertyChanged();

                if (string.IsNullOrEmpty(NewUsernameInput))
                {
                    PHNewUsernameVisibility = Visibility.Visible;
                }
                else
                {
                    PHNewUsernameVisibility = Visibility.Collapsed;
                }
            }
        }
        private Visibility pHNewUsernameVisibility;
        public Visibility PHNewUsernameVisibility
        {
            get { return pHNewUsernameVisibility; }
            set
            {
                pHNewUsernameVisibility = value;
                OnPropertyChanged();
            }
        }

        // Lösenord med döljbar stödtext
        private string newPasswordInput;
        public string NewPasswordInput
        {
            get { return newPasswordInput; }
            set
            {
                newPasswordInput = value;
                OnPropertyChanged();

                if (string.IsNullOrEmpty(NewPasswordInput))
                {
                    PHNewPasswordVisibility = Visibility.Visible;
                }
                else
                {
                    PHNewPasswordVisibility = Visibility.Collapsed;
                }
            }
        }
        private Visibility pHNewPasswordVisibility;
        public Visibility PHNewPasswordVisibility
        {
            get { return pHNewPasswordVisibility; }
            set
            {
                pHNewPasswordVisibility = value;
                OnPropertyChanged();
            }
        }

        // Bekräfta lösenord med döljbar stödtext
        private string confirmNewPasswordInput;
        public string ConfirmNewPasswordInput
        {
            get { return confirmNewPasswordInput; }
            set
            {
                confirmNewPasswordInput = value;
                OnPropertyChanged();

                if (string.IsNullOrEmpty(ConfirmNewPasswordInput))
                {
                    PHConfirmNewPasswordVisibility = Visibility.Visible;
                }
                else
                {
                    PHConfirmNewPasswordVisibility = Visibility.Collapsed;
                }
            }
        }
        private Visibility pHConfirmNewPasswordVisibility;
        public Visibility PHConfirmNewPasswordVisibility
        {
            get { return pHConfirmNewPasswordVisibility; }
            set
            {
                pHConfirmNewPasswordVisibility = value;
                OnPropertyChanged();
            }
        }

        // Lista med säkerhetsfrågor och vald säkerhetsfråga
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

        // Säkerhetssvar med döljbar stödtext
        private string securityAnswerInput;
        public string SecurityAnswerInput
        {
            get { return securityAnswerInput; }
            set
            {
                securityAnswerInput = value;
                OnPropertyChanged();

                if (string.IsNullOrEmpty(SecurityAnswerInput))
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

        // Lista med länder och valt land
        public ObservableCollection<string> Countries { get; set; }
        public string CountryComboBox { get; set; }

        // Relaykommandon som representerar knappklick
        public RelayCommand SaveUserDetailsCommand => new RelayCommand(execute => SaveUserDetails());
        public RelayCommand CancelCommand => new RelayCommand(execute => Cancel());

        // KONSTRUKTOR ↓
        public UserDetailsWindowViewModel(Window userDetailsWindow)
        {
            _userDetailsWindow = userDetailsWindow;

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

            // Förifylld användarinformation
            NewUsernameInput = Manager.Instance.CurrentUser.Username;
            NewPasswordInput = Manager.Instance.CurrentUser.Password;
            ConfirmNewPasswordInput = Manager.Instance.CurrentUser.Password;
            SelectedSecurityQuestion = Manager.Instance.CurrentUser.SecurityQuestion;
            SecurityAnswerInput = Manager.Instance.CurrentUser.SecurityAnswer;
            CountryComboBox = Manager.Instance.CurrentUser.Country;
        }

        // METOD ↓
        public void SaveUserDetails()
        {
            // Kontrollerar så alla inputs har inmatning
            if (!string.IsNullOrEmpty(NewUsernameInput) && !string.IsNullOrEmpty(NewPasswordInput) &&
                !string.IsNullOrEmpty(ConfirmNewPasswordInput) && !string.IsNullOrEmpty(CountryComboBox) &&
                !string.IsNullOrEmpty(SelectedSecurityQuestion) && !string.IsNullOrEmpty(SecurityAnswerInput))
            {
                // Kontrollerar längden på användarnamn
                if (NewUsernameInput.Length >= 3)
                {
                    // Kollar om användarnamn finns
                    bool isUserNameAvailable = true;

                    // Kollar om användarnamnet redan finns
                    foreach (User user in Manager.Instance.AllUsers)
                    {
                        // Kollar om användarnamn är upptaget så länge det inte är användarens egna användarnamn
                        if (NewUsernameInput == user.Username && NewUsernameInput != Manager.Instance.CurrentUser.Username)
                        {
                            // Användernamnet var upptaget
                            isUserNameAvailable = false;

                            break;
                        }
                    }

                    // Om användarnamnet är ledigt
                    if (isUserNameAvailable)
                    {
                        // Kontrollerar så lösenordet är starkt nog
                        string specialCharacters = "!@#$%^&*()-_=+[{]};:’\"|\\,<.>/?"; // Mall för lösenordskontroll av specialtecken
                        bool hasSpecial = NewPasswordInput.Any(c => specialCharacters.Contains(c)); // Innehåller det specialtecken?
                        bool hasLength = NewPasswordInput.Length > 7; // Innehåller det minst åtta tecken?
                        bool hasDigit = NewPasswordInput.Any(char.IsDigit); // Innehåller det minst en siffra?

                        // Om lösenordet är starkt nog
                        if (hasSpecial && hasLength && hasDigit)
                        {
                            // Om lösenord och bekräftat lösenord matchar
                            if (NewPasswordInput == ConfirmNewPasswordInput)
                            {
                                // Skriv över den gamla användarinformationen
                                Manager.Instance.CurrentUser.Username = NewUsernameInput;
                                Manager.Instance.CurrentUser.Password = NewPasswordInput;
                                Manager.Instance.CurrentUser.Country = CountryComboBox;
                                Manager.Instance.CurrentUser.SecurityQuestion = SelectedSecurityQuestion;
                                Manager.Instance.CurrentUser.SecurityAnswer = SecurityAnswerInput;

                                MessageBox.Show($"Tack {NewUsernameInput}! Din användarprofil har uppdaterats..");

                                // Öppna WorkoutWindow
                                OpenWorkoutWindow();

                                // Stäng UserDetailsWindow
                                _userDetailsWindow.Close();
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

        // Avbryt redigering och återgå till WorkoutWindow
        public void Cancel()
        {
            // Öppna WorkoutWindow
            OpenWorkoutWindow();

            // Stäng UserDetailsWindow
            _userDetailsWindow.Close();
        }

        // Öppnar WorkoutWindow
        public void OpenWorkoutWindow()
        {
            WorkoutWindow workoutWindow = new WorkoutWindow();
            workoutWindow.Show();
        }
    }
}
