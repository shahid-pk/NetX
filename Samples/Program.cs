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
                using(var application = new XApplication())
                {
                    application.MainWindow = new XWindow();
                    application.Run();
                    application.Flush();
                    Console.ReadKey();
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine($"{ex.Message}");
                Console.WriteLine($"{ex.StackTrace}");
            }
        }
    }
}
