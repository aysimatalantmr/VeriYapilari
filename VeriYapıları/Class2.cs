using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VeriYapıları
{
        public class BSTNode
        {
            public Item Data;
            public BSTNode Left;
            public BSTNode Right;

            public BSTNode(Item data)
            {
                Data = data;
                Left = null;
                Right = null;
            }
        }

    
}
