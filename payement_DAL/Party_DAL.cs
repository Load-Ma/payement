using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payement.DAL
{
    public class Party_DAL
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public Party_DAL(string name) => (Name) = (name);

        public Party_DAL(int id, string name)
            => (ID, Name) = (id, name);
    }
}
