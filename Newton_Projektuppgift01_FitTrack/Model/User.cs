using System.Collections.ObjectModel;
using System.Windows;

namespace Newton_Projektuppgift01_FitTrack.Model
{
    public class User : Person
    {
        // EGENSKAPER ↓
        // Användarens land, säkerhetsfråga och säkerhetssvar
        public string Country { get; set; }
        public string SecurityQuestion { get; set; }
        public string SecurityAnswer { get; set; }

        // Lista som lagrar användarens träningar
        public ObservableCollection<Workout> UserWorkouts { get; set; }

        // KONSTRUKTOR ↓
        // Anropas vid inloggning
        public User(string Username, string Password, string Country) : base(Username, Password)
        {
            this.Country = Country;

            // Instansiera användarens träningslista
            UserWorkouts = new ObservableCollection<Workout>();
        }

        // Anropas vid användarregistrering för att kunna inkludera land, säkerhetsfråga och säkerhetssvar
        public User(string Username, string Password, string Country, string SecurityQuestion, string SecurityAnswer) : base(Username, Password)
        {
            this.Country = Country;
            this.SecurityQuestion = SecurityQuestion;
            this.SecurityAnswer = SecurityAnswer;

            // Instansiera användarens träningslista
            UserWorkouts = new ObservableCollection<Workout>();
        }

        // METODER ↓
        // Överskuggande metod som skriver ut text för lyckad inloggning
        public override void SignIn()
        {
            MessageBox.Show($"Användaren {Username} från {Country} är inloggad..");
        }

        // Överskuggande metod som Återställer användarens lösenord
        public bool ResetPassword(string securityAnswer)
        {
            // Kolla så användaren har ställt in en säkerhetsfråga
            if (SecurityAnswer != null)
            {
                // Kolla så inmatning finns
                if (!string.IsNullOrEmpty(securityAnswer))
                {
                    // Om svaret är rätt
                    if (SecurityAnswer == securityAnswer)
                    {
                        // Skapa ett objekt för att slumpa nummer
                        Random random = new Random();

                        // Slumpa ett sexsiffrigt nummer
                        int randomizedPin = random.Next(100000, 1000000);

                        // Omvandla från heltal till sträng och ersätt tidigare lösenord
                        Password = randomizedPin.ToString();

                        MessageBox.Show($"Ditt nya lösenord är {Password}", "Success!", MessageBoxButton.OK, MessageBoxImage.Information);

                        // Återställningen lyckades
                        return true;
                    }
                    else { MessageBox.Show("Tyvärr var det fel svar..", "Wrong input!", MessageBoxButton.OK, MessageBoxImage.Warning); }
                }
                else { MessageBox.Show("Du måste skriva ett svar på din säkerhetsfråga..", "Missing input!", MessageBoxButton.OK, MessageBoxImage.Warning); }
            }
            else { MessageBox.Show("Du har inte ställt in någon säkerhetsfråga..", "Missing input!", MessageBoxButton.OK, MessageBoxImage.Warning); }

            // Återställningen misslyckades
            return false;
        }
    }
}
