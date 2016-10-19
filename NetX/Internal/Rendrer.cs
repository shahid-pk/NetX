using System;
using NetX.XWindows;
using NetX.XWindows.Graphics;
using NetX.Interop;
using NetX.Windows;

namespace NetX.Internal
{
    internal sealed class Renderer : IDisposable
    {
        private XApplication application;
        private bool disposed = false;
        private CairoSurface cairoSurface;
        private CairoContext cairoContext;

        // todo: remove this after adding CairoPattern class
        private IntPtr cairoPattern;

        internal  Renderer(XApplication application)
        {
            this.application = application;
            cairoSurface = new CairoSurface(this.application);
            cairoContext = new CairoContext(cairoSurface);

            this.application.ApplicationTerminated += (obj,arguments) => {
                if(!disposed)
                {
                    Dispose(true);
                }
            };
        }

        // todo-future : make at work for other controls
        internal void Render(Visual exposedVisual)
        {
            if(exposedVisual is Button)
                DrawButton();
        }

        private void DrawButton()
        {
            try
            {
                var x = application.MainWindow.Width/2;
                var y = application.MainWindow.Height/2;
                var Width = application.MainWindow.Width;
                var Height = application.MainWindow.Height;

                cairoContext.Rectangle(0,0,Width,Height);
                cairoContext.SetSourceRGBA(255,255,255,1);
                cairoContext.Fill();

                cairoContext.MoveTo(x,y);
                cairoContext.SetLineJoin(CairoLineJoin.CAIRO_LINE_JOIN_ROUND);
                cairoContext.Rectangle(x,y,60,20);

                cairoContext.SetSourceRGBA(192, 192, 192,1);
                cairoContext.SetLineWidth(4);
                
                cairoContext.Stroke();
                
                cairoContext.Rectangle(x,y,60,20);
                cairoContext.Fill();
            }
            catch
            {
                Console.WriteLine($"Renderer.reder : Could not Render {typeof(Button)}");
            }
        }

        void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    LibCairo.cairo_pattern_destroy(cairoPattern);
                }
                cairoContext.Dispose();

                disposed = true;
            }
        }

        ~Renderer() {
           Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}