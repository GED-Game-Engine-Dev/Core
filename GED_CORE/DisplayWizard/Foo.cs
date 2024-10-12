using System.Numerics;
using Avalonia.Media.Imaging;
using GED.Core;
using GED.SanityCheck;

namespace GED.DisplayWizerd {
    public class Foo {
        private WriteableBitmap? memory;

        public Foo(out int Error, BitmapElementSize pxbc, int w, int h, Avalonia.Vector dpi) {
            Error = 0;
            switch(pxbc) {
                case BitmapElementSize.RGB24:
                memory = new WriteableBitmap(new Avalonia.PixelSize(w,h), dpi, Avalonia.Platform.PixelFormats.Rgb24);
                break;

                case BitmapElementSize.RGBA32:
                memory = new WriteableBitmap(new Avalonia.PixelSize(w,h), dpi, Avalonia.Platform.PixelFormats.Rgba8888);
                break;

                default:
                Error = 1;
                break;
            }
        }
    }
}