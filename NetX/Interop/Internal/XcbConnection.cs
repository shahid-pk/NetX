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
}