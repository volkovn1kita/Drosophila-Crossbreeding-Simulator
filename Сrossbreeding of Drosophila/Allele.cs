using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Сrossbreeding_of_Drosophila
{
    public class Allele
    {
        public string Symbol { get; set; } = string.Empty;

        public bool IsDominant { get; set; }

        public Allele(string symbol, bool isDominant)
        {
            Symbol = symbol;
            IsDominant = isDominant;
        }

        public Allele() { }

        public override string ToString() => Symbol;
    }
}
