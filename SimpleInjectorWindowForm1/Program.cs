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
            //Bootstrap();
            //Application.Run(CustomerContainer.GetInstance<Form1>());
            Application.Run(new Form1());
        }
        private static void Bootstrap()
        {
            // Create the container as usual.
            CustomerContainer = new Container();

            // Register your types, for instance:
            CustomerContainer.Register<ICustomer, InMemoryCustomerData>(Lifestyle.Singleton);
            //container.Register<ICustomer, InMemoryCustomerData>();

            //container.Register<Form1>();

            // Optionally verify the container.
            CustomerContainer.Verify();
        }
    }
}
