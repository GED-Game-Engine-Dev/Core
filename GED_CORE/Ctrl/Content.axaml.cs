using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using GED.SanityCheck;
using System.Diagnostics;

namespace GED.Core.Ctrl
{
    public partial class Content : ContentControl
    {
        #region Member Fields
        internal uint ZeroPosX, ZeroPosY;
        internal double screenWidth, screenHeight;
        internal WriteableBitmap __DisplayBuffer;

        public uint VisualWidth, VisualHeight;
        #endregion

        #region Properties
        public double ScreenWidth { get => screenWidth; }
        public double ScreenHeight { get => screenHeight; }
        public WriteableBitmap DisplayBuffer { get => __DisplayBuffer; }
        #endregion

        public Content(out int err, uint VisualWidth, uint VisualHeight)
        {

            __DisplayBuffer = new WriteableBitmap(new PixelSize(1, 1), new Vector(96, 96));

            err = (int)FuckedNumbers.OK;
            if (VisualWidth == 0 || VisualHeight == 0)
            {
                err = (int)FuckedNumbers.WRONG_OPERATION;
                return;
            }

            this.VisualWidth = VisualWidth;
            this.VisualHeight = VisualHeight;

            try
            {
                PointerMoved += OnPointerMoved;
                SizeChanged += OnSizeChanged;

                AvaloniaXamlLoader.Load(this);
            }
            catch
            {
                err = (int)FuckedNumbers.ALLOC_FAILED;
                return;
            }
        }

        public int SetVisualWin(uint width, uint height)
        {
            if (width == 0 || height == 0)
            {
                return (int)FuckedNumbers.WRONG_OPERATION;
            }

            VisualWidth = width;
            VisualHeight = height;

            uint newwidth, newheight;

            if (VisualWidth * screenHeight > VisualHeight * screenWidth)
            {
                newheight = (uint)screenHeight;
                newwidth = (uint)(screenHeight * width / height);

                ZeroPosX = (uint)((screenWidth - newwidth) / 2);
                ZeroPosY = 0;
            }
            else
            {
                newwidth = (uint)screenWidth;
                newheight = (uint)(screenWidth * height / width);

                ZeroPosX = 0;
                ZeroPosY = (uint)((screenHeight - newheight) / 2);
            }

#if true

            __DisplayBuffer = new WriteableBitmap
            (
                new PixelSize(
                    (int)newwidth,
                    (int)newheight
                    ),
                new Vector(96, 96),
                format: PixelFormats.Bgr24
                );

#endif

            return (int)FuckedNumbers.OK;
        }

        #region Private Functions
        private unsafe void OnPointerMoved(object? sender, PointerEventArgs e)
        {
            var pos = e.GetPosition(this);
            fMousePoint.X[0] = pos.X;
            fMousePoint.Y[0] = pos.Y;
        }

        private void OnSizeChanged(object? sender, SizeChangedEventArgs e)
        {
            var nSize = e.NewSize;
            screenWidth = nSize.Width;
            screenHeight = nSize.Height;
            SetVisualWin(VisualWidth, VisualHeight);
        }
        #endregion
    }
}