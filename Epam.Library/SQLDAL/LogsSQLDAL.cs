using Epam.Library.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLDAL
{
    public class LogsSQLDAL
    {
        private string _connectionString = @"Data Source=DESKTOP-SL9L2I0\SQLEXPRESS;Initial Catalog=Library;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        public bool AddLog(InformationResource resource, User user, string description)
        {
            using (var _connection = new SqlConnection(_connectionString))
            {
                var stProc = "Logs_AddLog";

                var command = new SqlCommand(stProc, _connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };

                string type = "";
                if (resource is Book)
                {
                    type = "Book";
                }
                else if (resource is Paper)
                {
                    type = "Paper";
                }
                else if (resource is Patent)
                {
                    type = "Patent";
                }

                command.Parameters.AddWithValue("@ResourceID", resource.Id);
                command.Parameters.AddWithValue("@Description", description);
                command.Parameters.AddWithValue("@UserName", user.Name);
                command.Parameters.AddWithValue("@Type", type);

                _connection.Open();

                command.ExecuteNonQuery();


                _connection.Close();

                return true;
            }
        }

        public bool AddLog(Guid resourceID, string type, User user, string description)
        {
            using (var _connection = new SqlConnection(_connectionString))
            {
                var stProc = "Logs_AddLog";

                var command = new SqlCommand(stProc, _connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("@ResourceID", resourceID);
                command.Parameters.AddWithValue("@Description", description);
                command.Parameters.AddWithValue("@UserName", user.Name);
                command.Parameters.AddWithValue("@Type", type);


                _connection.Open();

                command.ExecuteNonQuery();


                _connection.Close();

                return true;
            }
        }
    }
}
