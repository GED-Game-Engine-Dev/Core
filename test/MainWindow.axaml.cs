using System;
using System.Diagnostics;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using GED.Core;
using GED.Core.DisplayWizard;

namespace test
{
    public partial class MainWindow : MainWin
    {
        public Camera camera;

        public MainWindow(out int err) : base(out err, 1920, 1080)
        {
            InitializeComponent();

            Buffer = this.FindControl<Image>("MyImage");
            camera = new Camera(out err);
            camera.Resize(100);
            GED.Core.Manager.Bitmap.EmplaceBack(Resource1.Bitmap1);

            BmpSource source;
            err = GED.Core.Manager.Bitmap.GetSource(0, out source);

            // Stopwatch 객체 생성
            Stopwatch stopwatch = new Stopwatch();


            // Implemented as not Stretch, but trim
            Camera.Element[] element = {
                new(out err, 255,   640, 1080, 0, 0, 1, in source, 2),
                new(out err, 255,   640, 1080, 640, 0, 1, in source, 3),
                new(out err, 255,   640, 1080, 1280, 0, 1, in source, 1),
            };

            for(int i = 0; i < 3; i++) {
                camera.Write((uint)i, in element[i]);
                Console.WriteLine($"{element[i].CheckParameter(out err).HeightAsResized}");
            }

            stopwatch.Start();
            camera.BuffAll(DisplayBuffer, 0x00);
            stopwatch.Stop();
            Console.WriteLine("Time elasped: " + stopwatch.ElapsedMilliseconds + " ms");
        }

        void InitializeComponent() {
            AvaloniaXamlLoader.Load(this);
        }
    }
}