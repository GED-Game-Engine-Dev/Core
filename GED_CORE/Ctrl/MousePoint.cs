using System.Runtime.InteropServices;

namespace GED.Core.Ctrl
{
    unsafe partial struct MousePoint
    {
        [LibraryImport("RCore.dll", EntryPoint = "GED_Core_Ctrl_MousePoint_ptrX")]
        private static partial double* pX();
        [LibraryImport("RCore.dll", EntryPoint = "GED_Core_Ctrl_MousePoint_ptrY")]
        private static partial double* pY();

        private static double* x = pX();
        private static double* y = pY();

        public static double X { get => x[0]; set => x[0] = value; }
        public static double Y { get => y[0]; set => y[0] = value; }
    }
}