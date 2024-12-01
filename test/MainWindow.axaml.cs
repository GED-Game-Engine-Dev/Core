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
        public CamRectCL camera;
        public BmpMgr mgr = new BmpMgr();
        public static CamRectCLMgr clmgr = new CamRectCLMgr();

        public MainWindow(out int err) : base(out err, 800, 600)
        {
            InitializeComponent();

            Buffer = this.FindControl<Image>("MyImage");
            var TextBuff = this.FindControl<TextBlock>("MyText");

            camera = new CamRectCL(out err);
            
            camera.Resize(1);
            mgr.EmplaceBack(Resource1.Bitmap1);

            BmpSource source;
            err = mgr.GetSource(0, out source);
            CamRectPrm prm;
            prm.Alpha = 255;
            prm.AddrXForDest = 400;
            prm.AddrYForDest = 400;
            prm.AxisX = prm.AxisY = -200;
            prm.DataToIgnore = 1;
            prm.HeightAsResized = 500;
            prm.WidthAsResized = 500;
            prm.ReverseIdx = CamRectPrm.YReverse;
            prm.RotateXYClockWise.val = 0;

            clmgr.EmplaceBack(new CamRectCLMgr.Prm(source, prm));

            CamRectCL.El element;
            clmgr.GetSource(0, out element);
            
            camera.BuffAll(DisplayBuffer, 0xFF00FF);
            
#if true
            Task.Run(() => {
                byte i = 0;
                int err;

                int max = 0;

                Stopwatch stopwatch = new Stopwatch();
                int mil = 0;
                while(true) {
                    element.CheckPrm(out err).RotateXYClockWise.val = i / 10.0f;
                    element.CheckPrm(out err).ReverseIdx = (byte)(i % 3);
                    camera.Write((uint)0, in element);

                    stopwatch.Restart();
                    int fucked = camera.BuffAll(DisplayBuffer, (uint)((0) | (mil << 16) | (i & 255))); // buffering
                    stopwatch.Stop();
                    mil = (int)stopwatch.ElapsedMilliseconds & 255;
                    if(max < mil) max = mil;
                    i++;

                    Dispatcher.UIThread.Invoke(() => {
                        Buffer.Source = null;
                        Buffer.Source = DisplayBuffer;

                        TextBuff.Text = $"FPS: {1000 / stopwatch.ElapsedMilliseconds}, [Block: {1000 / max}]";
                    });
                }
            });

#endif
        }

        void InitializeComponent() {
            AvaloniaXamlLoader.Load(this);
        }
    }
}