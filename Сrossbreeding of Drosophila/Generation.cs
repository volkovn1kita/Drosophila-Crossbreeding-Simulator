using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Сrossbreeding_of_Drosophila
{
    public class Generation
    {
        public List<Organism> Offspring { get; set; } = new List<Organism>();
        public string Name { get; set; } = string.Empty;

        public Generation(string name, List<Organism> offspring)
        {
            Offspring = offspring;
            Name = name;
        }

        public Generation() { }

        public void PrintSummary()
        {
            Console.WriteLine($"\n  Generation {Name}:");

            int total = Offspring.Count;

            // Підрахунок генотипів
            var genotypeCounts = new Dictionary<string, int>();
            foreach (var o in Offspring)
            {
                string gt = o.GetGenotype();
                if (!genotypeCounts.ContainsKey(gt))
                    genotypeCounts[gt] = 0;
                genotypeCounts[gt]++;
            }

            Console.WriteLine("\n   Genotypes:");
            foreach (var kvp in genotypeCounts)
            {
                double percent = kvp.Value * 100.0 / total;
                Console.WriteLine($"    {kvp.Key} : {percent:F1}%");
            }

            // Підрахунок фенотипів
            var phenotypeCounts = new Dictionary<string, int>();
            foreach (var o in Offspring)
            {
                string pt = o.GetPhenotype();
                if (!phenotypeCounts.ContainsKey(pt))
                    phenotypeCounts[pt] = 0;
                phenotypeCounts[pt]++;
            }

            Console.WriteLine("\n   Phenotypes:");
            foreach (var kvp in phenotypeCounts)
            {
                double percent = kvp.Value * 100.0 / total;
                Console.WriteLine($"    {kvp.Key} : {percent:F1}%");
            }
        }

    }
}
