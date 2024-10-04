using System.Runtime.InteropServices;

namespace GED.Core.Ctrl {
    public class iEventCircularQ<T> : xEvent<T> where T : struct {
        public iEventCircularQ(UIntPtr size, out int perr)
        : base(size, out perr) {}      
    }
}