using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Сrossbreeding_of_Drosophila
{
    public class Cross
    {

        public Organism Parent1 { get; set; } = new Organism();
        public Organism Parent2 { get; set; } = new Organism();

        public Cross(Organism parent1, Organism parent2)
        {
            Parent1 = parent1;
            Parent2 = parent2;
        }

        public Cross() { }

        public Generation MakeOffspringGeneration(string name)
        {
            var offspring = new List<Organism>();

            var gametes1 = Parent1.ProduceGametes();
            var gametes2 = Parent2.ProduceGametes();

            foreach (var g1 in gametes1)
                foreach (var g2 in gametes2)
                {
                    var childGenes = new List<Gene>();

                    for (int i = 0; i < Parent1.Genes.Count; i++)
                    {
                        // i-та алель у гаметі мами
                        char symbol1 = g1[i];
                        char symbol2 = g2[i];

                        // визначаємо домінантність автоматично по символу
                        bool isDom1 = char.IsUpper(symbol1);
                        bool isDom2 = char.IsUpper(symbol2);

                        var a1 = new Allele(symbol1.ToString(), isDom1);
                        var a2 = new Allele(symbol2.ToString(), isDom2);

                        var geneType = Parent1.Genes[i].Type; // використовуємо i-й ген обох батьків
                        childGenes.Add(new Gene(geneType, a1, a2));
                    }

                    var child = new Organism(childGenes);
                    offspring.Add(child);
                }

            return new Generation(name, offspring);
        }



    }
}
