namespace Newton_Projektuppgift01_FitTrack.Model
{
    public class AdminUser : User
    {
        public AdminUser(string UserName, string Password, string Country, string SecurityQuestion, string SecurityAnswer) : base(UserName, Password, Country, SecurityQuestion, SecurityAnswer)
        {
        }

        public void ManageAllWorkouts()
        {
            // Kod för att kunna radera alla användare och dess inbokade tider
        }
    }
}
