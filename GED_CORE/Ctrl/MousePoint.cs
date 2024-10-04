using System.Runtime.InteropServices;

namespace GED.Core.Ctrl
{
    public unsafe static partial class fMousePoint {
        [LibraryImport(SanityCheck.DllNames.RCore, EntryPoint = "GED_Core_Ctrl_MousePoint_ptrX")]
        private static partial double* pX();
        
        [LibraryImport(SanityCheck.DllNames.RCore, EntryPoint = "GED_Core_Ctrl_MousePoint_ptrY")]
        private static partial double* pY();

        public static double* X = pX();
        public static double* Y = pY();
    }

    public unsafe class MousePoint
    {
        public static double X {
            get { return fMousePoint.X[0]; }
            set { fMousePoint.X[0] = value; }
        }

        public static double Y {
            get { return fMousePoint.Y[0]; }
            set { fMousePoint.Y[0] = value; }
        }
    }
}