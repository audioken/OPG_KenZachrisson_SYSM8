using Newton_Projektuppgift01_FitTrack.ViewModel;
using System.Windows;

namespace Newton_Projektuppgift01_FitTrack.View
{
    public partial class WorkoutWindow : Window
    {
        public WorkoutWindow()
        {
            InitializeComponent();
            WorkoutWindowViewModel viewModel = new WorkoutWindowViewModel();
            DataContext = viewModel;
        }
    }
}
