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
    public class PatentSQLDAL : IPatentDAL
    {
        private string _connectionString = @"Data Source=DESKTOP-SL9L2I0\SQLEXPRESS;Initial Catalog=Library;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        public bool AddPatent(Patent patent)
        {
            using (var _connection = new SqlConnection(_connectionString))
            {
                var AddPatentProc = "Patents_AddPatent";
                var AddAuthorProc = "Authors_AddAuthor";
                var AddAuthorIDProc = "AuthorsForResources_AddAuthorID";

                var AddPatentCommand = new SqlCommand(AddPatentProc, _connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                var AddAuthorCommand = new SqlCommand(AddAuthorProc, _connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                var AddAuthorIDCommand = new SqlCommand(AddAuthorIDProc, _connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };

                AddPatentCommand.Parameters.AddWithValue("@Type", "Patent");
                AddPatentCommand.Parameters.AddWithValue("@Id", patent.Id);
                AddPatentCommand.Parameters.AddWithValue("@Name", patent.Name);
                AddPatentCommand.Parameters.AddWithValue("@NumberOfPages", patent.NumberOfPages);
                AddPatentCommand.Parameters.AddWithValue("@Note", patent.Note);

                AddPatentCommand.Parameters.AddWithValue("@Country", patent.Country);
                AddPatentCommand.Parameters.AddWithValue("@RegistrationNumber", patent.RegistrationNumber);
                AddPatentCommand.Parameters.AddWithValue("@DateOfApplication", patent.DateOfApplication);
                AddPatentCommand.Parameters.AddWithValue("@DateOfPublication", patent.DateOfPublication);


                _connection.Open();

                foreach (var author in patent.Inventors)
                {
                    AddAuthorCommand.Parameters.AddWithValue("@AuthorID", author.Id);
                    AddAuthorCommand.Parameters.AddWithValue("@Name", author.Name);
                    AddAuthorCommand.Parameters.AddWithValue("@SurName", author.Surname);

                    AddAuthorCommand.ExecuteNonQuery();

                    AddAuthorCommand.Parameters.Clear();

                    AddAuthorIDCommand.Parameters.AddWithValue("@AuthorID", author.Id);
                    AddAuthorIDCommand.Parameters.AddWithValue("@ResourceID", patent.Id);
                    AddAuthorIDCommand.Parameters.AddWithValue("@Name", author.Name);
                    AddAuthorIDCommand.Parameters.AddWithValue("@SurName", author.Surname);

                    AddAuthorIDCommand.ExecuteNonQuery();

                    AddAuthorIDCommand.Parameters.Clear();
                }

                AddPatentCommand.ExecuteNonQuery();

                _connection.Close();

                return true;
            }
        }

        public bool DeletePatent(Guid id)
        {
            using (var _connection = new SqlConnection(_connectionString))
            {
                var stProc = "Patents_DeletePatent";
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

                _connection.Open();

                command.ExecuteNonQuery();
                DeleteResourceIDProcCommand.ExecuteNonQuery();

                _connection.Close();

                return true;
            }
        }
    }
}
