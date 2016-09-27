using System;
using NetX.Interop;
using System.Collections.Generic;
using NetX.Interop.Structs;
using System.Runtime.InteropServices;

namespace NetX.XWindows 
{
    public class XSetup : IDisposable
    {
        private XcbSetup xcbSetup;
        private IntPtr xcbSetupPtr;
        private int screenNumber;

        internal XSetup(IntPtr xcbSetup,int screenNumber)
        {
            xcbSetupPtr = xcbSetup;
            this.screenNumber = screenNumber;
            this.xcbSetup = Marshal.PtrToStructure<XcbSetup>(xcbSetup);
        }

        public void Dispose()
        {
            xcbSetupPtr = IntPtr.Zero;
        }
        
        public uint Status => xcbSetup.status; 
        public byte Pad0 => xcbSetup.pad0;
        public ushort ProtocolMajorVersion => xcbSetup.protocol_major_version;
        public ushort ProtocolMinorVersion => xcbSetup.protocol_minor_version;
        public ushort Length => xcbSetup.length;
        public uint ReleaseNumber => xcbSetup.release_number;         
        public uint ResourceIdBase => xcbSetup.resource_id_base;
        public uint ResourceIdMask => xcbSetup.resource_id_mask;
        public uint MotionBufferSize => xcbSetup.motion_buffer_size;
        public ushort VendorLength => xcbSetup.vendor_len;
        public ushort MaximumRequestLength => xcbSetup.maximum_request_length;
        public byte RootsLength => xcbSetup.roots_len; 
        public byte PixmapFormatsLength => xcbSetup.pixmap_formats_len;
        public byte ImageByteOrder => xcbSetup.image_byte_order;
        public byte BitmapFormatBitOrder => xcbSetup.bitmap_format_bit_order;
        public byte BitmapFormatScanlineUnit => xcbSetup.bitmap_format_scanline_unit;
        public byte BitmapFormatScanlinePad => xcbSetup.bitmap_format_scanline_pad;
        public byte MinimumKeycode => xcbSetup.min_keycode;
        public byte MaximumKeyCode => xcbSetup.max_keycode;
        public byte[] Pad1 => xcbSetup.pad1;

        public IEnumerable<XScreen> GetScreens()
        {
            var iter = LibXcb.xcb_setup_roots_iterator(xcbSetupPtr);

            if(screenNumber == 0)
                yield return new XScreen(iter.data);

            for(; screenNumber > 0; screenNumber--)
            {     
                LibXcb.xcb_screen_next(ref iter);
                yield return new XScreen(iter.data);
            }
        }   
    }
}