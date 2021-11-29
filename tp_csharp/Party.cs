using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payement
{
    public class Party
    {
        public string ID { get; set; }
        public int Organisator_ID { get; set; }
        public string Name { get; set; }

        public Party(int organisator_id, string name)
        {
            ID = GenerateUID();
            Organisator_ID = organisator_id;
            Name = name;
        }

        public Party(string id, int organisator_id, string name)
        {
            ID = id;
            Organisator_ID = organisator_id;
            Name = name;
        }

        private string GenerateUID()
        {
            Guid g = Guid.NewGuid();

            return g.ToString();
        }
    }
}
