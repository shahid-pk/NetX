using System;
using NetX.Interop;
using NetX.XWindows.Internal;

namespace NetX.XWindows
{
    public class XWindow
    {
        private XApplication application;

        private XScreen screen;

        internal uint windowHandle;

        protected internal bool Initialized {get;set;}

        // Public properties
        public short XPosition {get;set;}
        public short YPosition {get;set;}
        public ushort Width {get;set;} = 400;
        public ushort Height {get;set;} = 300;

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

        public event WindowCreatedEventHandler WindowCreated;
        public event WindowInitializedEventHandler WindowInitialized;

        // Public handlers and events end

        public XWindow() 
        {
            Initialized = false;
        }

        public XWindow(XApplication application)
        {
            Initialize(application);
            OnWindowInitialized(EventArgs.Empty);
            Initialized = true;
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
            CreateWindow();
            OnWindowCreated(EventArgs.Empty);
        }

        protected void CreateWindow()
        {
            uint mask = (uint) (XcbCW.XCB_CW_BACK_PIXEL | XcbCW.XCB_CW_EVENT_MASK) ;
            uint[] valueList = { screen.WhitePixel, (uint)XcbEventMask.EVENT_MASK_EXPOSURE };
            windowHandle = LibXcb.xcb_generate_id(application.Connection);
            LibXcb.xcb_create_window (application.Connection,           /* Connection          */
                screen.RootDepth,                                       /* depth (same as root)*/
                windowHandle,                                           /* window Id           */
                screen.Root,                                            /* parent window       */
                0, 0,                                                   /* x, y                */
                Width, Height,                                          /* width, height       */
                BorderWidth,                                            /* border_width        */
                (ushort)WindowClass.WINDOW_CLASS_INPUT_OUTPUT,          /* class               */
                screen.RootVisual,                                      /* visual              */
                mask, valueList);                                       /* masks, not used yet */
        }
        
        protected internal void Update(uint mask, uint[] valueList)
        {
            LibXcb.xcb_change_window_attributes(application.Connection,windowHandle,mask, valueList);
        }

        public virtual void Show()
        {
            if(!Initialized)
                throw new Exception("XWindow.Show() : window not Initialized !");           
            LibXcb.xcb_map_window(application.Connection, windowHandle);
        }
    }
}