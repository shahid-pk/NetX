using System;
using NetX.Interop;
using NetX.XWindows.Internal;

namespace NetX.XWindows.Graphics
{
    public sealed class XGraphicsContext : IDisposable
    {   
        private bool disposed = false;

        internal uint gcHandle;

        internal XApplication application;
        
        public XGraphicsContext(XApplication application)
        {
            this.application = application;
            CreateXGraphicsContext();

            // free our gc resources if not already done by the user
            this.application.ApplicationTerminated += (obj,arguments) => {
                if(!disposed)
                {
                    Dispose(true);
                    GC.SuppressFinalize(this);
                }
            };
        }

        public XGraphicsContext(XApplication application, uint mask, uint[] valueList)
        {
            this.application = application;
            gcHandle = LibXcb.xcb_generate_id(application.Connection);
            
            try
            {
                LibXcb.xcb_create_gc(application.Connection,gcHandle,application.Screen.Root,mask,valueList);
            }
            catch
            {
                throw new Exception("XGraphicsContext() : Could not create Graphics context ");
            }

            // free our gc resources if not already done by the user
            this.application.ApplicationTerminated += (obj,arguments) => {
                if(!disposed)
                {
                    Dispose(true);
                    GC.SuppressFinalize(this);
                }
            };
        }

        ~XGraphicsContext()
        {
            Dispose(true);
        }

        private void CreateXGraphicsContext()
        {
            gcHandle = LibXcb.xcb_generate_id(application.Connection);
            uint[] valueList = { application.Screen.BlackPixel, 0 };
            uint mask = (uint) (GraphicsContext.GC_FOREGROUND | GraphicsContext.GC_GRAPHICS_EXPOSURES);

            try
            {
                LibXcb.xcb_create_gc(application.Connection,gcHandle,application.Screen.Root,mask,valueList);
            }
            catch
            {
                throw new Exception("XGraphicsContext() : Could not create Graphics context ");
            }
        }

        public void Update(uint mask, uint[] valueList)
        {
            try
            {
                LibXcb.xcb_change_gc(application.Connection,gcHandle,mask,valueList);
            }
            catch
            {
                throw new Exception("XGraphicsContext.Update() : Could not update Graphics context");
            }
        }

        void Dispose(bool disposing)
        {
            if(!disposed)
            {
                if (disposing)
                {
                    LibXcb.xcb_free_gc(application.Connection,gcHandle);
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