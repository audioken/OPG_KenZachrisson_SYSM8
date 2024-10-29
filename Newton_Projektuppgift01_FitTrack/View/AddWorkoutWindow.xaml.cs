using Newton_Projektuppgift01_FitTrack.ViewModel;
using System.Windows;

namespace Newton_Projektuppgift01_FitTrack.View
{
    public partial class AddWorkoutWindow : Window
    {
        public AddWorkoutWindow()
        {
            InitializeComponent();

            // Skapar en instans av ViewModel för fönstret och sätter dess DataContext
            AddWorkWindowViewModel viewModel = new AddWorkWindowViewModel(this);

            // Sätter DataContext så att fönstret kan binda till ViewModel-egenskaper och kommandon
            DataContext = viewModel;
        }
    }
}
