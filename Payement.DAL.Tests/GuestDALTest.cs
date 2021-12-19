using System;
using Xunit;

namespace Payement.DAL.Tests
{
    public class GuestDALTest
    {
        [Fact]
        public void Guest_valideConstructor()
        {
            var id = 3;
            var partyID = 10;
            var username = "Elouan";
            var spent = 10.5f;

            var p = new Guest_DAL(partyID, username, spent);
            var p2 = new Guest_DAL(id, partyID, username, spent);


            Assert.NotNull(p);
            Assert.IsType<Guest_DAL>(p);
            Assert.Equal(partyID, p.Party_ID_DAL);
            Assert.Equal(username, p.Username);
            Assert.Equal(spent, p.Spent);

            Assert.NotNull(p2);
            Assert.IsType<Guest_DAL>(p2);
            Assert.Equal(id, p2.ID);
            Assert.Equal(partyID, p2.Party_ID_DAL);
            Assert.Equal(username, p2.Username);
            Assert.Equal(spent, p2.Spent);
        }

        [Fact]
        public void Insert()
        {
            var partyID = 10;
            var username = "Elouan";
            var spent = 10.5f;
            var srv = new GuestDepot_DAL();

            var g = new Guest_DAL(partyID, username, spent);
            var g2 = srv.Insert(g);

            Assert.NotNull(g);
            Assert.NotNull(g2);
            Assert.IsType<Guest_DAL>(g2);
            Assert.Equal(g, g2);
            srv.Delete(g.ID);
        }

        [Fact]
        public void getbyID()
        {
            var srv = new GuestDepot_DAL();
            var partyID = 10;
            var username = "Elouan";
            var spent = 10.5f;
            var guest = new Guest_DAL(partyID, username, spent);
            var g2 = srv.Insert(guest);

            var g = srv.GetByID(g2.ID);

            Assert.NotNull(g);
            Assert.IsType<Guest_DAL>(g);
            Assert.Equal(g2.ID, g.ID);
            Assert.Equal(g2.Username, g.Username);
            Assert.Equal(g2.Party_ID_DAL, g.Party_ID_DAL);
            Assert.Equal(g2.Spent, g.Spent);

        }

        [Theory]
        [InlineData("Elouan", 10.5f)]
        [InlineData("Romain", 15.6f)]
        [InlineData("Enzo", 1.0f)]
        [InlineData("Mael", 11f)]
        public void getAllFromParty(string username, int spent)
        {
            var srv = new GuestDepot_DAL();
            var srvP = new PartyDepot_DAL();
            var party = new Party_DAL("super soirée");
            party = srvP.Insert(party);
            var guest = new Guest_DAL(party.ID, username, spent);
            var g2 = srv.Insert(guest);

            var guests = srv.GetAllFromParty(party.ID);

            guests.ForEach(g =>
            {
                Assert.NotNull(g);
                Assert.IsType<Guest_DAL>(g);
                Assert.Equal(g2.ID, g.ID);
                Assert.Equal(g2.Username, g.Username);
                Assert.Equal(g2.Party_ID_DAL, g.Party_ID_DAL);
                Assert.Equal(g2.Spent, g.Spent);
            });
            srvP.Delete(party.ID);
        }

        [Fact]
        public void update()
        {
            var srv = new GuestDepot_DAL();
            var partyID = 10;
            var username = "Elouan";
            var spent = 10.5f;
            var guest = new Guest_DAL(partyID, username, spent);
            var g2 = srv.Insert(guest);

            var name = "Romain";
            var spent2 = 132.8f;

            guest.Username = name;
            guest.Spent = 132.8f;

            var g = srv.Update(guest);

            Assert.NotNull(g);
            Assert.IsType<Guest_DAL>(g);
            Assert.Equal(g.Username, name);
            Assert.Equal(g.Spent, spent2);

            // je n'ai plus besoin de l'invité donc je le supprime de la base de donnée
            // de plus c'est une méthode Void donc il n'y a aucun résultat à tester
            srv.Delete(g.ID);
        }

        [Fact]
        public void getAll()
        {
            var srv = new GuestDepot_DAL();

            var g = srv.GetAll();

            Assert.NotNull(g);
            foreach (var g2 in g)
            {
                Assert.IsType<Guest_DAL>(g2);
            }
        }
    }
}
