using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payement.DAL
{
    public class User_DAL
    {
        public int ID { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public User_DAL(string username, string email) => (Username, Email) = (username, email);

        public User_DAL(int id, string username, string email, string password)
            => (ID, Username, Email, Password) = (id, username, email, password);

        /*internal void Insert(SqlConnection connection)
        {
            using (var command = new SqlCommand())
            {
                command.Connection = connection;
                command.CommandText = "insert into users(username, email, password)"
                    + " values (@username, @email, @password)";
                command.Parameters.Add(new SqlParameter("@username", Username));
                command.Parameters.Add(new SqlParameter("@email", Email));
                command.Parameters.Add(new SqlParameter("@password", Password));
                command.ExecuteNonQuery();
            }
        }*/

    }
}
