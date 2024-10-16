using System;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Media.Imaging;
using GED.Core;
using GED.Core.DisplayWizard;

namespace test
{
    public partial class MainWindow : MainWin
    {
        public Camera camera;

        public MainWindow(out int err) : base(out err, 48, 48)
        {
            InitializeComponent();
            Buffer = this.FindControl<Image>("MyImage");
            camera = new Camera(out err);
            camera.Resize(1);
            GED.Core.Manager.Bitmap.EmplaceBack(Resource1.Bitmap1);

            BmpSource source;
            err = GED.Core.Manager.Bitmap.GetSource(0, out source);
            Camera.Element element = new(out err, 255, 48, 48, 0, 0, 1, in source);


            camera.Write(0, in element);
            try {
                camera.BuffAll(DisplayBuffer, 0x0);
            } catch(Exception e) {
                Console.WriteLine(e);
            }
        }

        void InitializeComponent() {
            AvaloniaXamlLoader.Load(this);
        }
    }
}