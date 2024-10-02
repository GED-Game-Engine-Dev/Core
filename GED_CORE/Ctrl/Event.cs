using GED.Core.CXX;
using GED.SanityCheck;
using System.Runtime.InteropServices;

namespace GED.Core.Ctrl
{

    internal static unsafe partial class fEvent
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
        public static partial int Sort(nint mgr, iEvent.fpElCmp_t FunctionCompare);

        [LibraryImport(DllNames.RCore, EntryPoint = "GED_Core_Ctrl_Ev_Sort")]
        public static partial int GetRange(nint mgr, iEvent.fpCond_t Condition, out UIntPtr Min, out UIntPtr Max);

        [LibraryImport(DllNames.RCore, EntryPoint = "GED_Core_Ctrl_Ev_Element")]
        public static partial int GetElement(nint mgr, UIntPtr i, byte[] lpEl);

        [LibraryImport(DllNames.RCore, EntryPoint = "GED_Core_Ctrl_Ev_Element_Set")]
        public static partial int SetElement(nint mgr, UIntPtr i, byte[] lpSrc);
    }


    public unsafe class iEvent
    {
        [StructLayout(LayoutKind.Sequential)]
        private struct member {
            public byte elementSize;
        }

        private XClassMem<member> memory;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public unsafe delegate int fpElCmp_t(void* a, void* b);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public unsafe delegate bool fpCond_t(void* a);
        private iEvent(out int state) {
            FuckedNumbers a;
            memory = new(out a, (int)fEvent.TypeSize());
            state = (int)a;
        }

        public iEvent(UIntPtr size, byte wel, out int perr) : this(out perr) {
            if((perr = fEvent.Make(memory.bytes, wel)) != 0) return;
            perr = fEvent.Resize(memory.bytes, size);
        }

        ~iEvent() => fEvent.Kill(memory.bytes);

        public int Resize(UIntPtr size) => fEvent.Resize(memory.bytes, size);
        public int Sort(fpElCmp_t FunctionCompare) => fEvent.Sort(memory.bytes, FunctionCompare);
        public int GetRange(fpCond_t Condition, out UIntPtr Min, out UIntPtr Max) => fEvent.GetRange(memory.bytes, Condition, out Min, out Max);
        
        public int GetElement(UIntPtr i, out byte[] output)
        {

            output = new byte[memory.Instance()[0].elementSize];
            return fEvent.GetElement(memory.bytes, i, output);
        }
        
        public int SetElement(UIntPtr i, byte[] element) => fEvent.SetElement(memory.bytes, i, element);
    }
}