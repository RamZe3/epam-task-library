using Epam.Library.DAL.Interfaces;
using Epam.Library.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLDAL
{
    public class UsersSQLDAL : IUserDAL
    {
        private string _connectionString = @"Data Source=DESKTOP-SL9L2I0\SQLEXPRESS;Initial Catalog=Library;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        public bool AddUser(User user)
        {
            using (var _connection = new SqlConnection(_connectionString))
            {
                var stProc = "Users_AddUser";

                var command = new SqlCommand(stProc, _connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("@UserID", user.id);
                command.Parameters.AddWithValue("@UserName", user.Name);
                command.Parameters.AddWithValue("@UserPass", user.Password);

                _connection.Open();

                command.ExecuteNonQuery();

                _connection.Close();

                foreach (var role in user.Roles)
                {
                    AddRole(user.id, role);
                }

                return true;
            }
        }

        public bool DeleteUser(Guid id)
        {
            using (var _connection = new SqlConnection(_connectionString))
            {
                var stProc = "Users_DeleteUser";

                var command = new SqlCommand(stProc, _connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("@UserID", id);

                _connection.Open();

                command.ExecuteNonQuery();


                _connection.Close();

                return true;
            }
        }

        //TODO DELETE
        //public bool UpdateUserRole(Guid id, string role)
        //{
        //    using (var _connection = new SqlConnection(_connectionString))
        //    {
        //        var stProc = "Users_UpdateUserRole";

        //        var command = new SqlCommand(stProc, _connection)
        //        {
        //            CommandType = System.Data.CommandType.StoredProcedure
        //        };

        //        command.Parameters.AddWithValue("@UserID", id);
        //        command.Parameters.AddWithValue("@Role", role);


        //        _connection.Open();

        //        command.ExecuteNonQuery();


        //        _connection.Close();

        //        return true;
        //    }
        //}

        public bool AddRole(Guid id, string role)
        {
            using (var _connection = new SqlConnection(_connectionString))
            {
                var stProc = "RolesForUsers_AddRole";

                var command = new SqlCommand(stProc, _connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("@UserID", id);
                command.Parameters.AddWithValue("@RoleName", role);


                _connection.Open();

                command.ExecuteNonQuery();


                _connection.Close();

                return true;
            }
        }

        public bool DeleteRole(Guid id, string role)
        {
            using (var _connection = new SqlConnection(_connectionString))
            {
                var stProc = "RolesForUsers_DeleteRole";

                var command = new SqlCommand(stProc, _connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("@UserID", id);
                command.Parameters.AddWithValue("@RoleName", role);


                _connection.Open();

                command.ExecuteNonQuery();


                _connection.Close();

                return true;
            }
        }

        public bool DeleteAllRoleForUser(Guid id, string role)
        {
            using (var _connection = new SqlConnection(_connectionString))
            {
                var stProc = "RolesForUsers_DeleteAllRoleForUser";

                var command = new SqlCommand(stProc, _connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("@UserID", id);


                _connection.Open();

                command.ExecuteNonQuery();


                _connection.Close();

                return true;
            }
        }

        public User GetUser(string UserName, string UserPass)
        {
            User user = new User();

            using (var _connection = new SqlConnection(_connectionString))
            {
                var stProc = "Users_GetUser";

                var command = new SqlCommand(stProc, _connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };

                _connection.Open();

                command.Parameters.AddWithValue("@UserName", UserName);
                command.Parameters.AddWithValue("@UserPass", UserPass);

                var reader = command.ExecuteReader();



                while (reader.Read())
                {
                    user = new User(
                        id: (Guid)reader["UserID"],
                        name: reader["UserName"] as string,
                        password: reader["UserPass"] as string);
                }

                reader.Close();

                var stProcRoles = "RolesForUsers_GetRoles";

                var commandRoles = new SqlCommand(stProcRoles, _connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };

                commandRoles.Parameters.AddWithValue("@UserID", user.id);

                var readerRoles = commandRoles.ExecuteReader();

                

                while (readerRoles.Read())
                {
                    user.Roles.Add(readerRoles["RoleName"] as string);
                }

                _connection.Close();

                return user;
            }
        }

        public List<User> GetUsers()
        {
            List<User> users = new List<User>();

            using (var _connection = new SqlConnection(_connectionString))
            {
                var stProc = "Users_GetUsers";

                var command = new SqlCommand(stProc, _connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };

                _connection.Open();

                var reader = command.ExecuteReader();



                while (reader.Read())
                {
                    users.Add(new User(
                        id: (Guid)reader["UserID"],
                        name: reader["UserName"] as string,
                        password: reader["UserPass"] as string));
                }

                reader.Close();

                var stProcRoles = "RolesForUsers_GetRoles";

                var commandRoles = new SqlCommand(stProcRoles, _connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };

                for (int i = 0; i < users.Count; i++)
                {
                    commandRoles.Parameters.AddWithValue("@UserID", users[i].id);
                    var readerRoles = commandRoles.ExecuteReader();
                    while (readerRoles.Read())
                    {
                        users[i].Roles.Add(readerRoles["RoleName"] as string);
                    }

                    readerRoles.Close();
                    commandRoles.Parameters.Clear();
                }

                _connection.Close();

                return users;
            }
        }
    }
}
