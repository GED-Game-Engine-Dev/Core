using System.Runtime.InteropServices;

using ae2f_float_t = @ae2f_float@;

namespace GED.Core.Ctrl
{
    internal static unsafe partial class fMousePoint {
        [LibraryImport(SanityCheck.DllNames.RCore, EntryPoint = "GED_CtrlMousePointptrX")]
        private static partial ae2f_float_t* pX();
        
        [LibraryImport(SanityCheck.DllNames.RCore, EntryPoint = "GED_CtrlMousePointptrY")]
        private static partial ae2f_float_t* pY();

        public static ae2f_float_t* X = pX();
        public static ae2f_float_t* Y = pY();
    }

    public static unsafe class MousePoint
    {
        public static ae2f_float_t X {
            get { return fMousePoint.X[0]; }
        }

        public static ae2f_float_t Y {
            get { return fMousePoint.Y[0]; }
        }
    }
}