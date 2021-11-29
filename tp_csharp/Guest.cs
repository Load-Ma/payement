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
        public string Party_ID { get; set; }
        public string Username { get; set; }
        public string Spent { get; set; }


        public Guest(string party_id, string username, string spent)
        {
            Party_ID = party_id;
            Username = username;
            Spent = spent;
        }

        public Guest(int id, string party_id, string username, string spent)
        {
            ID = id;
            Party_ID = party_id;
            Username = username;
            Spent = spent;
        }
    }
}
