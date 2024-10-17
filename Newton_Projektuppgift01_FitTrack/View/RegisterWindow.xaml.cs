using Newton_Projektuppgift01_FitTrack.ViewModel;
using System.Windows;

namespace Newton_Projektuppgift01_FitTrack.View
{
    public partial class RegisterWindow : Window
    {
        public RegisterWindow()
        {
            InitializeComponent();
            RegisterWindowViewModel viewModel = new RegisterWindowViewModel();
            DataContext = viewModel;
        }
    }
}
