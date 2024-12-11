using GED.Core.SanityCheck;

namespace GED.Core
{
    
    public class BmpMgr : Mgr<byte[], BmpMgr.el, BmpSourceRef>
    {
        public class el {
            internal BmpSource src;
            internal XClassMem mem;

            internal el(BmpSource a, byte[] b, out int code) {
                src = a;
                mem = new XClassMem(out code, (nuint)b.Count());
                
            }
        }
        protected override int ItoS(in byte[] _in, out el? _el)
        {
            byte[] hey = new byte[_in.Length];
            _el = null;

            if (hey == null) {
                return FuckedNumbers.PTR_IS_NULL;
            }

            int err;
            _el = new el(new BmpSource(out err, _in), _in, out err);
            return err;
        }
        protected override int StoO(in el _el, out BmpSourceRef? _out)
        {
            _out = _el.src;
            return 0;
        }
    }
}