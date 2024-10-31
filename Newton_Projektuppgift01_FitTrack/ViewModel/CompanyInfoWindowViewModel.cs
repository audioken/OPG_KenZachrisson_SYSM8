using Newton_Projektuppgift01_FitTrack.MVVM;
using System.Windows;

namespace Newton_Projektuppgift01_FitTrack.ViewModel
{
    public class CompanyInfoWindowViewModel : ViewModelBase
    {
        // EGENSKAPER ↓
        // Möjliggör stängning av detta fönster
        public Window _companyInfoWindow { get; set; }

        // Read-only logotyp för applikationen utan setter som får sina värden i konstruktorn
        public string LabelTitle { get; } // "Fit"
        public string LabelTitle2 { get; } // "Track"

        // Relaykommando som representerar knappklick
        public RelayCommand CloseCommand => new RelayCommand(execute => Close());

        // KONSTRUKTOR ↓
        public CompanyInfoWindowViewModel(Window _companyInfoWindow)
        {
            this._companyInfoWindow = _companyInfoWindow;

            LabelTitle = "Fit";
            LabelTitle2 = "Track";
        }

        // METODER ↓
        // Stäng fönster
        private void Close()
        {
            _companyInfoWindow.Close();
        }
    }
}
