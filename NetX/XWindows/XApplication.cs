using System;
using System.Linq;
using System.Collections.Generic;
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
            Dispose(true);
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

        internal IntPtr VisualType => GetXcbVisualTypePtr().First();

        private IntPtr GetXcbSetupPtr() => LibXcb.xcb_get_setup(xcbConnection);

        private IEnumerable<IntPtr> GetXcbVisualTypePtr ()
        {
            var setup = GetXcbSetupPtr();
            var screenIter = LibXcb.xcb_setup_roots_iterator(setup);

            for(; screenIter.rem > 0; LibXcb.xcb_screen_next(ref screenIter))
            {     
                var depthIter = LibXcb.xcb_screen_allowed_depths_iterator(screenIter.data);
                for (; depthIter.rem > 0; LibXcb.xcb_screen_next(ref depthIter))
                {
                    var visualIter = LibXcb.xcb_depth_visuals_iterator(depthIter.data);

                    if(visualIter.rem == 0)
                    {
                        yield return visualIter.data;
                    }

                    for (; visualIter.rem > 0; LibXcb.xcb_screen_next(ref visualIter))
                    {
                        yield return visualIter.data;
                    }
                }
            }
        }

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
            eventEngine.BlockingEventLoop();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
           
        } 

        protected void CalculateQuitToken()
        {
            // All this for getting window close event
            // 1 is for true, 12 is lenth of "WM_PROTOCOLS"
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

        internal uint ReadWMState(string state)
        {
            // 1 is for true
            var cookie =  LibXcb.xcb_intern_atom(Connection, 1, (ushort) state.Length,state);
            var reply = LibXcb.xcb_intern_atom_reply(Connection, cookie, IntPtr.Zero);
            var rep =  Marshal.PtrToStructure<XcbInternAtomReply>(reply);
            return rep.atom;
        }

        internal uint GetValueForProperty(uint atom)
        {
            uint val = 7;
            var cookie = LibXcb.xcb_get_property(   Connection,
                                                    0,
                                                    MainWindow.windowHandle,
                                                    atom,
                                                    (uint)XcbAtom.XCB_ATOM_ATOM,
                                                    0,
                                                    32);

            var reply = LibXcb.xcb_get_property_reply(Connection,cookie,IntPtr.Zero);
            try
            {
                var ptr = LibXcb.xcb_get_property_value(reply);
                val = Marshal.PtrToStructure<uint>(ptr);
            }
            catch 
            {
                throw new Exception("XApplication:GetValueForProperty: Could not Marshal");
            }
            
            return val;
        }        

        protected virtual void Dispose(bool disposing)
        {
            if(!disposed)
            {
                // deallocate memmory accessed by xcb_connection_t structure
                if (disposing)
                { 
                    LibXcb.xcb_disconnect(xcbConnection);

                    // if we reach here our IntPtr is refrencing invalid memmory
                    if(xcbConnection != IntPtr.Zero)
                        xcbConnection = IntPtr.Zero;  
                }

                // Flip disposed to true so we don't try do de-allocate
                // already de-allocated memmory 
                disposed = true;
            }
        }
    }
}