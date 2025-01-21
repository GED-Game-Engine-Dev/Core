using GED.Core.SanityCheck;

namespace GED.Core
{
    public class BmpMgr : Mgr<byte[], BmpMgr.el, BmpSourceRef>
    {
        #region Overrides

        protected override int ItoS(in byte[] _in, out el? _el)
        {
            byte[] hey = new byte[_in.Length];
            _el = null;

            if (hey == null)
            {
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

        #endregion

        #region Sub Class

        public unsafe class el
        {
            #region Member Fields

            internal BmpSource src;
            XClassMem mem;

            #endregion

            #region Constructors

            internal el(byte[] raw, out int code)
            {
                int err;
                mem = new XClassMem(out code, raw);
                src = new BmpSource(out err, (byte*)mem.bytes, (nuint)raw.Length);
            }

            #endregion
        }

        #endregion
    }
}