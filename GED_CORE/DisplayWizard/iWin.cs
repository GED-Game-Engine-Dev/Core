using Avalonia.Controls;

namespace GED.Core.DisplayWizard {
    public interface iWin {
        /// <summary>
        /// Initiate the interface to alternate the constructor.
        /// </summary>
        /// <returns>
        /// State number from <see cref="SanityCheck.States"/>
        /// </returns>
        public byte Main(object prm);
        public Window win { get; protected set; }

        public abstract class __Win : iWin
        {
            protected Window __win;
            public Window win { get => __win; set => __win = value; }

            public abstract byte Main(object prm);
        }

        public class Built<T, P> : Window where T : __Win, new() {
            public readonly T _engine;
            public Built(out byte err, P prm) {
                _engine = new T();
                _engine.win = this;
                err = _engine.Main(prm);
            }
        }
    }
}