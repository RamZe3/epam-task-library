﻿using Epam.Library.Entities;
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
        private string _connectionString = @"Data Source=DESKTOP-SL9L2I0\SQLEXPRESS;Initial Catalog=Library;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        public bool AddAuthor(Author author)
        {
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

                return true;
            }

        }

        public bool UpdateAuthor(Author author)
        {
            using (var _connection = new SqlConnection(_connectionString))
            {
                var AddAuthorProc = "Authors_UpdateAuthor";

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

                return true;
            }
        }

        public bool DeleteBook(Guid id)
        {
           throw new  NotImplementedException();
        }
    }
}