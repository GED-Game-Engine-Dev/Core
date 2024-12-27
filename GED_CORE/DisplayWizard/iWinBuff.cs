using Avalonia;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using GED.Core.SanityCheck;

namespace GED.Core.DisplayWizard
{
    /// <summary>
    /// 
    /// </summary>
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

        public byte Main(object _prm) {
            MainPrm prm = (MainPrm)_prm;
            if (prm.VisualWidth == 0 || prm.VisualHeight == 0)
            { return States.WRONG_OPERATION; }
            DisplayBuffer = new WriteableBitmap(
                new PixelSize(prm.VisualWidth, prm.VisualHeight), 
                new Vector(96, 96), 
                format: PixelFormats.Bgr24
            );
            return States.OK;
        }
    }
}