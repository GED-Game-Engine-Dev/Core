using GED.SanityCheck;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalonia.Input;
using GED.Core.Ctrl;

namespace GED_CORE.ViewModels
{
    public class ContentViewModel
    {
        public ContentViewModel(out int err)
        {
            err = (int)FuckedNumbers.OK;

            try
            {
                Test();
            }
            catch
            {
                err = (int)FuckedNumbers.ALLOC_FAILED;
                return;
            }
        }
        private unsafe void Test()
        {
            fMousePoint.X[0] = 10;
            fMousePoint.Y[0] = 20;
        }
    }
}
