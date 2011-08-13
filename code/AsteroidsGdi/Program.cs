using System;
using System.Windows.Forms;

namespace AsteroidsGdiApp
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// Test for git push
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}