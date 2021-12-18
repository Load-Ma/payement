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
        public int ID { get; set; }
        public string Name { get; set; }

        public Party(string name)
        {
            Name = name;
        }

        public Party(int id, string name)
        {
            ID = id;
            Name = name;
        }

        public override string ToString()
        {
            return $"ID: {this.ID}, Name: {this.Name}";
        }
    }
}
