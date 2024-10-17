namespace Newton_Projektuppgift01_FitTrack.Model
{
    public class User : Person
    {
        public string Country { get; set; }
        public string SecurityQuestion { get; set; }
        private string SecurityAnswer { get; set; }

        public User(string UserName, string Password, string Country, string SecurityQuestion, string SecurityAnswer) : base(UserName, Password)
        {
            this.Country = Country;
            this.SecurityQuestion = SecurityQuestion;
            this.SecurityAnswer = SecurityAnswer;
        }

        public override void SignIn()
        {

        }

        public void ResetPassword(string securityAnswer)
        {

        }
    }
}
