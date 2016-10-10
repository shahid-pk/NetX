using System;
using NetX.XWindows;
using NetX.XWindows.Graphics;

namespace ConsoleApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                var application = new XApplication();
                application.MainWindow = new XWindow();

                var surface = new CairoSurface(application);
                var cr = new CairoContext(surface);

                application.MainWindow.WindowExposed += (o,arguments) => {

                        var width = arguments.Width + arguments.X;
                        var height = arguments.Height + arguments.Y;
                        surface.SetSize(width,height);
                        cr.SetSourceRGB(0, 1, 0);
 			            cr.Paint();
 			            cr.SetSourceRGB(1, 0, 0);
 			            cr.MoveTo(0, 0);
 			            cr.LineTo(width, 0);
 			            cr.LineTo(width, height);
 			            cr.ClosePath();
 			            cr.Fill();
 			            cr.SetSourceRGB(0, 0, 1);
 			            cr.LineWidth = 20;
 			            cr.MoveTo(0, height);
 			            cr.LineTo(width, 0);
 			            cr.Stroke();
			            surface.Flush();

                    // hello world below
                    /*
                    var write = new XFont(application,"7x13");
                    write.Write("Hello World !");
                    */
                };
                application.Run();
            }
            catch(Exception ex)
            {
                Console.WriteLine($"{ex.Message}");
                Console.WriteLine($"{ex.StackTrace}");
            }
        }
    }
}
