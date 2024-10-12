using Avalonia.Input;
using GED.SanityCheck;

namespace GED.Core.Ctrl {
    public abstract class Content : Avalonia.Controls.ContentControl {
        public Content(out int err)  {
            err = (int)FuckedNumbers.OK;

            try {
                Avalonia.Markup.Xaml.AvaloniaXamlLoader.Load(this);
                PointerMoved += OnPointerMoved;
            } catch {
                err = (int)FuckedNumbers.ALLOC_FAILED;
                return;
            }
        }

        unsafe private void OnPointerMoved(object sender, PointerEventArgs e) {
            var pos = e.GetPosition(this);
            fMousePoint.X[0] = pos.X;
            fMousePoint.Y[0] = pos.Y;
        }
    }
}