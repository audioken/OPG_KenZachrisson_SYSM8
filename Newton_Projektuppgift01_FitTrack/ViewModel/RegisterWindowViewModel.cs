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
        private string countryComboBox;
        public string CountryComboBox
        {
            get { return countryComboBox; }
            set
            {
                countryComboBox = value;
                //OnPropertyChanged(); Finns inget behov för OnPropertyChanged här vad jag kan förstå
            }
        }

        // Lista där användaren väljer land och som speglas i ComboBox
        private ObservableCollection<string> countries;
        public ObservableCollection<string> Countries
        {
            get { return countries; }
            set
            {
                countries = value;
                //OnPropertyChanged(); Finns inget behov för OnPropertyChanged här vad jag kan förstå
            }
        }

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
        // Registera ny användare baserat på inmatad information
        public void RegisterNewUser()
        {
            if (!string.IsNullOrEmpty(UsernameInput) && !string.IsNullOrEmpty(PasswordInput) && !string.IsNullOrEmpty(ConfirmPasswordInput))
            {
                if (PasswordInput == ConfirmPasswordInput)
                {
                    Manager.Instance.AllUsers.Add(new User(UsernameInput, PasswordInput, CountryComboBox));

                    // Testutskrift
                    //Manager.Instance.PrintAllUsers();

                    // Öppna MainWindow
                    MainWindow mainWindow = new MainWindow(); // Kanske ska instansieras någon annanstans?
                    mainWindow.Show();

                    // KOD HÄR för att stänga detta fönster
                }
                else
                {
                    MessageBox.Show("Lösenorden matchar inte!");
                }
            }
            else
            {
                MessageBox.Show("Någon ruta saknar text!");
            }
        }
    }
}
