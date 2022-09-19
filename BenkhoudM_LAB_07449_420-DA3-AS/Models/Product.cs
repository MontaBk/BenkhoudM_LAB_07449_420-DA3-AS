using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BenkhoudM_LAB_07449_420_DA3_AS.Utils;

namespace BenkhoudM_LAB_07449_420_DA3_AS.Models
{
    internal class Product : IModel<Product>
    {

        private static readonly string DATABASE_TABLE = "dbo.Product";

        public int Id { get; protected set; }
        public long GtinCode { get; set; }
        public int QtyInStock { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }


        #region Constructors


        public Product()
        {           
        }
        public Product(int id)
        {
            Id = id;
        }

        public Product(string name)
        {
            this.Name = name;
            this.QtyInStock = 0;
        }

        protected Product(int qtyInStock, string name)
        {
            this.QtyInStock = qtyInStock;
            this.Name = name;
        }

        protected Product(long gtinCode, int qtyInStock, string name) : this(qtyInStock, name)
        {
            this.GtinCode = gtinCode;
        }

        protected Product(int qtyInStock, string name, string description) : this(qtyInStock, name)
        {
            this.Description = description;
        }

        public Product(long gtinCode, int qtyInStock, string name, string description) : this(gtinCode, qtyInStock, name)
        {
            this.Description = description;
        }
        public Product(int id,long gtinCode, int qtyInStock, string name, string description) : this(gtinCode, qtyInStock, name)
        {
            this.Id= id;
            this.Description = description;
        }


        #endregion



        #region Methods


        public void Delete()
        {
            if (!(this.Id > 0))
            {
                // Id has not been set, it is initialized by default at 0;
                throw new DataAccessException($"Cannot use method {this.GetType().FullName}.Delete() : Id value is 0.");
            }

            using (SqlConnection connection = DbUtilsSimple.GetDefaultConnection())
            {
                string statement = $"DELETE FROM {DATABASE_TABLE} WHERE id = {this.Id};";
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = statement;

                #region POUR PROCHAIN COURS
                /*
                SqlParameter param = cmd.CreateParameter();
                param.ParameterName = "@id";
                param.DbType = DbType.Int32;
                param.Value = this.Id;
                cmd.Parameters.Add(param);
                */
                #endregion

                connection.Open();
                int affectedRows = cmd.ExecuteNonQuery();
                MessageBox.Show("Data deleted!!!");

                if (!(affectedRows > 0))
                {
                    // No affected rows: no deletion occured -> row with matching Id not found
                    throw new DataAccessException($"Failed to delete {this.GetType().FullName}: no database entry to delete found for Id# {this.Id}.");
                }
            }
        }

        public Product GetById()
        {
            if (!(this.Id > 0))
            {
                // Id has not been set, it is initialized by default at 0;
                throw new DataAccessException("Cannot use method Product.GetById() : Id value is 0.");
            }

            using (SqlConnection connection = DbUtilsSimple.GetDefaultConnection())
            {
                string statement = $"SELECT * FROM {DATABASE_TABLE} WHERE id = {this.Id};";
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = statement;

                #region POUR PROCHAIN COURS
                /*
                SqlParameter param = cmd.CreateParameter();
                param.ParameterName = "@id";
                param.DbType = DbType.Int32;
                param.Value = this.Id;
                cmd.Parameters.Add(param);
                */
                #endregion

                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    this.Id = reader.GetInt32(0);
                    // gtinCode in the database can be NULL
                    if (!reader.IsDBNull(1))
                    {
                        this.GtinCode = reader.GetInt64(1);
                    }
                    this.QtyInStock = reader.GetInt32(2);
                    this.Name = reader.GetString(3);
                    // gtinCode in the database can be NULL
                    if (!reader.IsDBNull(4))
                    {
                        this.Description = reader.GetString(4);
                    }

                    return this;

                }
                else
                {
                    throw new DataAccessException($"No database entry for {this.GetType().FullName} id# {this.Id}.");
                }
            }
        }

        public Product Insert()
        {
            if (this.Id != 0)
            {
                // Id has been set, cannot insert a product with a specific Id without risking
                // to mess up the database.
                throw new DataAccessException($"Cannot use method {this.GetType().FullName}.Insert() : Id value is not 0.");
            }

            using (SqlConnection connection = DbUtilsSimple.GetDefaultConnection())
            {

                // Create the INSERT statement. We do not pass any Id value since this is insertion
                // and the id is auto-generated by the database on insertion (identity).
                string statement = $"INSERT INTO {DATABASE_TABLE} (gtinCode, qtyInStock, name, description) " +
                    $"VALUES ({this.GtinCode}, {this.QtyInStock}, ('{this.Name}'), ('{this.Description}')); " +
                    $"SELECT CAST(SCOPE_IDENTITY() AS int);";
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = statement;
                cmd.Parameters.AddWithValue("@gtinCode", GtinCode);
                cmd.Parameters.AddWithValue("@qtyInStock", QtyInStock);
                cmd.Parameters.AddWithValue("@name",Name);
                cmd.Parameters.AddWithValue("@description", Description);
                MessageBox.Show("Data insered!!!");

                #region POUR PROCHAIN COURS
                /*
                // Create and add parameters
                SqlParameter gtinCodeParam = cmd.CreateParameter();
                gtinCodeParam.ParameterName = "@gtinCode";
                gtinCodeParam.DbType = DbType.Int64;
                gtinCodeParam.Value = this.GtinCode;
                cmd.Parameters.Add(gtinCodeParam);

                SqlParameter qtyInStockParam = cmd.CreateParameter();
                qtyInStockParam.ParameterName = "@qtyInStock";
                qtyInStockParam.DbType = DbType.Int32;
                qtyInStockParam.Value = this.QtyInStock;
                cmd.Parameters.Add(qtyInStockParam);

                SqlParameter nameParam = cmd.CreateParameter();
                nameParam.ParameterName = "@name";
                nameParam.DbType = DbType.String;
                nameParam.Value = this.Name;
                cmd.Parameters.Add(nameParam);

                SqlParameter descriptionParam = cmd.CreateParameter();
                descriptionParam.ParameterName = "@description";
                descriptionParam.DbType = DbType.String;
                descriptionParam.Value = this.Description;
                cmd.Parameters.Add(descriptionParam);
                */
                #endregion

                connection.Open();
                //this.Id = (Int32)cmd.ExecuteScalar();
                cmd.ExecuteNonQuery();
                return this;

            }
        }

        public Product Update()
        {
            if (!(this.Id > 0))
            {
                // Id has not been set, cannot update a product with no specific Id to track the correct db row.
                throw new DataAccessException($"Cannot use method {this.GetType().FullName}.Update() : Id value is 0.");
            }

            using (SqlConnection connection = DbUtilsSimple.GetDefaultConnection())
            {

                // Create the Update statement.
                string statement = $"UPDATE {DATABASE_TABLE} SET gtinCode = {this.GtinCode}, " +
                    $"qtyInStock = {this.QtyInStock}, " +
                    $"name = '{this.Name}', " +
                    $"description = '{this.Description}' " +
                    $"WHERE id = {this.Id};";
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = statement;

                #region POUR PROCHAIN COURS
                /*
                // Create and add parameters
                SqlParameter whereIdParam = cmd.CreateParameter();
                whereIdParam.ParameterName = "@id";
                whereIdParam.DbType = DbType.Int32;
                whereIdParam.Value = this.Id;
                cmd.Parameters.Add(whereIdParam);

                SqlParameter gtinCodeParam = cmd.CreateParameter();
                gtinCodeParam.ParameterName = "@gtinCode";
                gtinCodeParam.DbType = DbType.Int64;
                gtinCodeParam.Value = this.GtinCode;
                cmd.Parameters.Add(gtinCodeParam);

                SqlParameter qtyInStockParam = cmd.CreateParameter();
                qtyInStockParam.ParameterName = "@qtyInStock";
                qtyInStockParam.DbType = DbType.Int32;
                qtyInStockParam.Value = this.QtyInStock;
                cmd.Parameters.Add(qtyInStockParam);

                SqlParameter nameParam = cmd.CreateParameter();
                nameParam.ParameterName = "@name";
                nameParam.DbType = DbType.String;
                nameParam.Value = this.Name;
                cmd.Parameters.Add(nameParam);

                SqlParameter descriptionParam = cmd.CreateParameter();
                descriptionParam.ParameterName = "@description";
                descriptionParam.DbType = DbType.String;
                descriptionParam.Value = this.Description;
                cmd.Parameters.Add(descriptionParam);
                */
                #endregion

                connection.Open();
                int affectedRows = cmd.ExecuteNonQuery();

                MessageBox.Show("Data updated!!!");

                // Check that a row has been updated, if not, throw exception (no row with the id
                // value found in the database, thus no update done)
                if (!(affectedRows > 0))
                {
                    throw new DataAccessException($"Failed to update {this.GetType().FullName}: no database entry to update found for Id# {this.Id}.");
                }

                return this;

            }
        }


        #endregion


    }
}
