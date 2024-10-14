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
        private Bitmap bitmapImage;

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

        public Bitmap BitmapImage
        {
            get => bitmapImage;
            set
            {
                bitmapImage = value;
                OnPropertyChanged(nameof(value));
            }
        }
        #endregion

        public MainViewModel()
        {
            int err;
            var content = new Content(out err, 1920, 1080);
            ContentControl = content;
            BitmapImage = ConvertBitmap(content.DisplayBuffer);
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// WriteableBitmap을 Bitmap으로 변환해주는 함수
        /// </summary>
        /// <param name="writeableBitmap"></param>
        /// <returns></returns>
        private Bitmap? ConvertBitmap(WriteableBitmap writeableBitmap)
        {
            Bitmap? bitmap = null;
            using(var stream = new MemoryStream())
            {
                writeableBitmap.Save(stream);
                stream.Seek(0, SeekOrigin.Begin);
                bitmap = new Bitmap(stream);
            }

            return bitmap;
        }
    }
}
