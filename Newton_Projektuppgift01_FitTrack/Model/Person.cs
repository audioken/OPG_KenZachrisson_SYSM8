namespace Newton_Projektuppgift01_FitTrack.Model
{
    // Abstrakt klass som tvingar härledda klasser att implementera dess medlemmar
    public abstract class Person
    {
        // EGENSKAPER ↓
        // Användarnamn och lösenord
        public string Username { get; set; }
        public string Password { get; set; }

        // KONSTRUKTOR ↓
        public Person(string Username, string Password)
        {
            this.Username = Username;
            this.Password = Password;
        }

        // METODER ↓
        // Skriver ut välkomstmeddelande i härledda klasser
        public abstract void SignIn();
    }
}
