using System.Diagnostics;

namespace GED.Core.SanityCheck
{
    internal static class OS
    {
        #region Member Fields

        #if GED_WIN
        public const string Dll_Name_Prefix = "lib";
        public const string Dll_Name_Suffix = "";
#elif GED_LINUX
        public const string Dll_Name_Prefix = "";
        public const string Dll_Name_Suffix = "";
#elif GED_APPLE
        public const string Dll_Name_Prefix = "";
        public const string Dll_Name_Suffix = "";
#else
        public const string Dll_Name_Prefix = "";
        public const string Dll_Name_Suffix = "";
#endif

        #endregion

        #region Constructors

        static OS()
        {
            #if !(GED_LINUX || GED_WINDOWS)
            Trace.Assert(false, "No implementation. Go away. (It must be windows or linux)");
            #endif
        }

        #endregion
    }
}