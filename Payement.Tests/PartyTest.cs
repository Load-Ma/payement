using System;
using Xunit;

namespace Payement.Tests
{
    public class PartyTest
    {
        public PayementService srv = new PayementService();

        [Theory]
        [InlineData(10, "Top soirée")]
        [InlineData(165, "giga soirée")]
        public void Party_valideConstructor(int id, string name)
        {
            var p = new Party(name);
            var p2 = new Party(id, name);


            Assert.NotNull(p);
            Assert.IsType<Party>(p);
            Assert.Equal(name, p.Name);

            Assert.NotNull(p2);
            Assert.IsType<Party>(p2);
            Assert.Equal(id, p2.ID);
            Assert.Equal(name, p2.Name);
        }

        [Theory]
        [InlineData("soirée repos")]
        [InlineData("soirée C#")]
        public void Insert(string name)
        {
            var p = new Party(name);
            var p2 = srv.InsertParty(p);

            Assert.NotNull(p);
            Assert.NotNull(p2);
            Assert.IsType<Party>(p2);
            Assert.Equal(p, p2);

            // je n'ai plus besoin de l'invité donc je le supprime de la base de donnée
            // de plus c'est une méthode Void donc il n'y a aucun résultat à tester
            srv.DeleteParty(p.ID);
        }

        [Fact]
        public void getbyID()
        {
            var name = "Soirée cool";
            var party = new Party(name);
            var p2 = srv.InsertParty(party);

            var p = srv.GetPartyByID(p2.ID);

            Assert.NotNull(p);
            Assert.IsType<Party>(p);
            Assert.Equal(p2.ID, p.ID);
            Assert.Equal(p2.Name, p.Name);

            srv.DeleteParty(p.ID);
        }

        [Theory]
        [InlineData("Soirée 1")]
        [InlineData("soirée 2")]
        public void update(string username)
        {
            var party = new Party(username);
            var p2 = srv.InsertParty(party);

            var name = "pas de soirée";

            party.Name = name;

            var p = srv.UpdateParty(party);

            Assert.NotNull(p);
            Assert.IsType<Party>(p);
            Assert.Equal(p.Name, name);

            srv.DeleteParty(p.ID);
        }

        [Fact]
        public void getAll()
        {
            var p = srv.GetAllParties();

            Assert.NotNull(p);
            foreach (var p2 in p)
            {
                Assert.IsType<Party>(p2);
            }
        }

        [Fact]
        public void verifyToString()
        {
            var p = new Party("Soirée cool");
            var p2 = new Party(1, "Soirée cool 2");

            var str = p.ToString();
            var str2 = p2.ToString();

            Assert.IsType<String>(str);
            Assert.IsType<String>(str2);
        }
    }
}
