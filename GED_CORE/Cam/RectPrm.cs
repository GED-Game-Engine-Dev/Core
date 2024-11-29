using GED.Core.SanityCheck;

namespace GED.Core {
    public struct CamRectPrm {
        public byte 
            Alpha, // Global Fucking Alpha
            ReverseIdx; // Reverse Idx?
        public uint
            WidthAsResized, 	// want to resize?
            HeightAsResized, 	// want to resize?
            AddrXForDest, 		// where to copy
            AddrYForDest, 		// where to copy
            DataToIgnore;

        public Float
            RotateXYClockWise;

        public int 
            AxisX, AxisY;

        public const byte XReverse = 0b1;
        public const byte YReverse = 0b10;
        public const byte NoneReverse = 0;
    };
}