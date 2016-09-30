using System;
using System.Linq;
using NetX.Interop;

namespace NetX.XWindows
{
    public class XApplication : IDisposable
    {
        private IntPtr xcbConnection;

        private bool screenIsNull = true;

        private int screeNumber;

        private XWindow window;

        public virtual XWindow MainWindow 
        { 
            get { return this.window; }
            set 
                { 
                    window = value;
                    if(!window.Initialized)
                        window.Initialize(this);
                        window.Initialized = true;
                 }
        }

        public XApplication()
        {
            // Pick screen number from Display environment variable
            xcbConnection = LibXcb.xcb_connect(null,IntPtr.Zero);
        }

        ~XApplication()
        {
            Dispose(false);
        }

        public XApplication(XWindow mainWindow)
        {
            this.MainWindow = mainWindow;
            // Pick screen number from Display environment variable
            xcbConnection = LibXcb.xcb_connect(null,IntPtr.Zero);
        }

        public XApplication(int screeNumber, XWindow mainWindow)
        {
            this.MainWindow = mainWindow;
            this.screeNumber = screeNumber;
            xcbConnection = LibXcb.xcb_connect(null,ref screeNumber);
            screenIsNull = false;
        }

        public XApplication(int screeNumber)
        {
            this.screeNumber = screeNumber;
            xcbConnection = LibXcb.xcb_connect(null,ref screeNumber);
            screenIsNull = false;
        }

        internal IntPtr Connection => xcbConnection;

        internal XScreen Screen => CreateXSetup().GetScreens().First();

        private IntPtr GetXcbSetupPtr() => LibXcb.xcb_get_setup(xcbConnection);

        private XSetup CreateXSetup () 
        {
            if(screenIsNull)
                return new XSetup(GetXcbSetupPtr(),0);
            else
                return new XSetup(GetXcbSetupPtr(),screeNumber); 
        }

        public virtual void Flush()
        {
            LibXcb.xcb_flush(xcbConnection);
        }

        public virtual void Run()
        {
            if(MainWindow == null)
                throw new Exception("No MainWindow associated with the Application");
            MainWindow.Show();
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
           
        } 

        protected virtual void Dispose(bool disposing)
        {
            // deallocate memmory accessed by xcb_connection_t structure
            LibXcb.xcb_disconnect(xcbConnection);
            if (disposing)
            { 
              if(xcbConnection != IntPtr.Zero)
                xcbConnection = IntPtr.Zero;      //if we reach here our IntPtr is refrencing invalid memmory  
            }
        }
    }
}