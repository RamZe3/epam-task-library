using Epam.Library.DAL.Interfaces;
using Epam.Library.Entities;
using SQLDAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Library.SQLDAL
{
    public class BookSQLDAL : IBookDAL
    {
        private string _connectionString = @"Data Source=DESKTOP-SL9L2I0\SQLEXPRESS;Initial Catalog=Library;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        private AuthorsForResourcesSQLDAL authorsForResources = new AuthorsForResourcesSQLDAL();
        private AuthorSQLDAL authorSQLDAL = new AuthorSQLDAL();
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
                SqlParameter sinceDateTimeParam = new SqlParameter("@YearOfPublishing", SqlDbType.Date);
                sinceDateTimeParam.Value = new DateTime(book.YearOfPublishing, 1, 1);
                command.Parameters.Add(sinceDateTimeParam);
                //command.Parameters.AddWithValue("@YearOfPublishing", new DateTime(book.YearOfPublishing, 1, 1));
                command.Parameters.AddWithValue("@ISBN", book.ISBN);

                

                foreach (var author in book.Authors)
                {
                    authorSQLDAL.AddAuthor(author);
                    authorsForResources.AddResourceIDWithAuthorID(book, author);
                }

                _connection.Open();

                command.ExecuteNonQuery();

                _connection.Close();

                return true;
            }
        }

        public bool UpdateBook(Book book)
        {
            using (var _connection = new SqlConnection(_connectionString))
            {
                var stProc = "Books_UpdateBook";

                var command = new SqlCommand(stProc, _connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("@Id", book.Id);
                command.Parameters.AddWithValue("@Name", book.Name);
                command.Parameters.AddWithValue("@NumberOfPages", book.NumberOfPages);
                command.Parameters.AddWithValue("@Note", book.Note);

                command.Parameters.AddWithValue("@PlaceOfPublication", book.PlaceOfPublication);
                command.Parameters.AddWithValue("@Publisher", book.Publisher);
                SqlParameter sinceDateTimeParam = new SqlParameter("@YearOfPublishing", SqlDbType.Date);
                sinceDateTimeParam.Value = new DateTime(book.YearOfPublishing, 1, 1);
                command.Parameters.Add(sinceDateTimeParam);
                //command.Parameters.AddWithValue("@YearOfPublishing", new DateTime(book.YearOfPublishing, 1, 1));
                command.Parameters.AddWithValue("@ISBN", book.ISBN);



                //foreach (var author in book.Authors)
                //{
                //    authorSQLDAL.AddAuthor(author);
                //    authorsForResources.AddResourceIDWithAuthorID(book, author);
                //}

                _connection.Open();

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

                var command = new SqlCommand(stProc, _connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("@ID", id);

                authorsForResources.DeleteResourceIDWithAuthorID(id);

                _connection.Open();

                command.ExecuteNonQuery();
                

                _connection.Close();

                return true;
            }
        }
    }
}
