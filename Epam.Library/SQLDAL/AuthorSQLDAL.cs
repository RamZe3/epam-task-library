using Epam.Library.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLDAL
{
    public class AuthorSQLDAL
    {
        private SqlConnection _connection;
        private SqlTransaction transaction;

        public AuthorSQLDAL(SqlConnection connection)
        {
            _connection = connection;
        }

        public AuthorSQLDAL(SqlConnection connection, SqlTransaction transaction) : this(connection)
        {
            this.transaction = transaction;
        }

        public AuthorSQLDAL()
        {
        }

        public bool AddAuthor(Author author)
        {

                var AddAuthorProc = "Authors_AddAuthor";

                var AddAuthorCommand = new SqlCommand(AddAuthorProc, _connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };

                AddAuthorCommand.Parameters.AddWithValue("@AuthorID", author.Id);
                AddAuthorCommand.Parameters.AddWithValue("@Name", author.Name);
                AddAuthorCommand.Parameters.AddWithValue("@SurName", author.Surname);

            AddAuthorCommand.Transaction = transaction;

            AddAuthorCommand.ExecuteNonQuery();


                return true;

        }

        public bool UpdateAuthor(Author author)
        {
                var AddAuthorProc = "Authors_UpdateAuthor";

                var UpdateAuthorCommand = new SqlCommand(AddAuthorProc, _connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };

                UpdateAuthorCommand.Parameters.AddWithValue("@AuthorID", author.Id);
                UpdateAuthorCommand.Parameters.AddWithValue("@Name", author.Name);
                UpdateAuthorCommand.Parameters.AddWithValue("@SurName", author.Surname);

                UpdateAuthorCommand.Transaction = transaction;
                //_connection.Open();

                UpdateAuthorCommand.ExecuteNonQuery();

                //_connection.Close();

                return true;
        }

        public List<Author> GetAuthors()
        {
            string _connectionString = @"Data Source=DESKTOP-SL9L2I0\SQLEXPRESS;Initial Catalog=Library;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            List<Author> authors = new List<Author>();

            using (var _connection = new SqlConnection(_connectionString))
            {
                var stProc = "Authors_GetAllAuthors";

                var command = new SqlCommand(stProc, _connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };

                _connection.Open();

                var reader = command.ExecuteReader();



                while (reader.Read())
                {
                    Guid id = (Guid)reader["AuthorID"];
                    Author author = new Author(
                        id: (Guid)reader["AuthorID"],
                        name: reader["Name"] as string,
                        surname: reader["SurName"] as string
                    );
                    authors.Add(author);
                }


                _connection.Close();

                return authors;
            }
        }

        public bool AddAuthorWithoutTran(Author author)
        {
            string _connectionString = @"Data Source=DESKTOP-SL9L2I0\SQLEXPRESS;Initial Catalog=Library;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            using (var _connection = new SqlConnection(_connectionString))
            {
                var AddAuthorProc = "Authors_AddAuthor";

                var AddAuthorCommand = new SqlCommand(AddAuthorProc, _connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };

                AddAuthorCommand.Parameters.AddWithValue("@AuthorID", author.Id);
                AddAuthorCommand.Parameters.AddWithValue("@Name", author.Name);
                AddAuthorCommand.Parameters.AddWithValue("@SurName", author.Surname);

                
                _connection.Open();

                AddAuthorCommand.ExecuteNonQuery();

                _connection.Close();
            }
            return true;

        }

        public bool DeleteResourceID(Guid id)
        {
            var DeleteResourceID = "AuthorsForResources_DeleteResourceID";

            var AddAuthorCommand = new SqlCommand(DeleteResourceID, _connection)
            {
                CommandType = System.Data.CommandType.StoredProcedure
            };

            AddAuthorCommand.Parameters.AddWithValue("@ResourceID", id);

            AddAuthorCommand.Transaction = transaction;

            AddAuthorCommand.ExecuteNonQuery();


            return true;
        }
    }
}
