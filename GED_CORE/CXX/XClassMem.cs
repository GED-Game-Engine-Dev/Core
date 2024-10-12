using System.Diagnostics;
using System.Runtime.InteropServices;

namespace GED.Core.CXX {
    /// <summary>
    /// It owns the unmanaged memory.
    /// </summary>
    /// <typeparam name="T">The sequentially / explicitly layered structure</typeparam>
    public unsafe class XClassMem<T> where T : struct {
        internal readonly nint bytes;

        internal XClassMem(out int state, int sizeWantedExact = 0) {

            var layoutAttributes = typeof(T).GetCustomAttributes(typeof(StructLayoutAttribute), false);
            if (
                layoutAttributes.Length == 0 || !(layoutAttributes[0] is StructLayoutAttribute layoutAttribute) || 
                !(layoutAttribute.Value == LayoutKind.Sequential || layoutAttribute.Value == LayoutKind.Explicit) 
            )
            {
                Trace.Assert(false, "Following structure must be explicit or sequential.");
                bytes = 0;
                state = (int)SanityCheck.FuckedNumbers.WRONG_OPERATION;
                return;
            }

            try {
                int __sz = Marshal.SizeOf(typeof(T));
                __sz = sizeWantedExact > __sz ? sizeWantedExact : __sz;

                bytes = Marshal.AllocHGlobal(__sz);
                for(int i = 0; i < __sz; i++)
                ((byte*)bytes)[i] = 0;
                
            } catch {
                bytes = 0;
                state = (int)SanityCheck.FuckedNumbers.ALLOC_FAILED;
                return;
            }

            state = (int)SanityCheck.FuckedNumbers.OK;
            return;
        }

        ~XClassMem() {
            if(bytes != 0) {
                Marshal.FreeHGlobal(bytes);
            }
        }

        public ref T Instance() {
            return ref ((T*)bytes)[0];
        }
    }
}