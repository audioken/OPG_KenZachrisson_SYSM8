using Newton_Projektuppgift01_FitTrack.ViewModel;
using System.Windows;

namespace Newton_Projektuppgift01_FitTrack.View
{
    public partial class UserDetailsWindow : Window
    {
        public UserDetailsWindow()
        {
            InitializeComponent();

            // Skapar en instans av ViewModel för fönstret och sätter dess DataContext
            UserDetailsWindowViewModel viewModel = new UserDetailsWindowViewModel(this);

            // Sätter DataContext så att fönstret kan binda till ViewModel-egenskaper och kommandon
            DataContext = viewModel;
        }
    }
}
