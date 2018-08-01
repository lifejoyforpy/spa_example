using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaffManager.Framework.Dapper
{
    public class DapperFactory
    {
        private static readonly string mysqlconnectionString = ConfigurationManager.AppSettings["MySqlConnection"];

        private static readonly string mysqlconnectionString_1 = ConfigurationManager.AppSettings["MySqlConnection_1"];
        public static MySqlConnection CreateMySqlConnection(bool type=true)
        {
            var connection = new MySqlConnection(type==true? mysqlconnectionString: mysqlconnectionString_1);
            connection.Open();
            return connection;
        }
    }
}
