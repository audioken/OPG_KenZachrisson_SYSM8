using Newton_Projektuppgift01_FitTrack.ViewModel;
using System.Windows;

namespace Newton_Projektuppgift01_FitTrack
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MainWindowViewModel viewModel = new MainWindowViewModel();
            DataContext = viewModel;
        }
    }
}