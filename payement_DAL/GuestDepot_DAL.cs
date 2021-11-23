using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payement.DAL
{
    public class GuestDepot_DAL : Depot_DAL<Guest_DAL>
    {
        public override void Delete(Guest_DAL g)
        {
            CreateConnectionAndCommand();

            command.CommandText = "delete from guests where ID=@ID";
            command.Parameters.Add(new SqlParameter("@ID", g.ID));
            var nombreDeLignesAffectees = (int)command.ExecuteNonQuery();

            if (nombreDeLignesAffectees != 1)
            {
                throw new Exception($"Impossible de supprimer l'invité d'ID {g.ID}");
            }

            DestroyConnectionAndCommand();
        }

        public override List<Guest_DAL> GetAll()
        {
            CreateConnectionAndCommand();

            command.CommandText = "select id, party_id, username, spent from guests";
            var reader = command.ExecuteReader();

            var listGuest= new List<Guest_DAL>();

            while (reader.Read())
            {
                var g = new Guest_DAL(reader.GetInt32(0),
                                reader.GetString(1),
                                reader.GetString(2),
                                reader.GetFloat(3));

                listGuest.Add(g);
            }

            DestroyConnectionAndCommand();

            return listGuest;
        }

        public override Guest_DAL GetByID(int ID)
        {
            CreateConnectionAndCommand();

            command.CommandText = "select id, party_id, username, spent from guests where ID=@ID";
            command.Parameters.Add(new SqlParameter("@ID", ID));
            var reader = command.ExecuteReader();

            Guest_DAL g;
            if (reader.Read())
            {
                g = new Guest_DAL(reader.GetInt32(0),
                        reader.GetString(1),
                        reader.GetString(2),
                        reader.GetFloat(3));
            }
            else
                throw new Exception($"Pas d'invité dans la BDD avec l'ID {ID}");

            DestroyConnectionAndCommand();

            return g;
        }

        public override Guest_DAL Insert(Guest_DAL g)
        {
            CreateConnectionAndCommand();

            command.CommandText = "insert into guests(party_id, username, spent)"
                                    + " values (@party_id, @username, @spent); select scope_identity()";
            command.Parameters.Add(new SqlParameter("@party_id", g.Party_ID_DAL));
            command.Parameters.Add(new SqlParameter("@username", g.Username));
            command.Parameters.Add(new SqlParameter("@spent", g.Spent));

            var ID = Convert.ToInt32((decimal)command.ExecuteScalar());

            g.ID = ID;

            DestroyConnectionAndCommand();

            return g;
        }

        public override Guest_DAL Update(Guest_DAL g)
        {
            CreateConnectionAndCommand();

            command.CommandText = "update guests set party_id=@party_id, username=@username, spent=@spent"
                                    + " where ID=@id";
            command.Parameters.Add(new SqlParameter("@id", g.ID));
            command.Parameters.Add(new SqlParameter("@party_id", g.Party_ID_DAL));
            command.Parameters.Add(new SqlParameter("@username", g.Username));
            command.Parameters.Add(new SqlParameter("@spent", g.Spent));
            var nombreDeLignesAffectees = (int)command.ExecuteNonQuery();

            if (nombreDeLignesAffectees != 1)
            {
                throw new Exception($"Impossible de mettre à jour l'invité d'ID {g.ID}");
            }

            DestroyConnectionAndCommand();

            return g;
        }
    }
}
