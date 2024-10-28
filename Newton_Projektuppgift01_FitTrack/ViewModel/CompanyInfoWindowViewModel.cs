using Newton_Projektuppgift01_FitTrack.MVVM;
using System.Windows;

namespace Newton_Projektuppgift01_FitTrack.ViewModel
{
    public class CompanyInfoWindowViewModel : ViewModelBase
    {
        public Window _companyInfoWindow;

        public RelayCommand CloseCommand => new RelayCommand(execute => Close());

        public CompanyInfoWindowViewModel(Window _companyInfoWindow)
        {
            this._companyInfoWindow = _companyInfoWindow;
        }

        public void Close()
        {
            _companyInfoWindow.Close();
        }
    }
}
