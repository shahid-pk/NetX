using System;
using System.Runtime.InteropServices;
using NetX.Interop;
using NetX.Interop.Structs;

namespace ConsoleApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            int screeNo = 0;
            uint mask = 0;
            XcbScreen screen;
            IntPtr connection = LibXcb.xcb_connect(null,ref screeNo);
            try
            {
                IntPtr ptr = LibXcb.xcb_get_setup(connection);
                var iter = LibXcb.xcb_setup_roots_iterator(ptr);
                screen = Marshal.PtrToStructure<XcbScreen>(iter.data);
                var win = LibXcb.xcb_generate_id(connection);
                LibXcb.xcb_create_window (connection,  /* Connection          */
                     0,                                /* depth (same as root)*/
                     win,                              /* window Id           */
                     screen.root,                      /* parent window       */
                     0, 0,                             /* x, y                */
                     150, 150,                         /* width, height       */
                     10,                               /* border_width        */
                     1,                                /* class               */
                     screen.root_visual,               /* visual              */
                     0, ref mask);                     /* masks, not used yet */
                
                LibXcb.xcb_map_window(connection,win);

                LibXcb.xcb_flush(connection);

                Console.ReadKey();
            }
            catch(Exception ex)
            {
                Console.WriteLine($"{ex.Message}");
                Console.WriteLine($"{ex.StackTrace}");
            }
            finally 
            { 
                //LibXcb.xcb_disconnect(connection);
                Marshal.FreeHGlobal(connection);
            }
        }
    }
}
