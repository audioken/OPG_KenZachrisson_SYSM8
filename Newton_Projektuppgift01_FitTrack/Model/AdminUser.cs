namespace Newton_Projektuppgift01_FitTrack.Model
{
    public class AdminUser : User
    {
        public AdminUser(string Username, string Password, string Country) : base(Username, Password, Country)
        {
        }

        // Private eftersom den inte behövs i andra klasser
        private void ManageAllWorkouts()
        {
            // Kod för att kunna radera alla användare och dess inbokade tider
        }
    }
}
