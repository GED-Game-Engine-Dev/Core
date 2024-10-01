using System;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace GED.SanityCheck {
    internal static class OS {
        public const string Dll_Name_Prefix = "";


#if GED_WIN
        public const string Dll_Name_Suffix = ".dll";
#elif GED_LINUX
        public const string Dll_Name_Suffix = ".so.dll";
#else
        public const string Dll_Name_Suffix = "";
#endif

        static OS() {
            #if GED_LINUX || GED_WINDOWS
            #else
            Trace.Assert(false, "No implementation. Go away. (It must be windows or linux)");
            #endif

            
        }
    }
}