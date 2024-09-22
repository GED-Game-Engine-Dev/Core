using GED.Core.CXX;
using System.Runtime.InteropServices;

namespace GED.Core.Ctrl
{

    internal unsafe struct Event
    {
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
        public static extern int Element(byte[] mgr, UIntPtr i, out UIntPtr elsize, out byte* lpEl);
    }

    public unsafe class iEvent : iXClass
    {
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public unsafe delegate int fpElCmp_t(void* a, void* b);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public unsafe delegate bool fpCond_t(void* a);

        private iEvent() : base(Event.TypeSize()) { }

        public iEvent(UIntPtr size, out int perr) : this() {
            perr = Event.Resize(Raw, size);
        }

        ~iEvent() => Event.Kill(Raw);
        public int Resize(UIntPtr size) => Event.Resize(Raw, size);
        public int Sort(fpElCmp_t FunctionCompare) => Event.Sort(Raw, FunctionCompare);
        public int GetRange(fpCond_t Condition, out UIntPtr Min, out UIntPtr Max) => Event.GetRange(Raw, Condition, out Min, out Max);
        public unsafe struct rElement
        {
            public byte* Ptr;
            public UIntPtr Size;
        }
        
        public rElement Element(UIntPtr i, out int perr)
        {
            var el = new rElement();
            perr = Event.Element(Raw, i, out el.Size, out el.Ptr);
            return el;
        }
    }
}
