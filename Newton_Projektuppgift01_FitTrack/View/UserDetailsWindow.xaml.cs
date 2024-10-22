using Newton_Projektuppgift01_FitTrack.ViewModel;
using System.Windows;

namespace Newton_Projektuppgift01_FitTrack.View
{
    public partial class UserDetailsWindow : Window
    {
        public UserDetailsWindow()
        {
            InitializeComponent();
            UserDetailsWindowViewModel viewModel = new UserDetailsWindowViewModel(this);
            DataContext = viewModel;
        }
    }
}
