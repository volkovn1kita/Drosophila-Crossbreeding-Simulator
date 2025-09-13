using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Сrossbreeding_of_Drosophila
{
    public class Organism
    {

        public List<Gene> Genes { get; set; } = new List<Gene>();

        public Organism(params Gene[] genes)
        {
            Genes.AddRange(genes);
        }
        public Organism() { }

        public Organism(List<Gene> genes)
        {
            Genes.AddRange(genes);
        }

        public string GetGenotype()
        {
            return string.Join(" ", Genes.Select(g => g.GetGenotype()));
        }

        public string GetPhenotype()
        {
            return string.Join("; ", Genes.Select(g => g.GetPhenotype()));
        }



        public List<string> ProduceGametes()
        {
            if (Genes.Count == 0) return new List<string>();

            // Використовуємо HashSet, щоб уникнути дублікатів
            var gametes = new HashSet<string>();

            // Починаємо з переліку алелей першого гена
            gametes.Add(Genes[0].Allele1.Symbol);
            gametes.Add(Genes[0].Allele2.Symbol);

            // Проходимо решту генів
            for (int i = 1; i < Genes.Count; i++)
            {
                var newGametes = new HashSet<string>();
                foreach (var g in gametes)
                {
                    newGametes.Add(g + Genes[i].Allele1.Symbol);
                    newGametes.Add(g + Genes[i].Allele2.Symbol);
                }
                gametes = newGametes;
            }

            return gametes.ToList(); // перетворюємо назад у список
        }



    }
}
