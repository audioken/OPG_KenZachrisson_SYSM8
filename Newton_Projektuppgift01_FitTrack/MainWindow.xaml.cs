using Newton_Projektuppgift01_FitTrack.ViewModel;
using System.Windows;

namespace Newton_Projektuppgift01_FitTrack
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // Skapar en instans av ViewModel för fönstret och sätter dess DataContext.
            MainWindowViewModel viewModel = new MainWindowViewModel();

            // Sätter DataContext så att fönstret kan binda till ViewModel-egenskaper och kommandon
            DataContext = viewModel;
        }
    }
}