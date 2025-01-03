// This file is auto-generated.

using Avalonia;
using Avalonia.Input;
using GED.Core.Ctrl;
using GED.Core.SanityCheck;

using float_t = @ae2f_float@;

namespace GED.Core.DisplayWizard {
    /// <summary>
    /// Supports tracking of mouse point. <br/>
    /// See <see cref="Main"/>
    /// </summary>
    public interface iWinPtr : iWin
    {
        /// <summary>
        /// Possible Main function defined. <br/>
        /// Initialises the interface. <br/><br/>
        /// 
        /// After this function, the window on <see cref="iWin"/>
        /// will track the mousepoint and store in global Mousepoint. <br/><br/>
        /// 
        /// <seealso cref="gMousePoint"/>
        /// </summary>
        /// <param name="_prm">Unused</param>
        /// <returns><seealso cref="States.OK"/></returns>
        public byte Main(object _prm) {
            win.PointerMoved += OnPointerMoved;
            return States.OK;
        }

        private unsafe void OnPointerMoved(object? sender, PointerEventArgs e)
        {
            Point pos = e.GetPosition(this.win);
            fMousePoint.X[0] = (float_t)pos.X;
            fMousePoint.Y[0] = (float_t)pos.Y;
        }
    }
}