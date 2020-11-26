using System;
using System.Windows.Forms;

namespace MatrixPlayground
{
    /// <summary>
    /// 
    /// </summary>
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            using var mainForm = new Form1();
            Application.Run(mainForm);
        }
    }
}
