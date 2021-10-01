using Epam.Library.DAL.Interfaces;
using Epam.Library.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Library.SQLDAL
{
    public class BookSQLDAL : IBookDAL
    {
        private string _connectionString = @"Data Source=DESKTOP-SL9L2I0\SQLEXPRESS;Initial Catalog=Library;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        public bool AddBook(Book book)
        {
            //using (var _connection = new SqlConnection(_connectionString))
            //{
            //    var query = "INSERT INTO dbo.Resouces(Type, ID, Name, NumberOfPages, Note) " +
            //        "VALUES(@Type, @ID, @Name, @NumberOfPages, @Note)";
            //    var command = new SqlCommand(query, _connection);

            //    command.Parameters.AddWithValue("@Type", "Book");
            //    command.Parameters.AddWithValue("@Id", book.Id);
            //    command.Parameters.AddWithValue("@Name", book.Name);
            //    command.Parameters.AddWithValue("@NumberOfPages", book.NumberOfPages);
            //    command.Parameters.AddWithValue("@Note", book.Note);

            //    _connection.Open();

            //    command.ExecuteNonQuery();

            //    _connection.Close();

            //    return true;
            //}

            using (var _connection = new SqlConnection(_connectionString))
            {
                var stProc = "Books_AddBook";
                var AddAuthorProc = "Authors_AddAuthor";
                var AddAuthorIDProc = "AuthorsForResources_AddAuthorID";
                var AddAuthorCommand = new SqlCommand(AddAuthorProc, _connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                var AddAuthorIDCommand = new SqlCommand(AddAuthorIDProc, _connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };

                var command = new SqlCommand(stProc, _connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("@Type", "Book");
                command.Parameters.AddWithValue("@Id", book.Id);
                command.Parameters.AddWithValue("@Name", book.Name);
                command.Parameters.AddWithValue("@NumberOfPages", book.NumberOfPages);
                command.Parameters.AddWithValue("@Note", book.Note);

                command.Parameters.AddWithValue("@PlaceOfPublication", book.PlaceOfPublication);
                command.Parameters.AddWithValue("@Publisher", book.Publisher);
                command.Parameters.AddWithValue("@YearOfPublishing", new DateTime(book.YearOfPublishing, 1, 1));
                command.Parameters.AddWithValue("@ISBN", book.ISBN);

                _connection.Open();

                foreach (var author in book.Authors)
                {
                    AddAuthorCommand.Parameters.AddWithValue("@AuthorID", author.Id);
                    AddAuthorCommand.Parameters.AddWithValue("@Name", author.Name);
                    AddAuthorCommand.Parameters.AddWithValue("@SurName", author.Surname);

                    AddAuthorCommand.ExecuteNonQuery();

                    AddAuthorCommand.Parameters.Clear();

                    AddAuthorIDCommand.Parameters.AddWithValue("@AuthorID", author.Id);
                    AddAuthorIDCommand.Parameters.AddWithValue("@ResourceID", book.Id);
                    AddAuthorIDCommand.Parameters.AddWithValue("@Name", author.Name);
                    AddAuthorIDCommand.Parameters.AddWithValue("@SurName", author.Surname);

                    AddAuthorIDCommand.ExecuteNonQuery();

                    AddAuthorIDCommand.Parameters.Clear();
                }

                command.ExecuteNonQuery();

                _connection.Close();

                return true;
            }
        }

        public bool DeleteBook(Guid id)
        {
            using (var _connection = new SqlConnection(_connectionString))
            {
                var stProc = "Books_DeleteBook";
                var DeleteResourceIDProc = "AuthorsID_DeleteResourceID";

                var command = new SqlCommand(stProc, _connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };

                var DeleteResourceIDProcCommand = new SqlCommand(DeleteResourceIDProc, _connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("@ID", id);

                DeleteResourceIDProcCommand.Parameters.AddWithValue("@ResourceID", id);

                _connection.Open();

                command.ExecuteNonQuery();
                DeleteResourceIDProcCommand.ExecuteNonQuery();

                _connection.Close();

                return true;
            }
        }
    }
}
