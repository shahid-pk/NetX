using System;
using NetX.Interop.Structs;
using System.Runtime.InteropServices;

namespace NetX.XWindows
{
    public class XScreen
    {
        private XcbScreen xcbScreen;
        internal XScreen(IntPtr xcbScreen)
        {
            this.xcbScreen = Marshal.PtrToStructure<XcbScreen>(xcbScreen);
        }

        public uint Root => xcbScreen.root;
        public uint DefaultColorMap => xcbScreen.default_colormap;
        public uint WhitePixel => xcbScreen.white_pixel;
        public uint BlackPixel => xcbScreen.black_pixel;
        public uint CurrentInputMasks => xcbScreen.current_input_masks;
        public ushort WidthInPixels => xcbScreen.width_in_pixels;
        public ushort HeightInPixels => xcbScreen.height_in_pixels;
        public ushort WidthInMillimeters => xcbScreen.width_in_millimeters;
        public ushort HeightInMillimeters => xcbScreen.height_in_millimeters;
        public ushort MinumumInstalledMaps => xcbScreen.min_installed_maps;
        public ushort MaximumInstalledMaps => xcbScreen.max_installed_maps;
        public uint RootVisual => xcbScreen.root_visual;
        public byte BackingStores => xcbScreen.backing_stores;
        public byte SaveUnders => xcbScreen.save_unders;
        public byte RootDepth => xcbScreen.root_depth;
        public byte AllowedDepthsLenght => xcbScreen.allowed_depths_len;
    }
}