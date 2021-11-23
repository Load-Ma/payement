using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payement.DAL
{
    public class Party_DAL
    {
        public string ID { get; set; }
        public int Organisator_ID { get; set; }
        public string Name { get; set; }

        public Party_DAL(int organisatorID, string name) => (Organisator_ID, Name) = (organisatorID, name);

        public Party_DAL(string id, int organisatorID, string name)
            => (ID, Organisator_ID, Name) = (id, organisatorID, name);
    }
}
