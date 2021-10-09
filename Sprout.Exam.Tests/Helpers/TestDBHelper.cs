using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace Sprout.Exam.Tests.Helpers
{
    public sealed class TestDBHelper : IDisposable
    {
        private IConfiguration Config;
        private readonly SqlConnection Connection;

        public TestDBHelper(IConfiguration config)
        {
            Config = config;
            Connection = new SqlConnection(Config.GetConnectionString("DefaultConnection"));
            Connection.Open();
        }
       public void TruncateTable(string tableName)
        {
            var query = $"TRUNCATE TABLE {tableName}";
            Connection.Execute(query);
        }

        public void Dispose()
        {
            if(Connection != null)
            {
                if (Connection.State == System.Data.ConnectionState.Open)
                    Connection.Close();
                Connection.Dispose();
            }
        }
    }
}
