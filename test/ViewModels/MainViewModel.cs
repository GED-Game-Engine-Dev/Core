using Avalonia.Controls;
using System.ComponentModel;
using System.Threading.Tasks;
using GED.Core.Ctrl;

namespace test.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private ContentControl contentControl;

        public event PropertyChangedEventHandler? PropertyChanged;
        public ContentControl ContentControl
        {
            get => contentControl;
            set
            {
                contentControl = value;
                OnPropertyChanged(nameof(value));
            }
        }

        public MainViewModel()
        {
            int err;
            contentControl = new Content(out err, 1920, 1080);
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
