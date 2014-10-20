using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DrinkLib;

namespace DrinktenderUI
{
    class Program
    {
        static void Main(string[] args)
        {
            RecipeCollection DrinkBook = new RecipeCollection();

            Console.WriteLine("DrinkBook currently has: {0} drinks loaded.", DrinkBook.Count);

            Console.WriteLine("");
            Console.WriteLine("(l)oad recipes.txt, else quit.");
            string command = Console.ReadLine();

            if (command != "l")
            {
                Environment.Exit(0);
            }
            else
            {
                RecipeReader newReader = new RecipeReader();
                try
                {
                    DrinkBook = newReader.loadRecipesFromFile(DrinkBook);
                }
                catch (InvalidRecipeException)
                {
                    Console.WriteLine("Improper drink found in recipe file.");
                }
            }
            Console.WriteLine();
            Console.WriteLine("DrinkBook currently has: {0} drinks loaded.", DrinkBook.Count);
            Console.WriteLine();
            Console.WriteLine(DrinkBook.ListFullDrinks());
            Console.ReadKey();
        }
    }
}
