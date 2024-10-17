using Newton_Projektuppgift01_FitTrack.MVVM;
using Newton_Projektuppgift01_FitTrack.View;
using System.Windows;

namespace Newton_Projektuppgift01_FitTrack.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        public string LabelTitle { get; set; }
        public string UsernameInput { get; set; }
        public string PasswordInput { get; set; }

        public RelayCommand SignInCommand => new RelayCommand(execute => SignIn());
        public RelayCommand RegisterCommand => new RelayCommand(execute => Register());
        public RelayCommand ForgottPasswordCommand => new RelayCommand(execute => ForgotPassword()); // För VG

        public void SignIn()
        {
            if (UsernameInput != null && PasswordInput != null)
            {

            }
        }

        public void Register()
        {
            // Öppnar RegisterWindow
            RegisterWindow registerWindow = new RegisterWindow();
            registerWindow.Show();

            // Stänger ner MainWindow
            Application.Current.MainWindow.Close();
        }

        // För VG 
        public void ForgotPassword()
        {
            // Kod som hanterar glömt lösenord här
        }
    }
}
