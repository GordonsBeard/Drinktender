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
        const string defaultPath = @"C:\Users\doc\Documents\GitHub\CSharpJunk\Drinktender\";
        static public string recipeFileName
        {
            get
            {
                return defaultRecipeFileName;
            }
        }

        public RecipeCollection loadRecipesFromFile(RecipeCollection drinkBook, string fileName = defaultRecipeFileName)
        {
            // Open the recipes from local disk
            string target = String.Format(@"{0}{1}", defaultPath, fileName);

            // Put each recipe onto it's own line for parsing.
            string[] lines = System.IO.File.ReadAllLines(target);

            using (TextFieldParser parser = new TextFieldParser(target))
            {

                // Setting up the text parser to read a CSV file
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(",");

                // Each loop of this while reads one recipe.
                while (!parser.EndOfData)
                {
                    Dictionary<Ingredient, string> ingredientsDict = new Dictionary<Ingredient, string>();
                    string[] fields = parser.ReadFields();
                    string name = fields[0];
                    Glass glass = new Glass((DrinkEnums.GlassTypeEnum)Enum.Parse(typeof(DrinkEnums.GlassTypeEnum), fields[1]));

                    foreach (string rawIngredients in fields.Skip(2))
                    {
                        string[] ingredientPair = rawIngredients.Split(':');

                        // This is attempting to parse something like "WhiteRum" into Alcohol/Juice/etc
                        //IngredientType type = new IngredientType((DrinkEnums.IngredientTypeEnum)Enum.Parse(typeof(DrinkEnums.IngredientTypeEnum), ingredientPair[1]));

                        IngredientType type = new IngredientType(DrinkEnums.IngredientTypeEnum.Unknown);
                        Ingredient key = new Ingredient(ingredientPair[1], type);
                        ingredientsDict.Add(key, ingredientPair[0]);
                    }

                    Recipe newRecipe = new Recipe(name, glass, ingredientsDict);
                    drinkBook.Add(newRecipe);
                }
            }
            return drinkBook;
        }
    }
}
