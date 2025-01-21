using Avalonia.Controls;
using GED.Core.SanityCheck;

namespace GED.Core.DisplayWizard
{
    /// <summary>
    /// An interface holding a <see cref="Window"/>.
    /// </summary>
    public interface iWin
    {
        #region Properties

        /// <summary>
        /// A placeholder for windows in order to somehow manage an interface.
        /// </summary>
        public Window win { get; protected set; }

        #endregion
    }
}