namespace Newton_Projektuppgift01_FitTrack.Model
{
    public class AdminUser : User
    {
        public AdminUser(string Username, string Password, string Country) : base(Username, Password, Country)
        {
        }

        // Möjliggör hantering av användares träningspass
        public void ManageAllWorkouts()
        {
            // Spegla alla användares träningar i admins lista för träningar
            this.UserWorkouts = Manager.Instance.AllWorkouts;
        }
    }
}
