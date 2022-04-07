using System;
using System.Drawing;
using OpenTK.Graphics.ES11;
using OpenTK.Mathematics;
using OpenTK.Windowing.Desktop;

namespace ComputerGraphicOpenGL
{
    //my first openGL project
    //followed a tutorial by Lutz Karau on youtube in german
    class Program
    {
        static void Main(string[] args)
        {
            GameWindowSettings gameWindowSettings = new GameWindowSettings();
            //gameWindowSettings.IsMultiThreaded = false;
            gameWindowSettings.RenderFrequency = 0;
            gameWindowSettings.UpdateFrequency = 60; //your max fps
            

            NativeWindowSettings nativeWindowSettings = new NativeWindowSettings();
            
            nativeWindowSettings.Flags = OpenTK.Windowing.Common.ContextFlags.Debug;
            nativeWindowSettings.WindowState = 0;
            nativeWindowSettings.NumberOfSamples = 0; // FSAA
            nativeWindowSettings.Title = "ComputerGraphicOpenGL-FirstTest";
            nativeWindowSettings.WindowBorder = OpenTK.Windowing.Common.WindowBorder.Fixed;
            nativeWindowSettings.Size = new Vector2i(600, 600);

            ApplicationWindow window = new ApplicationWindow(gameWindowSettings, nativeWindowSettings);
            window.CenterWindow();
            window.Run();
            window.Dispose();
        }
    }
}
