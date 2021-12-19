using System;
using Xunit;

namespace Payement.DAL.Tests
{
    public class PartyDALTest
    {
        [Theory]
        [InlineData(10, "Top soirée")]
        [InlineData(165, "giga soirée")]
        public void Party_valideConstructor(int id, string name)
        {
            var p = new Party_DAL(name);
            var p2 = new Party_DAL(id, name);


            Assert.NotNull(p);
            Assert.IsType<Party_DAL>(p);
            Assert.Equal(name, p.Name);

            Assert.NotNull(p2);
            Assert.IsType<Party_DAL>(p2);
            Assert.Equal(id, p2.ID);
            Assert.Equal(name, p2.Name);
        }

        [Theory]
        [InlineData("soirée repos")]
        [InlineData("soirée C#")]
        public void Insert(string name)
        {
            var srv = new PartyDepot_DAL();

            var p = new Party_DAL(name);
            var p2 = srv.Insert(p);

            Assert.NotNull(p);
            Assert.NotNull(p2);
            Assert.IsType<Party_DAL>(p2);
            Assert.Equal(p, p2);

            // je n'ai plus besoin de l'invité donc je le supprime de la base de donnée
            // de plus c'est une méthode Void donc il n'y a aucun résultat à tester
            srv.Delete(p.ID);
        }

        [Fact]
        public void getbyID()
        {
            var srv = new PartyDepot_DAL();
            var name = "Soirée cool";
            var party = new Party_DAL(name);
            var p2 = srv.Insert(party);

            var p = srv.GetByID(p2.ID);

            Assert.NotNull(p);
            Assert.IsType<Party_DAL>(p);
            Assert.Equal(p2.ID, p.ID);
            Assert.Equal(p2.Name, p.Name);
            
            srv.Delete(p.ID);
        }

        [Theory]
        [InlineData("Soirée 1")]
        [InlineData("soirée 2")]
        public void update(string username)
        {
            var srv = new PartyDepot_DAL();
            var party = new Party_DAL(username);
            var p2 = srv.Insert(party);

            var name = "pas de soirée";

            party.Name = name;

            var p = srv.Update(party);

            Assert.NotNull(p);
            Assert.IsType<Party_DAL>(p);
            Assert.Equal(p.Name, name);

            srv.Delete(p.ID);
        }

        [Fact]
        public void getAll()
        {
            var srv = new PartyDepot_DAL();

            var p = srv.GetAll();

            Assert.NotNull(p);
            foreach (var p2 in p)
            {
                Assert.IsType<Party_DAL>(p2);
            }
        }
    }
}

