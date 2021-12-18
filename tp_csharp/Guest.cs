using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payement
{
    public class Guest
    {
        public int ID { get; set; }
        public int Party_ID { get; set; }
        public string Username { get; set; }
        public float Spent { get; set; }


        public Guest(int party_id, string username, float spent)
        {
            Party_ID = party_id;
            Username = username;
            Spent = spent;
        }

        public Guest(int id, int party_id, string username, float spent)
        {
            ID = id;
            Party_ID = party_id;
            Username = username;
            Spent = spent;
        }

        public override string ToString()
        {
            return $"ID: {this.ID}, Party_ID: {this.Party_ID}, Username: {this.Username}, Spent: {this.Spent}";
        }
    }
}
