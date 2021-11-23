using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payement.DAL
{
    public class UserDepot_DAL : Depot_DAL<User_DAL>
    {
        public override void Delete(User_DAL u)
        {
            CreateConnectionAndCommand();

            command.CommandText = "delete from users where ID=@ID";
            command.Parameters.Add(new SqlParameter("@ID", u.ID));
            var nombreDeLignesAffectees = (int)command.ExecuteNonQuery();

            if (nombreDeLignesAffectees != 1)
            {
                throw new Exception($"Impossible de supprimer l'utilisateur d'ID {u.ID}");
            }

            DestroyConnectionAndCommand();
        }

        public override List<User_DAL> GetAll()
        {
            CreateConnectionAndCommand();

            command.CommandText = "select id, username, email, password from users";
            var reader = command.ExecuteReader();

            var listUser = new List<User_DAL>();

            while (reader.Read())
            {
                var u = new User_DAL(reader.GetInt32(0),
                                reader.GetString(1),
                                reader.GetString(2),
                                reader.GetString(3));

                listUser.Add(u);
            }

            DestroyConnectionAndCommand();

            return listUser;
        }

        public override User_DAL GetByID(int ID)
        {
            CreateConnectionAndCommand();

            command.CommandText = "select id, username, email, password from users where ID=@ID";
            command.Parameters.Add(new SqlParameter("@ID", ID));
            var reader = command.ExecuteReader();

            User_DAL u;
            if (reader.Read())
            {
                u = new User_DAL(reader.GetInt32(0),
                        reader.GetString(1),
                        reader.GetString(2),
                        reader.GetString(3));
            }
            else
                throw new Exception($"Pas d'utilisateur dans la BDD avec l'ID {ID}");

            DestroyConnectionAndCommand();

            return u;
        }

        public override User_DAL Insert(User_DAL u)
        {
            CreateConnectionAndCommand();

            command.CommandText = "insert into users(username, email, password )"
                                    + " values (@username, @email, @password); select scope_identity()";
            command.Parameters.Add(new SqlParameter("@societe", u.Username));
            command.Parameters.Add(new SqlParameter("@email", u.Email));
            command.Parameters.Add(new SqlParameter("@adresse", u.Password));

            var ID = Convert.ToInt32((decimal)command.ExecuteScalar());

            u.ID = ID;

            DestroyConnectionAndCommand();

            return u;
        }

        public override User_DAL Update(User_DAL u)
        {
            CreateConnectionAndCommand();

            command.CommandText = "update users set email=@email, username=@username, password=@password"
                                    + " where ID=@id";
            command.Parameters.Add(new SqlParameter("@id", u.ID));
            command.Parameters.Add(new SqlParameter("@email", u.Email));
            command.Parameters.Add(new SqlParameter("@username", u.Username));
            command.Parameters.Add(new SqlParameter("@password", u.Password));
            var nombreDeLignesAffectees = (int)command.ExecuteNonQuery();

            if (nombreDeLignesAffectees != 1)
            {
                throw new Exception($"Impossible de mettre à jour l'utilisateurs d'ID {u.ID}");
            }

            DestroyConnectionAndCommand();

            return u;
        }
    }
}
