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
            camera.Resize(10);
            GED.Core.Manager.Bitmap.EmplaceBack(Resource1.Bitmap1);

            BmpSource source;
            err = GED.Core.Manager.Bitmap.GetSource(0, out source);

            // Stopwatch 객체 생성
            Stopwatch stopwatch = new Stopwatch();


            // Implemented as not Stretch, but trim
            Camera.Element[] element = {
                new(out err, 255, 3000, 3000, 0, 0, 1, in source),
                new(out err, 40, 40, 200, 49, 500, 1, in source),
                new(out err, 100, 90, 80, 500, 100, 1, in source),
            };

            for(int i = 0; i < 3; i++) {
                camera.Write((uint)i, in element[i]);
            }

            stopwatch.Start();

            try {
                camera.BuffAll(DisplayBuffer);
            } catch(Exception e) {
                Console.WriteLine(e);
            }

            // 시간 측정 종료
            stopwatch.Stop();

            // 경과 시간 출력
            Console.WriteLine("경과 시간: " + stopwatch.ElapsedMilliseconds + " ms");

        }

        void InitializeComponent() {
            AvaloniaXamlLoader.Load(this);
        }
    }
}