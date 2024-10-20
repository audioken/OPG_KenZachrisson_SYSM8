using System.Collections.ObjectModel;
using System.Windows;

namespace Newton_Projektuppgift01_FitTrack.Model
{
    public class User : Person
    {
        public string Country { get; set; }
        public string SecurityQuestion { get; set; }
        private string SecurityAnswer { get; set; }

        public ObservableCollection<Workout> UserWorkouts { get; set; } = new ObservableCollection<Workout>();

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

        // OBS OTESTAD KOD
        public void ResetPassword(string securityAnswer)
        {
            if (SecurityAnswer != null && securityAnswer != null)
            {
                if (securityAnswer == "2")
                {
                    Random random = new Random();

                    int randomizedPin = random.Next(100000, 1000000);

                    Password = randomizedPin.ToString();

                    MessageBox.Show($"Ditt nya lösenord är {Password}");
                }
                else
                {
                    MessageBox.Show("Tyvärr var det fel svar..");
                }
            }
            else
            {
                MessageBox.Show("Du måste skriva in något..");
            }
        }
    }
}
