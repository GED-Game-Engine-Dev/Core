using System.Runtime.InteropServices;

namespace GED.Core.Ctrl
{
    public unsafe struct MousePoint
    {
        [DllImport("RCore.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "GED_Core_Ctrl_MousePoint_ptrX")]
        extern private static double* pX();

        [DllImport("RCore.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint="GED_Core_Ctrl_MousePoint_ptrY")]
        extern private static double* pY();

        private static double* x = pX();
        private static double* y = pY();

        public static double X { get => x[0]; set => x[0] = value; }
        public static double Y { get => y[0]; set => y[0] = value; }
    }
}