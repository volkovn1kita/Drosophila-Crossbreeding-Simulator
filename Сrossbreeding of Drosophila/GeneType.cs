using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Сrossbreeding_of_Drosophila
{
    public class GeneType
    {
        public string Name { get; set; } = string.Empty;
        public string DominantPhenotype { get; set; } = string.Empty;
        public string RecessivePhenotype { get; set; } = string.Empty;

        public GeneType(string name, string dominantPhenotype, string recessivePhenotype)
        {
            Name = name;
            DominantPhenotype = dominantPhenotype;
            RecessivePhenotype = recessivePhenotype;
        }

        public GeneType() { }

    }
}
