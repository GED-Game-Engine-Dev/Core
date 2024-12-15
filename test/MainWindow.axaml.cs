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
    public partial class MinCtrlWindow : MinCtrlWin
    {
        public CamRectCL camera;
        public BmpMgr mgr = new BmpMgr();
        public CamRectCLMgr clmgr = new CamRectCLMgr();
        CamRectCL.El element;

        BmpSourceRef source;
        CamRectPrm prm;

        public MinCtrlWindow(out int err) : base(out err, 1920, 1080)
        {
            InitializeComponent();

            Buffer = this.FindControl<Image>("MyImage");
            var TextBuff = this.FindControl<TextBlock>("MyText");

            camera = new CamRectCL(out err);
            
            camera.Resize(2);
            mgr.EmplaceBack(Resource1.Bitmap1);

            
            err = mgr.GetSource(0, out source);
            
            prm.Alpha = 255;
            prm.AddrDest.x = 500;
            prm.AddrDest.y = 500;
            prm.Axis.x = 0;
            prm.Axis.y = 0;
            prm.DataToIgnore = 1;
            prm.Resz.x = 1000;
            prm.Resz.y = 1000;
    
            prm.ReverseIdx = CamRectPrm.NoneReverse;
            prm.RotateXYClockWise = 0;

            clmgr.EmplaceBack(new CamRectCLMgr.Prm(source, prm));
            clmgr.GetSource(0, out element);

            camera.BuffAll(DisplayBuffer, 0xFF00FF);
#if true
            Task.Run(() => {
                int i = 0;
                int err;

                int max = 0;
                Stopwatch stopwatch = new Stopwatch();
                int mil = 0;
                while(true) {
                    element.CheckPrm(out err).RotateXYClockWise = ((float)i) / 100.0f;
                    camera.Write((uint)0, in element);
                    element.CheckPrm(out err).RotateXYClockWise = ((float)i) / 100.0f + 3;

                    stopwatch.Restart();
                    int fucked = camera.BuffAll(DisplayBuffer
                    , (uint)((0) | (mil << 16) | (i & 255))); // buffering
                    ;

                    Debug.WriteLine(fucked);
                    stopwatch.Stop();
                    mil = (int)stopwatch.ElapsedMilliseconds & 255;
                    if(max < mil) max = mil;
                    i++;

                    Dispatcher.UIThread.Invoke(() => {
                        Buffer.Source = null;
                        Buffer.Source = DisplayBuffer;

                        TextBuff.Text = $"FPS: {i} {1000 / stopwatch.ElapsedMilliseconds}, [Block: {1000 / max}]";
                    });
                }
            });
#endif
        }

        void InitializeComponent() {
            AvaloniaXamlLoader.Load(this);
        }
        
        public int a() {
            return 3;
        }
    }
}