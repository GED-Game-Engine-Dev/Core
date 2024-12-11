using System.Diagnostics;
using System.Runtime.InteropServices;

namespace GED.Core.SanityCheck {
    public unsafe class XClassMemRef {
        internal nint bytes;
        internal XClassMemRef() {
            bytes = 0;
        }
    }

    /// <summary>
    /// It owns the unmanaged memory.
    /// </summary>
    public unsafe class XClassMem : XClassMemRef {
        internal XClassMem(out int state, nuint __sz) : base() {
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