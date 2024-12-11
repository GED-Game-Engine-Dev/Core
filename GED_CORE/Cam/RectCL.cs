using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using GED.Core.SanityCheck;

namespace GED.Core {

    internal static partial class fCamRectCL {
        [LibraryImport(DllNames.RCore, EntryPoint = "GED_CLCamBuff")]
        public static partial int BuffAll(nint _this, nint dest, uint background_asRGB);

        [LibraryImport(DllNames.RCore, EntryPoint = "GED_CLCamResize")]
        public static partial int Resize(nint _this, nuint count);

        [LibraryImport(DllNames.RCore, EntryPoint = "GED_CLCamDel")]
        public static partial int Free(nint _this);

        [LibraryImport(DllNames.RCore, EntryPoint = "GED_CLCamMk")]
        public static partial int Make(nint _this);

        [LibraryImport(DllNames.RCore, EntryPoint = "GED_CLCamRead")]
        public static partial int Read(nint _this, nint Element, nuint index);

        [LibraryImport(DllNames.RCore, EntryPoint = "GED_CLCamWrite")]
        public static partial int Write(nint _this, nint Element, nuint index);

        public readonly static nuint size;

        static fCamRectCL() {
            size = fCamRect.size;
        }
    }

    internal static partial class fCamRectCLEl {
        [LibraryImport(DllNames.RCore, EntryPoint = "GED_CLCamElInit")]
        public static unsafe partial void Init(nint el, nint src, CamRectPrm* prm);

        [LibraryImport(DllNames.RCore, EntryPoint = "GED_CLCamElDel")]
        public static partial void Del(nint block);

        [LibraryImport(DllNames.RCore, EntryPoint = "GED_CLCamElSize")]
        public static partial nuint Size();

        [LibraryImport(DllNames.RCore, EntryPoint = "GED_CLCamElPrm")]
        public static unsafe partial int ElPrm(nint _this, CamRectPrm** prm);

        public readonly static nuint size;

        static fCamRectCLEl() {
            size = Size();
        }
    }

#if true
    public class CamRectCL : iCamRect<CamRectCL.El> {
        internal XClassMem memory;

        public CamRectCL(out int _err) {
            memory = new XClassMem(out _err, fCamRectCL.size);
            if(_err != FuckedNumbers.OK) return;
            _err = fCamRectCL.Make(memory.bytes);
        }

        public override int Read(nuint index, out iCamRectEl dest) {
            int code;
            El buffer = new El(out code);
            dest = buffer;
            if(code != FuckedNumbers.OK) return code;
            return fCamRectCL.Read(memory.bytes, buffer.memory.bytes, index);
        }

        public override int Resize(nuint count)
        => fCamRectCL.Resize(memory.bytes, count);

        public override int Write(nuint index, in El buffer)
        => fCamRectCL.Write(memory.bytes, buffer.memory.bytes, index);

        protected override int _BuffAll(BmpSourceRef dest, uint Colour_Background)
        => fCamRectCL.BuffAll(memory.bytes, dest.memory.bytes, Colour_Background);

        public class El : iCamRectEl {
            internal XClassMem memory;

            internal El(out int state) { memory = new(out state, fCamRectCLEl.size); }

            unsafe internal El(
                out int state,
                in BmpSourceRef source,
                in CamRectPrm prm
            ) : this(out state) {
                if(
                    state == FuckedNumbers.OK || 
                    (state & FuckedNumbers.DONE_HOWEV) == FuckedNumbers.DONE_HOWEV
                ) {
                    fixed(CamRectPrm* _prm = &prm) 
                    fCamRectCLEl.Init(memory.bytes, source.memory.bytes, _prm);
                }
            }

            public override unsafe ref CamRectPrm CheckPrm(out int err) {
                CamRectPrm* param;
                err = fCamRectCLEl.ElPrm(memory.bytes, &param);
                return ref param[0];
            }

            unsafe ~El() => fCamRectCLEl.Del(memory.bytes);
        }
    }
#endif
}