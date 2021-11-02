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
        private SqlConnection _connection;
        private SqlTransaction transaction;

        public AuthorsForResourcesSQLDAL(SqlConnection connection, SqlTransaction transaction)
        {
            _connection = connection;
            this.transaction = transaction;
        }

        public AuthorsForResourcesSQLDAL()
        {
        }

        public bool AddResourceIDWithAuthorID(InformationResource resource, Author author)
        {
                var AddAuthorIDProc = "AuthorsForResources_AddAuthorIDForResourceID";
                var AddAuthorIDCommand = new SqlCommand(AddAuthorIDProc, _connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };

                AddAuthorIDCommand.Parameters.AddWithValue("@AuthorID", author.Id);
                AddAuthorIDCommand.Parameters.AddWithValue("@ResourceID", resource.Id);
                AddAuthorIDCommand.Parameters.AddWithValue("@Name", author.Name);
                AddAuthorIDCommand.Parameters.AddWithValue("@SurName", author.Surname);

            //_connection.Open();
                AddAuthorIDCommand.Transaction = transaction;

                AddAuthorIDCommand.ExecuteNonQuery();

                //_connection.Close();

                return true;
        }

        public bool UpdateResourceIDWithAuthorID(Guid id)
        {
                var AddAuthorIDProc = "AuthorsForResources_UpdateAuthorID";
                var AddAuthorIDCommand = new SqlCommand(AddAuthorIDProc, _connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };

                AddAuthorIDCommand.Parameters.AddWithValue("@AuthorID", id);

                //_connection.Open();
                AddAuthorIDCommand.Transaction = transaction;

                AddAuthorIDCommand.ExecuteNonQuery();

               // AddAuthorIDCommand.ExecuteNonQuery();

                //_connection.Close();

                return true;
        }

        public bool DeleteResourceIDWithAuthorID(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
