using System;
using System.Runtime.InteropServices;

namespace NetX.Interop.Structs
{
    // struct xcb_screen_t
    [StructLayout(LayoutKind.Sequential)]
    public class XcbScreen
    {
        public uint root;
        public uint default_colormap;
        public uint white_pixel;
        public uint black_pixel;
        public uint current_input_masks;
        public ushort width_in_pixels;
        public ushort height_in_pixels;
        public ushort width_in_millimeters;
        public ushort height_in_millimeters;
        public ushort min_installed_maps;
        public ushort max_installed_maps;
        public uint root_visual;
        public byte backing_stores;
        public byte save_unders;
        public byte root_depth;
        public byte allowed_depths_len;
    }

    // struct xcb_screen_iterator_t
    [StructLayout(LayoutKind.Sequential)]
    public class XcbScreenIterator
    {
        // Pointer to XcbScreen
        [MarshalAs(UnmanagedType.LPStruct)]
        public XcbScreen data;
        public int rem;
        public int index;
    }

    // struct xcb_void_cookie_t
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct XcbVoidCookie
    {
        uint sequence;
    }
}