using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Threading;
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

            Camera.Element element = new(out err, 255, 400, 400, 1000, 500,  1, in source, 2);
            element.CheckPrm(out err).AxisX = -200;
            element.CheckPrm(out err).AxisY = -200;

            camera.BuffAll(DisplayBuffer, 0x00FF00);

            Task.Run(() => {
                byte i = 0;
                int err;

                Stopwatch stopwatch = new Stopwatch();

                while(true) {
                    stopwatch.Restart();
                    element.CheckPrm(out err).RotateXYClockWise = i / 10.0;
                    element.CheckPrm(out err).WidthAsResized = (uint)i * 5 + 1;
                    element.CheckPrm(out err).ReverseIdx = (byte)(i % 3);
                    camera.Write((uint)0, in element);
                    i++;
                    int fucked = camera.BuffThreaded(DisplayBuffer, i, 50); // buffering
                    stopwatch.Stop();

                    if(stopwatch.ElapsedMilliseconds > 50)
                    Console.WriteLine($"{stopwatch.ElapsedMilliseconds} mil");

                    Dispatcher.UIThread.Invoke(() => {
                        Buffer.Source = null;
                        Buffer.Source = DisplayBuffer;
                    });
                }
            });
        }

        void InitializeComponent() {
            AvaloniaXamlLoader.Load(this);
        }
    }
}