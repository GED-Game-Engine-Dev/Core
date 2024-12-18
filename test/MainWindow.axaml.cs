using System;
using System.Diagnostics;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using Avalonia.Media.Imaging;
using Avalonia.Threading;
using GED.Core;
using GED.Core.DisplayWizard;
using GED.Core.SanityCheck;

namespace test
{
    public partial class MinCtrlWindow : Window, iWinBuff, iWinLoop, iWinPtr
    {
        public CamRectCL camera;
        public BmpMgr mgr = new BmpMgr();
        public CamRectCLMgr clmgr = new CamRectCLMgr();
        public Stopwatch stopwatch = new Stopwatch();

        BmpSourceRef source;
        CamRectPrm prm;
        TextBlock? TextBuff;

        Image Buffer;

        public MinCtrlWindow(out byte err) : base()
        {
            AvaloniaXamlLoader.Load(this);
            iWinBuff.MainPrm prm = new iWinBuff.MainPrm(1920, 1080);
            err = Main((object)prm);
            TextBuff = this.FindControl<TextBlock>("MyText");
            Buffer = this.FindControl<Image>("MyImage");

            KeyDown += OnKeyDown;
        }

        private unsafe void OnKeyDown(object? sender, KeyEventArgs e)
        {
            Dim2Sclr ___dir;
            prm.GetCentre(out ___dir);
            const int mov_sclr = 15;

            switch(e.Key) {
                case Key.Up: {
                    ___dir.y -= mov_sclr;
                    prm.SetCentre(in ___dir);
                } break;

                case Key.Down: {
                    ___dir.y += mov_sclr;
                    prm.SetCentre(in ___dir);
                } break;

                case Key.Right: {
                    ___dir.x += mov_sclr;
                    prm.SetCentre(in ___dir);
                } break;

                case Key.Left: {
                    ___dir.x -= mov_sclr;
                    prm.SetCentre(in ___dir);
                } break;
            }
        }

        public byte LoopBaseEnd()
        {
            return 0;
        }

        public byte LoopBaseStart()
        {
            CamRectCL.El element;
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

            camera.BuffAll(this.DisplayBuffer, 0xFF00FF);
            return 0;
        }

        int i = 0;
        int err = 0;

        int max = 0;
        int mil = 0;

        public WriteableBitmap DisplayBuffer { get; set; }
        Window iWin.win { get => this; set {  } }

        public bool LoopBaseUpdate(out byte _err)
        {
            CamRectCL.El element;
            // clmgr.Emplace(0, new CamRectCLMgr.Prm(source, prm));
            clmgr.GetSource(0, out element);
            element.CheckPrm(out err) = prm;
            camera.Write((uint)0, in element);
            
            stopwatch.Restart();
            int fucked = camera.BuffAll(DisplayBuffer
            , (uint)((0) | (mil << 16) | (i & 255))); // buffering
            ;
            stopwatch.Stop();
            mil = (int)stopwatch.ElapsedMilliseconds & 255;
            if(max < mil) max = mil;
            i++;

            Dispatcher.UIThread.Invoke(() => {
                Buffer.Source = null;
                Buffer.Source = DisplayBuffer;

                TextBuff.Text = $"FPS: {i} {1000 / stopwatch.ElapsedMilliseconds}, [Block: {1000 / max}]";
            });

            _err = 0;
            return true;
        }

        public byte Main(object prm)
        {
            byte a = States.IsActuallyOk(((iWinBuff)this).MainBuff(prm));
            if(a != States.OK) return a;
            a |= States.IsActuallyOk(((iWinLoop)this).MainLoop(prm));
            if(a != States.OK) return a;
            a |= States.IsActuallyOk(((iWinPtr)this).MainPtr(prm));
            return 0;
        }
    }
}