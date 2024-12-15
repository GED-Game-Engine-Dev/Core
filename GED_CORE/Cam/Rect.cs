using System.Runtime.InteropServices;
using GED.Core.SanityCheck;

namespace GED.Core {
    internal static partial class fCamRect {
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

        static fCamRect() {
            size = Size();
        }
    }

    internal static partial class fCamRectEl {
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
        unsafe public static partial int GetParam(nint _this, CamRectPrm** param);

        static fCamRectEl() {
            size = Size();
        }
    }
    public class CamRect : iCamRect<CamRect.El>
    {
        internal XClassMem memory;
        public CamRect(out int _err) {
            memory = new XClassMem(out _err, fCamRect.size);
            if(_err != States.OK)
            return;
            _err = fCamRect.Make(memory.bytes);
        }

        public override int Resize(nuint count) => fCamRect.Resize(memory.bytes, count);
        protected override int _BuffAll(BmpSourceRef dest, uint Colour_Background)
        => fCamRect.BuffAll(memory.bytes, dest.memory.bytes, Colour_Background);
        ~CamRect() => fCamRect.Free(memory.bytes);

        public class El : iCamRectEl
        {
            internal XClassMem memory;
            internal El(out int state) {
                memory = new(out state, fCamRectEl.size);
            }

            public El(
                out int state, 
                byte Alpha,
                uint WidthAsResized,
                uint HeightAsResized,
                uint AddrXForDest,
                uint AddrYForDest,
                uint DataToIgnore,
                in BmpSourceRef source,
                byte ReverseIdx = CamRectPrm.YReverse
            ) : this(out state) {
                if(
                    state == States.OK || 
                    (state & States.DONE_HOWEV) == States.DONE_HOWEV
                )
                state = fCamRectEl.Init(
                    memory.bytes, 
                    Alpha, ReverseIdx,
                    WidthAsResized, HeightAsResized,
                    AddrXForDest, AddrYForDest,
                    DataToIgnore,
                    source.memory.bytes
                );
            }

            public El(
                out int state, 
                byte Alpha,
                uint WidthAsResized,
                uint HeightAsResized,
                uint AddrXForDest,
                uint AddrYForDest,
                uint DataToIgnore,
                in BmpSourceRef source,
                bool Reverse_X,
                bool Reverse_Y
            ) : this(out state)
            => state = fCamRectEl.Init(
                memory.bytes, 
                Alpha, (byte)((Reverse_X ? 1 : 0) | (Reverse_Y ? 2 : 0)),
                
                WidthAsResized, HeightAsResized,
                AddrXForDest, AddrYForDest,
                
                DataToIgnore,
                
                source.memory.bytes
            );

            public override unsafe ref CamRectPrm CheckPrm(out int err) {
                CamRectPrm* param;
                err = fCamRectEl.GetParam(memory.bytes, &param);
                return ref param[0];
            }
        }

        public override int Read(
            nuint index,
            out iCamRectEl buffer
        ) {
            int code;
            El _ = new El(out code);
            buffer = _; if(code != States.OK) return code;
            return fCamRect.Read(memory.bytes, _.memory.bytes, index);
        }

        public override int Write(
            nuint index,
            in El buffer
        ) => fCamRect.Write(memory.bytes, buffer.memory.bytes, index);
    }
}