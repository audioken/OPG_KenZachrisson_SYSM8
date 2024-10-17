namespace Newton_Projektuppgift01_FitTrack.ViewModel
{
    public class RegisterWindowViewModel
    {
        public string UsernameInput { get; set; }
        public string PasswordInput { get; set; }
        public string ConfirmPasswordInput { get; set; }
        public string CountryComboBox { get; set; }

        public RegisterWindowViewModel(string UsernameInput, string PasswordInput, string ConfirmPasswordInput, string CountryComboBox)
        {
            this.UsernameInput = UsernameInput;
            this.PasswordInput = PasswordInput;
            this.ConfirmPasswordInput = ConfirmPasswordInput;
            this.CountryComboBox = CountryComboBox;
        }

        public void RegisterNewUser()
        {
            // Kod för att registrera en ny användare
        }
    }
}
