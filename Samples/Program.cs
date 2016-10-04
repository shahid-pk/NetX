using System;
using NetX.XWindows;

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
                application.MainWindow.WindowExposed += (o,arguments) => {
                    var write = new XFont(application,"7x13");
                    write.Write("Hello World !");
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
