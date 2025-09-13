using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.IO;

namespace Сrossbreeding_of_Drosophila
{
    internal class MenuManager
    {
        private List<Organism> population = new List<Organism>();
        private string dataFile = "population.json";

        // Збереження популяцiї у файл
        private void SavePopulation()
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(population, options);
            File.WriteAllText(dataFile, json);
        }

        // Завантаження популяцiї з файлу
        private void LoadPopulation()
        {
            if (File.Exists(dataFile))
            {
                string json = File.ReadAllText(dataFile);
                population = JsonSerializer.Deserialize<List<Organism>>(json);
            }
        }

        private void DrawHeader()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("╔══════════════════════════════════════════╗");
            Console.WriteLine("║           СХРЕЩУВАННЯ ДРОЗОФiЛ           ║");
            Console.WriteLine("╚══════════════════════════════════════════╝");
            Console.ResetColor();
            Console.WriteLine();
        }

        private void DrawMenuOptions()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("┌──────────────────────────────────────────┐");
            Console.WriteLine("│                ГОЛОВНЕ МЕНЮ              │");
            Console.WriteLine("├──────────────────────────────────────────┤");
            Console.ResetColor();

            Console.WriteLine("│  1  Додати нову особину                  │");
            Console.WriteLine("│  2  Переглянути всi особини              │");
            Console.WriteLine("│  3  Схрестити двох батькiв               │");
            Console.WriteLine("│  4  Видалити особину                     │");
            Console.WriteLine("│  5  Вихiд                                │");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("└──────────────────────────────────────────┘");
            Console.ResetColor();
            Console.WriteLine();
        }

        private void ShowPopulationInfo()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($" Популяцiя: {population.Count} особин");
            Console.ResetColor();
            Console.WriteLine();
        }

        public void Start()
        {
            LoadPopulation();
            while (true)
            {
                DrawHeader();
                ShowPopulationInfo();
                DrawMenuOptions();

                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(" Оберiть дiю (1-5): ");
                Console.ResetColor();

                string? choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddOrganism();
                        SavePopulation();
                        PressAnyKey();
                        break;
                    case "2":
                        ShowPopulation();
                        PressAnyKey();
                        break;
                    case "3":
                        CrossParents();
                        SavePopulation();
                        PressAnyKey();
                        break;
                    case "4":
                        DeleteOrganism();
                        SavePopulation();
                        PressAnyKey();
                        break;
                    case "5":
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("\n Данi збережено.");
                        Console.ResetColor();
                        SavePopulation();
                        return;
                    default:
                        ShowError(" Невiрний вибiр. Спробуйте ще раз.");
                        Thread.Sleep(1500);
                        break;
                }
            }
        }

        private void AddOrganism()
        {
            Console.Clear();

            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("┌─────────────────────────────────────────┐");
            Console.WriteLine("│             ДОДАТИ ОСОБИНУ              │");
            Console.WriteLine("└─────────────────────────────────────────┘");
            Console.ResetColor();
            Console.WriteLine();

            Console.WriteLine(" Введiть алелi для гена колiр очей:");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("   A - червонi очi (домiнантна)");
            Console.WriteLine("   a - бiлi очi (рецесивна)");
            Console.ResetColor();
            Console.Write("   Приклад: Aa, AA, aa  ");

            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("(або -1 для повернення)");
            Console.ResetColor();

            string? eye = Console.ReadLine();
            if (eye == "-1") return;

            if (string.IsNullOrEmpty(eye) || eye.Length != 2)
            {
                ShowError(" Неправильний формат! Введiть рiвно 2 символи.");
                return;
            }

            var geneEye = new Gene(
                new GeneType("Колiр очей", "Червонi", "Бiлi"),
                new Allele(symbol: eye[0].ToString(), char.IsUpper(eye[0])),
                new Allele(symbol: eye[1].ToString(), char.IsUpper(eye[1]))
            );

            Console.WriteLine();
            Console.WriteLine("  Введiть алелi для гена форма крил:");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("   B - звичайнi крила (домiнантна)");
            Console.WriteLine("   b - зачiпки (рецесивна)");
            Console.ResetColor();
            Console.Write("   Приклад: Bb, BB, bb  ");
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("(або -1 для повернення)");

            string? wing = Console.ReadLine();
            if (wing == "-1") return;
            if (string.IsNullOrEmpty(wing) || wing.Length != 2)
            {
                ShowError(" Неправильний формат! Введiть рiвно 2 символи.");
                return;
            }

            var geneWing = new Gene(
                new GeneType("Форма крил", "Звичайнi", "Зачiпки"),
                new Allele(symbol: wing[0].ToString(), char.IsUpper(wing[0])),
                new Allele(symbol: wing[1].ToString(), char.IsUpper(wing[1]))
            );

            population.Add(new Organism(geneEye, geneWing));

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n Особину успiшно додано!");
            Console.ResetColor();
        }

        private void ShowPopulation()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("┌─────────────────────────────────────────┐");
            Console.WriteLine("│             ПЕРЕГЛЯД ПОПУЛЯЦiЇ          │");
            Console.WriteLine("└─────────────────────────────────────────┘");
            Console.ResetColor();
            Console.WriteLine();

            if (population.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("  Популяцiя порожня. Додайте особин для початку роботи.");
                Console.ResetColor();
                return;
            }

            Console.WriteLine($" Всього особин: {population.Count}");
            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("┌─────┬─────────────────┬──────────────────────────────────┐");
            Console.WriteLine("│ №   │    ГЕНОТИП      │               ФЕНОТИП            │");
            Console.WriteLine("├─────┼─────────────────┼──────────────────────────────────┤");
            Console.ResetColor();

            for (int i = 0; i < population.Count; i++)
            {
                var org = population[i];
                Console.Write("│ ");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write($"{i,3}");
                Console.ResetColor();
                Console.Write(" │ ");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write($"{org.GetGenotype(),-15}");
                Console.ResetColor();
                Console.Write(" │ ");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write($"{org.GetPhenotype(),-31}");
                Console.ResetColor();
                Console.WriteLine(" │");
            }

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("└─────┴─────────────────┴──────────────────────────────────┘");
            Console.ResetColor();
        }

        private void CrossParents()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("┌─────────────────────────────────────────┐");
            Console.WriteLine("│             СХРЕЩУВАННЯ ОСОБИН          │");
            Console.WriteLine("└─────────────────────────────────────────┘");
            Console.ResetColor();
            Console.WriteLine();

            if (population.Count < 2)
            {
                ShowError(" Потрiбно мiнiмум 2 особини для схрещування!");
                return;
            }

            ShowPopulation();
            Console.WriteLine();

            Console.Write(" Введiть iндекс першого батька: ");
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("(або -1 для повернення)");
            Console.ResetColor();

            if (!int.TryParse(Console.ReadLine(), out int idx1))
            {
                ShowError(" Неправильний формат числа!");
                return;
            }
            if (idx1 == -1) return;
            if (idx1 < 0 || idx1 >= population.Count)
            {
                ShowError(" Неправильний iндекс!");
                return;
            }

            Console.Write(" Введiть iндекс другого батька: ");
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("(або -1 для повернення)");
            Console.ResetColor();
            if (!int.TryParse(Console.ReadLine(), out int idx2))
            {
                ShowError(" Неправильний формат числа!");
                return;
            }
            if (idx2 == -1) return;
            if (idx2 < 0 || idx2 >= population.Count)
            {
                ShowError(" Неправильний iндекс!");
                return;
            }

            var parent1 = population[idx1];
            var parent2 = population[idx2];

            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(" АНАЛiЗ СХРЕЩУВАННЯ:");
            Console.ResetColor();
            Console.WriteLine($" Батько 1: {parent1.GetGenotype()} ─> {parent1.GetPhenotype()}");
            Console.WriteLine($" Батько 2: {parent2.GetGenotype()} ─> {parent2.GetPhenotype()}");
            Console.WriteLine();

            // Статистика потомства
            var cross = new Cross(parent1, parent2);
            var generation = cross.MakeOffspringGeneration("F1");
            generation.PrintSummary();

            // Генеруємо випадкового потомка
            var gametes1 = parent1.ProduceGametes();
            var gametes2 = parent2.ProduceGametes();
            var rnd = new Random();

            string g1 = gametes1[rnd.Next(gametes1.Count)];
            string g2 = gametes2[rnd.Next(gametes2.Count)];

            var childGenes = new List<Gene>();
            for (int i = 0; i < parent1.Genes.Count; i++)
            {
                char symbol1 = g1[i];
                char symbol2 = g2[i];

                bool isDom1 = char.IsUpper(symbol1);
                bool isDom2 = char.IsUpper(symbol2);

                var a1 = new Allele(symbol1.ToString(), isDom1);
                var a2 = new Allele(symbol2.ToString(), isDom2);

                childGenes.Add(new Gene(parent1.Genes[i].Type, a1, a2));
            }

            var child = new Organism(childGenes);
            population.Add(child);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(" НАРОДЖЕНИЙ НАЩАДОК:");
            Console.WriteLine($" {child.GetGenotype()} ─> {child.GetPhenotype()}");
            Console.ResetColor();
        }

        private void DeleteOrganism()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("┌─────────────────────────────────────────┐");
            Console.WriteLine("│              ВИДАЛИТИ ОСОБИНУ           │");
            Console.WriteLine("└─────────────────────────────────────────┘");
            Console.ResetColor();
            Console.WriteLine();

            if (population.Count == 0)
            {
                ShowError(" Немає особин для видалення!");
                return;
            }

            ShowPopulation();
            Console.WriteLine();

            Console.Write(" Введiть iндекс особини для видалення: ");
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("(або -1 для повернення)");
            Console.ResetColor();

            if (!int.TryParse(Console.ReadLine(), out int idx))
            {
                ShowError(" Неправильний формат числа!");
                return;
            }
            if (idx == -1) return;

            if (idx >= 0 && idx < population.Count)
            {
                var organism = population[idx];
                population.RemoveAt(idx);

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($" Особину #{idx} ({organism.GetGenotype()}) видалено!");
                Console.ResetColor();
            }
            else
            {
                ShowError(" Неправильний iндекс!");
            }
        }

        private void ShowError(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\n{message}");
            Console.ResetColor();
        }

        private void PressAnyKey()
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("  Натиснiть будь-яку клавiшу для продовження...");
            Console.ResetColor();
            Console.ReadKey();
        }
    }
}