
using GED.Core.SanityCheck;

namespace GED.Core
{
    
    public class BmpMgr : Mgr<byte[], byte[], BmpSource>
    {
        protected override int ItoS(in byte[] _in, out byte[]? _el)
        {
            byte[] hey = new byte[_in.Length];
            _el = null;

            if (hey == null) {
                return FuckedNumbers.PTR_IS_NULL;
            }

            try {
                Array.Copy(_in, hey, _in.Length);
            } catch
            {
                return FuckedNumbers.WRONG_OPERATION;
            }
            _el = hey;
            return FuckedNumbers.OK;
        }
        protected override int StoO(in byte[] _el, out BmpSource? _out)
        {
            _out = null; int err;
            _out = new BmpSource(out err, _el);
            return err;
        }
    }
}