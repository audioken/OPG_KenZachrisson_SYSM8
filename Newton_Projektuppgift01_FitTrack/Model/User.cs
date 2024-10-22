using System.Collections.ObjectModel;
using System.Windows;

namespace Newton_Projektuppgift01_FitTrack.Model
{
    public class User : Person
    {
        // EGENSKAPER ↓
        public string Country { get; set; }
        public string SecurityQuestion { get; set; }
        private string SecurityAnswer { get; set; }

        public ObservableCollection<Workout> UserWorkouts { get; set; } = new ObservableCollection<Workout>();

        // KONSTRUKTOR ↓
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

        // METODER ↓
        // SKriv ut text för lyckad inloggning
        public override void SignIn()
        {
            MessageBox.Show($"Användaren {Username} från {Country} är inloggad..");
        }

        // Återställ lösenordet för användaren
        public void ResetPassword(string securityAnswer)         // OBS OTESTAD KOD
        {
            if (SecurityAnswer != null && securityAnswer != null)
            {
                if (string.IsNullOrEmpty(securityAnswer))
                {
                    if (SecurityAnswer == securityAnswer)
                    {
                        Random random = new Random();

                        int randomizedPin = random.Next(100000, 1000000);

                        Password = randomizedPin.ToString();

                        MessageBox.Show($"Ditt nya lösenord är {Password}");
                    }
                    else { MessageBox.Show("Tyvärr var det fel svar.."); }
                }
                else { MessageBox.Show("Du måste skriva ett svar på din säkerhetsfråga.."); }
            }
            else { MessageBox.Show("Du har inte ställt in någon säkerhetsfråga.."); }
        }
    }
}
