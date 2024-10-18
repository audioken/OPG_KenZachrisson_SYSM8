namespace Newton_Projektuppgift01_FitTrack.Model
{
    public abstract class Person
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public Person(string Username, string Password)
        {
            this.Username = Username;
            this.Password = Password;
        }

        public abstract void SignIn();
    }
}
