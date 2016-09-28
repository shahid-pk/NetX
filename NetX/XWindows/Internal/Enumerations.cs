

namespace NetX.XWindows.Internal
{
    // for xcb_mod_mask_t
    internal enum ModMask
     { 
        MOD_MASK_SHIFT = 1, 
        MOD_MASK_LOCK = 2, 
        MOD_MASK_CONTROL = 4, 
        MOD_MASK_1 = 8, 
        MOD_MASK_2 = 16, 
        MOD_MASK_3 = 32, 
        MOD_MASK_4 = 64, 
        MOD_MASK_5 = 128, 
        MOD_MASK_ANY = 32768 
    }

    // for xcb_event_mask_t
    internal enum EventMask 
    { 
        EVENT_MASK_NO_EVENT = 0, 
        EVENT_MASK_KEY_PRESS = 1, 
        EVENT_MASK_KEY_RELEASE = 2, 
        EVENT_MASK_BUTTON_PRESS = 4, 
        EVENT_MASK_BUTTON_RELEASE = 8, 
        EVENT_MASK_ENTER_WINDOW = 16, 
        EVENT_MASK_LEAVE_WINDOW = 32, 
        EVENT_MASK_POINTER_MOTION = 64, 
        EVENT_MASK_POINTER_MOTION_HINT = 128, 
        EVENT_MASK_BUTTON_1_MOTION = 256, 
        EVENT_MASK_BUTTON_2_MOTION = 512, 
        EVENT_MASK_BUTTON_3_MOTION = 1024, 
        EVENT_MASK_BUTTON_4_MOTION = 2048, 
        EVENT_MASK_BUTTON_5_MOTION = 4096, 
        EVENT_MASK_BUTTON_MOTION = 8192, 
        EVENT_MASK_KEYMAP_STATE = 16384, 
        EVENT_MASK_EXPOSURE = 32768, 
        EVENT_MASK_VISIBILITY_CHANGE = 65536, 
        EVENT_MASK_STRUCTURE_NOTIFY = 131072, 
        EVENT_MASK_RESIZE_REDIRECT = 262144, 
        EVENT_MASK_SUBSTRUCTURE_NOTIFY = 524288, 
        EVENT_MASK_SUBSTRUCTURE_REDIRECT = 1048576, 
        EVENT_MASK_FOCUS_CHANGE = 2097152, 
        EVENT_MASK_PROPERTY_CHANGE = 4194304, 
        EVENT_MASK_COLOR_MAP_CHANGE = 8388608, 
        EVENT_MASK_OWNER_GRAB_BUTTON = 16777216 
    }

    internal enum GraphicsContext 
    {
        GC_FUNCTION = 1,
        GC_PLANE_MASK = 2,
        GC_FOREGROUND = 4,
        GC_BACKGROUND = 8,
        GC_LINE_WIDTH = 16,
        GC_LINE_STYLE = 32,
        GC_CAP_STYLE = 64,
        GC_JOIN_STYLE = 128,
        GC_FILL_STYLE = 256,
        GC_FILL_RULE = 512,
        GC_TILE = 1024,
        GC_STIPPLE = 2048,
        GC_TILE_STIPPLE_ORIGIN_X = 4096,
        GC_TILE_STIPPLE_ORIGIN_Y = 8192,
        GC_FONT = 16384,
        GC_SUBWINDOW_MODE = 32768,
        GC_GRAPHICS_EXPOSURES = 65536,
        GC_CLIP_ORIGIN_X = 131072,
        GC_CLIP_ORIGIN_Y = 262144,
        GC_CLIP_MASK = 524288,
        GC_DASH_OFFSET = 1048576,
        GC_DASH_LIST = 2097152,
        GC_ARC_MODE = 4194304
    }

     // for xcb_visual_class_t
    public enum VisualClass : uint
    { 
        VISUAL_CLASS_STATIC_GRAY = 0, 
        VISUAL_CLASS_GRAY_SCALE = 1, 
        VISUAL_CLASS_STATIC_COLOR = 2, 
        VISUAL_CLASS_PSEUDO_COLOR = 3, 
        VISUAL_CLASS_TRUE_COLOR = 4, 
        VISUAL_CLASS_DIRECT_COLOR = 5 
    }

    //for xcb_window_class_t
    public enum WindowClass : ushort
    {
         WINDOW_CLASS_COPY_FROM_PARENT = 0, 
         WINDOW_CLASS_INPUT_OUTPUT = 1, 
         WINDOW_CLASS_INPUT_ONLY = 2 
    }

    // xcb_cw_t enumeration
    internal enum XcbCW : uint
    { 
        XCB_CW_BACK_PIXMAP = 1,
        XCB_CW_BACK_PIXEL = 2, 
        XCB_CW_BORDER_PIXMAP = 4, 
        XCB_CW_BORDER_PIXEL = 8, 
        XCB_CW_BIT_GRAVITY = 16, 
        XCB_CW_WIN_GRAVITY = 32, 
        XCB_CW_BACKING_STORE = 64, 
        XCB_CW_BACKING_PLANES = 128, 
        XCB_CW_BACKING_PIXEL = 256, 
        XCB_CW_OVERRIDE_REDIRECT = 512, 
        XCB_CW_SAVE_UNDER = 1024, 
        XCB_CW_EVENT_MASK = 2048, 
        XCB_CW_DONT_PROPAGATE = 4096, 
        XCB_CW_COLORMAP = 8192, 
        XCB_CW_CURSOR = 16384 
    }

    // xcb_button_mask_t
    internal enum ButtonMask 
    { 
        BUTTON_MASK_1 = 256, 
        BUTTON_MASK_2 = 512, 
        BUTTON_MASK_3 = 1024, 
        BUTTON_MASK_4 = 2048, 
        BUTTON_MASK_5 = 4096, 
        BUTTON_MASK_ANY = 32768 
    }

    // xcb_visibility_t
    internal enum Visibility 
    { 
        VISIBILITY_UNOBSCURED = 0, 
        VISIBILITY_PARTIALLY_OBSCURED = 1, 
        VISIBILITY_FULLY_OBSCURED = 2 
    }
}