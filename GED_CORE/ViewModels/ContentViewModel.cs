using GED.SanityCheck;

namespace GED.Core.ViewModels
{
    public class ContentViewModel
    {
        public ContentViewModel(out int err)
        {
            err = (int)FuckedNumbers.OK;
        }
    }
}
