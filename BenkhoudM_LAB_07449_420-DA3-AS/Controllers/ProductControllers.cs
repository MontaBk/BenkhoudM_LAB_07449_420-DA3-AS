using BenkhoudM_LAB_07449_420_DA3_AS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace BenkhoudM_LAB_07449_420_DA3_AS.Controllers
{
   
   internal class ProductControllers : IController
   {

            public void CreateProduct(string name)
            {
                Product newProduct = new Product(name);
            }

            public void CreateProduct(int qtyInStock, string name)
            {
                Product newProduct = new Product(qtyInStock,name);
            }

            public void CreateProduct(long gtinCode, int qtyInStock, string name, string description)
            {
                Product newProduct = new Product(gtinCode, qtyInStock, name, description);
                newProduct.Insert();
            }

            public void DisplayProduct(int productId)
            {
                Product product = new Product(productId);
                product.GetById();
                this.DisplayProduct(product);
            }

            public void DisplayProduct(Product product)
            {
                // do something later in the course when we have views, or, if you want,
                // dump the data to the console.

            }

            public void UpdateProduct(int productId, string name, int qtyInStock, long gtinCode, string description)
            {
                Product product = new Product(productId);
                product.GetById();
                product.Name = name;
                product.QtyInStock = qtyInStock;
                product.GtinCode = gtinCode;
                product.Description = description;
                product.Update();
            }

            public void DeleteProduct(int productId)
            {
                Product product = new Product(productId);
                this.DeleteProduct(product);
            }

            public void DeleteProduct(Product product)
            {
                product.Delete();
            }

            //public void DisplayProducts(List<Product> productList) {
            //    foreach (Product product in productList) {
            //        this.DisplayProduct(product);
            //    }
            //}

        }
    
}
