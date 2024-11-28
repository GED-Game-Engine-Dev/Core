using GED.Core.SanityCheck;

namespace GED.Core {
    public abstract class Mgr<I, S, O> {
        internal List<S> list;
        public Mgr() {
            list = new List<S>();
        }

        public int Length {
            get { return list.Count; }
        }

        abstract protected int ItoS(in I _in, out S? _el);
        abstract protected int StoO(in S _el, out O? _out);

        public int Emplace(int idx, in I raw) {
            S s; int r = ItoS(in raw, out s);
            if(
                r != FuckedNumbers.OK && 
                (r & FuckedNumbers.DONE_HOWEV) == 0
            ) {
                return r;
            }

            list[idx] = s;
            return FuckedNumbers.OK;
        }
        public int EmplaceBack(in I raw) {
            S s; int r = ItoS(in raw, out s);

            if(
                r != FuckedNumbers.OK && 
                (r & FuckedNumbers.DONE_HOWEV) == 0
            ) {
                return r;
            }

            list.Add(s);
            return FuckedNumbers.OK;
        }
        public int GetSource(int index, out O? retval) {
            retval = default;
            if (index >= list.Count)
            return FuckedNumbers.WRONG_OPERATION;
            S s = list[index]; return StoO(in s, out retval);
        }
    }
}