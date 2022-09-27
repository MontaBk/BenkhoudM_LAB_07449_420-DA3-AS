using BenkhoudM_LAB_07449_420_DA3_AS.Controllers;
using BenkhoudM_LAB_07449_420_DA3_AS.Models;
using BenkhoudM_LAB_07449_420_DA3_AS.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BenkhoudM_LAB_07449_420_DA3_AS
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        //public TextBox Qte
        //{
        //    get { return txtQte; }
        //    set { txtQte = value; }
        //}
        private void btnSave_Click(object sender, EventArgs e)
        {
            Product product = new Product(Convert.ToInt32(txtGtin.Text), Convert.ToInt32(txtQte.Text), txtName.Text,txtdesc.Text);
            product.Insert();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }
       
        private void btnTest_Click(object sender, EventArgs e)
        {
            //string DB_FILE_PATH = $"C:\\Program Files\\Microsoft SQL Server\\MSSQL15.SQL2019EXPRESS\\MSSQL\\DATA\\Lab.mdf";
            string DB_FILE_PATH = $"C:\\Program Files\\Microsoft SQL Server\\MSSQL15.SQL2019EXPRESS\\MSSQL\\DATA\\Lab.mdf";//SQL2019EXPRESS
            string connString = $"Server=.\\SQL2019Express;database=Lab;Integrated Security=true;AttachDbFilename={DB_FILE_PATH};";
            
            Debug.WriteLine($"Connection string: [{connString}].");
            // Création de l’object SqlConnection
            SqlConnection conn = new SqlConnection(connString);
            // Ouverture et fermeture de la connexion
            conn.Open();
            MessageBox.Show("Connection opened!");
            // faire de quoi avec la connexion ici 
            conn.Close();
            Debug.WriteLine("Connection closed!");
        }

        private void btnsearch_Click(object sender, EventArgs e)
        {
            //ProductControllers prod = new ProductControllers();
            //prod.DisplayProduct(Convert.ToUInt16(txtId.Text));
           // sans controllers
            Product produit = new Product(Convert.ToUInt16(txtId.Text));
            produit.GetById();
            txtGtin.Text = produit.GtinCode.ToString();
            txtName.Text = produit.Name.ToString();
            txtQte.Text = produit.QtyInStock.ToString();
            txtdesc.Text = produit.Description.ToString();


        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            Product product = new Product(Convert.ToInt32(txtId.Text), Convert.ToInt32(txtGtin.Text), Convert.ToInt32(txtQte.Text), txtName.Text, txtdesc.Text);
            product.Update();

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            Product produit = new Product(Convert.ToUInt16(txtId.Text));
            produit.Delete();
        }
    }
}
