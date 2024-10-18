using Newton_Projektuppgift01_FitTrack.ViewModel;
using System.Windows;

namespace Newton_Projektuppgift01_FitTrack.View
{
    public partial class WorkoutDetailsWindow : Window
    {
        public WorkoutDetailsWindow()
        {
            InitializeComponent();
            WorkoutDetailsWindowViewModel viewModel = new WorkoutDetailsWindowViewModel();
            DataContext = viewModel;
        }
    }
}
