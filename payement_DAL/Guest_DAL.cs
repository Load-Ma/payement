using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payement.DAL
{
    public class Guest_DAL
    {
        public int ID { get; set; }
        public string Party_ID_DAL { get; set; }
        public string Username { get; set; }
        public float Spent { get; set; }

        public Guest_DAL(string partyID, string username, float spent) => (Party_ID_DAL, Username, Spent) = (partyID, username, spent);

        public Guest_DAL(int id, string partyID, string username, float spent) 
            => (ID, Party_ID_DAL, Username, Spent) = (id, partyID, username, spent);
    }
}
