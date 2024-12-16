using System.Runtime.InteropServices;
using GED.Core.SanityCheck;
using ae2f_float_t = @ae2f_float@;

namespace GED.Core {
    internal static partial class fCamRectPrm {
        [LibraryImport(DllNames.RCore, EntryPoint = "GED_Mov2PrmColGet")]
        public static partial byte ColPos(nint _this, nint pos);

        [LibraryImport(DllNames.RCore, EntryPoint = "GED_Mov2PrmColGetRect")]
        public static partial byte ColRect(nint _this, nint rect);

        [LibraryImport(DllNames.RCore, EntryPoint = "GED_Mov2PrmColGetPrm")]
        public static partial byte ColPrm(nint _this, nint prm);
    }

    public struct CamRectPrm {
        unsafe MovCol_t Col(in Dim2Sclr a) {
            MovCol_t r; r.raw = 0;

            fixed(Dim2Sclr* ptr = &a)
            fixed(CamRectPrm* _this = &this)
            r.raw = fCamRectPrm.ColPos(
                (nint)_this, (nint)ptr
            );
            
            return r;
        }
        
        unsafe MovCol_t Col(in Dim2SclrRect a) {
            MovCol_t r; r.raw = 0;

            fixed(Dim2SclrRect* rect = &a)
            fixed(CamRectPrm* _this = &this)
            r.raw = fCamRectPrm.ColRect(
                (nint)_this, (nint)rect
            );

            return r;
        }

        unsafe MovCol_t Col(in CamRectPrm a) {
            MovCol_t r; r.raw = 0;
            fixed(CamRectPrm* _this = &this)
            fixed(CamRectPrm* prm = &a)
            r.raw = fCamRectPrm.ColPrm(
                (nint)_this, (nint)prm
            );
            return r;
        }

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