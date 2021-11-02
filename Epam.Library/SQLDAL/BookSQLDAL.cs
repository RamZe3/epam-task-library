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

        public bool AddBook(Book book)
        {
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

                _connection.Open();

                SqlTransaction transaction = _connection.BeginTransaction("Add Book");
                AuthorSQLDAL authorSQLDAL = new AuthorSQLDAL(_connection, transaction);
                AuthorsForResourcesSQLDAL authorsForResources = new AuthorsForResourcesSQLDAL(_connection, transaction);
                command.Transaction = transaction;

                try
                {
                    command.ExecuteNonQuery();

                    foreach (var author in book.Authors)
                    {
                        authorSQLDAL.AddAuthor(author);
                        authorsForResources.AddResourceIDWithAuthorID(book, author);
                    }

                    transaction.Commit();
                }
                catch(Exception ex)
                {
                    transaction.Rollback();
                }

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

                _connection.Open();

                SqlTransaction transaction = _connection.BeginTransaction("Update Book");
                AuthorSQLDAL authorSQLDAL = new AuthorSQLDAL(_connection, transaction);
                AuthorsForResourcesSQLDAL authorsForResources = new AuthorsForResourcesSQLDAL(_connection, transaction);
                command.Transaction = transaction;

                try
                {
                    command.ExecuteNonQuery();

                    authorSQLDAL.DeleteResourceID(book.Id);

                    foreach (var author in book.Authors)
                    {
                        authorSQLDAL.AddAuthor(author);
                        authorsForResources.AddResourceIDWithAuthorID(book, author);
                        //authorSQLDAL.UpdateAuthor(author);
                        //authorsForResources.AddResourceIDWithAuthorID(book, author);
                    }
                    transaction.Commit();
                }
                catch(Exception ex)
                {
                    transaction.Rollback();
                }

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

                //authorsForResources.DeleteResourceIDWithAuthorID(id);

                _connection.Open();

                command.ExecuteNonQuery();
                

                _connection.Close();

                return true;
            }
        }
    }
}
