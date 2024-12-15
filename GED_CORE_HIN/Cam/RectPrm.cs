using GED.Core.SanityCheck;
using ae2f_float_t = @ae2f_float@;

namespace GED.Core {

    public struct CamRectPrm {
        public byte 
            Alpha, // Global Alpha Hack
            ReverseIdx; // Would you like to Reverse Idx

        public Dim2Sclr
            Resz, AddrDest;
        public uint
            DataToIgnore;

        public ae2f_float_t
            RotateXYClockWise;

        public Dim2Vec 
            Axis;

        public const byte XReverse = 0b1;
        public const byte YReverse = 0b10;
        public const byte NoneReverse = 0;

        public void Dump() {
                    Console.WriteLine($"addrdest: {this.AddrDest.x} {this.AddrDest.y}");
                    Console.WriteLine($"axis: {this.Axis.x} {this.Axis.y}");
                    Console.WriteLine($"resz: {this.Resz.x} {this.Resz.y}");
                    Console.WriteLine($"alpha: {this.Alpha}");
                    Console.WriteLine($"ignore: {this.DataToIgnore}");
                    Console.WriteLine($"rvse idx: {this.ReverseIdx}");
                    Console.WriteLine($"rotate: {this.RotateXYClockWise}");
                }
    };
}