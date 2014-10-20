using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrinkLib
{
    public class RecipeReader
    {
        const string defaultRecipeFileName = @"DrinkLib\recipes.txt";
        const string defaultPath = @"C:\Users\doc\Documents\GitHub\Drinktender\";
        static public string recipeFileName
        {
            get
            {
                return defaultRecipeFileName;
            }
        }

        public RecipeCollection loadRecipesFromFile(RecipeCollection drinkBook, string fileName = defaultRecipeFileName)
        {
            #if DEBUG
            Console.WriteLine(@"Opening file: {0}{1}", defaultPath, fileName);
            #endif
            // Open the recipes from local disk
            string target = String.Format(@"{0}{1}", defaultPath, fileName);

            // Put each recipe onto it's own line for parsing.
            string[] lines = System.IO.File.ReadAllLines(target);

            #if DEBUG
            Console.WriteLine("Lines read to array. Apparent recipes:\t{0}", lines.Length);
            #endif

            // Fill this list with drinks to add.
            List<Recipe> tempRecipesToAdd = new List<Recipe>();

            using (TextFieldParser parser = new TextFieldParser(target))
            {

                // Setting up the text parser to read a CSV file
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(",");

                // Each loop of this while reads one recipe.
                while (!parser.EndOfData)
                {
                    Dictionary<Ingredient, string> tempIngDict = new Dictionary<Ingredient, string>();

                    // split one line up into fields
                    string[] fields = parser.ReadFields();

                    // There should be at minimum 3 elements. Name, Glass, Ingredient
                    if (fields.Length < 3)
                    {
                        throw new InvalidRecipeLineException(fields);
                    }

                    // set the drink name
                    string tempDrinkName = fields[0];

                    // set the glass
                    string tempGlassName = fields[1];

                    Glass tempGlass = new Glass();

                    // If it's an Invalid Glass, abort.
                    try
                    {
                        tempGlass.SetType(tempGlassName);
                    }
                    catch (InvalidGlassException ex)
                    {
                        throw ex;
                    }
                    
                    string tempIngName;
                    string tempIngAmount;

                    // everything past the 3rd field is an ingredient
                    // cycle through every chunk, checking for valid data.
                    // ##.##:Ingredient Name, spaces OK
                    foreach (string rawIngredients in fields.Skip(2))
                    {
                        string[] ingredientPair = rawIngredients.Split(':');
                        
                        if (ingredientPair.Length != 2)
                        {
                            throw new InvalidRecipeLineException(fields);
                        }

                        try
                        {
                            tempIngName = ingredientPair[1].Trim();
                            tempIngAmount = ingredientPair[0].Trim();
                        }
                        catch (IndexOutOfRangeException)
                        {
                            break;
                        }

                        // set up temporary IngredientType
                        IngredientType tempIngredientType = new IngredientType(tempIngName);
                        Ingredient tempIngredient = new Ingredient(tempIngName, tempIngredientType);

                        // if there are duplicate ingredients, abort importing.
                        bool value = tempIngDict.ContainsKey(tempIngredient);

                        tempIngDict.Add(tempIngredient, tempIngAmount);
                    }

                    #if DEBUG
                    Console.WriteLine("Creating Recipe: {0},\t({1}),\t({2})", tempDrinkName, tempGlass, String.Join(", ", tempIngDict.Keys.ToList()));
                    #endif

                    Recipe newRecipe = new Recipe(tempDrinkName, tempGlass, tempIngDict);
                    tempRecipesToAdd.Add(newRecipe);
                }
            }
            if (tempRecipesToAdd.Count > 0)
            {
                foreach (Recipe newRecipe in tempRecipesToAdd)
                {
                    try
                    {
                        #if DEBUG
                        Console.WriteLine("");
                        Console.WriteLine("Adding {0}.\t\tDrink Book Count = {1}", newRecipe.ToString(), drinkBook.Count);
                        #endif

                        drinkBook.Add(newRecipe);

                        #if DEBUG
                        Console.WriteLine("");
                        Console.WriteLine("Drink Book Count = {0}", drinkBook.Count);
                        #endif
                    }
                    catch (DuplicateRecipeException)
                    {
                        #if DEBUG
                        Console.WriteLine("");
                        Console.WriteLine("Exception Caught: {0} already exists in collection.", newRecipe.ToString());
                        #endif
                    }
                }
            }
            return drinkBook;
        }
    }
}
