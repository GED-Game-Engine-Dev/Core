using GED.Core.SanityCheck;

namespace GED.Core {
    public class CamRectCLMgr : Mgr<CamRectCLMgr.Prm, CamRectCL.El, CamRectCL.El> {
        protected override int ItoS(in Prm _in, out CamRectCL.El? _el)
        {
            int err; 
            _el = new CamRectCL.El(out err, _in.source, _in.prm);
            return err;
        }

        protected override int StoO(in CamRectCL.El _el, out CamRectCL.El? _out)
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