using System;
using System.Runtime.InteropServices;

namespace NetX.Interop.Internal
{
    [StructLayout(LayoutKind.Sequential)]
    internal class XcbConnection 
    {
        internal int has_error;

        // Pointer to XcbSetup
        internal IntPtr setup;
        internal int fd;
        internal PthreadMutex iolock;
        internal XcbIn In;
        internal XcbOut Out;
        internal XcbExt ext;
        internal XcbXid xid;
    }

    [StructLayoutAttribute(LayoutKind.Sequential)]
    internal class ReplyList
    {
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void reply();

        // Unmamanged pointer to ReplyList
        internal IntPtr next;
    }


    [StructLayoutAttribute(LayoutKind.Sequential)]
    internal class EventList
    {
        // Pointer to XcbGenericEvent
        internal IntPtr Event;

        // Pointer to EventList
        internal IntPtr next;
    }

    [StructLayoutAttribute(LayoutKind.Sequential)]
    internal class ReaderList
    {
        internal long request;

        // Pointer to pthread_cond_t
        internal IntPtr data;

        // Pointer to ReaderList
        internal IntPtr next;
    }

    [StructLayoutAttribute(LayoutKind.Sequential)]
    internal class XcbSpecialEvent
    {
        // Pointer to XcbSpecialEvent
        internal IntPtr next;
        internal byte extension; 
        internal uint eid;

        // Pointer to uint 
        internal IntPtr stamp;

        // Pointer to EventList
        internal IntPtr events;

        // Pointer to Pointer of EvenList
        internal IntPtr events_tail;
        internal PthreadCond special_event_cond;
    }

    [StructLayoutAttribute(LayoutKind.Sequential)]
    internal class PendingReply
    {
        internal long first_request;
        internal long last_request;
        internal Workarounds workaround;
        internal int flags;

        // Pointer to PendingReply
        internal IntPtr next;
    }

    // struct pthread_mutex_t
    [StructLayoutAttribute(LayoutKind.Sequential)]
    internal class PthreadMutex 
    {
        internal int Lock;               
        internal int recursion;
        internal int kind;
        
        // pthread_t by value
        internal IntPtr owner;

        // handle_t by value
        internal IntPtr Event;
    }

    // struct pthread_cond_t
    [StructLayoutAttribute(LayoutKind.Sequential)]
    internal class PthreadCond
    {
        internal int waiting;

        // handle_t by value
        internal IntPtr semaphore;
    }

    // struct _xcb_map
    [StructLayoutAttribute(LayoutKind.Sequential)]
    internal class XcbMap
    {
        // Pointer to Node
        internal IntPtr head;

        // Pointer to pointer of Node
        internal IntPtr tail;
    }

    // struct node
    [StructLayoutAttribute(LayoutKind.Sequential)]
    internal class Node
    {
        // Pointer to Node
 	    internal IntPtr next;
        internal uint key;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void data();
    }

    // struct _xcb_in
    [StructLayoutAttribute(LayoutKind.Sequential)]
    internal class XcbIn
    {
        internal PthreadCond event_cond;
        internal int reading;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 4096)]
        internal string queue;
        internal int queue_len;
        internal long request_expected;
        internal long request_read;
        internal long request_completed;

        // Pointer to ReplyList
        internal IntPtr current_reply;
        
        // Pointer to pointer of ReplyList
        internal IntPtr current_reply_tail;

        // Pointer to XcbMap
        internal IntPtr replies;

        // Pointer to EventList
        internal IntPtr events;
        
        //Pointer to pointer of EventList
        internal IntPtr events_tail;
        
        // Pointer to ReaderList
        internal IntPtr readers;
        
        // Pointer to SpecialList
        internal IntPtr special_waiters;

        // Pointer to PendingReply
        internal IntPtr pending_replies;
        
        // Pointer to pointer of PendingReply
        internal IntPtr pending_replies_tail;
        
        // Pointer to ExbSpecialEvent
        internal IntPtr special_events;
    }

    // struct _xcb_out
    [StructLayout(LayoutKind.Sequential)]
    internal class XcbOut
    {
        internal PthreadCond cond;
        internal int writing;
        internal PthreadCond socket_cond;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void return_socket(IntPtr closure);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void socket_closure();
        internal int socket_moving;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16384)]
        internal string queue;
        internal int queue_len;
        internal long request;
        internal long request_written;
        internal PthreadMutex reqlenlock;
        internal LazyReplyTag maximum_request_length_tag;
        internal MaximumRequestLenght maximum_request_length;
    }
    
    // struct lazyreply
    [StructLayout(LayoutKind.Sequential)]
    internal class Lazyreply
    {
        internal LazyReplyTag tag;
        internal Value value;
    }

    // struct xcb_big_requests_enable_cookie_t
    [StructLayout(LayoutKind.Sequential)]
    internal class XcbBigRequestsEnableCookie
    {
        uint sequence;
    }

    // union maximum_request_length
    [StructLayout(LayoutKind.Explicit)]
    internal struct MaximumRequestLenght
    {
        [FieldOffset(0)]
        internal XcbBigRequestsEnableCookie cookie;

        [FieldOffset(32)]
        internal uint value;
    }

    // union value
    [StructLayout(LayoutKind.Explicit)]
    internal struct Value
    {
        [FieldOffset(0)]
        internal XcbQueryExtensionCookie cookie;

        // Pointer to XcbQueryExtensionReply
        [FieldOffset(32)]
        internal IntPtr reply; 
    }

    // struct xcb_query_extension_cookie_t
    [StructLayout(LayoutKind.Sequential)]
    internal class XcbQueryExtensionCookie
    {
        uint sequence;
    }

    // struct xcb_query_extension_reply_t
    [StructLayout(LayoutKind.Sequential)]
    internal class XcbQueryExtensionReply
    {
        internal byte response_type;
        internal byte pad0;
        internal ushort sequence;
        internal uint length;
        internal byte present;
        internal byte major_opcode;
        internal byte first_event;
        internal byte first_error;
    }

    // struct _xcb_ext
    [StructLayout(LayoutKind.Sequential)]
    internal class XcbExt
    {
        internal PthreadMutex Lock;

        // Pointer to LazyReply
        internal IntPtr extensions;
        internal int extensions_size;

    }

    // struct _xcb_xid
    [StructLayout(LayoutKind.Sequential)]
    internal class XcbXid
    {
        internal PthreadMutex Lock;
        internal uint last;
        internal uint Base;
        internal uint max;
        internal uint inc;
    }

    //xcb_intern_atom_reply_t
    [StructLayout(LayoutKind.Sequential)]
    internal class XcbInternAtomReply 
    {
        internal byte response_type;
        internal byte pad0;
        internal ushort sequence;
        internal uint length;
        internal uint atom;
    }

    // enum workarounds
    internal enum Workarounds 
    {
        WORKAROUND_NONE,
        WORKAROUND_GLX_GET_FB_CONFIGS_BUG,
        WORKAROUND_EXTERNAL_SOCKET_OWNER
    }

    // enum lazy_reply_tag
    internal enum LazyReplyTag
    {
        LAZY_NONE = 0,
        LAZY_COOKIE,
        LAZY_FORCED
    }

    // enum xcb_atom_enum_t
    internal enum XcbAtom
    {
        XCB_ATOM_NONE = 0, 
        XCB_ATOM_ANY = 0, 
        XCB_ATOM_PRIMARY = 1, 
        XCB_ATOM_SECONDARY = 2, 
        XCB_ATOM_ARC = 3, 
        XCB_ATOM_ATOM = 4, 
        XCB_ATOM_BITMAP = 5, 
        XCB_ATOM_CARDINAL = 6, 
        XCB_ATOM_COLORMAP = 7, 
        XCB_ATOM_CURSOR = 8, 
        XCB_ATOM_CUT_BUFFER0 = 9, 
        XCB_ATOM_CUT_BUFFER1 = 10, 
        XCB_ATOM_CUT_BUFFER2 = 11, 
        XCB_ATOM_CUT_BUFFER3 = 12, 
        XCB_ATOM_CUT_BUFFER4 = 13, 
        XCB_ATOM_CUT_BUFFER5 = 14, 
        XCB_ATOM_CUT_BUFFER6 = 15, 
        XCB_ATOM_CUT_BUFFER7 = 16, 
        XCB_ATOM_DRAWABLE = 17, 
        XCB_ATOM_FONT = 18, 
        XCB_ATOM_INTEGER = 19, 
        XCB_ATOM_PIXMAP = 20, 
        XCB_ATOM_POINT = 21, 
        XCB_ATOM_RECTANGLE = 22, 
        XCB_ATOM_RESOURCE_MANAGER = 23, 
        XCB_ATOM_RGB_COLOR_MAP = 24,
        XCB_ATOM_RGB_BEST_MAP = 25,
        XCB_ATOM_RGB_BLUE_MAP = 26, 
        XCB_ATOM_RGB_DEFAULT_MAP = 27, 
        XCB_ATOM_RGB_GRAY_MAP = 28, 
        XCB_ATOM_RGB_GREEN_MAP = 29, 
        XCB_ATOM_RGB_RED_MAP = 30, 
        XCB_ATOM_STRING = 31, 
        XCB_ATOM_VISUALID = 32, 
        XCB_ATOM_WINDOW = 33, 
        XCB_ATOM_WM_COMMAND = 34, 
        XCB_ATOM_WM_HINTS = 35, 
        XCB_ATOM_WM_CLIENT_MACHINE = 36, 
        XCB_ATOM_WM_ICON_NAME = 37, 
        XCB_ATOM_WM_ICON_SIZE = 38, 
        XCB_ATOM_WM_NAME = 39, 
        XCB_ATOM_WM_NORMAL_HINTS = 40, 
        XCB_ATOM_WM_SIZE_HINTS = 41, 
        XCB_ATOM_WM_ZOOM_HINTS = 42, 
        XCB_ATOM_MIN_SPACE = 43, 
        XCB_ATOM_NORM_SPACE = 44, 
        XCB_ATOM_MAX_SPACE = 45, 
        XCB_ATOM_END_SPACE = 46, 
        XCB_ATOM_SUPERSCRIPT_X = 47, 
        XCB_ATOM_SUPERSCRIPT_Y = 48, 
        XCB_ATOM_SUBSCRIPT_X = 49, 
        XCB_ATOM_SUBSCRIPT_Y = 50, 
        XCB_ATOM_UNDERLINE_POSITION = 51, 
        XCB_ATOM_UNDERLINE_THICKNESS = 52, 
        XCB_ATOM_STRIKEOUT_ASCENT = 53, 
        XCB_ATOM_STRIKEOUT_DESCENT = 54, 
        XCB_ATOM_ITALIC_ANGLE = 55, 
        XCB_ATOM_X_HEIGHT = 56, 
        XCB_ATOM_QUAD_WIDTH = 57, 
        XCB_ATOM_WEIGHT = 58, 
        XCB_ATOM_POINT_SIZE = 59, 
        XCB_ATOM_RESOLUTION = 60, 
        XCB_ATOM_COPYRIGHT = 61, 
        XCB_ATOM_NOTICE = 62, 
        XCB_ATOM_FONT_NAME = 63, 
        XCB_ATOM_FAMILY_NAME = 64, 
        XCB_ATOM_FULL_NAME = 65, 
        XCB_ATOM_CAP_HEIGHT = 66, 
        XCB_ATOM_WM_CLASS = 67, 
        XCB_ATOM_WM_TRANSIENT_FOR = 68 
    }
}