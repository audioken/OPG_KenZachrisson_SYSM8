namespace Newton_Projektuppgift01_FitTrack.ViewModel
{
    public class MainWindowViewModel
    {
        public string LabelTitle { get; set; }
        public string UsernameInput { get; set; }
        public string PasswordInput { get; set; }

        public MainWindowViewModel(string LabelTitle, string UsernameInput, string PasswordInput)
        {
            this.LabelTitle = LabelTitle;
            this.UsernameInput = UsernameInput;
            this.PasswordInput = PasswordInput;
        }

        public void SignIn()
        {
            // Kod för att logga in
        }

        public void Register()
        {
            // Öppna fönster -> RegisterWindow
        }
    }
}
