using Epam.Library.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLDAL
{
    public class AuthorsForResourcesSQLDAL
    {
        private string _connectionString = @"Data Source=DESKTOP-SL9L2I0\SQLEXPRESS;Initial Catalog=Library;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        public bool AddResourceIDWithAuthorID(InformationResource resource, Author author)
        {
            using (var _connection = new SqlConnection(_connectionString))
            {
                var AddAuthorIDProc = "AuthorsForResources_AddAuthorID";
                var AddAuthorIDCommand = new SqlCommand(AddAuthorIDProc, _connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };

                AddAuthorIDCommand.Parameters.AddWithValue("@AuthorID", author.Id);
                AddAuthorIDCommand.Parameters.AddWithValue("@ResourceID", resource.Id);
                AddAuthorIDCommand.Parameters.AddWithValue("@Name", author.Name);
                AddAuthorIDCommand.Parameters.AddWithValue("@SurName", author.Surname);

                _connection.Open();


                AddAuthorIDCommand.ExecuteNonQuery();

                _connection.Close();

                return true;
            }
        }

        public bool UpdateResourceIDWithAuthorID(Guid id)
        {
            using (var _connection = new SqlConnection(_connectionString))
            {
                var AddAuthorIDProc = "AuthorsForResources_UpdateAuthorID";
                var AddAuthorIDCommand = new SqlCommand(AddAuthorIDProc, _connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };

                AddAuthorIDCommand.Parameters.AddWithValue("@AuthorID", id);

                _connection.Open();


                AddAuthorIDCommand.ExecuteNonQuery();

                _connection.Close();

                return true;
            }
        }

        public bool DeleteResourceIDWithAuthorID(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
