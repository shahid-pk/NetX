using System;
using NetX.Interop;

namespace NetX.XWindows.Graphics
{
    public class CairoContext : IDisposable
    {
        private IntPtr cairoContext;
        private bool disposed = false;
        private CairoSurface cairoSurface;

        public CairoContext(CairoSurface cairoSurface)
        {
            try
            {
                cairoContext = LibCairo.cairo_create(cairoSurface.Surface);
            }
            catch
            {
                throw new Exception("CairoContext.CairoContext : could not create CairoContext");
            }
            
            this.cairoSurface = cairoSurface;
        }

        public void Save()
        {
            LibCairo.cairo_save(cairoContext);
        }

        public void Restore()
        {
            LibCairo.cairo_restore(cairoContext);
        }

        public void SetSource(IntPtr cairoPattern)
        {
            LibCairo.cairo_set_source(cairoContext,cairoPattern);
        }

        public void SetLineJoin(CairoLineJoin lineJoin)
        {
            LibCairo.cairo_set_line_join(cairoContext, lineJoin);
        }

        public void Translate(double tx, double ty)
        {
            LibCairo.cairo_translate(cairoContext,tx,ty);
        }

        public void SetSourceRGB(double red, double green, double blue)
        {
            LibCairo.cairo_set_source_rgb(cairoContext,(double)red/255,(double)green/255,(double)blue/255);
        }

        public void SetSourceRGBA(double red, double green, double blue, double alpha)
        {
            LibCairo.cairo_set_source_rgba(cairoContext,(double)red/255,(double)green/255,(double)blue/255,alpha);
        }

        public void SetLineWidth(double width)
        {
            LibCairo.cairo_set_line_width(cairoContext,width);
        }

        public void LineTo(double x, double y)
        {
            LibCairo.cairo_line_to(cairoContext,x,y);
        }

        public void MoveTo(double x, double y)
        {
            LibCairo.cairo_move_to(cairoContext,x,y);
        }

        public void CurveTo(double x1, double y1, double x2, double y2, double x3, double y3)
        {
            LibCairo.cairo_curve_to(cairoContext,x1,y1,x2,y2,x3,y3);
        }

        public void ClosePath()
        {
            LibCairo.cairo_close_path(cairoContext);
        }

        public void Rectangle(double x, double y, double width, double height)
        {
            LibCairo.cairo_rectangle(cairoContext,x,y,width,height);
        }

        public void Arc(double xc, double yc, double radius, double angle1, double angel2)
        {
            LibCairo.cairo_arc(cairoContext,xc,yc,radius,angle1,angel2);
        }

        public void Paint()
        {
            LibCairo.cairo_paint(cairoContext);
        }

        public void PaintWithAlpha(double alpha)
        {
            LibCairo.cairo_paint_with_alpha(cairoContext, alpha);
        }

        public void Stroke()
        {
            LibCairo.cairo_stroke(cairoContext);
        }

        public void Fill()
        {
            LibCairo.cairo_fill(cairoContext);
        }

        public double LineWidth
        {
            get {
                return LibCairo.cairo_get_line_width(cairoContext);  
            }

            set {
                LibCairo.cairo_set_line_width(cairoContext,value);
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    LibCairo.cairo_destroy(cairoContext);
                }
                cairoSurface.Dispose();
                disposed = true;
            }
        }

        ~CairoContext() 
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