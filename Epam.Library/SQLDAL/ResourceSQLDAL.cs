using Epam.Library.DAL.Interfaces;
using Epam.Library.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLDAL
{
    public class ResourceSQLDAL : IResourceDAL
    {
        private string _connectionString = @"Data Source=DESKTOP-SL9L2I0\SQLEXPRESS;Initial Catalog=Library;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        public bool UpdateResourceStatus(Guid id, string status)
        {
            using (var _connection = new SqlConnection(_connectionString))
            {
                var stProc = "Resources_UpdateResourceStatus";

                var command = new SqlCommand(stProc, _connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("@ID", id);
                command.Parameters.AddWithValue("@Status", status);


                _connection.Open();

                command.ExecuteNonQuery();


                _connection.Close();

                return true;
            }
        }
    }
}
