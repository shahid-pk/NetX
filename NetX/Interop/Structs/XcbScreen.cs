using System;
using System.Runtime.InteropServices;

namespace NetX.Interop.Structs
{
    // struct xcb_screen_t
    [StructLayout(LayoutKind.Sequential)]
    internal class XcbScreen
    {
        internal uint root;
        internal uint default_colormap;
        internal uint white_pixel;
        internal uint black_pixel;
        internal uint current_input_masks;
        internal ushort width_in_pixels;
        internal ushort height_in_pixels;
        internal ushort width_in_millimeters;
        internal ushort height_in_millimeters;
        internal ushort min_installed_maps;
        internal ushort max_installed_maps;
        internal uint root_visual;
        internal byte backing_stores;
        internal byte save_unders;
        internal byte root_depth;
        internal byte allowed_depths_len;
    }

    // struct xcb_screen_iterator_t
    [StructLayout(LayoutKind.Sequential)]
    internal struct XcbScreenIterator
    {
        internal IntPtr data;
        internal int rem;
        internal int index;
    }

    // struct xcb_void_cookie_t
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct XcbVoidCookie
    {
        internal uint sequence;
    }
}