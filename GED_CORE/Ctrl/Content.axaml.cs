using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using GED.SanityCheck;
using GED_CORE.ViewModels;

namespace GED.Core.Ctrl
{
    public partial class Content : ContentControl
    {
        public Content(out int err)
        {
            //DataContext = new ContentViewModel(out err);

            err = (int)FuckedNumbers.OK;

            try
            {
                PointerMoved += OnPointerMoved;
                AvaloniaXamlLoader.Load(this);
            }
            catch
            {
                err = (int)FuckedNumbers.ALLOC_FAILED;
                return;
            }
        }

        private unsafe void OnPointerMoved(object? sender, PointerEventArgs e)
        {
            var pos = e.GetPosition(this);
            fMousePoint.X[0] = pos.X;
            fMousePoint.Y[0] = pos.Y;
        }
    }
}