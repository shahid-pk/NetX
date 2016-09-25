using System;
using System.Runtime.InteropServices;

namespace NetX.Interop.Structs
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public class XcbSetup 
    {
        public byte status;
        public byte pad0;
        public ushort protocol_major_version;
        public ushort protocol_minor_version;
        public ushort length;
        public uint release_number;         
        public uint resource_id_base;
        public uint resource_id_mask;
        public uint motion_buffer_size;
        public ushort vendor_len;
        public ushort maximum_request_length;
        public byte roots_len; 
        public byte pixmap_formats_len;
        public byte image_byte_order;
        public byte bitmap_format_bit_order;
        public byte bitmap_format_scanline_unit;
        public byte bitmap_format_scanline_pad;
        public byte min_keycode;
        public byte max_keycode;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst=4)]
        public byte[] pad1;
    }

    // struct xcb_setup_iterator_t
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct XcbSetupIterator
    {
        // Pointer to XcbSetup
        public IntPtr data;
        public int rem;
        public int index;
    }
}