using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using GED.Core.Ctrl;
using GED.Core.SanityCheck;

using ae2f_float_t = @ae2f_float@;

namespace GED.Core.DisplayWizard
{
    /// <summary>
    /// Window from Avalonia, Minimum Control included.
    /// </summary>
    public class MinCtrlWin : Window
    {
        #region Member Fields
        internal WriteableBitmap __DisplayBuffer;
        #endregion

        #region Properties
        public WriteableBitmap DisplayBuffer { get => __DisplayBuffer; }

        public Image? Buffer;
        #endregion

        unsafe public MinCtrlWin(out int err, int VisualWidth, int VisualHeight)
        {
            err = States.OK;
            if (VisualWidth == 0 || VisualHeight == 0)
            {
                err = States.WRONG_OPERATION;
                return;
            }

            __DisplayBuffer = new WriteableBitmap(new PixelSize(VisualWidth, VisualHeight), new Vector(96, 96), format: PixelFormats.Bgr24);

            try
            {
                PointerMoved += OnPointerMoved;
                AvaloniaXamlLoader.Load(this);
            }
            catch
            {
                err = States.ALLOC_FAILED;
                return;
            }
        }

        #region Private Functions
        private unsafe void OnPointerMoved(object? sender, PointerEventArgs e)
        {
            Point pos = e.GetPosition(this);
            fMousePoint.X[0] = (ae2f_float_t)pos.X;
            fMousePoint.Y[0] = (ae2f_float_t)pos.Y;
        }
        #endregion
    }
}