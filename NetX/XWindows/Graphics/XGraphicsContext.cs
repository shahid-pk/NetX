using System;
using System.Linq;
using NetX.Interop;

namespace NetX.XWindows.Graphics
{
    public class XGraphicsContext
    {
        public XGraphicsContext(IntPtr xcbConnection,uint graphicsContextHandle,
                                    uint xscreenRoot,uint graphicsContext,uint[] valueList)
        {
            LibXcb.xcb_create_gc(xcbConnection,graphicsContextHandle,xscreenRoot,graphicsContext,valueList);
        }
    }
}