using GED.Core.SanityCheck;

namespace GED.Core
{
    public class CamRectCLMgr : Mgr<CamRectCLMgr.Prm, CamRectCLMgr.El, CamRectCL.El>
    {
        #region Constructors

        public CamRectCLMgr()
        {
            CLPort.Init();
        }

        #endregion

        #region Destructors

        ~CamRectCLMgr()
        {
            CLPort.Del();
        }

        #endregion

        #region Overrides

        protected override int ItoS(in Prm _in, out El? _el)
        {
            int err; 
            _el = new El(out err, _in.source, _in.prm);

            return err;
        }

        protected override int StoO(in El _el, out CamRectCL.El? _out)
        {
            _out = _el;
            return States.OK;
        }

        #endregion

        #region Sub Class

        public class Prm // 이 클래스는 정직해서 맘에 드네.
        {
            #region Member Fields

            public BmpSourceRef source;
            public CamRectPrm prm;

            #endregion

            #region Constructors
            
            public Prm(BmpSourceRef source, CamRectPrm prm)
            {
                this.source = source;
                this.prm = prm;
            }

            #endregion
        }

        public class El : CamRectCL.El
        {
            #region Constructors

            unsafe internal El(out int state, in BmpSourceRef source, in CamRectPrm prm) : base(out state)
            {
                if (state == States.OK || (state & States.DONE_HOWEV) == States.DONE_HOWEV)
                {
                    fixed (CamRectPrm* _prm = &prm)
                    {
                        fCamRectCLEl.Init(memory.bytes, source.memory.bytes, _prm);
                    }
                }
            }

            #endregion

            #region Destructors

            unsafe ~El()
            {
                fCamRectCLEl.Del(memory.bytes);
            }

            #endregion
        }

        #endregion
    }
}