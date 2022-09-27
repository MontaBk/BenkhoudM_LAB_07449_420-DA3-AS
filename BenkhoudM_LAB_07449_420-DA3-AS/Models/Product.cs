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
    internal class Product 
    {

        private static readonly string DATABASE_TABLE = "[Lab].[dbo].[Product]";

        public int Id { get; protected set; }
        public long GtinCode { get; set; }
        public int QtyInStock { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

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

        public Product(int qtyInStock, string name)
        {
            this.QtyInStock = qtyInStock;
            this.Name = name;
        }

        public Product(long gtinCode, int qtyInStock, string name) : this(qtyInStock, name)
        {
            this.GtinCode = gtinCode;
        }

        public Product(int qtyInStock, string name, string description) : this(qtyInStock, name)
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


        public void Delete()
        {
            using (SqlConnection connection = DbUtilsSimple.GetDefaultConnection())
            {
                this.ExecuteDeleteCommand(connection.CreateCommand());
            }
        }
        public void Delete(SqlTransaction transaction)
        {
            SqlCommand cmd = transaction.Connection.CreateCommand();
            cmd.Transaction = transaction;
            this.ExecuteDeleteCommand(cmd);
        }
        private void ExecuteDeleteCommand(SqlCommand cmd)
        {
            if (!(this.Id > 0))
            {
                // Id has not been set, it is initialized by default at 0;
                throw new DataAccessException($"Cannot use method {this.GetType().FullName}.Delete() : Id value is 0.");
            }

            
            string statement = $"DELETE FROM {DATABASE_TABLE} WHERE id = @id;";
            cmd.CommandText = statement;

            SqlParameter param = cmd.CreateParameter();
            param.ParameterName = "@id";
            param.DbType = DbType.Int32;
            param.Value = this.Id;
            cmd.Parameters.Add(param);

            if(cmd.Connection.State != ConnectionState.Open)
            {
                 cmd.Connection.Open();
            }
            int affectedRows = cmd.ExecuteNonQuery();
            MessageBox.Show("Data deleted!!!");

            if (!(affectedRows > 0))
            {
                // No affected rows: no deletion occured -> row with matching Id not found
                throw new DataAccessException($"Failed to delete {this.GetType().FullName}: no database entry to delete found for Id# {this.Id}.");
            }
            
        }

        public void GetById()
        {
            using (SqlConnection connection = DbUtilsSimple.GetDefaultConnection())
            {
                this.ExecuteGetByIdCommand(connection.CreateCommand());
            }
        }
        public void GetById(SqlTransaction transaction)
        {
            SqlCommand cmd = transaction.Connection.CreateCommand();
            cmd.Transaction = transaction;
            this.ExecuteGetByIdCommand(cmd);
        }

        public Product ExecuteGetByIdCommand(SqlCommand cmd)
        {
            if (!(this.Id > 0))
            {
                // Id has not been set, it is initialized by default at 0;
                throw new DataAccessException("Cannot use method Product.GetById() : Id value is 0.");
            }
            string statement = $"SELECT * FROM {DATABASE_TABLE} WHERE id = @id;";
            cmd.CommandText = statement;

            SqlParameter param = cmd.CreateParameter();
            param.ParameterName = "@id";
            param.DbType = DbType.Int32;
            param.Value = this.Id;
            cmd.Parameters.Add(param);

            if(cmd.Connection.State != ConnectionState.Open)
            {
                cmd.Connection.Open();
            }

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

        public void Insert()
        {
            using (SqlConnection connection = DbUtilsSimple.GetDefaultConnection())
            {
                this.ExecuteInsertCommand(connection.CreateCommand());
            }
        }
        public void Insert(SqlTransaction transaction)
        {
            SqlCommand cmd = transaction.Connection.CreateCommand();
            cmd.Transaction = transaction;
            this.ExecuteInsertCommand(cmd);
        }
        public Product ExecuteInsertCommand(SqlCommand cmd)
        {
            if (this.Id != 0)
            {
                // Id has been set, cannot insert a product with a specific Id without risking
                // to mess up the database.
                throw new DataAccessException($"Cannot use method {this.GetType().FullName}.Insert() : Id value is not 0.");
            }            

            // Create the INSERT statement. We do not pass any Id value since this is insertion
            // and the id is auto-generated by the database on insertion (identity).
            string statement = $"INSERT INTO {DATABASE_TABLE} (gtinCode, qtyInStock, name, description) " +
                $"VALUES (@gtinCode, @qtyInStock, @name, @description); " +
                $"SELECT CAST(SCOPE_IDENTITY() AS int);";

            cmd.CommandText = statement;
            cmd.Parameters.AddWithValue("@gtinCode", GtinCode);
            cmd.Parameters.AddWithValue("@qtyInStock", QtyInStock);
            cmd.Parameters.AddWithValue("@name",Name);
            cmd.Parameters.AddWithValue("@description", Description);
                
            if(cmd.Connection.State != ConnectionState.Open)
            {
                cmd.Connection.Open();
            }
            //this.Id = (Int32)cmd.ExecuteScalar();
            cmd.ExecuteNonQuery();
            MessageBox.Show("Data insered!!!");
            return this;
        }

        public void Update()
        {
            using (SqlConnection connection = DbUtilsSimple.GetDefaultConnection())
            {
                this.ExecuteUpdateCommand(connection.CreateCommand());
            }
        }
        public void Update(SqlTransaction transaction)
        {
            SqlCommand cmd = transaction.Connection.CreateCommand();
            cmd.Transaction = transaction;
            this.ExecuteUpdateCommand(cmd);
        }
        public Product ExecuteUpdateCommand(SqlCommand cmd)
        {
            if (!(this.Id > 0))
            {
                // Id has not been set, cannot update a product with no specific Id to track the correct db row.
                throw new DataAccessException($"Cannot use method {this.GetType().FullName}.Update() : Id value is 0.");
            }

            // Create the Update statement.
            string statement = $"UPDATE {DATABASE_TABLE} SET gtinCode = @gtinCode, " +
                $"qtyInStock = @qtyInStock, " +
                $"name = @name, " +
                $"description = @description " +
                $"WHERE id = @id;";

            cmd.CommandText = statement;
            cmd.Parameters.AddWithValue("@id",Id);
            cmd.Parameters.AddWithValue("@gtinCode", GtinCode);
            cmd.Parameters.AddWithValue("@qtyInStock", QtyInStock);
            cmd.Parameters.AddWithValue("@name", Name);
            cmd.Parameters.AddWithValue("@description", Description);

            if(cmd.Connection.State != ConnectionState.Open)
            {
                cmd.Connection.Open();
            }

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
}
