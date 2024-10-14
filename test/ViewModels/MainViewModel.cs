using Avalonia.Controls;
using System.ComponentModel;
using System.Threading.Tasks;
using GED.Core.Ctrl;
using Avalonia.Media.Imaging;
using System.IO;
using SkiaSharp;

namespace test.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        #region Memver Fields
        private ContentControl contentControl;

        public event PropertyChangedEventHandler? PropertyChanged;
        #endregion

        #region Properties
        public ContentControl ContentControl
        {
            get => contentControl;
            set
            {
                contentControl = value;
                OnPropertyChanged(nameof(value));
            }
        }
        #endregion

        public MainViewModel()
        {
            int err;
            var content = new Content(out err, 1920, 1080);
            ContentControl = content;
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
