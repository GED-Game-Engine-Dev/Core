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
            Camera.Element element = new(out err, 255, 400, 400, 1000, 500,  1, in source, 2);
            element.CheckPrm(out err).AxisX = 200;
            element.CheckPrm(out err).AxisY = 200;

            for(int i = 0; i < 3; i++) {
                element.CheckPrm(out err).RotateXYClockWise = (float)i * 2;
                camera.Write((uint)i, in element);
            }

            stopwatch.Start();
            camera.BuffAll(DisplayBuffer);
            stopwatch.Stop();
            Console.WriteLine("Time elasped: " + stopwatch.ElapsedMilliseconds + " ms");
        }

        void InitializeComponent() {
            AvaloniaXamlLoader.Load(this);
        }
    }
}