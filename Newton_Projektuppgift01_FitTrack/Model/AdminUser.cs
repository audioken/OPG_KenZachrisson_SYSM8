using System.Collections.ObjectModel;

namespace Newton_Projektuppgift01_FitTrack.Model
{
    public class AdminUser : User
    {
        public AdminUser(string Username, string Password, string Country) : base(Username, Password, Country)
        {
        }

        // Möjliggör hantering av användares träningspass
        public ObservableCollection<Workout> ManageAllWorkouts()
        {
            ObservableCollection<Workout> WorkoutList = new ObservableCollection<Workout>();

            foreach (User user in Manager.Instance.AllUsers)
            {
                foreach (Workout workout in user.UserWorkouts)
                {
                    WorkoutList.Add(workout);
                }
            }

            return WorkoutList;
        }
    }
}
