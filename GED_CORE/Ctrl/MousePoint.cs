using System.Runtime.InteropServices;

namespace GED.Core.Ctrl
{
    internal static unsafe partial class fMousePoint {
        [LibraryImport(SanityCheck.DllNames.RCore, EntryPoint = "GED_Ctrl_MousePoint_ptrX")]
        private static partial SanityCheck.Float* pX();
        
        [LibraryImport(SanityCheck.DllNames.RCore, EntryPoint = "GED_Ctrl_MousePoint_ptrY")]
        private static partial SanityCheck.Float* pY();

        public static SanityCheck.Float* X = pX();
        public static SanityCheck.Float* Y = pY();
    }

    public static unsafe class MousePoint
    {
        public static SanityCheck.Float X {
            get { return fMousePoint.X[0]; }
        }

        public static SanityCheck.Float Y {
            get { return fMousePoint.Y[0]; }
        }
    }
}