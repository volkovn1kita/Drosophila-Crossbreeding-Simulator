using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Сrossbreeding_of_Drosophila
{
    public class Gene
    {
        public GeneType Type { get; set; } = new GeneType();
        public Allele Allele1 { get; set; } = new Allele();
        public Allele Allele2 { get; set; } = new Allele();

        public Gene(GeneType type, Allele allele1, Allele allele2)
        {
            Type = type;
            Allele1 = allele1;
            Allele2 = allele2;
        }

        public Gene() { }

        public string GetGenotype()
        {
            return Allele1.Symbol + Allele2.Symbol;
        }

        public string GetPhenotype()
        {
            // Якщо хоч один домінантний -> домінантна ознака
            if (Allele1.IsDominant || Allele2.IsDominant)
                return "Dominant Trait";
            else
                return "Recessive Trait";
        }

    }
}
