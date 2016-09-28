using System;
using System.Runtime.InteropServices;
using NetX.Interop.Internal;

namespace NetX.Interop
{
    internal static class LibXcb 
    {
        private const string libxcb = "libxcb.so";

        //returns pointer to XcbConnection 
        [DllImport(libxcb, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern IntPtr xcb_connect([MarshalAs(UnmanagedType.LPStr)] 
                                                    string displayName, ref int screen);

        [DllImport(libxcb, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern IntPtr xcb_connect([MarshalAs(UnmanagedType.LPStr)] 
                                                    string displayName, IntPtr screen);


        [DllImport(libxcb, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern void xcb_disconnect(IntPtr xcbConnection);

        // returns Pointer to XcbSetup
        [DllImport(libxcb, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern IntPtr xcb_get_setup(IntPtr xcbConnection);

        // return XcbSetupIterator
        [DllImport(libxcb, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern XcbScreenIterator xcb_setup_roots_iterator(IntPtr xcbSetup);

        [DllImport(libxcb, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern XcbVoidCookie xcb_create_window (IntPtr xcbConnection,    
                                                     byte depth,             
                                                     uint wid,               
                                                     uint parent,           
                                                     short x,                 
                                                     short y,                 
                                                     ushort width,
                                                     ushort height,            
                                                     ushort border_width,
                                                     ushort _class,
                                                     uint visual,
                                                     uint value_mask,
                                                     uint[] value_list);
        
        [DllImport(libxcb, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern XcbVoidCookie xcb_create_gc (IntPtr xcbConnection,
                                                uint cid,
                                                uint drawable, 
                                                uint value_mask,
                                                uint[] value_list);


        [DllImport(libxcb, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern XcbVoidCookie xcb_map_window (IntPtr xcbConnection, uint window);

        [DllImport(libxcb, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern uint xcb_generate_id(IntPtr xcbConnection);

        [DllImport(libxcb, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern int xcb_flush (IntPtr xcbConnection);

        [DllImport(libxcb, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern void xcb_screen_next (ref XcbScreenIterator xcbScreenIterator);
    }
}