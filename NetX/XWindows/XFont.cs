using System;
using NetX.Interop;
using NetX.XWindows.Internal;
using NetX.XWindows.Graphics;

namespace NetX.XWindows
{
    public sealed class XFont : IDisposable
    {
        uint fontHandle;

        XApplication application;

        uint gcHandle;

        private bool disposed = false;

        public XFont(XApplication application, string fontName)
        {
            this.application = application;
            fontHandle = LibXcb.xcb_generate_id(application.Connection);
            CreateFont(application,fontName);

            application.ApplicationTerminated += (obj,arguments) => {
                if(!disposed)
                {
                    Dispose(true);
                    GC.SuppressFinalize(this);
                }
            };
        }

        ~XFont()
        {
            Dispose(true);
        }

        private void CreateFont(XApplication application, string fontName)
        {
            try
            {
                LibXcb.xcb_open_font(application.Connection,fontHandle,(ushort)fontName.Length,fontName);
            }
            catch
            {
                throw new Exception("XFont() : Could not open font");
            }

            uint[] list = { application.Screen.BlackPixel, application.Screen.WhitePixel, fontHandle };
            var gc = new XGraphicsContext(  application,
                                            (uint)(GraphicsContext.GC_FOREGROUND | GraphicsContext.GC_BACKGROUND | 
                                                    GraphicsContext.GC_FONT),
                                            list);
            gcHandle = gc.gcHandle;
        }

        public void Write(string text)
        {
            try
            {
                LibXcb.xcb_image_text_8(application.Connection,(byte)text.Length,application.MainWindow.windowHandle,
                                        gcHandle,150,140,text);
            }
            catch
            {
                throw new Exception("XFont.Write() : Could not Write");
            }
        }

        void Dispose(bool disposing)
        {
            if(!disposed)
            {
                if (disposing)
                {
                    LibXcb.xcb_close_font(application.Connection,fontHandle);
                }

                disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}