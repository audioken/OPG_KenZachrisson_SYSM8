using Newton_Projektuppgift01_FitTrack.Model;
using Newton_Projektuppgift01_FitTrack.MVVM;
using System.Collections.ObjectModel;
using System.Windows;

namespace Newton_Projektuppgift01_FitTrack.ViewModel
{
    public class RegisterWindowViewModel : ViewModelBase
    {
        // EGENSKAPER ↓
        // Spårar information vid registrering av nytt användarkonto
        public string UsernameInput { get; set; }
        public string PasswordInput { get; set; }
        public string ConfirmPasswordInput { get; set; }

        // Spårar användarens valda land från "Countries" för lagring i användarkonto
        public string CountryComboBox { get; set; }

        // Lista där användaren väljer land och som speglas i ComboBox
        public ObservableCollection<string> Countries { get; set; }

        // Relay-kommando som anropar metoden "RegisterNewUser" vid klick
        public RelayCommand RegisterNewUserCommand => new RelayCommand(execute => RegisterNewUser());

        // KONSTRUKTOR ↓
        public RegisterWindowViewModel()
        {
            // Initierar "Countries" med en lista av länder
            Countries = new ObservableCollection<string>
            {
                "Sweden",
                "Denmark",
                "Norway",
                "Finland"
            };
        }

        // METOD ↓
        // Registrera ny användare baserad på inmatad information
        public void RegisterNewUser()
        {
            // Kontrollerar så alla inputs har inmatning
            if (!string.IsNullOrEmpty(UsernameInput) && !string.IsNullOrEmpty(PasswordInput) &&
                !string.IsNullOrEmpty(ConfirmPasswordInput) && !string.IsNullOrEmpty(CountryComboBox))
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
                    bool hasSpecial = PasswordInput.Any(c => specialCharacters.Contains(c)); // Innehåller det specialtecken?
                    bool hasLength = PasswordInput.Length > 7; // Innehåller det minst åtta tecken?
                    bool hasDigit = PasswordInput.Any(char.IsDigit); // Innehåller det minst en siffra?

                    // Om lösenordet är starkt nog
                    if (hasSpecial && hasLength && hasDigit)
                    {
                        // Om lösenord och bekräfta lösenord matchar
                        if (PasswordInput == ConfirmPasswordInput)
                        {
                            // Registrera ny användare och lagra i masterklassens lista för "AllUsers"
                            Manager.Instance.AllUsers.Add(new User(UsernameInput, PasswordInput, CountryComboBox));
                            MessageBox.Show($"Tack {UsernameInput}! Din användarprofil har skapats. Var god logga in..");

                            // Öppna MainWindow
                            MainWindow mainWindow = new MainWindow(); // Kanske ska instansieras någon annanstans?
                            mainWindow.Show();

                            // KOD HÄR för att stänga detta fönster
                        }
                        else { MessageBox.Show("Lösenorden matchar inte!"); }
                    }
                    else { MessageBox.Show("Minst en åtta tecken, en siffra och ett specieltecken!"); }
                }
                else { MessageBox.Show("Användarnamnet finns redan!"); }
            }
            else { MessageBox.Show("Du måste fylla i all information!"); }
        }
    }
}
