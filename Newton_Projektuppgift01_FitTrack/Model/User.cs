using System.Windows;

namespace Newton_Projektuppgift01_FitTrack.Model
{
    public class User : Person
    {
        public string Country { get; set; }
        public string SecurityQuestion { get; set; }
        private string SecurityAnswer { get; set; }

        // Anropas vid registrering av ny användare
        public User(string Username, string Password, string Country) : base(Username, Password)
        {
            this.Country = Country;
        }

        // Anropas när all information om användaren ska med
        public User(string Username, string Password, string Country, string SecurityQuestion, string SecurityAnswer) : base(Username, Password)
        {
            this.Country = Country;
            this.SecurityQuestion = SecurityQuestion;
            this.SecurityAnswer = SecurityAnswer;
        }

        public override void SignIn()
        {
            MessageBox.Show($"Användaren {Username} från {Country} är inloggad..");
        }

        public void ResetPassword(string securityAnswer)
        {

        }
    }
}
