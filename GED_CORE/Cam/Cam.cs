using System.Runtime.InteropServices;
using GED.Core.SanityCheck;

namespace GED.Core {
    internal static partial class fCam {
        [LibraryImport(DllNames.RCore, EntryPoint = "GED_CamBuffAll")]
        public static partial int BuffAll(nint _this, nint dest, uint background_asRGB);

        [LibraryImport(DllNames.RCore, EntryPoint = "GED_CamResize")]
        public static partial int Resize(nint _this, nuint count);

        [LibraryImport(DllNames.RCore, EntryPoint = "GED_CamDel")]
        public static partial int Free(nint _this);

        [LibraryImport(DllNames.RCore, EntryPoint = "GED_CamMk")]
        public static partial int Make(nint _this);

        [LibraryImport(DllNames.RCore, EntryPoint = "GED_CamRead")]
        public static partial int Read(nint _this, nint Element, nuint index);

        [LibraryImport(DllNames.RCore, EntryPoint = "GED_CamWrite")]
        public static partial int Write(nint _this, nint Element, nuint index);

        [LibraryImport(DllNames.RCore, EntryPoint = "GED_CamSize")]
        private static partial nuint Size();

        public readonly static nuint size;

        static fCam() {
            size = Size();
        }
    }

    internal static partial class fCamEl {
        [LibraryImport(DllNames.RCore, EntryPoint = "GED_CamEl_Size")]
        private static partial nuint Size();

        [LibraryImport(DllNames.RCore, EntryPoint = "GED_CamEl_Init")]
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

        [LibraryImport(DllNames.RCore, EntryPoint = "GED_CamEl_getParam")]
        unsafe public static partial int GetParam(nint _this, Cam.El.Prm** param);

        static fCamEl() {
            size = Size();
        }
    }
    public class Cam : iCam<Cam.El>
    {
        internal XClassMem memory;
        public Cam(out int _err) {
            memory = new XClassMem(out _err, fCam.size);
            if(_err != FuckedNumbers.OK)
            return;
            _err = fCam.Make(memory.bytes);
        }

        public override int Resize(nuint count) => fCam.Resize(memory.bytes, count);
        protected override int _BuffAll(GED.Core.BmpSource dest, uint Colour_Background)
        => fCam.BuffAll(memory.bytes, dest.memory.bytes, Colour_Background);
        ~Cam() => fCam.Free(memory.bytes);

        public class El
        {
            public struct Prm {
                public byte 
                    Alpha, // Global Fucking Alpha
                    ReverseIdx; // Reverse Idx?
                public uint
                    WidthAsResized, 	// want to resize?
                    HeightAsResized, 	// want to resize?
                    AddrXForDest, 		// where to copy
                    AddrYForDest, 		// where to copy
                    DataToIgnore;

                public Float
                    RotateXYClockWise;

                public int 
                    AxisX, AxisY;
            };

            internal XClassMem memory;
            internal El(out int state) {
                memory = new(out state, fCamEl.size);
            }

            public static class ReverseIdx {
                public const byte XReverse = 0b1;
                public const byte YReverse = 0b10;
                public const byte NoneReverse = 0;
            }

            public El(
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
            => state = fCamEl.Init(
                memory.bytes, 
                Alpha, ReverseIdx,
                
                WidthAsResized, HeightAsResized,
                AddrXForDest, AddrYForDest,
                
                DataToIgnore,
                
                source.memory.bytes
            );

            public El(
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
            => state = fCamEl.Init(
                memory.bytes, 
                Alpha, (byte)((Reverse_X ? 1 : 0) | (Reverse_Y ? 2 : 0)),
                
                WidthAsResized, HeightAsResized,
                AddrXForDest, AddrYForDest,
                
                DataToIgnore,
                
                source.memory.bytes
            );

            public unsafe ref Prm CheckPrm(out int err) {
                Prm* param;
                err = fCamEl.GetParam(memory.bytes, &param);
                return ref param[0];
            }
        }

        public override int Read(
            nuint index,
            out El buffer
        ) {
            int code;
            buffer = new El(out code);
            if(code != FuckedNumbers.OK) return code;
            return fCam.Read(memory.bytes, buffer.memory.bytes, index);
        }

        public override int Write(
            nuint index,
            in El buffer
        ) => fCam.Write(memory.bytes, buffer.memory.bytes, index);
    }
}