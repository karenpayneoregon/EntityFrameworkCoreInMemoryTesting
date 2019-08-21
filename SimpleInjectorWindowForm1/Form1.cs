using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataLibrary.Models;
using EntityFrameworkCoreLikeLibrary.Models;
using SimpleInjector;
using SimpleInjector.Lifestyles;
using Container = SimpleInjector.Container;

namespace SimpleInjectorWindowForm1
{
    public partial class Form1 : Form
    {
        public static Container CustomerContainer;
        public Form1()
        {
            InitializeComponent();

            CustomerContainer = new Container();
            CustomerContainer
                .Register<ICustomer, InMemoryCustomerData>(Lifestyle.Singleton);
        }

        private void GetCustomerByIdentifierButton_Click(object sender, EventArgs e)
        {
            using (AsyncScopedLifestyle.BeginScope(CustomerContainer))
            {
                var customerInstance = CustomerContainer.GetInstance<ICustomer>();

                var customer = customerInstance.GetById(2);
                if (customer != null)
                {
                    MessageBox.Show($"Company name {customer.CompanyName}");
                }
            }
        }
    }
}
