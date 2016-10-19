using System;
using NetX.Interop;

namespace NetX.XWindows.Graphics
{
    public class CairoSurface : IDisposable
    {
        private IntPtr cairoSurface;
        internal XApplication application;
        private bool disposed = false; // To detect redundant calls

        public CairoSurface(XApplication application)
        {
            this.application = application;
            CreateCairoSurface();
            
            this.application.ApplicationTerminated += (obj,arguments) => {
                if(!disposed)
                {
                    Dispose(true);
                }
            };
        }

        public void Flush()
        {
            LibCairo.cairo_surface_flush(cairoSurface);
        }

        private void CreateCairoSurface()
        {
            try
            {
                cairoSurface = LibCairo.cairo_xcb_surface_create (  application.Connection,
                                                                    application.MainWindow.windowHandle,
                                                                    application.VisualType,
                                                                    application.MainWindow.Width,
                                                                    application.MainWindow.Height);
            }
            catch
            {
                application.Dispose();
                throw new Exception("CairoSurface.CairoSurface: Could not create XcbSurface");
            }

            application.MainWindow.WindowExposed += (o,args) => {
                SetSize( args.Width + args.X, args.Height + args.Y );
            };
        }

        public void SetSize(int width, int height)
        {
            LibCairo.cairo_xcb_surface_set_size(cairoSurface,width,height);
        }

        internal IntPtr Surface => cairoSurface;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    LibCairo.cairo_surface_destroy(cairoSurface);
                    var count = LibCairo.cairo_surface_get_reference_count(cairoSurface);

                    if(count == 0)
                        LibCairo.cairo_surface_finish(cairoSurface);
                }
                disposed = true;
            }
        }

        ~CairoSurface() 
        {
           Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}