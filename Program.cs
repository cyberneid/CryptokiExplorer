using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Cyberneid.NCryptoki;

namespace CryptokiExplorer
{
    static class Program
    {
        public static string appDir;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);


            appDir = Application.ExecutablePath;
            appDir = appDir.Substring(0, appDir.LastIndexOf('\\'));

            Cryptoki.Licensee = "Ugo Chirico";
            Cryptoki.ProductKey = "CBRT-6M3X-ZZXG-QPIF-UNKF-N3UW-BQUC";// "ZB8T-YXR3-AZJ9-BFUV-K55A-2CPK-J6UC";

            Application.Run(new MainForm());
        }
    }
}