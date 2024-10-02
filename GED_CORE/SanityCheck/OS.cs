using System;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace GED.SanityCheck {
    internal static class OS {
#if GED_WIN
        public const string Dll_Name_Prefix = "lib";
        public const string Dll_Name_Suffix = "";
#elif GED_LINUX
        public const string Dll_Name_Prefix = "";
        public const string Dll_Name_Suffix = "";
#else
        public const string Dll_Name_Prefix = "";
        public const string Dll_Name_Suffix = "";
#endif

        static OS() {
#if !(GED_LINUX || GED_WINDOWS)
            Trace.Assert(false, "No implementation. Go away. (It must be windows or linux)");
#endif
        }
    }
}