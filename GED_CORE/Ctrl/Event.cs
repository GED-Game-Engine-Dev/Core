using GED.Core.CXX;
using System.Runtime.InteropServices;

namespace GED.Core.Ctrl
{

    internal static unsafe partial class fEvent
    {
        [LibraryImport(SanityCheck.DllNames.RCore, EntryPoint = "GED_Core_Ctrl_Ev_Make")]
        public static partial int Make(byte[] mgr, byte elwidth);

        [LibraryImport(SanityCheck.DllNames.RCore, EntryPoint = "GED_Core_Ctrl_Ev_Resize")]
        public static partial int Resize(byte[] mgr, UIntPtr size);

        [LibraryImport(SanityCheck.DllNames.RCore,  EntryPoint = "GED_Core_Ctrl_Ev_Kill")]
        public static partial void Kill(byte[] mgr);

        [LibraryImport(SanityCheck.DllNames.RCore, EntryPoint = "GED_Core_Ctrl_Ev_TypeSize")]
        public static partial UIntPtr TypeSize();

        [LibraryImport(SanityCheck.DllNames.RCore, EntryPoint = "GED_Core_Ctrl_Ev_Sort")]
        public static partial int Sort(byte[] mgr, iEvent.fpElCmp_t FunctionCompare);

        [LibraryImport(SanityCheck.DllNames.RCore, EntryPoint = "GED_Core_Ctrl_Ev_Sort")]
        public static partial int GetRange(byte[] mgr, iEvent.fpCond_t Condition, out UIntPtr Min, out UIntPtr Max);

        [LibraryImport(SanityCheck.DllNames.RCore, EntryPoint = "GED_Core_Ctrl_Ev_Element")]
        public static partial int GetElement(byte[] mgr, UIntPtr i, byte[] lpEl);

        [LibraryImport(SanityCheck.DllNames.RCore, EntryPoint = "GED_Core_Ctrl_Ev_Element_Set")]
        public static partial int SetElement(byte[] mgr, UIntPtr i, byte[] lpSrc);
    }

    public unsafe class iEvent : iXClass
    {
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public unsafe delegate int fpElCmp_t(void* a, void* b);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public unsafe delegate bool fpCond_t(void* a);

        private iEvent() : base(fEvent.TypeSize()) { }

        public iEvent(UIntPtr size, byte wel, out int perr) : this() {
            if((perr = fEvent.Make(Raw, wel)) != 0) return;
            perr = fEvent.Resize(Raw, size);
        }

        ~iEvent() => fEvent.Kill(Raw);
        public int Resize(UIntPtr size) => fEvent.Resize(Raw, size);
        public int Sort(fpElCmp_t FunctionCompare) => fEvent.Sort(Raw, FunctionCompare);
        public int GetRange(fpCond_t Condition, out UIntPtr Min, out UIntPtr Max) => fEvent.GetRange(Raw, Condition, out Min, out Max);
        
        public byte[] GetElement(UIntPtr i, out int perr)
        {
            byte[] a = new byte[Raw[0]];
            perr = fEvent.GetElement(Raw, i, a);
            return a;
        }

        public int SetElement(UIntPtr i, byte[] element) => fEvent.SetElement(Raw, i, element);
    }
}