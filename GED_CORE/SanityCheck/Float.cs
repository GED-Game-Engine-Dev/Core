using System.Runtime.InteropServices;

namespace GED.Core.SanityCheck {
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct Float {
        public float val;

        public static explicit operator Float(double v)
        {
            Float a; a.val = (float)v;
            return a;
        }

        public static explicit operator Float(float v)
        {
            Float a; a.val = (float)v;
            return a;
        }
    }

}
