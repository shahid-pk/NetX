using System;
using System.Runtime.InteropServices;

namespace NetX.Interop.Structs
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal class XcbSetup 
    {
        internal byte status;
        internal byte pad0;
        internal ushort protocol_major_version;
        internal ushort protocol_minor_version;
        internal ushort length;
        internal uint release_number;         
        internal uint resource_id_base;
        internal uint resource_id_mask;
        internal uint motion_buffer_size;
        internal ushort vendor_len;
        internal ushort maximum_request_length;
        internal byte roots_len; 
        internal byte pixmap_formats_len;
        internal byte image_byte_order;
        internal byte bitmap_format_bit_order;
        internal byte bitmap_format_scanline_unit;
        internal byte bitmap_format_scanline_pad;
        internal byte min_keycode;
        internal byte max_keycode;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst=4)]
        internal byte[] pad1;
    }
}