using System.Runtime.InteropServices;
using GED.Core.SanityCheck;

namespace GED.Core {
    internal static partial class fBmpSource {
        [LibraryImport(DllNames.RCore, EntryPoint = "GED_BmpSrc_size")]
        private static partial nuint Size();

        [LibraryImport(DllNames.Bmp, EntryPoint = "ae2f_cBmpSrcRef")]
        public static partial int Read(nint _this, nint bytes, nuint bytes_len);

        [LibraryImport(DllNames.RCore, EntryPoint = "GED_BmpSrc_init")]
        public static partial int Init(
            nint _this,
            uint Width,
            uint Height,
            byte elsize,
            nint Addr
        );


        public static readonly nuint size;

        static fBmpSource() {
            size = Size();
        }
    }

    public class BmpSourceRef {
        internal XClassMemRef memory;
        internal BmpSourceRef(nint ptr = 0) {
            memory = new XClassMemRef();
            memory.bytes = ptr;
        }
    }

    internal class BmpSource : BmpSourceRef {

        unsafe internal BmpSource(out int err)
        => memory = new XClassMem(out err, fBmpSource.size);

        unsafe internal BmpSource(out int err, byte[] raw) : this(out err)
        {
            if(err != States.OK) return;
            fixed (byte* raw_ptr = raw)
            {
                err = fBmpSource.Read(memory.bytes, (nint)raw_ptr, (nuint)raw.Length);
            }
        }

        unsafe internal BmpSource(out int err, byte* raw, nuint len) : this(out err) {
            if(err != States.OK) return;
            err = fBmpSource.Read(memory.bytes, (nint)raw, len);
        }

        unsafe internal BmpSource(
            out int err,
            uint Width,
            uint Height,
            BmpElSize elsize,
            nint Addr
        ) : this(out err) {
            if(err != States.OK) return;
            err = fBmpSource.Init(memory.bytes, Width, Height, (byte)elsize, Addr);
        }
    }
}