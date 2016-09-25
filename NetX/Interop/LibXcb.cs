using System;
using System.Runtime.InteropServices;
using NetX.Interop.Structs;

namespace NetX.Interop
{
    public  class LibXcb 
    {
        const string libxcb = "libxcb.so";

        //returns pointer to XcbConnection 
        [DllImport(libxcb, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern IntPtr xcb_connect([MarshalAs(UnmanagedType.LPStr)] 
                                                    string displayName, ref int screen);

        [DllImport(libxcb, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern void xcb_disconnect(IntPtr xcbConnection);

        // returns Pointer to XcbSetup
        [DllImport(libxcb, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern IntPtr xcb_get_setup(IntPtr xcbConnection);

        // return XcbSetupIterator
        [DllImport(libxcb, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern XcbSetupIterator xcb_setup_roots_iterator(IntPtr xcbSetup);

        [DllImport(libxcb, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern XcbVoidCookie xcb_create_window (IntPtr xcbConnection,    
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
                                                     ref uint value_list);
        
        [DllImport(libxcb, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern XcbVoidCookie xcb_create_gc (IntPtr xcbConnection,
                                                uint cid,
                                                uint drawable, 
                                                uint value_mask,
                                                ref uint value_list);


        [DllImport(libxcb, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern XcbVoidCookie xcb_map_window (IntPtr xcbConnection, uint window);

        [DllImport(libxcb, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern uint xcb_generate_id(IntPtr xcbConnection);

        [DllImport(libxcb, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int xcb_flush (IntPtr xcbConnection);
    }

    public class Xcb
    {
        // todo: should return XcbConnecton
        public void Connect(string displayName, int screen)
        {   
            try 
            {
                // todo : implement
            } 
            finally {}
        }
    }
    
}