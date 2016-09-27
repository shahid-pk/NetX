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

    public enum GraphicsContext 
    {
        GC_FUNCTION = 1,
        GC_PLANE_MASK = 2,
        GC_FOREGROUND = 4,
        GC_BACKGROUND = 8,
        GC_LINE_WIDTH = 16,
        GC_LINE_STYLE = 32,
        GC_CAP_STYLE = 64,
        GC_JOIN_STYLE = 128,
        GC_FILL_STYLE = 256,
        GC_FILL_RULE = 512,
        GC_TILE = 1024,
        GC_STIPPLE = 2048,
        GC_TILE_STIPPLE_ORIGIN_X = 4096,
        GC_TILE_STIPPLE_ORIGIN_Y = 8192,
        GC_FONT = 16384,
        GC_SUBWINDOW_MODE = 32768,
        GC_GRAPHICS_EXPOSURES = 65536,
        GC_CLIP_ORIGIN_X = 131072,
        GC_CLIP_ORIGIN_Y = 262144,
        GC_CLIP_MASK = 524288,
        GC_DASH_OFFSET = 1048576,
        GC_DASH_LIST = 2097152,
        GC_ARC_MODE = 4194304
    }   
}