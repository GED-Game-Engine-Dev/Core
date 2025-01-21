using System.Runtime.InteropServices;
using GED.Core.SanityCheck;

namespace GED.Core
{
    internal static partial class CLPort
    {
        #region Dll Functions

        [LibraryImport(DllNames.RCore, EntryPoint = "GED_CLMk")]
        public static partial byte Init();

        [LibraryImport(DllNames.RCore, EntryPoint = "GED_CLDel")]
        public static partial void Del();

        #endregion
    }
}