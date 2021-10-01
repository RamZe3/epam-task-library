using Epam.Library.DAL.Interfaces;
using Epam.Library.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Library.SQLDAL
{
    public class PaperSQLDAL : IPaperDAL
    {
        private string _connectionString = @"Data Source=DESKTOP-SL9L2I0\SQLEXPRESS;Initial Catalog=Library;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        public bool AddPaper(Paper paper)
        {
            using (var _connection = new SqlConnection(_connectionString))
            {
                var stProc = "Papers_AddPaper";

                var command = new SqlCommand(stProc, _connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("@Type", "Paper");
                command.Parameters.AddWithValue("@Id", paper.Id);
                command.Parameters.AddWithValue("@Name", paper.Name);
                command.Parameters.AddWithValue("@NumberOfPages", paper.NumberOfPages);
                command.Parameters.AddWithValue("@Note", paper.Note);

                command.Parameters.AddWithValue("@PlaceOfPublication", paper.PlaceOfPublication);
                command.Parameters.AddWithValue("@Publisher", paper.Publisher);
                command.Parameters.AddWithValue("@YearOfPublishing", new DateTime(paper.YearOfPublishing, 1, 1));
                command.Parameters.AddWithValue("@Number", paper.Number);
                SqlParameter sinceDateTimeParam = new SqlParameter("@Date", SqlDbType.Date);
                sinceDateTimeParam.Value = paper.Date;
                command.Parameters.Add(sinceDateTimeParam);
                //command.Parameters.AddWithValue("@Date", sinceDateTimeParam);
                command.Parameters.AddWithValue("@ISSN", paper.Number);

                _connection.Open();

                command.ExecuteNonQuery();

                _connection.Close();

                return true;
            }
        }

        public bool DeletePaper(Guid id)
        {
            using (var _connection = new SqlConnection(_connectionString))
            {
                var stProc = "Papers_DeletePaper";

                var command = new SqlCommand(stProc, _connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("@ID", id);

                _connection.Open();

                command.ExecuteNonQuery();

                _connection.Close();

                return true;
            }
        }
    }
}
