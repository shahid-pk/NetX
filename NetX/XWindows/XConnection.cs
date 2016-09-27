using System;
using NetX.Interop;

namespace NetX.XWindows
{
    public class XConnection : IDisposable
    {
        private IntPtr xcbConnection;
        private int screeNumber;

        // By default xwindow gets current screen number from Display environment variable
        private bool screenIsNull = true;
        public XConnection()
        {
            xcbConnection = LibXcb.xcb_connect(null,IntPtr.Zero);
        }

        public XConnection(string displayName)
        {
            xcbConnection = LibXcb.xcb_connect(displayName,IntPtr.Zero);
        }

        public XConnection(string displayName, int screenNumber)
        {
            this.screeNumber = screenNumber;
            xcbConnection = LibXcb.xcb_connect(displayName,ref screenNumber);
        }

        public XSetup CreateXSetup () 
        {
            if(screenIsNull)
                return new XSetup(GetXcbSetupPtr(),0);
            else
                return new XSetup(GetXcbSetupPtr(),screeNumber); 
        }

        private IntPtr GetXcbSetupPtr() => LibXcb.xcb_get_setup(xcbConnection);

        public void Dispose()
        {
            LibXcb.xcb_disconnect(xcbConnection);
        }
    }
}