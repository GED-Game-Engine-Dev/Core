using Avalonia;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using GED.Core.SanityCheck;

using ae2f_float_t = float;

namespace GED.Core.DisplayWizard
{
    public interface iWinBuff : iWin {
        #region Properties
        public abstract WriteableBitmap DisplayBuffer { get; protected set; }
        #endregion

        public class MainPrm {
            public int VisualHeight, VisualWidth;
            public MainPrm(int VisualWidth, int VisualHeight) {
                this.VisualHeight = VisualHeight;
                this.VisualWidth = VisualWidth;
            }
        };

        public byte MainBuff(object _prm) {
            MainPrm prm = (MainPrm)_prm;
            if (prm.VisualWidth == 0 || prm.VisualHeight == 0)
            { return States.WRONG_OPERATION; }
            DisplayBuffer = new WriteableBitmap(new PixelSize(prm.VisualWidth, prm.VisualHeight), new Vector(96, 96), format: PixelFormats.Bgr24);
            return States.OK;
        }

        public abstract class __Win : iWin.__Win, iWinBuff
        {
            public abstract WriteableBitmap DisplayBuffer { get; set; }
        }

        public class Built<T, P> : iWin.Built<T, P> where T : iWinBuff.__Win, new() where P : MainPrm
        {
            public Built(out byte err, P prm) : base(out err, prm) {}
        }
    }
}