using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payement.DAL
{
    public class PartyDepot_DAL : Depot_DAL<Party_DAL>
    {
        public override void Delete(int id)
        {
            CreateConnectionAndCommand();

            command.CommandText = "delete from parties where ID=@ID";
            command.Parameters.Add(new SqlParameter("@ID", id));
            var nombreDeLignesAffectees = (int)command.ExecuteNonQuery();

            if (nombreDeLignesAffectees != 1)
            {
                throw new Exception($"Impossible de supprimer la soirée d'ID {id}");
            }

            DestroyConnectionAndCommand();
        }

        public override List<Party_DAL> GetAll()
        {
            CreateConnectionAndCommand();

            command.CommandText = "select id, name from parties";
            var reader = command.ExecuteReader();

            var listParty = new List<Party_DAL>();

            while (reader.Read())
            {
                var p = new Party_DAL(reader.GetInt32(0),
                                reader.GetString(1));

                listParty.Add(p);
            }

            DestroyConnectionAndCommand();

            return listParty;
        }

        public override Party_DAL GetByID(int ID)
        {

            CreateConnectionAndCommand();

            command.CommandText = "select id, name from parties where ID=@ID";
            command.Parameters.Add(new SqlParameter("@ID", ID));
            var reader = command.ExecuteReader();

            Party_DAL p;
            if (reader.Read())
            {
                p = new Party_DAL(reader.GetInt32(0),
                        reader.GetString(1));
            }
            else
                throw new Exception($"Pas de soirée dans la BDD avec l'ID {ID}");

            DestroyConnectionAndCommand();

            return p;
        }

        public override Party_DAL Insert(Party_DAL p)
        {
            CreateConnectionAndCommand();

            command.CommandText = "insert into parties(name)"
                                    + " values (@name); select scope_identity()";
            command.Parameters.Add(new SqlParameter("@name", p.Name));

            var ID = Convert.ToInt32((decimal)command.ExecuteScalar());

            p.ID = ID;

            DestroyConnectionAndCommand();

            return p;
        }

        public override Party_DAL Update(Party_DAL p)
        {
            CreateConnectionAndCommand();

            command.CommandText = "update parties set name=@name"
                                    + " where ID=@id";
            command.Parameters.Add(new SqlParameter("@id", p.ID));
            command.Parameters.Add(new SqlParameter("@name", p.Name));
            var nombreDeLignesAffectees = (int)command.ExecuteNonQuery();

            if (nombreDeLignesAffectees != 1)
            {
                throw new Exception($"Impossible de mettre à jour la partie d'ID {p.ID}");
            }

            DestroyConnectionAndCommand();

            return p;
        }
    }
}
