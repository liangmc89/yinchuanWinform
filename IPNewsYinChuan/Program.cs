using CefSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IPNewsYinChuan
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {

            try
            {
                Process instance = RunningInstance();
                if (instance == null)
                {
                    //For Windows 7 and above, best to include relevant app.manifest entries as well
                    Cef.EnableHighDPISupport();

                    var settings = new CefSettings()
                    {
                        //By default CefSharp will use an in-memory cache, you need to specify a Cache Folder to persist data
                        //CachePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "CefSharp\\Cache")
                        // CachePath = AppDomain.CurrentDomain.BaseDirectory + "CefSharp\\Cache"
                        CachePath = @"D:\Cache"
                    };
                    // Enable WebRTC                           
                    settings.CefCommandLineArgs.Add("enable-media-stream", "1");
                    //Perform dependency check to make sure all relevant resources are in our output directory.
                    // Cef.Initialize(settings, performDependencyCheck: true, browserProcessHandler: null);
                    Cef.Initialize(settings, shutdownOnProcessExit: true, performDependencyCheck: false);                

                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    Application.Run(new Form1());
                }
                else
                {
                    HandleRunningInstance(instance);
                }
            }
            catch (Exception e) { }

           
            
            
            
        }

        [DllImport("User32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);
        [DllImport("User32.dll")]
        private static extern bool ShowWindowAsync(IntPtr hWnd, int cmdShow);
        private static void HandleRunningInstance(Process instance)
        {
            // 确保窗口没有被最小化或最大化   
            ShowWindowAsync(instance.MainWindowHandle, 4);
            // 设置真实例程为foreground  window    
            SetForegroundWindow(instance.MainWindowHandle);// 放到最前端   
        }

        private static Process RunningInstance()
        {
            Process current = Process.GetCurrentProcess();
            Process[] processes = Process.GetProcessesByName(current.ProcessName);
            foreach (Process process in processes)
            {
                if (process.Id != current.Id)
                {
                    // 确保例程从EXE文件运行   
                    if (Assembly.GetExecutingAssembly().Location.Replace("/", "\\") == current.MainModule.FileName)
                    {
                        return process;
                    }
                }
            }
            return null;
        }

    }
}
