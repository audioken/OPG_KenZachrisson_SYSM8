namespace Newton_Projektuppgift01_FitTrack.ViewModel
{
    public class UserDetailsWindowViewModel
    {
        public string UsernameInput { get; set; }
        public string PasswordInput { get; set; }
        public string ConfirmPasswordInput { get; set; }
        public string CountryComboBox { get; set; }

        public UserDetailsWindowViewModel(string UsernameInput, string PasswordInput, string ConfirmPasswordInput, string CountryComboBox)
        {
            this.UsernameInput = UsernameInput;
            this.PasswordInput = PasswordInput;
            this.ConfirmPasswordInput = ConfirmPasswordInput;
            this.CountryComboBox = CountryComboBox;
        }

        public void SaveUserDetails()
        {
            // Kod för att spara användardetaljer
        }

        public void Cancel()
        {
            // Stäng fönstret utan att spara
        }
    }
}
