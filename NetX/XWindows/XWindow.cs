using System;
using NetX.Interop;
using NetX.XWindows.Graphics;

namespace NetX.XWindows
{
    public class XWindow
    {
        private XApplication application;

        private XScreen screen;

        private uint windowHandle;

        private uint gcHandle;

        internal bool Initialized {get;set;}

        // Public properties
        public XGraphicsContext Context {get;set;}
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

        // end public handlers and events

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
            this.screen = this.application.Screen;
            CreateXGraphicsContext();
            CreateWindow();
            OnWindowCreated(EventArgs.Empty);
        }

        private void CreateXGraphicsContext()
        {
            gcHandle = LibXcb.xcb_generate_id(this.application.Connection);
            uint[] valueList = {screen.BlackPixel};
            Context = new XGraphicsContext(application.Connection,gcHandle,screen.Root,
                                            (uint)GraphicsContext.GC_FOREGROUND,valueList);
        }

        private void CreateWindow()
        {
            uint[] valueList = {screen.WhitePixel};
            windowHandle = LibXcb.xcb_generate_id(this.application.Connection);
            LibXcb.xcb_create_window (this.application.Connection,      /* Connection          */
                screen.RootDepth,                                       /* depth (same as root)*/
                windowHandle,                                           /* window Id           */
                screen.Root,                                            /* parent window       */
                0, 0,                                                   /* x, y                */
                Width, Height,                                          /* width, height       */
                BorderWidth,                                            /* border_width        */
                (ushort)WindowClass.WINDOW_CLASS_INPUT_OUTPUT,          /* class               */
                screen.RootVisual,                                      /* visual              */
                (uint)XcbCW.XCB_CW_BACK_PIXEL, valueList);              /* masks, not used yet */
        }
        
        public virtual void Show()
        {
            if(!Initialized)
                throw new Exception("XWindow.Show() : window not Initialized !");           
            LibXcb.xcb_map_window(application.Connection, windowHandle);
        }
    }

    // for xcb_visual_class_t
    public enum VisualClass : uint
    { 
        VISUAL_CLASS_STATIC_GRAY = 0, 
        VISUAL_CLASS_GRAY_SCALE = 1, 
        VISUAL_CLASS_STATIC_COLOR = 2, 
        VISUAL_CLASS_PSEUDO_COLOR = 3, 
        VISUAL_CLASS_TRUE_COLOR = 4, 
        VISUAL_CLASS_DIRECT_COLOR = 5 
    }

    //for xcb_window_class_t
    public enum WindowClass : ushort
    {
         WINDOW_CLASS_COPY_FROM_PARENT = 0, 
         WINDOW_CLASS_INPUT_OUTPUT = 1, 
         WINDOW_CLASS_INPUT_ONLY = 2 
    }

    // xcb_cw_t enumeration
    internal enum XcbCW : uint
    { 
        XCB_CW_BACK_PIXMAP = 1,
        XCB_CW_BACK_PIXEL = 2, 
        XCB_CW_BORDER_PIXMAP = 4, 
        XCB_CW_BORDER_PIXEL = 8, 
        XCB_CW_BIT_GRAVITY = 16, 
        XCB_CW_WIN_GRAVITY = 32, 
        XCB_CW_BACKING_STORE = 64, 
        XCB_CW_BACKING_PLANES = 128, 
        XCB_CW_BACKING_PIXEL = 256, 
        XCB_CW_OVERRIDE_REDIRECT = 512, 
        XCB_CW_SAVE_UNDER = 1024, 
        XCB_CW_EVENT_MASK = 2048, 
        XCB_CW_DONT_PROPAGATE = 4096, 
        XCB_CW_COLORMAP = 8192, 
        XCB_CW_CURSOR = 16384 
    }
}