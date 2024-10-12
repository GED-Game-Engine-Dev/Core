using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace GED.DisplayWizerd {
    internal abstract class Bindable : INotifyPropertyChanged {
        public event PropertyChangedEventHandler? PropertyChanged;

        public int SetProperty<T>(ref T storage, T? value, [CallerMemberName]string? propertyName = null) {
            if(value != null) {
                storage = value;
                OnPropertyChanged(propertyName);
                return 0;
            }

            return 1;
        }

        protected int OnPropertyChanged([CallerMemberName] string? propertyName = null) {
            if(PropertyChanged != null) {
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
                return 0;
            }
            
            else return 1;
        }
    }
}