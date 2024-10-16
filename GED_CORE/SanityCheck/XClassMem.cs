using System.Diagnostics;
using System.Runtime.InteropServices;

namespace GED.Core.SanityCheck {
    /// <summary>
    /// It owns the unmanaged memory.
    /// </summary>
    public unsafe class XClassMem {
        internal readonly nint bytes;

        internal XClassMem(out int state, nuint __sz) {
            try {
                bytes = Marshal.AllocHGlobal((nint)__sz);
                for(nuint i = 0; i < __sz; i++)
                ((byte*)bytes)[i] = 0;

            } catch {
                bytes = 0;
                state = FuckedNumbers.ALLOC_FAILED;
                return;
            }

            state = FuckedNumbers.OK;
            return;
        }

        ~XClassMem() {
            if(bytes != 0) {
                Marshal.FreeHGlobal(bytes);
            }
        }
    }
}