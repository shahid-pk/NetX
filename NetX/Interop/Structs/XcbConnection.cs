using System;
using System.Runtime.InteropServices;

namespace NetX.Interop.Structs
{
    [StructLayout(LayoutKind.Sequential)]
    public class XcbConnection 
    {
        public int has_error;

        // Pointer to XcbSetup
        public IntPtr setup;
        public int fd;
        public PthreadMutex iolock;
        public XcbIn In;
        public XcbOut Out;
        public XcbExt ext;
        public XcbXid xid;
    }

    [StructLayoutAttribute(LayoutKind.Sequential)]
    public class ReplyList
    {
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void reply();

        // Unmamanged pointer to ReplyList
        public IntPtr next;
    }

    [StructLayoutAttribute(LayoutKind.Sequential)]
    public class XcbGenericEvent
    {
        public byte response_type;
        public byte pad0;
        public ushort sequence;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst=7)]
        public ushort[] pad;
        public ushort full_sequence;
    }

    [StructLayoutAttribute(LayoutKind.Sequential)]
    public class EventList
    {
        // Pointer to XcbGenericEvent
        public IntPtr Event;

        // Pointer to EventList
        public IntPtr next;
    }

    [StructLayoutAttribute(LayoutKind.Sequential)]
    public class ReaderList
    {
        public long request;

        // Pointer to pthread_cond_t
        public IntPtr data;

        // Pointer to ReaderList
        public IntPtr next;
    }

    [StructLayoutAttribute(LayoutKind.Sequential)]
    public class XcbSpecialEvent
    {
        // Pointer to XcbSpecialEvent
        public IntPtr next;
        public byte extension; 
        public uint eid;

        // Pointer to uint 
        public IntPtr stamp;

        // Pointer to EventList
        public IntPtr events;

        // Pointer to Pointer of EvenList
        public IntPtr events_tail;
        public PthreadCond special_event_cond;
    }

    [StructLayoutAttribute(LayoutKind.Sequential)]
    public class PendingReply
    {
        public long first_request;
        public long last_request;
        public Workarounds workaround;
        public int flags;

        // Pointer to PendingReply
        public IntPtr next;
    }

    // struct pthread_mutex_t
    [StructLayoutAttribute(LayoutKind.Sequential)]
    public class PthreadMutex 
    {
        public int Lock;               
        public int recursion;
        public int kind;
        
        // pthread_t by value
        public IntPtr owner;

        // handle_t by value
        public IntPtr Event;
    }

    // struct pthread_cond_t
    [StructLayoutAttribute(LayoutKind.Sequential)]
    public class PthreadCond
    {
        public int waiting;

        // handle_t by value
        public IntPtr semaphore;
    }

    // struct _xcb_map
    [StructLayoutAttribute(LayoutKind.Sequential)]
    public class XcbMap
    {
        // Pointer to Node
        public IntPtr head;

        // Pointer to pointer of Node
        public IntPtr tail;
    }

    // struct node
    [StructLayoutAttribute(LayoutKind.Sequential)]
    public class Node
    {
        // Pointer to Node
 	    public IntPtr next;
        public uint key;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void data();
    }

    // struct _xcb_in
    [StructLayoutAttribute(LayoutKind.Sequential)]
    public class XcbIn
    {
        public PthreadCond event_cond;
        public int reading;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 4096)]
        public string queue;
        public int queue_len;
        public long request_expected;
        public long request_read;
        public long request_completed;

        // Pointer to ReplyList
        public IntPtr current_reply;
        
        // Pointer to pointer of ReplyList
        public IntPtr current_reply_tail;

        // Pointer to XcbMap
        public IntPtr replies;

        // Pointer to EventList
        public IntPtr events;
        
        //Pointer to pointer of EventList
        public IntPtr events_tail;
        
        // Pointer to ReaderList
        public IntPtr readers;
        
        // Pointer to SpecialList
        public IntPtr special_waiters;

        // Pointer to PendingReply
        public IntPtr pending_replies;
        
        // Pointer to pointer of PendingReply
        public IntPtr pending_replies_tail;
        
        // Pointer to ExbSpecialEvent
        public IntPtr special_events;
    }

    // struct _xcb_out
    [StructLayout(LayoutKind.Sequential)]
    public class XcbOut
    {
        public PthreadCond cond;
        public int writing;
        public PthreadCond socket_cond;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void return_socket(IntPtr closure);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void socket_closure();
        public int socket_moving;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16384)]
        public string queue;
        public int queue_len;
        public long request;
        public long request_written;
        public PthreadMutex reqlenlock;
        public LazyReplyTag maximum_request_length_tag;
        public MaximumRequestLenght maximum_request_length;
    }
    
    // struct lazyreply
    [StructLayout(LayoutKind.Sequential)]
    public class Lazyreply
    {
        public LazyReplyTag tag;
        public Value value;
    }

    // struct xcb_big_requests_enable_cookie_t
    [StructLayout(LayoutKind.Sequential)]
    public class XcbBigRequestsEnableCookie
    {
        uint sequence;
    }

    // union maximum_request_length
    [StructLayout(LayoutKind.Explicit)]
    public struct MaximumRequestLenght
    {
        [FieldOffset(0)]
        public XcbBigRequestsEnableCookie cookie;

        [FieldOffset(32)]
        public uint value;
    }

    // union value
    [StructLayout(LayoutKind.Explicit)]
    public struct Value
    {
        [FieldOffset(0)]
        public XcbQueryExtensionCookie cookie;

        // Pointer to XcbQueryExtensionReply
        [FieldOffset(32)]
        public IntPtr reply; 
    }

    // struct xcb_query_extension_cookie_t
    [StructLayout(LayoutKind.Sequential)]
    public class XcbQueryExtensionCookie
    {
        uint sequence;
    }

    // struct xcb_query_extension_reply_t
    [StructLayout(LayoutKind.Sequential)]
    public class XcbQueryExtensionReply
    {
        public byte response_type;
        public byte pad0;
        public ushort sequence;
        public uint length;
        public byte present;
        public byte major_opcode;
        public byte first_event;
        public byte first_error;
    }

    // struct _xcb_ext
    [StructLayout(LayoutKind.Sequential)]
    public class XcbExt
    {
        public PthreadMutex Lock;

        // Pointer to LazyReply
        public IntPtr extensions;
        public int extensions_size;

    }

    // struct _xcb_xid
    [StructLayout(LayoutKind.Sequential)]
    public class XcbXid
    {
        public PthreadMutex Lock;
        public uint last;
        public uint Base;
        public uint max;
        public uint inc;
    }

    // enum workarounds
    public enum Workarounds 
    {
        WORKAROUND_NONE,
        WORKAROUND_GLX_GET_FB_CONFIGS_BUG,
        WORKAROUND_EXTERNAL_SOCKET_OWNER
    }

    // enum lazy_reply_tag
    public enum LazyReplyTag
    {
        LAZY_NONE = 0,
        LAZY_COOKIE,
        LAZY_FORCED
    }
}