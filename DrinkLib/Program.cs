using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrinkLib
{
    class Program
    {
        static void Main(string[] args)
        {

        }

        //#region Menu
        //private static void MainMenu()
        //{
        //    int drinkCount = drinkBook.Count;

        //    Console.WriteLine("Drinktender");
        //    Console.WriteLine("");
        //    Console.WriteLine("Total drinks: {0}", drinkCount);
        //    Console.WriteLine("");
        //    Console.WriteLine("(l)oad: Loads drinks from recipes.txt.");
        //    Console.WriteLine("(r)andom drink: Prints a random recipe.");
        //    Console.WriteLine("(q)uit");
        //    string command = Console.ReadLine();

        //    int returnCode = 0;

        //    switch (command)
        //    {
        //        case "l":
        //        case "load":
        //            Console.Clear();
        //            Console.WriteLine("Load From File:");
        //            Console.WriteLine("---------------");
        //            Console.WriteLine("");
        //            drinkBook = LoadDrinksFromFile(drinkBook);
        //            MainMenu(drinkBook);
        //            break;

        //        case "r":
        //        case "random":
        //            Console.Clear();
        //            Console.WriteLine("Random Drink:");
        //            Console.WriteLine("-------------");
        //            Console.WriteLine("");
        //            RandomDrink(drinkBook);
        //            MainMenu(drinkBook);
        //            break;

        //        case "q":
        //            returnCode = 0;
        //            break;

        //        default:
        //            Console.Clear();
        //            MainMenu(drinkBook);
        //            break;
        //    }

        //    if (returnCode == 0)
        //    {
        //        // Graceful Goodbye
        //        Console.WriteLine("Drive fast, take chances!");
        //        Console.ReadKey();
        //        Environment.Exit(0);
        //    }
        //    else
        //    {
        //        Console.Clear();
        //        MainMenu();
        //    }
        //}

        //#endregion

        #region Random Drink
        /// <summary>
        /// Will return a random individual Recipe.
        /// </summary>
        /// <param name="drinkBook">The Drink Book.</param>
        //
        //private static void RandomDrink(RecipeCollection drinkBook)
        //{
        //    Console.WriteLine(drinkBook.PickRandom());
        //}

        #endregion
    }

}

