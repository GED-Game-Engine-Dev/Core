using Avalonia.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GED.Core.Ctrl;
using System.Diagnostics;

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
            contentControl = new Content(out err);

            Task.Run(() =>
            {
                while (true)
                {
                    Debug.WriteLine($"X: {MousePoint.X}, Y: {MousePoint.Y}");
                }
            });
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
