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
        public Window _userDetailsWindow { get; set; }

        // Spårar information vid registrering av nytt användarkonto
        public string NewUsernameInput { get; set; }
        public string NewPasswordInput { get; set; }
        public string ConfirmNewPasswordInput { get; set; }
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
        public string SecurityAnswerInput { get; set; }
        public string CountryComboBox { get; set; }

        // Lagrar alla säkerhetsfrågor
        public ObservableCollection<string> SecurityQuestions { get; set; }

        // Lagrar alla möjliga landsval
        public ObservableCollection<string> Countries { get; set; }

        // Relay-kommando som anropar metoden "RegisterNewUser" vid klick
        public RelayCommand SaveUserDetailsCommand => new RelayCommand(execute => SaveUserDetails());
        public RelayCommand CancelCommand => new RelayCommand(execute => Cancel());

        // KONSTRUKTOR ↓
        public UserDetailsWindowViewModel(Window userDetailsWindow)
        {
            _userDetailsWindow = userDetailsWindow;

            // Förifylld information för att underlätta
            NewUsernameInput = Manager.Instance.CurrentUser.Username;
            NewPasswordInput = Manager.Instance.CurrentUser.Password;
            ConfirmNewPasswordInput = Manager.Instance.CurrentUser.Password;
            SelectedSecurityQuestion = Manager.Instance.CurrentUser.SecurityQuestion;
            SecurityAnswerInput = Manager.Instance.CurrentUser.SecurityAnswer;
            CountryComboBox = Manager.Instance.CurrentUser.Country;

            // Instansierar och tilldelar listan med länder
            Countries = new ObservableCollection<string>
            {
                "Sweden",
                "Denmark",
                "Norway",
                "Finland"
            };

            // Instansierar och tilldelar listan med säkerhetsfrågor
            SecurityQuestions = new ObservableCollection<string>
            {
                "The name of your favourite pet?",
                "What was the name of your first car?",
                "What was the name of your childhood best friend?",
                "What city were your mother born in?"
            };
        }

        // METOD ↓
        public void SaveUserDetails()
        {
            // Kontrollerar så alla inputs har inmatning
            if (!string.IsNullOrEmpty(NewUsernameInput) && !string.IsNullOrEmpty(NewPasswordInput) &&
                !string.IsNullOrEmpty(ConfirmNewPasswordInput) && !string.IsNullOrEmpty(CountryComboBox) &&
                !string.IsNullOrEmpty(SelectedSecurityQuestion) && !string.IsNullOrEmpty(SecurityAnswerInput))
            {
                if (NewUsernameInput.Length >= 3)
                {
                    // Kontrollbool för tillgängligt användarnamn
                    bool isUserNameAvailable = true;

                    // Kollar om användarnamnet redan finns
                    foreach (User user in Manager.Instance.AllUsers)
                    {
                        // Användarnamn redan upptaget om det inte gäller det inloggade kontots användarnamn
                        if (NewUsernameInput == user.Username && NewUsernameInput != Manager.Instance.CurrentUser.Username)
                        {
                            // Användernamnet var upptaget
                            isUserNameAvailable = false;

                            // Behöver inte iterera fler gånger
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
                            // Om lösenord och bekräfta lösenord matchar
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
