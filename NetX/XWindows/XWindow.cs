using System;
using System.Runtime.InteropServices;
using NetX.Interop;
using NetX.Interop.Internal;
using NetX.XWindows.Internal;
using NetX.XWindows.Events;
using NetX.Windows;

namespace NetX.XWindows
{
    public class XWindow : Visual ,IDisposable
    {
        private XApplication application;

        private XScreen screen;

        private bool disposed = false;

        internal uint windowHandle;

        public virtual Visual Content {get; set;}

        protected internal bool Initialized {get;set;}

        // Public properties
        public short XPosition {get;set;} = 0;
        public short YPosition {get;set;} = 0;
        public int Width {get;set;} = 400;
        public int Height {get;set;} = 300;

        // Parent window
        public uint Parent {get;set;}
        public VisualClass VisualClass {get;set;} 
        public WindowClass WindowClass {get;set;}
        public ushort BorderWidth {get;set;} = 10;
        public uint Mask {get;set;}
        // Public properties end

        // Public handlers and events

        public delegate void WindowCreatedEventHandler(object sender, EventArgs e);

        public delegate void WindowInitializedEventHandler(object sender, EventArgs e);

        public delegate void WindowExposedEventHandler(object sender, ExposeEventArgs e);

        public delegate void WindowMaximizedEventHandler(object sender, EventArgs e);

        public event WindowCreatedEventHandler WindowCreated;
        public event WindowInitializedEventHandler WindowInitialized;
        public event WindowExposedEventHandler WindowExposed;
        public event WindowMaximizedEventHandler WindowMaximized;

        // Public handlers and events end

        public XWindow() 
        {
            Initialized = false;
        }

        ~XWindow()
        {
            Dispose(true);
        }

        public XWindow(XApplication application)
        {
            Initialize(application);
            OnWindowInitialized(EventArgs.Empty);
            Initialized = true;
            
            if(application.MainWindow == null)
            {
                application.MainWindow = this;
            }
        }

        public virtual void OnWindowCreated(EventArgs args)
        {
            if (WindowCreated != null)
            WindowCreated(this, args);
        }

        public virtual void OnWindowInitialized(EventArgs args)
        {
             if (WindowInitialized != null)
            WindowInitialized(this, args);
        }

        public virtual void Initialize(XApplication application)
        {
            this.application = application;
            this.screen = application.Screen;
            OnWindowInitialized(EventArgs.Empty);
            CreateWindow();
        }

        protected void CreateWindow()
        {
            windowHandle = LibXcb.xcb_generate_id(application.Connection);

            var vis = Marshal.PtrToStructure<XcbVisualType>(application.VisualType);

            uint mask = (uint) (XcbCW.XCB_CW_BACK_PIXEL | XcbCW.XCB_CW_EVENT_MASK ) ;
            uint[] valueList = { screen.WhitePixel, (uint)( XcbEventMask.EVENT_MASK_EXPOSURE       |
                                                            XcbEventMask.EVENT_MASK_PROPERTY_CHANGE|
                                                            XcbEventMask.EVENT_MASK_BUTTON_PRESS   |
                                                            XcbEventMask.EVENT_MASK_BUTTON_RELEASE | 
                                                            XcbEventMask.EVENT_MASK_POINTER_MOTION |
                                                            XcbEventMask.EVENT_MASK_ENTER_WINDOW   | 
                                                            XcbEventMask.EVENT_MASK_LEAVE_WINDOW   |
                                                            XcbEventMask.EVENT_MASK_KEY_PRESS      | 
                                                            XcbEventMask.EVENT_MASK_KEY_RELEASE ) };

            LibXcb.xcb_create_window (application.Connection,           /* Connection          */
                screen.RootDepth,                                       /* depth (same as root)*/
                windowHandle,                                           /* window Id           */
                screen.Root,                                            /* parent window       */
                XPosition, YPosition,                                   /* x, y                */
                (ushort)Width,(ushort)Height,                           /* width, height       */
                BorderWidth,                                            /* border_width        */
                (ushort)WindowClass.WINDOW_CLASS_INPUT_OUTPUT,          /* class               */
                vis.visual_id,                                          /* visual              */
                mask, valueList);                                       /* masks, not used yet */
            
            OnWindowCreated(EventArgs.Empty);
        }

        public virtual void OnWindowExposed(ExposeEventArgs args)
        {
            if (!(args == EventArgs.Empty))
            {
                Width = args.Width + args.X;
                Height = args.Height + args.Y; 
            }

            if (WindowExposed != null)
                WindowExposed(this, args);
        }

        public virtual void OnWindowMaximized(EventArgs args)
        {
            if (WindowMaximized != null)
                WindowMaximized(this,args);
        }
        
        protected internal void Update(uint mask, uint[] valueList)
        {
            LibXcb.xcb_change_window_attributes(application.Connection,windowHandle,mask, valueList);
        }

        public virtual void Show()
        {
            if(!Initialized)
                throw new Exception("XWindow.Show() : window not Initialized !");         
            this.application.Run();
        }   

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    LibXcb.xcb_destroy_window(application.Connection,windowHandle);
                }
                disposed = true;
            }
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}