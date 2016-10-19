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
        internal static extern XcbIterator xcb_setup_roots_iterator(IntPtr xcbSetup);

        [DllImport(libxcb, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern void xcb_screen_next (ref XcbIterator xcbScreenIterator);

        [DllImport(libxcb, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern XcbIterator xcb_screen_allowed_depths_iterator(IntPtr xcbScreen);

        [DllImport(libxcb, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern XcbIterator xcb_depth_visuals_iterator(IntPtr xcbDepth);

        [DllImport(libxcb, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern uint xcb_generate_id(IntPtr xcbConnection);

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
        internal static extern XcbVoidCookie xcb_change_window_attributes(  IntPtr xcbConnection, 
                                                                            uint windowHandle, 
                                                                            uint value_mask, 
                                                                            uint[] value_list);

        [DllImport(libxcb, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern XcbVoidCookie xcb_map_window (IntPtr xcbConnection, uint window);


        [DllImport(libxcb, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern XcbVoidCookie xcb_get_property(  IntPtr xcbConnection, 
                                                                byte _delete, 
                                                                uint window, 
                                                                uint property, 
                                                                uint type, 
                                                                uint long_offset,
	                                                            uint long_length);
        
        [DllImport(libxcb, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)] 
        internal static extern IntPtr xcb_get_property_reply (  IntPtr xcbConnection, 
                                                                XcbVoidCookie cookie,                      
                                                                IntPtr e);

        [DllImport(libxcb, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern IntPtr xcb_get_property_value (IntPtr xcbGetPropertyReply);
                                                                
        [DllImport(libxcb, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern XcbVoidCookie xcb_destroy_window(IntPtr xcbConnection, uint windowHandle);

        [DllImport(libxcb, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern XcbVoidCookie xcb_create_pixmap( IntPtr xcbConnection, 
                                                                byte depth, 
                                                                uint pixmapHandle, 
                                                                uint drawable, 
                                                                ushort width, 
                                                                ushort height);
        
        [DllImport(libxcb, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern XcbVoidCookie xcb_create_colormap (  IntPtr xcbConnection,
                                                                    byte alloc,
                                                                    uint colormapId,
                                                                    uint windowId,
                                                                    uint visualId );
        
        [DllImport(libxcb, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern XcbVoidCookie xcb_free_colormap (IntPtr xcbConnection, uint colormapId );


        [DllImport(libxcb, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern XcbVoidCookie xcb_free_pixmap(IntPtr xcbConnection, uint pixmapHandle);

        [DllImport(libxcb, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern XcbVoidCookie xcb_alloc_color (  IntPtr xcbConnection,
                                                                uint colormapId,
                                                                ushort red,
                                                                ushort green,
                                                                ushort blue );
                                                                
        [DllImport(libxcb, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern IntPtr xcb_alloc_color_reply (   IntPtr xcbConnection, 
                                                                XcbVoidCookie cookie, 
                                                                IntPtr genericError );
        
        [DllImport(libxcb, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern XcbVoidCookie xcb_create_gc (IntPtr xcbConnection,
                                                            uint cid,
                                                            uint drawable, 
                                                            uint value_mask,
                                                            uint[] value_list);

        [DllImport(libxcb, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern XcbVoidCookie xcb_change_gc (IntPtr xcbConnection,
                                                            uint gcHandle,
                                                            uint value_mask,
                                                            uint[] value_list);
        
        [DllImport(libxcb, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern XcbVoidCookie xcb_free_gc(IntPtr xcbConnection, uint gcHandle);
        
        [DllImport(libxcb, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern XcbVoidCookie xcb_poly_point (   IntPtr xcbConnection,
                                                                byte cordinateMode,          
                                                                uint drawable,
                                                                uint gcHandle,
                                                                uint points_len,
                                                                XcbPoint[] points);

        [DllImport(libxcb, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern XcbVoidCookie xcb_fill_poly (IntPtr xcbConnection,        
                                                            uint drawable,
                                                            uint gcHandle,
                                                            uint shape,
                                                            byte cordinateMode,
                                                            uint points_len,
                                                            XcbPoint[] points);
        
        [DllImport(libxcb, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern XcbVoidCookie xcb_poly_segment ( IntPtr xcbConnection,
                                                                uint drawable,
                                                                uint gcHandle,             
                                                                uint segments_len,   
                                                                XcbSegment[] segments);

        [DllImport(libxcb, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern XcbVoidCookie  xcb_poly_rectangle (  IntPtr xcbConnection,
                                                                    uint drawable,
                                                                    uint gcHandle,
                                                                    uint rectangles_len,
                                                                    XcbRectangle[] rectangles);

        [DllImport(libxcb, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern XcbVoidCookie  xcb_poly_fill_rectangle ( IntPtr xcbConnection,
                                                                        uint drawable,
                                                                        uint gcHandle,
                                                                        uint rectangles_len,
                                                                        XcbRectangle[] rectangles);
        
        [DllImport(libxcb, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern XcbVoidCookie xcb_poly_arc ( IntPtr xcbConnection,
                                                            uint drawable,
                                                            uint gcHandle,
                                                            uint arcs_len,
                                                            XcbArc[] arcs); 

        [DllImport(libxcb, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern XcbVoidCookie xcb_poly_fill_arc (IntPtr xcbConnection,
                                                                uint drawable,
                                                                uint gcHandle,
                                                                uint arcs_len,
                                                                XcbArc[] arcs); 
        
        // Blocking
        [DllImport(libxcb, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern IntPtr xcb_wait_for_event (IntPtr xcbConnection);

        // Non-Blocking
        [DllImport(libxcb, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern IntPtr xcb_poll_for_event(IntPtr xcbConnection, out int error);

        [DllImport(libxcb, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern int xcb_flush (IntPtr xcbConnection);

        [DllImport(libxcb, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern XcbVoidCookie xcb_intern_atom(   IntPtr xcbConnection, 
                                                                byte only_if_exists, 
                                                                ushort name_len,
                                                                [MarshalAs(UnmanagedType.LPStr)] 
                                                                string name);

         [DllImport(libxcb, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
         internal static extern IntPtr xcb_intern_atom_reply (  IntPtr xcbConnection , 
                                                                XcbVoidCookie cookie , 
                                                                IntPtr e);

        [DllImport(libxcb, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern XcbVoidCookie xcb_change_property (IntPtr xcbConnection,
                                                                    byte mode,
                                                                    uint window,
                                                                    uint property,
                                                                    uint type,
                                                                    byte format,
                                                                    uint data_len,
                                                                    ref uint data);

        [DllImport(libxcb, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern XcbVoidCookie xcb_open_font (IntPtr xcbConnection, uint fid, ushort name_len,
                                                            [MarshalAs(UnmanagedType.LPStr)]
                                                            string name);
        
        [DllImport(libxcb, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern XcbVoidCookie xcb_close_font(IntPtr xcbConnection, uint fid);

        [DllImport(libxcb, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern XcbVoidCookie xcb_image_text_8 ( IntPtr xcbConnection,
                                                                byte string_len,
                                                                uint drawable,
                                                                uint gc,
                                                                short x,
                                                                short y,
                                                                [MarshalAs(UnmanagedType.LPStr)]
                                                                string write);

    }
}