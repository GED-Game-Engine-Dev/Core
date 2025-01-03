using Avalonia.Media.Imaging;
using Avalonia.Platform;
using GED.Core.SanityCheck;

namespace GED.Core
{
    /// <summary>
    /// An abstraction of bitmap buffering. <br/><br/>
    /// 
    /// It is a linear list suggested, able to resize.   <br/>
    /// Storing data must be a parameter for buffering (ex. original image and positions and angle)
    /// </summary>
    /// <typeparam name="I">
    /// Notice that type must not have responsiblity of memory.<br/>
    /// A constructing form to configure the data.
    /// </typeparam>
    /// <typeparam name="O">
    /// Notice that type must not have responsiblity of memory.<br/>
    /// An output form for getting the data.
    /// </typeparam>
    public abstract class Cam<I, O> {
        /// <summary>
        /// Represents the white colour as a 3-channel-colour.
        /// </summary>
        public const uint WHITE = 0xFFFFFF;

        /// <summary>
        /// Represents the transparent as a 4-channel-colour.
        /// </summary>
        public const uint TRANSPARENT = 0xFFFFFFFF;

        /// <summary>
        /// Abstraction of buffering. <br/>
        /// Iterates all parameters inside and do buff as inside.
        /// </summary>
        /// <param name="dest">
        /// A buffer as a bitmap.
        /// </param>
        /// <param name="Colour_Background">
        /// Basic background colour. <br/>
        /// Since there will be no pre-layer, alphpa channel will not be required.<br/><br/>
        /// 
        /// Buffering without background colour would not erase the previous written value,
        /// therefore cause an undefined behaviour. <br/><br/>
        /// 
        /// Following constant values are vast examples for background. <br/>
        /// <see cref="WHITE"/><br/>
        /// <see cref="TRANSPARENT"/>
        /// </param>
        /// <returns>
        /// State code in 4-bytes.
        /// </returns>
        protected abstract int _BuffAll(
            BmpSourceRef dest, 
            uint Colour_Background
        );

        /// <summary>
        /// Executes <see cref="_BuffAll"/><br/><br/>
        /// 
        /// Passing no argument of [Colour_Background] from <see cref="_BuffAll"/>. <br/>
        /// Therefore would be considered as transparent.
        /// </summary>
        /// <param name="dest">
        /// A buffer as a bitmap.
        /// </param>
        /// <returns>
        /// 
        /// </returns>
        public int BuffAll(BmpSourceRef dest) 
        => _BuffAll(dest, TRANSPARENT);

        /// <summary>
        /// Esecutes <see cref="_BuffAll"/>.
        /// </summary>
        /// <param name="dest"></param>
        /// <param name="Colour_Background">
        /// Basic background colour. <br/>
        /// Since there will be no pre-layer, alphpa channel will not be required.<br/><br/>
        /// 
        /// Buffering without background colour would not erase the previous written value,
        /// therefore cause an undefined behaviour. <br/><br/>
        /// 
        /// Following constant values are vast examples for background. <br/>
        /// <see cref="WHITE"/><br/>
        /// <see cref="TRANSPARENT"/>
        /// </param>
        /// <returns></returns>
        public int BuffAll(BmpSourceRef dest, uint Colour_Background) 
        => _BuffAll(dest, WHITE & Colour_Background);

        /// <summary>
        /// See <see cref="BuffAll"/>
        /// </summary>
        /// <param name="dest"></param>
        /// <returns></returns>
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

        /// <summary>
        /// See <see cref="BuffAll"/>
        /// </summary>
        /// <param name="dest"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Resize the container.
        /// </summary>
        /// <param name="count">
        /// New count for container.
        /// </param>
        /// <returns></returns>
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