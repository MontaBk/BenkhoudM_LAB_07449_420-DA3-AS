using BenkhoudM_LAB_07449_420_DA3_AS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace BenkhoudM_LAB_07449_420_DA3_AS.Views
{
    internal class ProductConsoleDisplayView
    {
        public ProductConsoleDisplayView()
        {
        }

        public void Render(Product modelInstance)
        {
            Console.WriteLine($"Id: {modelInstance.Id} " +
                $"\nName: {modelInstance.Name} " +
                $"\nQty In Stock: {modelInstance.QtyInStock} " +
                $"\nGTIN code: {modelInstance.GtinCode} " +
                $"\nDescription: {modelInstance.Description}");

        }
    }
}
