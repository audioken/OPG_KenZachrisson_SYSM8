using Newton_Projektuppgift01_FitTrack.ViewModel;
using System.Windows;

namespace Newton_Projektuppgift01_FitTrack.View
{
    public partial class RegisterWindow : Window
    {
        public RegisterWindow()
        {
            InitializeComponent();

            // Skapar en instans av ViewModel för fönstret och sätter dess DataContext
            RegisterWindowViewModel viewModel = new RegisterWindowViewModel(this);

            // Sätter DataContext så att fönstret kan binda till ViewModel-egenskaper och kommandon
            DataContext = viewModel;
        }
    }
}
