using Avalonia.Controls;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using GED.Core.SanityCheck;
using System.Runtime.InteropServices;

namespace GED.Core
{
    internal static partial class fCamera {
        [LibraryImport(DllNames.RCore, EntryPoint = "GED_Core_Camera_Buff_All")]
        public static partial int BuffAll(nint _this, nint dest, uint background_asRGB);

        [LibraryImport(DllNames.RCore, EntryPoint = "GED_Core_Camera_Resize")]
        public static partial int Resize(nint _this, nuint count);

        [LibraryImport(DllNames.RCore, EntryPoint = "GED_Core_Camera_Free")]
        public static partial int Free(nint _this);

        [LibraryImport(DllNames.RCore, EntryPoint = "GED_Core_Camera_Make")]
        public static partial int Make(nint _this);

        [LibraryImport(DllNames.RCore, EntryPoint = "GED_Core_Camera_Read")]
        public static partial int Read(nint _this, nint Element, nuint index);

        [LibraryImport(DllNames.RCore, EntryPoint = "GED_Core_Camera_Write")]
        public static partial int Write(nint _this, nint Element, nuint index);

        [LibraryImport(DllNames.RCore, EntryPoint = "GED_Core_Camera_Size")]
        private static partial nuint Size();

        public readonly static nuint size;

        static fCamera() {
            size = Size();
        }
    }

    internal static partial class fCameraEl {
        [LibraryImport(DllNames.RCore, EntryPoint = "GED_Core_Camera_El_Size")]
        private static partial nuint Size();

        [LibraryImport(DllNames.RCore, EntryPoint = "GED_Core_Camera_El_Init")]
        public static partial int Init(
            nint _this,
            byte Alpha,
            uint WidthAsResized,
            uint HeightAsResized,
            uint AddrXForDest,
            uint AddrYForDest,
            uint DataToIgnore,
            nint __PTr_BmpSource
        );

        public readonly static nuint size;

        static fCameraEl() {
            size = Size();
        }
    }

    internal struct CameraField {
        public nint A;
        public nint B;
        public nint C;
    }

    public class Camera
    {
        internal XClassMem memory;
        public Camera(out int _err) {
            memory = new XClassMem(out _err, fCamera.size);
            if(_err != FuckedNumbers.OK)
            return;
            _err = fCamera.Make(memory.bytes);
        }

        public int Resize(nuint count) => fCamera.Resize(memory.bytes, count);
        ~Camera() => fCamera.Free(memory.bytes);

        public int BuffAll(BmpSource dest, uint Colour_Background)
            => fCamera.BuffAll(memory.bytes, dest.memory.bytes, Colour_Background & 0xFFFFFF);

        public int BuffAll(BmpSource dest)
            => fCamera.BuffAll(memory.bytes, dest.memory.bytes, 0xFFFFFFFF);

        public int BuffAll(WriteableBitmap dest, uint Colour_Background) {
            BitmapElementSize elsize;
            if(dest.Format == PixelFormats.Bgr24) {
                elsize = BitmapElementSize.RGB24;
            } else if (dest.Format == PixelFormats.Bgra8888) {
                elsize = BitmapElementSize.RGBA32;
            } else return FuckedNumbers.IMP_NOT_FOUND;

            using(var locked = dest.Lock()) {
                int err;

                BmpSource bitmap = new BmpSource(out err, 
                (uint)dest.PixelSize.Width, (uint)dest.PixelSize.Height,
                elsize, locked.Address
                );

                return err == FuckedNumbers.OK ? BuffAll(bitmap, Colour_Background & 0xFFFFFF) : err;
            }
        }

        public int BuffAll(WriteableBitmap dest) {
            BitmapElementSize elsize;
            if(dest.Format == PixelFormats.Bgr24) {
                elsize = BitmapElementSize.RGB24;
            } else if (dest.Format == PixelFormats.Bgra8888) {
                elsize = BitmapElementSize.RGBA32;
            } else return FuckedNumbers.IMP_NOT_FOUND;

            using(var locked = dest.Lock()) {
                int err;

                BmpSource bitmap = new BmpSource(out err, 
                (uint)dest.PixelSize.Width, (uint)dest.PixelSize.Height,
                elsize, locked.Address
                );

                return err == FuckedNumbers.OK ? BuffAll(bitmap, 0xFFFFFFFF) : err;
            }
        }

        public class Element
        {
            internal XClassMem memory;
            internal Element(out int state) {
                memory = new(out state, fCameraEl.size);
            }

            public Element(
                out int state, 
                byte Alpha,
                uint WidthAsResized,
                uint HeightAsResized,
                uint AddrXForDest,
                uint AddrYForDest,
                uint DataToIgnore,
                in BmpSource source
            ) : this(out state)
            => state = fCameraEl.Init(
                memory.bytes, 
                Alpha, 
                
                WidthAsResized, HeightAsResized,
                AddrXForDest, AddrYForDest,
                
                DataToIgnore,
                
                source.memory.bytes
            );
        }

        public int Read(
            nuint index,
            out Element buffer
        ) {
            int code;
            buffer = new Element(out code);
            if(code != FuckedNumbers.OK) return code;
            return fCamera.Read(memory.bytes, buffer.memory.bytes, index);
        }

        public int Write(
            nuint index,
            in Element buffer
        ) => fCamera.Write(memory.bytes, buffer.memory.bytes, index);
    }
}