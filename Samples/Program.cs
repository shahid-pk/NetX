using System;
using NetX.Windows;
using NetX.XWindows;
using NetX.XWindows.Graphics;

namespace ConsoleApplication
{
    public class Program
    {
        // Comment out one function in main at a time, every function show a different functionality 
        public static void Main(string[] args)
        {
            try
            {
                var application = new XApplication();
                application.MainWindow = new XWindow();
                //CairoDrawing(application);
                //HelloWorld(application);
                ButtonTest(application);
                application.Run();
            }
            catch(Exception ex)
            {
                Console.WriteLine($"{ex.Message}");
                Console.WriteLine($"{ex.StackTrace}");
            }
        }

        private static void CairoDrawing(XApplication application)
        {
            var surface = new CairoSurface(application);
            var cr = new CairoContext(surface);

            application.ApplicationTerminated += (o,args) => { cr.Dispose(); };

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
 			    cr.SetSourceRGB(100, 100, 100);
 			    cr.LineWidth = 20;
 			    cr.MoveTo(0, height);
 			    cr.LineTo(width, 0);
 			    cr.Stroke();
			    surface.Flush();
            };
        }

        private static void HelloWorld(XApplication application)
        {
            application.MainWindow.WindowExposed += (o,arguments) => {
                var write = new XFont(application,"7x13");
                write.Write("Hello World !");
            };
        }

        private static void ButtonTest(XApplication application)
        {
            application.MainWindow.Content = new Button();
        }
    }
}
