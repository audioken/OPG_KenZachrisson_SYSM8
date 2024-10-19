﻿using System.Collections.ObjectModel;
using System.Windows;

namespace Newton_Projektuppgift01_FitTrack.Model
{
    public class Manager
    {
        //EGENSKAPER ↓
        // Singleton-implementering av Manager för att möjliggöra åtkomst över hela projektet
        private static Manager _instance;
        public static Manager Instance => _instance ??= new Manager();

        public ObservableCollection<User> AllUsers { get; private set; } // Alla användare
        public ObservableCollection<Workout> AllWorkouts { get; private set; } // Alla träningspass

        // Håller koll på inloggad användare
        public User CurrentUser { get; set; }
        public Workout CurrentWorkout { get; set; }

        // KONSTRUKTOR ↓
        private Manager()
        {
            // Instansierar listor
            AllUsers = new ObservableCollection<User>();
            AllWorkouts = new ObservableCollection<Workout>();
        }

        // METODER ↓
        // Metod för testning så datan kommer fram
        public void PrintAllUsers()
        {
            foreach (User user in AllUsers)
            {
                MessageBox.Show($"{user.Username} {user.Password} {user.Country}");
            }
        }
    }
}
