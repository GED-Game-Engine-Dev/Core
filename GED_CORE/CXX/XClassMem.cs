using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace GED.Core.CXX {
    /// <summary>
    /// It owns the unmanaged memory.
    /// </summary>
    /// <typeparam name="T">The sequentially / explicitly layered structure</typeparam>
    internal unsafe class XClassMem<T> where T : struct {
        public readonly nint bytes;

        public XClassMem(out SanityCheck.FuckedNumbers state, int sizeWantedExact = 0) {

            var layoutAttributes = typeof(T).GetCustomAttributes(typeof(StructLayoutAttribute), false);
            if (
                layoutAttributes.Length == 0 || !(layoutAttributes[0] is StructLayoutAttribute layoutAttribute) || 
                !(layoutAttribute.Value == LayoutKind.Sequential || layoutAttribute.Value == LayoutKind.Explicit) 
            )
            {
                Trace.Assert(false, "Follow structure must be explicit or sequential.");
                bytes = 0;
                state = SanityCheck.FuckedNumbers.WRONG_OPERATION;
                return;
            }
            try {
                int __sz = Marshal.SizeOf(typeof(T));
                bytes = Marshal.AllocHGlobal(sizeWantedExact > __sz ? sizeWantedExact : __sz); 
            } catch {
                bytes = 0;
                state = SanityCheck.FuckedNumbers.ALLOC_FAILED;
                return;
            }

            state = SanityCheck.FuckedNumbers.OK;
            return;
        }

        ~XClassMem() {
            if(bytes != 0) {
                Marshal.FreeHGlobal(bytes);
            }
        }

        public T* Instance() {
            return (T*)bytes;
        }
    }
}