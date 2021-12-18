using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payement
{
    public interface IPayementService
    {
        // Guest
        public void DeleteGuest(int id);
        public List<Guest> GetAllGuestsFromParty(int ID);
        public List<Guest> GetAllGuests();
        public Guest GetGuestByID(int ID);
        public Guest InsertGuest(Guest g);
        public Guest UpdateGuest(Guest g);

        // Party
        public void DeleteParty(int id);
        public List<Party> GetAllParties();
        public Party GetPartyByID(int ID);
        public Party InsertParty(Party g);
        public Party UpdateParty(Party g);
    }
}
