using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomersApp
{
    public class Connection
    {
        public static SqlConnection getConnection()
        {
            return new SqlConnection("server=LIV_291114\\SQLEXPRESS; database=Northwind; integrated security =true");
        }
    }
}
