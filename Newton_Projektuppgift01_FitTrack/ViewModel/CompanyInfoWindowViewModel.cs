using Newton_Projektuppgift01_FitTrack.MVVM;
using System.Windows;

namespace Newton_Projektuppgift01_FitTrack.ViewModel
{
    public class CompanyInfoWindowViewModel : ViewModelBase
    {
        // EGENSKAPER ↓
        // Möjliggör stängning av detta fönster
        public Window _companyInfoWindow { get; set; }

        // Relaykommando som representerar knappklick
        public RelayCommand CloseCommand => new RelayCommand(execute => Close());

        // KONSTRUKTOR ↓
        public CompanyInfoWindowViewModel(Window _companyInfoWindow)
        {
            this._companyInfoWindow = _companyInfoWindow;
        }

        // METODER ↓
        // Stäng fönster
        public void Close()
        {
            _companyInfoWindow.Close();
        }
    }
}
