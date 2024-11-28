using System.Runtime.InteropServices;
using GED.Core.SanityCheck;

namespace GED.Core {

    internal static partial class fCamCL {
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

        static fCamCL() {
            size = fCam.size;
        }
    }

    internal static partial class fCamCLEl {
        [LibraryImport(DllNames.RCore, EntryPoint = "GED_CLCamElInit")]
        public static partial void Init(nint el, nint src, nint prm);

        [LibraryImport(DllNames.RCore, EntryPoint = "GED_CLCamElDel")]
        public static partial void Del(nint block);
    }

    public class CamCL : iCam<CamCL.El> {
        public class El {
            
        }
    }
}