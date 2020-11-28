using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dama_v1
{
    class Hrac
    {
        public List<Kaminek> kaminky = new List<Kaminek>();
        private int _cisloHrace;

        public int CisloHrace { get => _cisloHrace; set => _cisloHrace = value; }

        public Hrac()
        {
         
        }
    }
}
