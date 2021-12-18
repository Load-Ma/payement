using Payement.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payement
{
    public class PayementService : IPayementService
    {
        private GuestDepot_DAL depotGuest = new GuestDepot_DAL();
        private PartyDepot_DAL depotParty = new PartyDepot_DAL();
        public void DeleteGuest(int id)
        {
            depotGuest.Delete(id);
        }

        public void DeleteParty(int id)
        {
            depotParty.Delete(id);
        }

        public List<Guest> GetAllGuestsFromParty(int ID)
        {
            var guests = depotGuest.GetAllFromParty(ID)
                .Select(g => new Guest(g.ID, g.Party_ID_DAL, g.Username, g.Spent))
                .ToList();
            return guests;
        }

        public List<Guest> GetAllGuests()
        {
            var guests = depotGuest.GetAll()
                .Select(g => new Guest(g.ID, g.Party_ID_DAL, g.Username, g.Spent))
                .ToList();

            return guests;
        }

        public List<Party> GetAllParties()
        {
            var parties = depotParty.GetAll()
                .Select(p => new Party(p.ID, p.Name))
                .ToList();

            return parties;
        }

        public Guest GetGuestByID(int ID)
        {
            var g = depotGuest.GetByID(ID);
            return new Guest(g.ID, g.Party_ID_DAL, g.Username, g.Spent);
        }

        public Party GetPartyByID(int ID)
        {
            var p = depotParty.GetByID(ID);
            return new Party(p.ID, p.Name);
        }

        public Guest InsertGuest(Guest g)
        {
            var guest = new Guest_DAL(g.Party_ID, g.Username, g.Spent);
            depotGuest.Insert(guest);

            g.ID = guest.ID;

            return g;
        }

        public Party InsertParty(Party p)
        {
            var party = new Party_DAL(p.ID, p.Name);
            depotParty.Insert(party);

            p.ID = party.ID;

            return p;
        }

        public Guest UpdateGuest(Guest g)
        {
            var guest = new Guest_DAL(g.ID, g.Party_ID, g.Username, g.Spent);
            depotGuest.Update(guest);

            return g;
        }

        public Party UpdateParty(Party p)
        {
            var party = new Party_DAL(p.ID, p.Name);
            depotParty.Update(party);

            return p;
        }
    }
}
