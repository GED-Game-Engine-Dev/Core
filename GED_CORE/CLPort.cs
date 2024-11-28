using System.Runtime.InteropServices;
using GED.Core.SanityCheck;

namespace GED.Core {
    internal static partial class CLPort {
        [LibraryImport(DllNames.RCore, EntryPoint = "GED_CLMk")]
        private static partial byte Init();

        [LibraryImport(DllNames.RCore, EntryPoint = "GED_CLDel")]
        private static partial void Del();

        class ___ {
            public ___() => Init();
            ~___() => Del();
        }

        // cycle for opencl library
        private static ___ _____ = new ___();
    }
}