using GED.Core.CXX;
using GED.SanityCheck;
using System.Diagnostics;
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

        [LibraryImport(DllNames.RCore, EntryPoint = "GED_Core_Camera_Append")]
        public static partial int Append(nint _this, nint Element);
    }

    class Camera
    {
        [StructLayout(LayoutKind.Sequential)]
        protected struct member
        {
            public nint A;
            public nint B;
            public nint C;
        }

        protected XClassMem<member> memory;
    }
}