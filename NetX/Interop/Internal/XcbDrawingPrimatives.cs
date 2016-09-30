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
}