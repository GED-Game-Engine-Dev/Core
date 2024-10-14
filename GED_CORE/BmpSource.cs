using System.Runtime.InteropServices;
using GED.Core.CXX;
using GED.SanityCheck;

namespace GED.Core {
    internal static partial class fBmpSource {
        [LibraryImport(DllNames.Bmp, EntryPoint = "ae2f_Bmp_cSrc_Read")]
        public static partial int Read(nint _this, nint bytes, nuint bytes_len);
    }

    internal struct IndexerField {
        public uint a, b, c, d;
    }

    internal struct BmpSourceField {
        public IndexerField a;
        public byte b;
        public nint c;
    }

    public class BmpSource {
        internal XClassMem<BmpSourceField> memory;

        unsafe internal BmpSource(out int err) {
            memory = new XClassMem<BmpSourceField>(out err);
        }

        unsafe internal BmpSource(out int err, byte[] raw) : this(out err)
        {
            fixed (byte* raw_ptr = raw)
            {
                err = fBmpSource.Read(memory.bytes, (nint)raw_ptr, (nuint)raw.Length);
            }
        }

        unsafe internal BmpSource(
            out int err, 
            uint Width,
            uint Height,
            BitmapElementSize elsize,
            nint Addr
        ) : this(out err) {
            ref BmpSourceField ins = ref memory.Instance();
            ins.c = Addr;
            ins.b = (byte)elsize;
            
            ref IndexerField idxer = ref ins.a;
            idxer.a = Width;
            idxer.b = idxer.a * Height;
            idxer.c = 0;
            idxer.d = idxer.a;
        }
    }
}