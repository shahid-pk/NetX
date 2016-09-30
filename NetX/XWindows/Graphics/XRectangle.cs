using System;
using NetX.Interop;
using NetX.Interop.Internal;

namespace NetX.XWindows.Graphics
{
    public sealed class XRectangle
    {
        private uint rectsToDraw = 1;
        private XcbRectangle[] rectangles;

        public short X {get;set;}
        public short Y {get;set;}
        public short Width {get;set;}
        public short Height {get;set;}

        public XRectangle(short x, short y , short width, short height) 
        {
            Width = width;
            Height = height;
            X = x;
            Y = y;
            rectangles = new XcbRectangle[] { new XcbRectangle() { x = X, y = Y, width = Width, height = Height } } ;
        
       }

        public XRectangle(short[][] rects)
        {
            if(rects.Length % 4 == 0)
                throw new Exception("XRectangle: One or many rectangles lack one of four properties");
            rectsToDraw = (uint) rects.Length/4;
            CreateXRectangles(rects);
        }

        private void CreateXRectangles(short[][] rectangles)
        {
            for(int i = 0 ; i < rectsToDraw; i++)
            {
                this.rectangles = new XcbRectangle[rectsToDraw];
                this.rectangles[i] = new XcbRectangle() {
                                            x = rectangles[i][0],
                                            y = rectangles[i][1], 
                                            width = rectangles[i][2],
                                            height = rectangles[i][3] 
                                        }; 
            }
        }
                

        public void Draw(XWindow window, XGraphicsContext graphicsContext)
        {
            LibXcb.xcb_poly_rectangle( graphicsContext.application.Connection, 
                                        window.windowHandle, 
                                        graphicsContext.gcHandle, 
                                        rectsToDraw,
                                        rectangles);
        }
    }
}