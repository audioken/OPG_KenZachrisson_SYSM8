using System.Windows.Input;

namespace Newton_Projektuppgift01_FitTrack.MVVM
{
    // Definierar en klass som implementerar ICommand gränssnittet
    public class RelayCommand : ICommand
    {
        // Två privata fält för att hålla funktionsreferenser
        private Action<object> execute; // Referens till metoden som ska exekveras
        private Func<object, bool>? canExecute; // Referens till metoden som bestämmer om kommandot kan exekveras

        // Händelse som utlöses när villkoren för exekvering ändras
        public event EventHandler? CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; } // Registrera händelsen
            remove { CommandManager.RequerySuggested -= value; } // Avregistrera händelsen
        }

        // Konstruktor som initialiserar RelayCommand med exekverings- och villkorsmetoder
        public RelayCommand(Action<object> execute, Func<object, bool>? canExecute = null)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }

        // Metod som bestämmer om kommandot kan exekveras
        public bool CanExecute(object? parameter)
        {
            // Om canExecute är null, tillåt exekvering; annars anropa canExecute-metoden
            return canExecute == null || canExecute(parameter);
        }

        // Metod som exekverar kommandot
        public void Execute(object? parameter)
        {
            // Anropa execute-metoden med det angivna parametervärdet
            execute(parameter);
        }
    }
}
