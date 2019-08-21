using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataLibrary.Models;
using SimpleInjector;

namespace SimpleInjectorWindowForm1
{
    static class Program
    {
        public static Container CustomerContainer; 

        /// <summary>
        /// The main entry point for the application.
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
