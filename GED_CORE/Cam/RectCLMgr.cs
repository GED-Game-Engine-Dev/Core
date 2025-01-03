using GED.Core.SanityCheck;

namespace GED.Core {
    public class CamRectCLMgr : Mgr<CamRectCLMgr.Prm, CamRectCLMgr.El, CamRectCL.El> {
        public class El : CamRectCL.El {
            unsafe internal El(
                out int state,
                in BmpSourceRef source,
                in CamRectPrm prm
            ) : base(out state) {
                if(
                    state == States.OK || 
                    (state & States.DONE_HOWEV) == States.DONE_HOWEV
                ) {
                    fixed(CamRectPrm* _prm = &prm)
                    fCamRectCLEl.Init(memory.bytes, source.memory.bytes, _prm);
                }
            }

            unsafe ~El() {
                fCamRectCLEl.Del(memory.bytes);
            }
        }

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

        public class Prm {
            public BmpSourceRef source;
            public CamRectPrm prm;

            public Prm(BmpSourceRef source, CamRectPrm prm) {
                this.source = source;
                this.prm = prm;
            }
        }

        public CamRectCLMgr() => CLPort.Init();
        ~CamRectCLMgr() => CLPort.Del();
    }
}