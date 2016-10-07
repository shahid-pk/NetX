using System;
using System.Runtime.InteropServices;

namespace NetX.Interop.Internal
{
    // enum _cairo_surface_type
    internal enum CairoSurfaceType
    {
        CAIRO_SURFACE_TYPE_IMAGE,
        CAIRO_SURFACE_TYPE_PDF,
        CAIRO_SURFACE_TYPE_PS,
        CAIRO_SURFACE_TYPE_XLIB,
        CAIRO_SURFACE_TYPE_XCB,
        CAIRO_SURFACE_TYPE_GLITZ,
        CAIRO_SURFACE_TYPE_QUARTZ,
        CAIRO_SURFACE_TYPE_WIN32,
        CAIRO_SURFACE_TYPE_BEOS,
        CAIRO_SURFACE_TYPE_DIRECTFB,
        CAIRO_SURFACE_TYPE_SVG,
        CAIRO_SURFACE_TYPE_OS2
    }

    // for cairo_path_data_type_t
    internal enum CairoPathDataType
    {
        CAIRO_PATH_MOVE_TO,
        CAIRO_PATH_LINE_TO,
        CAIRO_PATH_CURVE_TO,
        CAIRO_PATH_CLOSE_PATH
    }

    // for cairo_intialias_t
    internal enum CairoAntialias
    {
        CAIRO_ANTIALIAS_DEFAULT,
        CAIRO_ANTIALIAS_NONE,
        CAIRO_ANTIALIAS_GRAY,
        CAIRO_ANTIALIAS_SUBPIXEL,
        CAIRO_ANTIALIAS_FAST,
        CAIRO_ANTIALIAS_GOOD,
        CAIRO_ANTIALIAS_BEST
    }

    // for cairo_fill_rule_t
    internal enum CairoFillRule
    {
        CAIRO_FILL_RULE_WINDING,
        CAIRO_FILL_RULE_EVEN_ODD
    }

    // for cairo_line_cap_t
    internal enum CairoLineCap
    {
        CAIRO_LINE_CAP_BUTT,
        CAIRO_LINE_CAP_ROUND,
        CAIRO_LINE_CAP_SQUARE
    }

    // cairo_extend_t
    internal enum CairoExtend
    {
        CAIRO_EXTEND_NONE,
        CAIRO_EXTEND_REPEAT,
        CAIRO_EXTEND_REFLECT,        	 
        CAIRO_EXTEND_PAD
    }

    // cairo_filter_t
    internal enum CairoFilter
    {
        CAIRO_FILTER_FAST,
        CAIRO_FILTER_GOOD,
        CAIRO_FILTER_BEST,
        CAIRO_FILTER_NEAREST,
        CAIRO_FILTER_BILINEAR,
        CAIRO_FILTER_GAUSSIAN,
    }

    // cairo_pattern_type_t
    internal enum CairoPatternType
    {
        CAIRO_PATTERN_TYPE_SOLID,
        CAIRO_PATTERN_TYPE_SURFACE,
        CAIRO_PATTERN_TYPE_LINEAR,
        CAIRO_PATTERN_TYPE_RADIAL,
        CAIRO_PATTERN_TYPE_MESH,
        CAIRO_PATTERN_TYPE_RASTER_SOURCE,
    }

    // cairo_matrix_t
    [StructLayout(LayoutKind.Sequential)]
    internal struct CairoMatrix
    {
        internal double xx;
        internal double yx;
        internal double xy; 
        internal double yy;
        internal double x0; 
        internal double y0;
    }

    // for cairo_status_t
    internal enum CairoStatus
    {
        CAIRO_STATUS_SUCCESS, 
        CAIRO_STATUS_NO_MEMORY,
        CAIRO_STATUS_INVALID_RESTORE,
        CAIRO_STATUS_INVALID_POP_GROUP,
        CAIRO_STATUS_NO_CURRENT_POINT,
        CAIRO_STATUS_INVALID_MATRIX,
        CAIRO_STATUS_INVALID_STATUS,
        CAIRO_STATUS_NULL_POINTER,
        CAIRO_STATUS_INVALID_STRING,
        CAIRO_STATUS_INVALID_PATH_DATA,
        CAIRO_STATUS_READ_ERROR,
        CAIRO_STATUS_WRITE_ERROR,
        CAIRO_STATUS_SURFACE_FINISHED,
        CAIRO_STATUS_SURFACE_TYPE_MISMATCH,
        CAIRO_STATUS_PATTERN_TYPE_MISMATCH,
        CAIRO_STATUS_INVALID_CONTENT,
        CAIRO_STATUS_INVALID_FORMAT,
        CAIRO_STATUS_INVALID_VISUAL,
        CAIRO_STATUS_FILE_NOT_FOUND,
        CAIRO_STATUS_INVALID_DASH,
        CAIRO_STATUS_INVALID_DSC_COMMENT,
        CAIRO_STATUS_INVALID_INDEX,
        CAIRO_STATUS_CLIP_NOT_REPRESENTABLE,
        CAIRO_STATUS_TEMP_FILE_ERROR,
        CAIRO_STATUS_INVALID_STRIDE,
        CAIRO_STATUS_FONT_TYPE_MISMATCH,
        CAIRO_STATUS_USER_FONT_IMMUTABLE,
        CAIRO_STATUS_USER_FONT_ERROR,
        CAIRO_STATUS_NEGATIVE_COUNT,
        CAIRO_STATUS_INVALID_CLUSTERS,
        CAIRO_STATUS_INVALID_SLANT,
        CAIRO_STATUS_INVALID_WEIGHT,
        CAIRO_STATUS_INVALID_SIZE,
        CAIRO_STATUS_USER_FONT_NOT_IMPLEMENTED,
        CAIRO_STATUS_DEVICE_TYPE_MISMATCH,
        CAIRO_STATUS_DEVICE_ERROR,
        CAIRO_STATUS_INVALID_MESH_CONSTRUCTION,
        CAIRO_STATUS_DEVICE_FINISHED,
        CAIRO_STATUS_JBIG2_GLOBAL_MISSING,
        CAIRO_STATUS_LAST_STATUS
    }

    // cairo_region_overlap_t
    internal enum CairoRegionOverlap
    {
        CAIRO_REGION_OVERLAP_IN,
        CAIRO_REGION_OVERLAP_OUT,
        CAIRO_REGION_OVERLAP_PART
    }

    // cairo_rectangle_t
    [StructLayout(LayoutKind.Sequential)]
    internal struct CairoRectangleInt
    {
        internal int x, y;
        internal int width, height;
    }
}