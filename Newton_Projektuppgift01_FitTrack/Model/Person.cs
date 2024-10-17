namespace Newton_Projektuppgift01_FitTrack.Model
{
    public abstract class Person
    {
        public string UserName { get; set; }
        public string Password { get; set; }

        public Person(string UserName, string Password)
        {
            this.UserName = UserName;
            this.Password = Password;
        }

        public abstract void SignIn();
    }
}
