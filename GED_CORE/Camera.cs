using Avalonia.Media.Imaging;
using Avalonia.Platform;
using GED.Core.SanityCheck;
using System.Runtime.InteropServices;

namespace GED.Core
{
    internal static partial class fCamera {
        [LibraryImport(DllNames.RCore, EntryPoint = "GED_Core_Camera_Buff_All")]
        public static partial int BuffAll(nint _this, nint dest, uint background_asRGB);

        [LibraryImport(DllNames.RCore, EntryPoint = "GED_Core_Camera_Buff_Threaded")]
        public static partial int BuffThreaded(nint _this, nint dest, uint background_asRGB, byte tdcount);

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
            byte ReverseIdx,
            uint WidthAsResized,
            uint HeightAsResized,
            uint AddrXForDest,
            uint AddrYForDest,
            uint DataToIgnore,
            nint __PTr_BmpSource
        );

        public readonly static nuint size;

        [LibraryImport(DllNames.RCore, EntryPoint = "GED_Core_Camera_El_getParam")]
        unsafe public static partial int GetParam(nint _this, Camera.Element.Param** param);

        static fCameraEl() {
            size = Size();
        }
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

        internal const uint WHITE = 0xFFFFFF;
        internal const uint TRANSPARENT = 0xFFFFFFFF;

        public int BuffAll(BmpSource dest, uint Colour_Background)
            => fCamera.BuffAll(memory.bytes, dest.memory.bytes, Colour_Background & WHITE);

        public int BuffAll(BmpSource dest)
            => fCamera.BuffAll(memory.bytes, dest.memory.bytes, TRANSPARENT);

        public int BuffThreaded(BmpSource dest, uint Colour_Background, byte tdcount)
            => fCamera.BuffThreaded(memory.bytes, dest.memory.bytes, Colour_Background & WHITE, tdcount);

        public int BuffThreaded(BmpSource dest, byte tdcount)
            => fCamera.BuffThreaded(memory.bytes, dest.memory.bytes, TRANSPARENT, tdcount);

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

                return err == FuckedNumbers.OK ? BuffAll(bitmap, Colour_Background) : err;
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

                return err == FuckedNumbers.OK ? BuffAll(bitmap) : err;
            }
        }

        public int BuffThreaded(WriteableBitmap dest, uint Colour_Background, byte tdcount) {
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

                return err == FuckedNumbers.OK ? BuffThreaded(bitmap, Colour_Background, tdcount) : err;
            }
        }

        public int BuffThreaded(WriteableBitmap dest, byte tdcount) {
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

                return err == FuckedNumbers.OK ? BuffThreaded(bitmap, tdcount) : err;
            }
        }

        public class Element
        {
            public struct Param {
                public byte 
                    Alpha, // Global Fucking Alpha
                    ReverseIdx; // Reverse Idx?
                public uint
                    WidthAsResized, 	// want to resize?
                    HeightAsResized, 	// want to resize?
                    AddrXForDest, 		// where to copy
                    AddrYForDest, 		// where to copy
                    DataToIgnore;

                public double 
                    RotateXYClockWise;

                public int 
                    AxisX, AxisY;
            };

            internal XClassMem memory;
            internal Element(out int state) {
                memory = new(out state, fCameraEl.size);
            }

            public static class ReverseIdx {
                public const byte XReverse = 0b1;
                public const byte YReverse = 0b10;
                public const byte NoneReverse = 0;
            }

            public Element(
                out int state, 
                byte Alpha,
                uint WidthAsResized,
                uint HeightAsResized,
                uint AddrXForDest,
                uint AddrYForDest,
                uint DataToIgnore,
                in BmpSource source,
                byte ReverseIdx = ReverseIdx.YReverse
            ) : this(out state)
            => state = fCameraEl.Init(
                memory.bytes, 
                Alpha, ReverseIdx,
                
                WidthAsResized, HeightAsResized,
                AddrXForDest, AddrYForDest,
                
                DataToIgnore,
                
                source.memory.bytes
            );

            public Element(
                out int state, 
                byte Alpha,
                uint WidthAsResized,
                uint HeightAsResized,
                uint AddrXForDest,
                uint AddrYForDest,
                uint DataToIgnore,
                in BmpSource source,
                bool Reverse_X,
                bool Reverse_Y
            ) : this(out state)
            => state = fCameraEl.Init(
                memory.bytes, 
                Alpha, (byte)((Reverse_X ? 1 : 0) | (Reverse_Y ? 2 : 0)),
                
                WidthAsResized, HeightAsResized,
                AddrXForDest, AddrYForDest,
                
                DataToIgnore,
                
                source.memory.bytes
            );

            public unsafe ref Param CheckPrm(out int err) {
                Param* param;
                err = fCameraEl.GetParam(memory.bytes, &param);
                return ref param[0];
            }
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