using Newton_Projektuppgift01_FitTrack.MVVM;

namespace Newton_Projektuppgift01_FitTrack.ViewModel
{
    public class ClearableTextBoxViewModel : ViewModelBase
    {

        private string textInput;
        public string TextInput
        {
            get { return textInput; }
            set
            {
                textInput = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand ClearTextCommand => new RelayCommand(execute => ClearText());

        public void ClearText()
        {
            TextInput = "";
        }
    }
}
