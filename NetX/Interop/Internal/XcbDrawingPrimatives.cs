using System;
using System.Runtime.InteropServices;

namespace NetX.Interop.Internal
{
    // xcb_point_t
    [StructLayout(LayoutKind.Sequential)]
    internal struct XcbPoint 
    {
        internal short x;
        internal short y;
    }

    // xcb_segment_t
    [StructLayout(LayoutKind.Sequential)]
    internal struct XcbSegment 
    {
        internal short x1;
        internal short y1;
        internal short x2;
        internal short y2;
    }

    // xcb_rectangle_t
    [StructLayout(LayoutKind.Sequential)]
    internal struct XcbRectangle 
    {
        internal short x;
        internal short y;
        internal short width;
        internal short height;
    }

    // xcb_arc_t
    // Note: the angles are expressed in units of 1/64 of a degree, so to have an angle of 90 degrees, 
    // starting at 0, angle1 = 0 and angle2 = 90 << 6. Positive angles indicate counterclockwise motion, 
    // while negative angles indicate clockwise motion.
    [StructLayout(LayoutKind.Sequential)]
    internal struct XcbArc 
    {
        internal short x;       /* Top left x coordinate of the rectangle surrounding the ellipse */
        internal short y;       /* Top left y coordinate of the rectangle surrounding the ellipse */
        internal short width;   /* Width of the rectangle surrounding the ellipse */
        internal short height;  /* Height of the rectangle surrounding the ellipse */
        internal short angle1;  /* Angle at which the arc begins */
        internal short angle2;  /* Angle at which the arc ends */
    }

    //xcb_visualtype_t
    [StructLayout(LayoutKind.Sequential)]
    internal class XcbVisualType
    {
        internal uint visual_id;
        internal byte _class;
        internal byte bits_per_rgb_value;
        internal ushort colormap_entries;
        internal uint red_mask;
        internal uint green_mask;
        internal uint blue_mask;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst=4)]
        internal byte[] pad0;
    }

    //xcb_renderdirect_format
    [StructLayout(LayoutKind.Sequential)]
    internal struct XcbRenderDirectFormat
    {
        internal ushort red_shift;
        internal ushort red_mask;
        internal ushort green_shift;
        internal ushort green_mask;
        internal ushort blue_shift;
        internal ushort blue_mask;
        internal ushort alpha_shift;
        internal ushort alpha_mask;
    }

    //xcb_pictforminfo_t
    [StructLayout(LayoutKind.Sequential)]
    internal struct XcbPictFormInfo
    {
        internal uint id;
        internal byte type;
        internal byte depth;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst=2)]
        internal byte[] pad0;

        internal XcbRenderDirectFormat direct;
        internal uint colormap;
    }
}