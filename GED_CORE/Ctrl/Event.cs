using GED.Core.CXX;
using System.Runtime.InteropServices;

namespace GED.Core.Ctrl
{

    internal unsafe struct Event
    {
        [DllImport("RCore.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "GED_Core_Ctrl_Ev_Make")]
        public static extern int Make(byte[] mgr, byte elwidth);

        [DllImport("RCore.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "GED_Core_Ctrl_Ev_Resize")]
        public static extern int Resize(byte[] mgr, UIntPtr size);

        [DllImport("RCore.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "GED_Core_Ctrl_Ev_Kill")]
        public static extern void Kill(byte[] mgr);

        [DllImport("RCore.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "GED_Core_Ctrl_Ev_TypeSize")]
        public static extern UIntPtr TypeSize();

        [DllImport("RCore.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "GED_Core_Ctrl_Ev_Sort")]
        public static extern int Sort(byte[] mgr, iEvent.fpElCmp_t FunctionCompare);

        [DllImport("RCore.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "GED_Core_Ctrl_Ev_Sort")]
        public static extern int GetRange(byte[] mgr, iEvent.fpCond_t Condition, out UIntPtr Min, out UIntPtr Max);

        [DllImport("RCore.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "GED_Core_Ctrl_Ev_Element")]
        public static extern int GetElement(byte[] mgr, UIntPtr i, byte[] lpEl);

        [DllImport("RCore.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "GED_Core_Ctrl_Ev_Element_Set")]
        public static extern int SetElement(byte[] mgr, UIntPtr i, byte[] lpSrc);
    }

    public unsafe class iEvent : iXClass
    {
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public unsafe delegate int fpElCmp_t(void* a, void* b);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public unsafe delegate bool fpCond_t(void* a);

        private iEvent() : base(Event.TypeSize()) { }

        public iEvent(UIntPtr size, byte wel, out int perr) : this() {
            if((perr = Event.Make(Raw, wel)) != 0) return;
            perr = Event.Resize(Raw, size);
        }

        ~iEvent() => Event.Kill(Raw);
        public int Resize(UIntPtr size) => Event.Resize(Raw, size);
        public int Sort(fpElCmp_t FunctionCompare) => Event.Sort(Raw, FunctionCompare);
        public int GetRange(fpCond_t Condition, out UIntPtr Min, out UIntPtr Max) => Event.GetRange(Raw, Condition, out Min, out Max);
        
        public byte[] GetElement(UIntPtr i, out int perr)
        {
            byte[] a = new byte[Raw[0]];
            perr = Event.GetElement(Raw, i, a);
            return a;
        }

        public int SetElement(UIntPtr i, byte[] element) => Event.SetElement(Raw, i, element);
    }
}