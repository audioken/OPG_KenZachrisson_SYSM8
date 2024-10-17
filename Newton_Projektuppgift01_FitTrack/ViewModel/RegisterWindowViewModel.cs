using Newton_Projektuppgift01_FitTrack.MVVM;

namespace Newton_Projektuppgift01_FitTrack.ViewModel
{
    public class RegisterWindowViewModel
    {
        public string UsernameInput { get; set; }
        public string PasswordInput { get; set; }
        public string ConfirmPasswordInput { get; set; }
        public string CountryComboBox { get; set; }

        public RelayCommand RegisterNewUserCommand => new RelayCommand(execute => RegisterNewUser());

        public void RegisterNewUser()
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();

            // Kod för att stänga fönstret här
        }
    }
}
