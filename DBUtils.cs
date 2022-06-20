using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Makar
{
    internal class DBUtils
    {
        public static MySqlConnection GetDBConnection()
        {
            string host = "192.168.4.151";
            int port = 3311;
            string database = "user1_db";
            string user = "user1";
            string password = "intel";

            return DBMySQLUtils.GetDBConnection(host, port, database, user, password);
        }
    }
}
