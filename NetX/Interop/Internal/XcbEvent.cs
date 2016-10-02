using System;
using System.Runtime.InteropServices;

namespace NetX.Interop.Internal
{  
    // xcb_generic_event_t
    [StructLayoutAttribute(LayoutKind.Sequential)]
    internal struct XcbGenericEvent
    {
        internal byte response_type;
        internal byte pad0;
        internal ushort sequence;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst=7)]
        internal ushort[] pad;
        internal ushort full_sequence;
    }

    // xcb_expose_event_t
    [StructLayoutAttribute(LayoutKind.Sequential)]
    internal struct XcbExposeEvent 
    {
        internal byte response_type;    /* The type of the event, here it is XCB_EXPOSE */
        internal byte pad0;
        internal ushort sequence;
        internal uint window;           /* The Id of the window that receives the event (in case */
                                        /* our application registered for events on several windows */
        internal ushort x;              /* The x coordinate of the top-left part of the window that needs to be redrawn */
        internal ushort y;              /* The y coordinate of the top-left part of the window that needs to be redrawn */
        internal ushort width;          /* The width of the part of the window that needs to be redrawn */
        internal ushort height;         /* The height of the part of the window that needs to be redrawn */
        internal ushort count;
    }

    // xcb_button_press_event_t and xcb_button_release_event_t 
    [StructLayoutAttribute(LayoutKind.Sequential)]
    internal struct XcbButtonPressReleaseEvent
    {
        internal byte response_type;         /* The type of the event, here it is xcb_button_press_event_t or xcb_button_release_event_t */
        internal byte detail;
        internal ushort sequence;
        internal uint time;                  /* Time, in milliseconds the event took place in */
        internal uint root;
        internal uint Event;
        internal uint child;
        internal short root_x;
        internal short root_y;
        internal short event_x;              /* The x coordinate where the mouse has been pressed in the window */
        internal short event_y;              /* The y coordinate where the mouse has been pressed in the window */
        internal ushort state;               /* A mask of the buttons (or keys) during the event */
        internal byte same_screen;
    }

    //xcb_motion_notify_event_t
    [StructLayoutAttribute(LayoutKind.Sequential)]
    internal struct XcbMouseMotionEvent
    {
        internal byte response_type;    /* The type of the event */
        internal byte detail;
        internal ushort sequence;
        internal uint time;             /* Time, in milliseconds the event took place in */
        internal uint root;
        internal uint Event;
        internal uint child;
        internal short root_x;
        internal short root_y;
        internal short event_x;         /* The x coordinate of the mouse when the  event was generated */
        internal short event_y;         /* The y coordinate of the mouse when the  event was generated */
        internal ushort state;          /* A mask of the buttons (or keys) during the event */
        internal byte same_screen;
    }

    // xcb_enter_notify_event_t and xcb_leave_notify_event_t
    [StructLayoutAttribute(LayoutKind.Sequential)]
    internal struct XcbMousePointerEnterLeaveEvent
    {
        internal byte response_type;        /* The type of the event */
        internal byte detail;
        internal ushort sequence;
        internal uint time;                 /* Time, in milliseconds the event took place in */
        internal uint root;
        internal uint Event;
        internal uint child;
        internal short root_x;
        internal short root_y;
        internal short event_x;             /* The x coordinate of the mouse when the  event was generated */
        internal short event_y;             /* The y coordinate of the mouse when the  event was generated */
        internal ushort state;              /* A mask of the buttons (or keys) during the event */
        internal byte mode;                 /* The number of mouse button that was clicked */
        internal byte same_screen_focus;
    }

    // xcb_key_press_event_t and xcb_key_release_event_t
    [StructLayoutAttribute(LayoutKind.Sequential)]
    internal struct XcbKeyPressReleaseEvent
    {
        internal byte response_type;    /* The type of the event */
        internal byte detail;
        internal ushort sequence;
        internal uint time;             /* Time, in milliseconds the event took place in */
        internal uint root;
        internal uint Event;
        internal uint child;
        internal short root_x;
        internal short root_y;
        internal short event_x;
        internal short event_y;
        internal ushort state;
        internal byte same_screen;
    }

     // xcb_client_message_event_t
    [StructLayoutAttribute(LayoutKind.Sequential)]
    internal struct XcbClientMessageEvent 
    {
        internal byte response_type;
        internal byte format;
        internal ushort sequence;
        internal uint window;
        internal uint type;
        internal XcbClientMessageDate data;
    }

    //xcb_client_message_data_t union
    [StructLayout(LayoutKind.Explicit)]
    internal struct XcbClientMessageDate 
    {
        [FieldOffset(0)]
        [MarshalAs(UnmanagedType.ByValArray, SizeConst=20)]
        internal byte[] data8;

        [FieldOffset(0)]
        [MarshalAs(UnmanagedType.ByValArray, SizeConst=10)]
        internal ushort[] data16;

        [FieldOffset(0)]
        [MarshalAs(UnmanagedType.ByValArray, SizeConst=5)]
        internal uint[] data32;
    }
}