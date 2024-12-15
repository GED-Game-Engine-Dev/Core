using GED.Core.SanityCheck;

namespace GED.Core
{
    public class BmpMgr : Mgr<byte[], BmpMgr.el, BmpSourceRef>
    {
        public unsafe class el {
            internal BmpSource src;
            XClassMem mem;

            internal el(byte[] b, out int code) {
                int a;
                mem = new XClassMem(out code, b);
                src = new BmpSource(out a, (byte*)mem.bytes, (nuint)b.Length);
            }
        }
        protected override int ItoS(in byte[] _in, out el? _el)
        {
            byte[] hey = new byte[_in.Length];
            _el = null;

            if (hey == null) {
                return States.PTR_IS_NULL;
            }

            int err;
            _el = new el(_in, out err);
            return err;
        }
        protected override int StoO(in el _el, out BmpSourceRef? _out)
        {
            _out = _el.src;
            return 0;
        }
    }
}