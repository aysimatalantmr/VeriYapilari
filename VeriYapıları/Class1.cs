using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VeriYapıları
{
    public class Item
    {
        public string Name { get; set; }

        public Item(string name)
        {
            Name = name.ToLower(); // Aramaları kolaylaştırmak için küçük harf
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
