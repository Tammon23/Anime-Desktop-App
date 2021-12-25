using System;
using System.Windows.Forms;

namespace MyAnimeListDesktopApplication2
{
    internal static class Program
    {

        /// <summary>
        ///     The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormAnimeListMenu());


            // var builder = WebApplication.CreateBuilder(args);
            //
            // // Add services to the container.
            // builder.Services.AddHttpClient();
        }
    }
}