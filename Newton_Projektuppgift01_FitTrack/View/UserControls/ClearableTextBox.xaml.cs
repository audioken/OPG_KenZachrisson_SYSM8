using Newton_Projektuppgift01_FitTrack.ViewModel;
using System.Windows.Controls;

namespace Newton_Projektuppgift01_FitTrack.View.UserControls
{
    public partial class ClearableTextBox : UserControl
    {
        public ClearableTextBox()
        {
            InitializeComponent();
            ClearableTextBoxViewModel viewModel = new ClearableTextBoxViewModel();
            DataContext = viewModel;
        }
    }
}
