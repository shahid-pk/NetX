using NetX.Interop;
using NetX.XWindows.Internal;

namespace NetX.XWindows.Graphics
{
    public class XGraphicsContext
    {   
        protected internal uint gcHandle;

        internal XApplication application;
        
        public XGraphicsContext(XApplication application)
        {
            this.application = application;
            CreateXGraphicsContext();
        }

        private void CreateXGraphicsContext()
        {
            var gchandle = LibXcb.xcb_generate_id(application.Connection);
            uint[] valueList = { application.Screen.BlackPixel, 0 };
            uint mask = (uint) (GraphicsContext.GC_FOREGROUND | GraphicsContext.GC_GRAPHICS_EXPOSURES);
            LibXcb.xcb_create_gc(application.Connection,gchandle,application.Screen.Root,mask,valueList);
        }

        public void Update(uint mask, uint[] valueList)
        {
            LibXcb.xcb_change_gc(application.Connection,gcHandle,mask,valueList);
        }
    }
}