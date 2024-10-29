using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Newton_Projektuppgift01_FitTrack.MVVM
{
    // En basklass för ViewModel-klasser som implementerar INotifyPropertyChanged
    public class ViewModelBase : INotifyPropertyChanged
    {
        // Händelse som utlöses när en egenskap har ändrats.
        public event PropertyChangedEventHandler? PropertyChanged;

        // Skyddad metod som utlöser PropertyChanged-händelsen
        // [CallerMemberName] attributet gör att du inte behöver specificera propertyName manuellt när du anropar metoden
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            // Om PropertyChanged inte är null utlöses händelsen och skickar med namnet på den ändrade egenskapen
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

