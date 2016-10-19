using System;
using System.Runtime.InteropServices;
using NetX.Interop.Internal;

namespace NetX.Interop
{
    internal static class LibCairo
    {
        const string libCairo = "libcairo.so.2";


        // return pointer to cairo_t aka cairo context struct
        [DllImport(libCairo)]
        internal static extern IntPtr cairo_create (IntPtr cairoSurface);

        // increases refrence count of cairo_t by 1
        [DllImport(libCairo)]
        internal static extern IntPtr cairo_reference (IntPtr cairoContext);

        // decreases refrence count of cairo_t by 1
        [DllImport(libCairo)]
        internal static extern void	cairo_destroy (IntPtr cairoContext);

        // checks if error has occured for this context
        [DllImport(libCairo)]
        internal static extern CairoStatus cairo_status (IntPtr cairoContext);

        // Makes a copy of the current state of cr and saves it on an internal stack of saved states for cr .
        // When cairo_restore() is called, cr will be restored to the saved state. Multiple calls to cairo_save()
        // and cairo_restore() can be nested; each call to cairo_restore() restores the state from the matching
        // paired cairo_save().
        [DllImport(libCairo)]
        internal static extern void	cairo_save (IntPtr cairoContext);

        [DllImport(libCairo)]
        internal static extern void cairo_set_source_rgb (  IntPtr cairoContext,
                                                            double red,
                                                            double green,
                                                            double blue);
        
        [DllImport(libCairo)]
        internal static extern void cairo_set_source_rgba ( IntPtr cairoContext,
                                                            double red,
                                                            double green,
                                                            double blue,
                                                            double alpha);
        
        [DllImport(libCairo)]
        internal static extern void cairo_paint (IntPtr cairoContext);

        [DllImport(libCairo)]
        internal static extern void cairo_fill (IntPtr cairoContext);

        [DllImport(libCairo)]
        internal static extern void cairo_paint_with_alpha (IntPtr cairoContext, double alpha);

        [DllImport(libCairo)]
        internal static extern void cairo_stroke (IntPtr cairoContext);

        [DllImport(libCairo)]
        internal static extern void cairo_set_line_width (IntPtr cairoContext, double width);

        [DllImport(libCairo)]
        internal static extern double cairo_get_line_width (IntPtr cairoContext);

        // Restores cr to the state saved by a preceding call to cairo_save() 
        // and removes that state from the stack of saved states.
        [DllImport(libCairo)]
        internal static extern void	cairo_restore (IntPtr cairoContext);

        // Gets the target surface for the cairo context as passed to cairo_create().
        [DllImport(libCairo)]
        internal static extern IntPtr cairo_get_target (IntPtr cairoContext);


        [DllImport(libCairo)]
        internal static extern void cairo_set_source (IntPtr cairoContext,IntPtr cairoPattern);

        [DllImport(libCairo)]
        internal static extern void cairo_set_line_join (IntPtr cairoContext, CairoLineJoin line_join);



        //////////////////////
        // cairo_path api's //
        //////////////////////

        // returns refrence to cairo_path_t , cairo_path_destroy must be called after using it
        [DllImport(libCairo)]
        internal static extern IntPtr cairo_copy_path (IntPtr cairoContext);

        // returns refrence to cairo_path_t
        [DllImport(libCairo)]
        internal static extern IntPtr cairo_copy_path_flat (IntPtr cairoContext);

        // get cairo_path_t, releases cairo_path_t allocated resources
        [DllImport(libCairo)]
        internal static extern void cairo_path_destroy (IntPtr cairoPath);

        // append path on to the current path
        [DllImport(libCairo)]
        internal static extern void cairo_append_path (IntPtr cairoContext, IntPtr cairoPath);

        // returns cairo_bool_t , the int here must be treated is boolean
        [DllImport(libCairo)]
        internal static extern int cairo_has_current_point (IntPtr cairoContext);

        // Gets the current point of the current path, which is conceptually 
        // the final point reached by the path so far.
        [DllImport(libCairo)]
        internal static extern void cairo_get_current_point (IntPtr cairoContext, out double x, out double y);

        // Clears the current path. After this call there will be no path and no current point.
        [DllImport(libCairo)]
        internal static extern void cairo_new_path (IntPtr cairoContext);

        // Begin a new sub-path. Note that the existing path is not affected. 
        // After this call there will be no current point.
        [DllImport(libCairo)]
        internal static extern void cairo_new_sub_path (IntPtr cairoContext);

        // Adds a line segment to the path from the current point to the beginning of the current sub-path, 
        // (the most recent point passed to cairo_move_to()), and closes this sub-path. After this call the 
        // current point will be at the joined endpoint of the sub-path.
        [DllImport(libCairo)]
        internal static extern void cairo_close_path (IntPtr cairoContext);

        // Adds a circular arc of the given radius to the current path. The arc is centered at (xc , yc ),
        // begins at angle1 and proceeds in the direction of increasing angles to end at angle2 . If angle2 
        // is less than angle1 it will be progressively increased by 2*M_PI until it is greater than angle1
        [DllImport(libCairo)]
        internal static extern void cairo_arc ( IntPtr cairoContext,
                                                double xc,
                                                double yc,
                                                double radius,
                                                double angle1,
                                                double angle2);

        [DllImport(libCairo)]
        internal static extern void cairo_arc_negative ( IntPtr cairoContext,
                                                         double xc,
                                                         double yc,
                                                         double radius,
                                                         double angle1,
                                                         double angle2);

        [DllImport(libCairo)]
        internal static extern void cairo_curve_to ( IntPtr cairoContext,
                                                     double x1,
                                                     double y1,
                                                     double x2,
                                                     double y2,
                                                     double x3,
                                                     double y3);

        [DllImport(libCairo)]
        internal static extern void cairo_line_to (IntPtr cairoContext, double x, double y);

        // begins a new sub-path , after this call current point will be (x,y)
        [DllImport(libCairo)]
        internal static extern void cairo_move_to (IntPtr cairoContext, double x, double y);

        [DllImport(libCairo)]
        internal static extern void cairo_rectangle ( IntPtr cairoContext,
                                                      double x,
                                                      double y,
                                                      double width,
                                                      double height);

        [DllImport(libCairo)]
        internal static extern void cairo_glyph_path (IntPtr cairoContext, IntPtr cairoGlyph, int num_glyphs);

        /*
        [DllImport(libCairo)]
        internal static extern void cairo_text_path ();
        */

        [DllImport(libCairo)]
        internal static extern void cairo_rel_curve_to ( IntPtr cairoContext,
                                                         double dx1,
                                                         double dy1,
                                                         double dx2,
                                                         double dy2,
                                                         double dx3,
                                                         double dy3);

        [DllImport(libCairo)]
        internal static extern void cairo_rel_line_to (IntPtr cairoContext, double dx, double dy);

        [DllImport(libCairo)]
        internal static extern void cairo_rel_move_to (IntPtr cairoContext, double dx, double dy);

        // Computes a bounding box in user-space coordinates covering the points on the current path.
        // If the current path is empty, returns an empty rectangle ((0,0), (0,0)). Stroke parameters, 
        // fill rule, surface dimensions and clipping are not taken into account.
        [DllImport(libCairo)]
        internal static extern void cairo_path_extents ( IntPtr cairoContext,
                                                         out double x1,
                                                         out double y1,
                                                         out double x2,
                                                         out double y2);

        // cairo_path api's end

        /////////////////////////
        // cairo_pattern api's //
        /////////////////////////

        [DllImport(libCairo)]
        internal static extern void cairo_pattern_add_color_stop_rgb (  IntPtr cairoPattern,
                                                                        double offset,
                                                                        double red,
                                                                        double green,
                                                                        double blue);

        [DllImport(libCairo)]
        internal static extern void cairo_pattern_add_color_stop_rgba ( IntPtr cairoPattern,
                                                                        double offset,
                                                                        double red,
                                                                        double green,
                                                                        double blue,
                                                                        double alpha);

        [DllImport(libCairo)]       
        internal static extern CairoStatus cairo_pattern_get_color_stop_count (IntPtr cairoPattern, out int count);

        [DllImport(libCairo)]
        internal static extern CairoStatus cairo_pattern_get_color_stop_rgba (  IntPtr cairoPattern,
                                                                                int index,
                                                                                out double offset,
                                                                                out double red,
                                                                                out double green,
                                                                                out double blue,
                                                                                out double alpha);
        
        // returns cairo_pattern_t , cairo_pattern_destroy needs to be called after use
        [DllImport(libCairo)]
        internal static extern IntPtr cairo_pattern_create_rgb (double red, double green, double blue);

        // returns cairo_pattern_t , cairo_pattern_destroy needs to be called after use
        [DllImport(libCairo)]
        internal static extern IntPtr cairo_pattern_create_rgba ( double red , 
                                                                  double green, 
                                                                  double blue, 
                                                                  double alpha);

        [DllImport(libCairo)]
        internal static extern CairoStatus cairo_pattern_get_rgba ( IntPtr cairoPattern,
                                                                    out double red,
                                                                    out double green,
                                                                    out double blue,
                                                                    out double alpha);

        [DllImport(libCairo)]
        internal static extern IntPtr cairo_pattern_create_for_surface (IntPtr cairoSurface);

        // Gets the surface of a surface pattern. The reference returned in surface is owned by the pattern; 
        // the caller should call cairo_surface_reference() if the surface is to be retained.
        [DllImport(libCairo)]
        internal static extern CairoStatus cairo_pattern_get_surface (IntPtr cairoPattern, IntPtr cairoSurface);

        // creates a linear-gradient pattern
        [DllImport(libCairo)]
        internal static extern IntPtr cairo_pattern_create_linear (double x0, double y0, double x1, double y1);

        // Gets the gradient endpoints for a linear gradient pattern.
        [DllImport(libCairo)]
        internal static extern CairoStatus cairo_pattern_get_linear_points ( IntPtr cairoPattern,
                                                                             out double x0,
                                                                             out double y0,
                                                                             out double x1,
                                                                             out double y1);
        
        // Creates a new radial gradient cairo_pattern_t between the two circles defined by
        // (cx0, cy0, radius0) and (cx1, cy1, radius1). Before using the gradient pattern, a 
        // number of color stops should be defined using cairo_pattern_add_color_stop_rgb() or 
        // cairo_pattern_add_color_stop_rgba().
        [DllImport(libCairo)]
        internal static extern IntPtr cairo_pattern_create_radial ( double cx0,
                                                                    double cy0,
                                                                    double radius0,
                                                                    double cx1,
                                                                    double cy1,
                                                                    double radius1);
        
        // Gets the gradient endpoint circles for a radial gradient, each specified 
        // as a center coordinate and a radius
        [DllImport(libCairo)]
        internal static extern CairoStatus cairo_pattern_get_radial_circles (   IntPtr cairoPattern,
                                                                                out double x0,
                                                                                out double y0,
                                                                                out double r0,
                                                                                out double x1,
                                                                                out double y1,
                                                                                out double r1);

        // creates a mesh pattern
        [DllImport(libCairo)]
        internal static extern IntPtr cairo_pattern_create_mesh ();

        [DllImport(libCairo)]
        internal static extern void cairo_mesh_pattern_begin_patch (IntPtr cairoPattern);

        [DllImport(libCairo)]
        internal static extern void cairo_mesh_pattern_end_patch (IntPtr cairoPattern);

        [DllImport(libCairo)]
        internal static extern void cairo_mesh_pattern_move_to (IntPtr cairoPattern, double x, double y);

        [DllImport(libCairo)]
        internal static extern void cairo_mesh_pattern_line_to (IntPtr cairoPattern, double x, double y);

        [DllImport(libCairo)]
        internal static extern void cairo_mesh_pattern_curve_to (   IntPtr cairoPattern,
                                                                    double x1,
                                                                    double y1,
                                                                    double x2,
                                                                    double y2,
                                                                    double x3,
                                                                    double y3);

        [DllImport(libCairo)]
        internal static extern void cairo_mesh_pattern_set_control_point (  IntPtr cairoPattern,
                                                                            uint point_num,
                                                                            double x,
                                                                            double y);

        [DllImport(libCairo)]
        internal static extern void cairo_mesh_pattern_set_corner_color_rgb (   IntPtr cairoPattern,
                                                                                uint corner_num,
                                                                                double red,
                                                                                double green,
                                                                                double blue);

        [DllImport(libCairo)]
        internal static extern void cairo_mesh_pattern_set_corner_color_rgba (  IntPtr cairoPattern,
                                                                                uint corner_num,
                                                                                double red,
                                                                                double green,
                                                                                double blue,
                                                                                double alpha);

        [DllImport(libCairo)]
        internal static extern CairoStatus cairo_mesh_pattern_get_patch_count (IntPtr cairoPattern, out int count);

        // returns refrence to cairo_path_t
        [DllImport(libCairo)]
        internal static extern IntPtr cairo_mesh_pattern_get_path (IntPtr cairoPattern,uint patch_num);

        [DllImport(libCairo)]
        internal static extern CairoStatus cairo_mesh_pattern_get_control_point (   IntPtr cairoPattern,
                                                                                    uint patch_num,
                                                                                    uint point_num,
                                                                                    out double x,
                                                                                    out double y);

        [DllImport(libCairo)]
        internal static extern CairoStatus cairo_mesh_pattern_get_corner_color_rgba (   IntPtr cairoPattern,
                                                                                        uint patch_num,
                                                                                        uint corner_num,
                                                                                        out double red,
                                                                                        out double green,
                                                                                        out double blue,
                                                                                        out double alpha);
        // increases cairo_pattern_t refrence by 1
        [DllImport(libCairo)]
        internal static extern IntPtr cairo_pattern_reference (IntPtr cairoPattern);

        // decreases cairo_pattern_t refrence by 1, when refrence equals to 0 its resources are deallocated
        [DllImport(libCairo)]
        internal static extern void cairo_pattern_destroy (IntPtr cairoPattern);

        [DllImport(libCairo)]
        internal static extern CairoStatus cairo_pattern_status (IntPtr cairoPattern);

        [DllImport(libCairo)]
        internal static extern void cairo_pattern_set_extend (IntPtr cairoPattern, CairoExtend extend);

        [DllImport(libCairo)]
        internal static extern CairoExtend cairo_pattern_get_extend (IntPtr cairoPattern);

        [DllImport(libCairo)]
        internal static extern void cairo_pattern_set_filter (IntPtr cairoPattern, CairoFilter filter);

        [DllImport(libCairo)]
        internal static extern CairoFilter cairo_pattern_get_filter (IntPtr cairoPattern);

        // Stores the pattern's transformation matrix into matrix
        [DllImport(libCairo)]
        internal static extern void cairo_pattern_set_matrix (IntPtr cairoPattern, ref CairoMatrix matrix);

        [DllImport(libCairo)]
        internal static extern void cairo_pattern_get_matrix (IntPtr cairoPattern, out CairoMatrix matrix);

        [DllImport(libCairo)]
        internal static extern CairoPatternType cairo_pattern_get_type (IntPtr cairoPattern);

        [DllImport(libCairo)]
        internal static extern uint cairo_pattern_get_reference_count (IntPtr cairoPattern);

        // end cairo_pattern api's

        ////////////////////////
        // cairo_region api's //
        ////////////////////////


        // returns refrence to empty allocated cairo_region_t
        [DllImport(libCairo)]
        internal static extern IntPtr cairo_region_create ();

        [DllImport(libCairo)]
        internal static extern IntPtr cairo_region_create_rectangle (ref CairoRectangleInt rectangle);

        [DllImport(libCairo)]
        internal static extern IntPtr cairo_region_create_rectangles (CairoRectangleInt[] rectangles, int count);

        [DllImport(libCairo)]
        internal static extern IntPtr cairo_region_copy (IntPtr cairoRegion);

        [DllImport(libCairo)]
        internal static extern IntPtr cairo_region_reference (IntPtr cairoRegion);

        [DllImport(libCairo)]
        internal static extern void cairo_region_destroy (IntPtr cairoRegion);

        [DllImport(libCairo)]
        internal static extern CairoStatus cairo_region_status (IntPtr cairoRegion);

        [DllImport(libCairo)]
        internal static extern void cairo_region_get_extents (IntPtr cairoRegion, out CairoRectangleInt extents);

        [DllImport(libCairo)]
        internal static extern int cairo_region_num_rectangles (IntPtr cairoRegion);

        [DllImport(libCairo)]
        internal static extern void cairo_region_get_rectangle ( IntPtr cairoRegoin, 
                                                                 int nth, 
                                                                 out CairoRectangleInt rectangle);

        // returns int as boolean
        [DllImport(libCairo)]
        internal static extern int cairo_region_is_empty (IntPtr cairoRegion);

        // returns int as boolean
        [DllImport(libCairo)]
        internal static extern int cairo_region_contains_point (IntPtr cairoRegsion, int x, int y);

        [DllImport(libCairo)]
        internal static extern CairoRegionOverlap cairo_region_contains_rectangle ( IntPtr cairoRegion, 
                                                                                    ref CairoRectangleInt rectangle);

        // returns int as boolean
        [DllImport(libCairo)]
        internal static extern int cairo_region_equal (IntPtr cairoRegionA, IntPtr cairoRegionB);

        [DllImport(libCairo)]
        internal static extern void cairo_region_translate (IntPtr cairoRegion, int dx, int dy);

        [DllImport(libCairo)]
        internal static extern CairoStatus cairo_region_intersect (IntPtr cairRegionDst, IntPtr cairoRegionOther);

        [DllImport(libCairo)]
        internal static extern CairoStatus cairo_region_intersect_rectangle ( IntPtr cairoRegionDst, 
                                                                              ref CairoRectangleInt rectangle);

        [DllImport(libCairo)]
        internal static extern CairoStatus cairo_region_subtract (IntPtr cairoRegionDst, IntPtr cairoRegionOther);

        [DllImport(libCairo)]
        internal static extern CairoStatus cairo_region_subtract_rectangle ( IntPtr cairoRegionDst,
                                                                             ref CairoRectangleInt rectangle);

        [DllImport(libCairo)]
        internal static extern CairoStatus cairo_region_union (IntPtr cairoRegionDst, IntPtr cairoRegionOther);

        [DllImport(libCairo)]
        internal static extern CairoStatus cairo_region_union_rectangle ( IntPtr cairoRegionDst, 
                                                                          ref CairoRectangleInt rectangle);

        [DllImport(libCairo)]
        internal static extern CairoStatus cairo_region_xor (IntPtr cairoRegionDst, IntPtr cairoRegionOther);

        [DllImport(libCairo)]              
        internal static extern CairoStatus cairo_region_xor_rectangle ( IntPtr cairoRegionDst, 
                                                                        ref CairoRectangleInt rectangle);

        // end cairo_region api's

        ///////////////////////////////////////
        // cairo matrix transformation api's //
        //////////////////////////////////////

        [DllImport(libCairo)] 
        internal static extern void cairo_translate (IntPtr cairoContext, double tx, double ty);

        [DllImport(libCairo)] 
        internal static extern void cairo_scale (IntPtr cairoContext, double sx, double sy);

        [DllImport(libCairo)] 
        internal static extern void cairo_rotate (IntPtr cairoContext, double angle);

        [DllImport(libCairo)] 
        internal static extern void cairo_transform (IntPtr cairoContext, ref CairoMatrix matrix);

        [DllImport(libCairo)] 
        internal static extern void cairo_set_matrix (IntPtr cairoContext, ref CairoMatrix matrix);

        [DllImport(libCairo)] 
        internal static extern void cairo_get_matrix (IntPtr cairoContext, out CairoMatrix matrix);

        [DllImport(libCairo)] 
        internal static extern void cairo_identity_matrix (IntPtr cairoContext);

        [DllImport(libCairo)] 
        internal static extern void cairo_user_to_device (IntPtr cairoContext, ref double x, ref double y);

        [DllImport(libCairo)] 
        internal static extern void cairo_user_to_device_distance ( IntPtr cairoContext, 
                                                                    ref double dx, 
                                                                    ref double dy);

        [DllImport(libCairo)] 
        internal static extern void cairo_device_to_user (IntPtr cairoContext, ref double x, ref double y);

        [DllImport(libCairo)] 
        internal static extern void cairo_device_to_user_distance ( IntPtr cairoContext, 
                                                                    ref double dx, 
                                                                    ref double dy);

        // end matrix transformation api's

        //////////////////////
        // cairo_text api's //
        //////////////////////

        [DllImport(libCairo)] 
        internal static extern void cairo_select_font_face (IntPtr cairoContext, 
                                                            [MarshalAs(UnmanagedType.LPStr)] string fontFamily,
                                                            CairoFontSlant slant,
                                                            CairoFontWeight weight);

        [DllImport(libCairo)] 
        internal static extern void cairo_set_font_size (IntPtr cairoContext, double size);

        [DllImport(libCairo)] 
        internal static extern void cairo_set_font_matrix (IntPtr cairoContext, ref CairoMatrix matrix);

        [DllImport(libCairo)] 
        internal static extern void cairo_get_font_matrix (IntPtr cairoContext, out CairoMatrix matrix);

        [DllImport(libCairo)] 
        internal static extern void cairo_set_font_options ();

        [DllImport(libCairo)] 
        internal static extern void cairo_get_font_options ();

        [DllImport(libCairo)] 
        internal static extern void cairo_set_font_face ();

        [DllImport(libCairo)] 
        internal static extern IntPtr cairo_get_font_face ();

        [DllImport(libCairo)] 
        internal static extern void cairo_set_scaled_font ();

        [DllImport(libCairo)] 
        internal static extern IntPtr cairo_get_scaled_font ();

        [DllImport(libCairo)] 
        internal static extern void cairo_show_text ();

        [DllImport(libCairo)] 
        internal static extern void cairo_show_glyphs ();

        [DllImport(libCairo)] 
        internal static extern void cairo_show_text_glyphs ();

        [DllImport(libCairo)] 
        internal static extern void cairo_font_extents ();

        [DllImport(libCairo)] 
        internal static extern void cairo_text_extents ();

        [DllImport(libCairo)] 
        internal static extern void cairo_glyph_extents ();

        [DllImport(libCairo)] 
        internal static extern IntPtr cairo_toy_font_face_create ();

        [DllImport(libCairo)] 
        [return: MarshalAs(UnmanagedType.LPStr)]
        internal static extern string cairo_toy_font_face_get_family ();

        [DllImport(libCairo)] 
        internal static extern CairoFontSlant cairo_toy_font_face_get_slant ();

        [DllImport(libCairo)] 
        internal static extern CairoFontWeight cairo_toy_font_face_get_weight ();

        [DllImport(libCairo)] 
        internal static extern IntPtr cairo_glyph_allocate ();

        [DllImport(libCairo)] 
        internal static extern void cairo_glyph_free ();

        [DllImport(libCairo)] 
        internal static extern IntPtr cairo_text_cluster_allocate ();

        [DllImport(libCairo)] 
        internal static extern void cairo_text_cluster_free ();

        // end cairo_font api's

        ///////////////////////////
        // cairo_surface_t api's //
        ///////////////////////////

        // returns pointer to cairo_surface_t
        [DllImport(libCairo)]
        internal static extern IntPtr cairo_xcb_surface_create ( IntPtr xcbConnection,
                                                                 uint drawable,
                                                                 IntPtr visualType,
                                                                 int width,
                                                                 int height);
        
        // Call this function when window size changes
        [DllImport(libCairo)]
        internal static extern void cairo_xcb_surface_set_size ( IntPtr cairoSurface,
                                                                 int width,
                                                                 int height);
        
        // Do any pending drawing for the surface and also restore any temporary 
        // modifications cairo has made to the surface's state. This function must 
        // be called before switching from drawing on the surface with cairo to drawing 
        // on it directly with native APIs, or accessing its memory outside of Cairo. 
        // If the surface doesn't support direct access, then this function does nothing.
        [DllImport(libCairo)]
        internal static extern void cairo_surface_flush (IntPtr cairoSurface);

        
        // Decrease cairo_surface_t refrence by 1 , if refrences are zero it frees unmanaged resources
        [DllImport(libCairo)]
        internal static extern void cairo_surface_destroy (IntPtr cairoSurface);

        [DllImport(libCairo)]
        internal static extern void cairo_surface_finish (IntPtr cairoSurface);

        [DllImport(libCairo)]
        internal static extern CairoStatus cairo_surface_status (IntPtr cairoSurface);

        [DllImport(libCairo)]
        internal static extern uint cairo_surface_get_reference_count (IntPtr cairoSurface);

        [DllImport(libCairo)]
        internal static extern IntPtr cairo_xcb_surface_create_for_bitmap ( IntPtr xcbConnection,
                                                                            IntPtr xcbScreen,
                                                                            uint pixmapHandle,
                                                                            int width,
                                                                            int height);

        [DllImport(libCairo)]
        internal static extern IntPtr cairo_xcb_surface_create_with_xrender_format ( IntPtr xcbConnection,
                                                                                     IntPtr xcbScreen,
                                                                                     uint drawable,
                                                                                     IntPtr xcbPictFormatInfo,
                                                                                     int width,
                                                                                     int height);

        [DllImport(libCairo)]
        internal static extern void	cairo_xcb_surface_set_drawable ( IntPtr cairoSurface,
                                                                     uint drawable,
                                                                     int width,
                                                                     int height);

        [DllImport(libCairo)]
        internal static extern IntPtr cairo_xcb_device_get_connection (IntPtr cairoDevice);

        [DllImport(libCairo)]
        internal static extern void	cairo_xcb_device_debug_cap_xrender_version ( IntPtr cairoDevice,
                                                                                 int major_version,
                                                                                 int minor_version);

        [DllImport(libCairo)]
        internal static extern void	cairo_xcb_device_debug_cap_xshm_version( IntPtr cairoDevice,
                                                                             int major_version,
                                                                             int minor_version);

        [DllImport(libCairo)]
        internal static extern int cairo_xcb_device_debug_get_precision (IntPtr cairoDevice);

        [DllImport(libCairo)]
        internal static extern void	cairo_xcb_device_debug_set_precision (IntPtr cairoDevice, int precision);

    }
}