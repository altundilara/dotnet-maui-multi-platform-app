using System;
using Microsoft.Maui;
using Microsoft.Maui.Hosting;

namespace _23010310007_DilaraAltun_FinalOdev
{
    internal class Program : MauiApplication
    {
        protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();

        static void Main(string[] args)
        {
            var app = new Program();
            app.Run(args);
        }
    }
}
