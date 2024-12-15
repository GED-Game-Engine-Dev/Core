using Avalonia.Media.Imaging;
using Avalonia.Platform;
using GED.Core.SanityCheck;

namespace GED.Core
{
    /// <summary>
    /// Interface for a camera type
    /// </summary>
    /// <typeparam name="I">Source Data</typeparam>
    /// <typeparam name="O">Destination Data Type</typeparam>
    public abstract class iCam<I, O> {

        internal const uint WHITE = 0xFFFFFF;
        internal const uint TRANSPARENT = 0xFFFFFFFF;

        protected abstract int _BuffAll(BmpSourceRef dest, uint Colour_Background);
        public int BuffAll(BmpSourceRef dest) 
        => _BuffAll(dest, TRANSPARENT);
        public int BuffAll(BmpSourceRef dest, uint Colour_Background) 
        => _BuffAll(dest, WHITE & Colour_Background);

        public int BuffAll(WriteableBitmap dest, uint Colour_Background) {
            BmpElSize elsize;
            if(dest.Format == PixelFormats.Bgr24) {
                elsize = BmpElSize.RGB24;
            } else if (dest.Format == PixelFormats.Bgra8888) {
                elsize = BmpElSize.RGBA32;
            } else return States.IMP_NOT_FOUND;

            using(var locked = dest.Lock()) {
                int err;

                BmpSource bitmap = new BmpSource(out err, 
                (uint)dest.PixelSize.Width, (uint)dest.PixelSize.Height,
                elsize, locked.Address
                );

                return err == States.OK ? BuffAll(bitmap, Colour_Background) : err;
            }
        }

        public int BuffAll(WriteableBitmap dest) {
            BmpElSize elsize;
            if(dest.Format == PixelFormats.Bgr24) {
                elsize = BmpElSize.RGB24;
            } else if (dest.Format == PixelFormats.Bgra8888) {
                elsize = BmpElSize.RGBA32;
            } else return States.IMP_NOT_FOUND;

            using(var locked = dest.Lock()) {
                int err;

                BmpSource bitmap = new BmpSource(out err, 
                (uint)dest.PixelSize.Width, (uint)dest.PixelSize.Height,
                elsize, locked.Address
                );

                return err == States.OK ? BuffAll(bitmap) : err;
            }
        }

        public abstract int Resize(nuint count);

        public abstract int Read(
            nuint index,
            out O dest
        );

        public abstract int Write(
            nuint index,
            in I src
        );
    }
}