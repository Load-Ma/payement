using System;
using Xunit;

namespace Payement.Tests
{
    public class GuestTest
    {
        public PayementService srv = new PayementService();

        [Theory]
        [InlineData(3, 5, "Romain", 13.0f)]
        public void ValiderConstructor(int id, int pID, string username, float spent)
        {
            var g = new Guest(pID, username, spent);
            var g2 = new Guest(id, pID, username, spent);

            Assert.NotNull(g);
            Assert.IsType<Guest>(g);
            Assert.Equal(pID, g.Party_ID);
            Assert.Equal(username, g.Username);
            Assert.Equal(spent, g.Spent);

            Assert.NotNull(g2);
            Assert.IsType<Guest>(g2);
            Assert.Equal(id, g2.ID);
            Assert.Equal(pID, g2.Party_ID);
            Assert.Equal(username, g2.Username);
            Assert.Equal(spent, g2.Spent);
        }

        [Fact]
        public void Insert()
        {
            var partyID = 10;
            var username = "Elouan";
            var spent = 10.5f;

            var g = new Guest(partyID, username, spent);
            var g2 = srv.InsertGuest(g);

            Assert.NotNull(g);
            Assert.NotNull(g2);
            Assert.IsType<Guest>(g2);
            Assert.Equal(g, g2);

            srv.DeleteGuest(g.ID);
        }

        [Fact]
        public void getbyID()
        {
            var partyID = 10;
            var username = "Elouan";
            var spent = 10.5f;
            var guest = new Guest(partyID, username, spent);
            var g2 = srv.InsertGuest(guest);

            var g = srv.GetGuestByID(g2.ID);

            Assert.NotNull(g);
            Assert.IsType<Guest>(g);
            Assert.Equal(g2.ID, g.ID);
            Assert.Equal(g2.Username, g.Username);
            Assert.Equal(g2.Party_ID, g.Party_ID);
            Assert.Equal(g2.Spent, g.Spent);
            srv.DeleteGuest(g.ID);
        }

        [Theory]
        [InlineData("Elouan", 10.5f)]
        [InlineData("Romain", 15.6f)]
        [InlineData("Enzo", 1.0f)]
        [InlineData("Mael", 11f)]
        public void getAllFromParty(string username, int spent)
        {
            var party = new Party("super soirée");
            party = srv.InsertParty(party);
            var guest = new Guest(party.ID, username, spent);
            var g2 = srv.InsertGuest(guest);

            var guests = srv.GetAllGuestsFromParty(party.ID);

            guests.ForEach(g =>
            {
                Assert.NotNull(g);
                Assert.IsType<Guest>(g);
                Assert.Equal(g2.ID, g.ID);
                Assert.Equal(g2.Username, g.Username);
                Assert.Equal(g2.Party_ID, g.Party_ID);
                Assert.Equal(g2.Spent, g.Spent);
            });
            srv.DeleteParty(party.ID);
        }

        [Fact]
        public void update()
        {
            var partyID = 10;
            var username = "Elouan";
            var spent = 10.5f;
            var guest = new Guest(partyID, username, spent);
            var g2 = srv.InsertGuest(guest);

            var name = "Romain";
            var spent2 = 132.8f;

            guest.Username = name;
            guest.Spent = 132.8f;

            var g = srv.UpdateGuest(guest);

            Assert.NotNull(g);
            Assert.IsType<Guest>(g);
            Assert.Equal(g.Username, name);
            Assert.Equal(g.Spent, spent2);

            srv.DeleteGuest(g.ID);
        }

        [Fact]
        public void getAll()
        { 

            var g = srv.GetAllGuests();

            Assert.NotNull(g);
            foreach (var g2 in g)
            {
                Assert.IsType<Guest>(g2);
            }
        }

        [Fact]
        public void verifyToString()
        {
            var g = new Guest(4, "Thomas", 154f);
            var g2 = new Guest(1, 4, "Cameron", 103.78f);

            var str = g.ToString();
            var str2 = g2.ToString();

            Assert.IsType<String>(str);
            Assert.IsType<String>(str2);
        }
    }
}
