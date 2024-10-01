using System.Runtime.InteropServices;

namespace GED.Core.Ctrl
{
    public unsafe static partial class fMousePoint {
        [LibraryImport(SanityCheck.DllNames.RCore, EntryPoint = "GED_Core_Ctrl_MousePoint_ptrX")]
        private static partial double* pX();

        [LibraryImport(SanityCheck.DllNames.RCore, EntryPoint = "GED_Core_Ctrl_MousePoint_ptrY")]
        private static partial double* pY();

        public static double* x;
        public static double* y = pY();

        static fMousePoint() { x = pX(); y = pY(); }
    }

    public unsafe struct MousePoint
    {
        public static double Y { get => fMousePoint.y[0]; set => fMousePoint.y[0] = value; }
        public static double X { get => fMousePoint.x[0]; set => fMousePoint.x[0] = value; }
    }
}