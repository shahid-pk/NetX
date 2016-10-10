using System;

namespace NetX.XWindows.Events
{
    public class ExposeEventArgs : EventArgs
    {
        public ExposeEventArgs(int width, int height,int x,int y)
        {   
            Width = width;
            Height = height;
            X = x;
            Y = y;    
        }

        public int Width { get; private set;}
        public int Height {get; private set;}
        public int X {get;private set;}
        public int Y {get;private set;}
    }
}