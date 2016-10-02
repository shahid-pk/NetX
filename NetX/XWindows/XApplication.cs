using System;
using System.Linq;
using System.Runtime.InteropServices;
using NetX.Interop;
using NetX.Interop.Internal;
using NetX.XWindows.Internal;

namespace NetX.XWindows
{
    public class XApplication : IDisposable
    {
        private IntPtr xcbConnection;

        private bool disposed = false;

        private bool screenIsNull = true;

        private int screeNumber;

        private XWindow window;

        private EventEngine eventEngine;

        protected internal uint quitToken;

        public delegate void ApplicationTerminatedEventHandler(Object obj,EventArgs args);

        public event ApplicationTerminatedEventHandler ApplicationTerminated;

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
            eventEngine = new EventEngine(this);
        }

        ~XApplication()
        {
            LibXcb.xcb_disconnect(xcbConnection);
            Dispose(false);
        }

        // This method should be overriden with care
        public virtual void OnApplicationTerminated()
        {
            if(ApplicationTerminated != null)
                ApplicationTerminated(this,EventArgs.Empty);

            // Dispose resources if not done already by the user    
            if(!disposed)
            {
                Dispose(true);
                GC.SuppressFinalize(this);
            }
        }

        public XApplication(XWindow mainWindow)
        {
            this.MainWindow = mainWindow;
            // Pick screen number from Display environment variable
            xcbConnection = LibXcb.xcb_connect(null,IntPtr.Zero);
            eventEngine = new EventEngine(this);
        }

        public XApplication(int screeNumber, XWindow mainWindow)
        {
            this.MainWindow = mainWindow;
            this.screeNumber = screeNumber;
            xcbConnection = LibXcb.xcb_connect(null,ref screeNumber);
            screenIsNull = false;
            eventEngine = new EventEngine(this);
        }

        public XApplication(int screeNumber)
        {
            this.screeNumber = screeNumber;
            xcbConnection = LibXcb.xcb_connect(null,ref screeNumber);
            screenIsNull = false;
            eventEngine = new EventEngine(this);
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
            CalculateQuitToken();
            LibXcb.xcb_map_window(this.Connection,MainWindow.windowHandle);
            Flush();
            eventEngine.Register(MainWindow);
            eventEngine.NonBlockingEventLoop();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
           
        } 

        protected void CalculateQuitToken()
        {
            // All this for getting window close event
            var cookie =  LibXcb.xcb_intern_atom(Connection, 1, 12,"WM_PROTOCOLS");
            var rep = LibXcb.xcb_intern_atom_reply(Connection, cookie, IntPtr.Zero);
            var cookie2 = LibXcb.xcb_intern_atom(Connection, 0, 16,"WM_DELETE_WINDOW");
            var rep2 = LibXcb.xcb_intern_atom_reply(Connection, cookie2, IntPtr.Zero);
            var reply = Marshal.PtrToStructure<XcbInternAtomReply>(rep);
            var reply2 = Marshal.PtrToStructure<XcbInternAtomReply>(rep2);
            // XCB_PROP_MODE_REPLACE
            LibXcb.xcb_change_property(Connection, 0, MainWindow.windowHandle, reply.atom, 4, 32, 1, ref reply2.atom);
            quitToken = reply2.atom;
        }

        protected virtual void Dispose(bool disposing)
        {
            // deallocate memmory accessed by xcb_connection_t structure
            if (disposing)
            { 
                LibXcb.xcb_disconnect(xcbConnection);
                if(xcbConnection != IntPtr.Zero)
                    xcbConnection = IntPtr.Zero;      // if we reach here our IntPtr is refrencing invalid memmory

                // Flip disposed to true so we don't try do de-allocate
                // already de-allocated memmory
                disposed = true;                        
            }
        }
    }
}