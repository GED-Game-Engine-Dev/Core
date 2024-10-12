using GED.Core.CXX;
using GED.SanityCheck;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace GED.Core.Ctrl
{
        
    internal static partial class fEvent
    {
        [LibraryImport(DllNames.RCore, EntryPoint = "GED_Core_Ctrl_Ev_Make")]
        public static partial int Make(nint mgr, byte elwidth);

        [LibraryImport(DllNames.RCore, EntryPoint = "GED_Core_Ctrl_Ev_Resize")]
        public static partial int Resize(nint mgr, UIntPtr size);

        [LibraryImport(DllNames.RCore,  EntryPoint = "GED_Core_Ctrl_Ev_Kill")]
        public static partial void Kill(nint mgr);

        [LibraryImport(DllNames.RCore, EntryPoint = "GED_Core_Ctrl_Ev_TypeSize")]
        public static partial UIntPtr TypeSize();

        [LibraryImport(DllNames.RCore, EntryPoint = "GED_Core_Ctrl_Ev_Sort")]
        public static partial int Sort(nint mgr, Event.fpElCmp_t FunctionCompare);

        [LibraryImport(DllNames.RCore, EntryPoint = "GED_Core_Ctrl_Ev_Sort")]
        public static partial int GetRange(nint mgr, Event.fpCond_t Condition, out UIntPtr Min, out UIntPtr Max);

        [LibraryImport(DllNames.RCore, EntryPoint = "GED_Core_Ctrl_Ev_Element")]
        public static partial int GetElement(nint mgr, UIntPtr i, nint lpEl);

        [LibraryImport(DllNames.RCore, EntryPoint = "GED_Core_Ctrl_Ev_Element_Set")]
        public static partial int SetElement(nint mgr, UIntPtr i, nint lpSrc);
    }

    namespace Event {
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public unsafe delegate int fpElCmp_t(void* a, void* b);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public unsafe delegate bool fpCond_t(void* a);
    }


    internal class iEvent
    {
        [StructLayout(LayoutKind.Sequential)]
        protected struct member {
            public byte elementSize;
        }

        protected XClassMem<member> memory;

        protected iEvent(out int state) {
            memory = new(out state, (int)fEvent.TypeSize());
        }

        public iEvent(UIntPtr size, byte wel, out int perr) : this(out perr) {
            if(perr != (int)FuckedNumbers.OK) return;
            if((perr = fEvent.Make(memory.bytes, wel)) != 0) return; 
            perr = fEvent.Resize(memory.bytes, size);
        }

        ~iEvent() => fEvent.Kill(memory.bytes);

        public int Resize(UIntPtr size) => fEvent.Resize(memory.bytes, size);
        public int Sort(Event.fpElCmp_t FunctionCompare) => fEvent.Sort(memory.bytes, FunctionCompare);
        public int GetRange(Event.fpCond_t Condition, out UIntPtr Min, out UIntPtr Max) => fEvent.GetRange(memory.bytes, Condition, out Min, out Max);
        
        public unsafe int GetElement(UIntPtr i, out byte[] output)
        {
            output = new byte[memory.Instance().elementSize];

            fixed(byte* outputPtr = output) {
                return fEvent.GetElement(memory.bytes, i, (nint)outputPtr);
            }
        }
        
        public unsafe int SetElement(UIntPtr i, out byte[] element) {
            fixed(byte* elementPtr = element) {
                return fEvent.SetElement(memory.bytes, i, (nint)elementPtr);
            }
        }
    }


    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal class xEvent<T> : iEvent where T : struct {
        protected xEvent(out int state) : base(out state) {
            if(state == (int)FuckedNumbers.OK)
            {
                var layoutAttributes = typeof(T).GetCustomAttributes(typeof(StructLayoutAttribute), false);
                if (
                    layoutAttributes.Length == 0 || !(layoutAttributes[0] is StructLayoutAttribute layoutAttribute) || 
                    !(layoutAttribute.Value == LayoutKind.Sequential || layoutAttribute.Value == LayoutKind.Explicit) 
                )
                {
                    Trace.Assert(false, "Follow structure must be explicit or sequential.");
                    state = (int)FuckedNumbers.WRONG_OPERATION;
                    return;
                }
            }
        }

        public xEvent(UIntPtr size, out int perr)
        : this(out perr) {
            if(perr != (int)FuckedNumbers.OK) return;

            var oppose = Marshal.SizeOf(typeof(T));
            if(oppose > 255) {
                perr = (int)FuckedNumbers.WRONG_OPERATION;
                return;
            }
            byte wel = (byte)oppose;

            if((perr = fEvent.Make(memory.bytes, wel)) != 0) return;
            perr = fEvent.Resize(memory.bytes, size);
        }

        public unsafe int GetElement(UIntPtr i, ref T output) {
            fixed(T* a = &output) {
                return fEvent.GetElement(memory.bytes, i, (nint)a);
            }
        }

        public unsafe int SetElement(UIntPtr i, T output) {
            return fEvent.SetElement(memory.bytes, i, (nint)(&output));
        }
    }
}