using System.Windows;
using System.Windows.Controls;

namespace Newton_Projektuppgift01_FitTrack.MVVM
{
    // En statisk hjälparklass för att binda ett lösenord från PasswordBox till en DependencyProperty
    public static class PasswordHelper
    {
        // Skapa en DependencyProperty för det bundna lösenordet
        public static readonly DependencyProperty BoundPasswordProperty =
            DependencyProperty.RegisterAttached("BoundPassword", typeof(string), typeof(PasswordHelper), new FrameworkPropertyMetadata(string.Empty, OnBoundPasswordChanged));

        // Hämtar värdet på BoundPasswordProperty
        public static string GetBoundPassword(DependencyObject d)
        {
            return (string)d.GetValue(BoundPasswordProperty);
        }

        // Sätter värdet på BoundPasswordProperty
        public static void SetBoundPassword(DependencyObject d, string value)
        {
            d.SetValue(BoundPasswordProperty, value);
        }

        // Hanterar ändringar i BoundPasswordProperty
        private static void OnBoundPasswordChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            // Kontrollerar att DependencyObject är en PasswordBox
            if (d is PasswordBox passwordBox)
            {
                // Avregistrerar den tidigare PasswordChanged-händelsen
                passwordBox.PasswordChanged -= PasswordBox_PasswordChanged;

                // Uppdaterar PasswordBoxens lösenord om det skiljer sig från det nya värdet
                if ((string)e.NewValue != passwordBox.Password)
                {
                    passwordBox.Password = (string)e.NewValue;
                }

                // Återregistrerar PasswordChanged-händelsen
                passwordBox.PasswordChanged += PasswordBox_PasswordChanged;
            }
        }

        // Hanterar PasswordChanged-händelsen för PasswordBox
        private static void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            // Kontrollerar att sender är en PasswordBox
            if (sender is PasswordBox passwordBox)
            {
                // Uppdaterar BoundPasswordProperty med det nya lösenordet från PasswordBox
                SetBoundPassword(passwordBox, passwordBox.Password);
            }
        }
    }
}
