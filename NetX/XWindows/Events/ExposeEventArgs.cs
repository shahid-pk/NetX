using System;

namespace NetX.XWindows.Events
{
    public class ExposeEventArgs : EventArgs
    {
        public ExposeEventArgs(int width, int height)
        {   
            Width = width;
            Height = height;    
        }

        public int Width { get; private set;}

        public int Height {get; private set;}
    }
}