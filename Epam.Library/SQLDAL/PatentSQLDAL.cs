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
    public class PatentSQLDAL : IPatentDAL
    {
        private string _connectionString = @"Data Source=DESKTOP-SL9L2I0\SQLEXPRESS;Initial Catalog=Library;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        private AuthorsForResourcesSQLDAL authorsForResources = new AuthorsForResourcesSQLDAL();
        private AuthorSQLDAL authorSQLDAL = new AuthorSQLDAL();

        public bool AddPatent(Patent patent)
        {
            using (var _connection = new SqlConnection(_connectionString))
            {
                var AddPatentProc = "Patents_AddPatent";

                var AddPatentCommand = new SqlCommand(AddPatentProc, _connection)
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
                SqlParameter sinceDateTimeParam = new SqlParameter("@DateOfApplication", SqlDbType.Date);
                sinceDateTimeParam.Value = patent.DateOfApplication;
                AddPatentCommand.Parameters.Add(sinceDateTimeParam);
                //AddPatentCommand.Parameters.AddWithValue("@DateOfApplication", patent.DateOfApplication);
                SqlParameter sinceDateTimeParam1 = new SqlParameter("@DateOfPublication", SqlDbType.Date);
                sinceDateTimeParam1.Value = patent.DateOfPublication;
                AddPatentCommand.Parameters.Add(sinceDateTimeParam1);
                //AddPatentCommand.Parameters.AddWithValue("@DateOfPublication", patent.DateOfPublication);


                

                foreach (var author in patent.Inventors)
                {
                    authorSQLDAL.AddAuthor(author);
                    authorsForResources.AddResourceIDWithAuthorID(patent, author);
                }

                _connection.Open();

                AddPatentCommand.ExecuteNonQuery();

                _connection.Close();

                return true;
            }
        }

        public bool UpdatePatent(Patent patent)
        {
            using (var _connection = new SqlConnection(_connectionString))
            {
                var AddPatentProc = "Patents_UpdatePatent";

                var AddPatentCommand = new SqlCommand(AddPatentProc, _connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };

                AddPatentCommand.Parameters.AddWithValue("@Id", patent.Id);
                AddPatentCommand.Parameters.AddWithValue("@Name", patent.Name);
                AddPatentCommand.Parameters.AddWithValue("@NumberOfPages", patent.NumberOfPages);
                AddPatentCommand.Parameters.AddWithValue("@Note", patent.Note);

                AddPatentCommand.Parameters.AddWithValue("@Country", patent.Country);
                AddPatentCommand.Parameters.AddWithValue("@RegistrationNumber", patent.RegistrationNumber);
                SqlParameter sinceDateTimeParam = new SqlParameter("@DateOfApplication", SqlDbType.Date);
                sinceDateTimeParam.Value = patent.DateOfApplication;
                AddPatentCommand.Parameters.Add(sinceDateTimeParam);
                //AddPatentCommand.Parameters.AddWithValue("@DateOfApplication", patent.DateOfApplication);
                SqlParameter sinceDateTimeParam1 = new SqlParameter("@DateOfPublication", SqlDbType.Date);
                sinceDateTimeParam.Value = patent.DateOfPublication;
                AddPatentCommand.Parameters.Add(sinceDateTimeParam1);
                //AddPatentCommand.Parameters.AddWithValue("@DateOfPublication", patent.DateOfPublication);




                //foreach (var author in patent.Inventors)
                //{
                //    authorSQLDAL.AddAuthor(author);
                //    authorsForResources.AddResourceIDWithAuthorID(patent, author);
                //}

                _connection.Open();

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
