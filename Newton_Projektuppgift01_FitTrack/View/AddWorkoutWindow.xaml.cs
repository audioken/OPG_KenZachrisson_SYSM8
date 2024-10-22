using Newton_Projektuppgift01_FitTrack.ViewModel;
using System.Windows;

namespace Newton_Projektuppgift01_FitTrack.View
{
    public partial class AddWorkoutWindow : Window
    {
        public AddWorkoutWindow()
        {
            InitializeComponent();
            AddWorkWindowViewModel viewModel = new AddWorkWindowViewModel(this);
            DataContext = viewModel;
        }
    }
}
