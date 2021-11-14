using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomersApp
{
    class CustomersApp
    {
        public static List<Customer> Customers = new List<Customer>();
        static void Main(string[] args)
        {
            GetCustomers();
            insertMassiveData();
        }

        public static void insertMassiveData()
        {
            //create table
            var table = new DataTable();
            table.Columns.Add("Id", typeof(string));
            table.Columns.Add("Name", typeof(string));
            table.Columns.Add("Address", typeof(string));
            table.Columns.Add("City", typeof(string));
            table.Columns.Add("Country", typeof(string));
            table.Columns.Add("PostalCode", typeof(string));
            table.Columns.Add("Phone", typeof(string));
            foreach (var item in Customers)
            {
                table.Rows.Add(new object[]
                    {
                        item.Id,
                        item.Name,
                        item.Address,
                        item.City,
                        item.Country,
                        item.PostalCode,
                        item.Phone
                    });
            }
            //insert to db
            using (var connection = Connection.getConnection())
            {
                connection.Open();
                using (SqlTransaction transaction = connection.BeginTransaction())
                {
                    using (SqlBulkCopy bulkCopy = new SqlBulkCopy(connection, SqlBulkCopyOptions.Default, transaction))
                    {
                        try
                        {
                            bulkCopy.DestinationTableName = "CustomersPrueba";
                            bulkCopy.WriteToServer(table);
                            transaction.Commit();
                        }
                        catch (Exception)
                        {
                            transaction.Rollback();
                            connection.Close();
                            throw;
                        }
                    }
                }
            }
        }
        public static void GetCustomers()
        {
            var reader = new StreamReader(File.OpenRead("../Customers.csv"));
            bool primeraLinea = true;
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                var values = line.Split(';');

                if (!primeraLinea)
                {
                    Customer customer = new Customer()
                    {
                        Id = values[0],
                        Name = values[1],
                        Address = values[2],
                        City = values[3],
                        Country = values[4],
                        PostalCode = values[5],
                        Phone = values[6]
                    };

                    Customers.Add(customer);
                }
                primeraLinea = false;
            }
        }


    }
}
