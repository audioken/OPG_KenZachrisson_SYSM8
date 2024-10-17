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
                OnPropertyChanged();
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
                OnPropertyChanged();
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
            if (UsernameInput != null && PasswordInput != null)
            {
                Manager.Instance.AllUsers.Add(new User(UsernameInput, PasswordInput, CountryComboBox));

                Manager.Instance.PrintAllUsers();
            }
            else
            {
                MessageBox.Show("Någon ruta saknar text!");
            }

            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();

            // Kod för att stänga fönstret här
        }
    }
}
