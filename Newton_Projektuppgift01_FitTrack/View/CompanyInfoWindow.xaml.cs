using Newton_Projektuppgift01_FitTrack.ViewModel;
using System.Windows;

namespace Newton_Projektuppgift01_FitTrack.View
{
    public partial class CompanyInfoWindow : Window
    {
        public CompanyInfoWindow()
        {
            InitializeComponent();
            CompanyInfoWindowViewModel viewModel = new CompanyInfoWindowViewModel(this);
            DataContext = viewModel;
        }
    }
}
