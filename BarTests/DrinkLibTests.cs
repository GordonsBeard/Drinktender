using DrinkLib;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace BarTests
{
    [TestClass]
    public class DrinkLibTests
    {
        #region Constants

        // Drinks
        const string testDrinkName = "Test & Debug";    // Normal name
        const string testBlankDrinkName = "";           // Blank string name.
        string testEmptyDrinkName = String.Empty; // Empty string name.
        const string testNullDrinkName = null;          // Null string name. 
        const string testLongName = "Somewhat Threatening Sophisticated Professional Killstreak Carbondado Botkiller Stickybomb Launcher Mk.I";
                                                        // Long string name.
        // Glasses
        const string testRocksGlassName = "Rocks";          // Proper cased rocks glass.
        const string testLowercaseRocksGlassName = "rocks"; // Lowercase parsing (for text file importing)

        Glass testLowercaseRocksGlass = new Glass((DrinkEnums.GlassTypeEnum)Enum.Parse(typeof(DrinkEnums.GlassTypeEnum), testLowercaseRocksGlassName, true));

        // Ingredients
        const string testRumName = "Test White Rum";
        const string testColaName = "Test Cola";
        #endregion

        #region Glass Tests
        /// <summary>
        /// Tests the creation of a rocks glass from "Rocks" string.
        /// </summary>
        [TestMethod]
        [TestCategory("BVT"), TestCategory("Glass")]
        public void Valid_Glass()
        {
            Glass testGlass = new Glass(DrinkEnums.GlassTypeEnum.Rocks);
            Assert.AreEqual(testRocksGlassName, testGlass.ToString());        
        }

        /// <summary>
        /// Tests the creation of a rocks glass from the "rocks" string.
        /// </summary>
        [TestMethod]
        [TestCategory("BVT"), TestCategory("Glass")]
        public void Valid_Glass_Lowercase()
        {
            Assert.AreEqual(testRocksGlassName, testLowercaseRocksGlass.ToString());
        }
        #endregion

        #region Ingredient Tests
        /// <summary>
        /// Tests valid Ingredients being formed and their ToString methods.
        /// </summary>
        [TestMethod]
        [TestCategory("BVT"), TestCategory("Ingredients")]
        public void Valid_Ingredient()
        {
            IngredientType testAlcoholType = new IngredientType(DrinkEnums.IngredientTypeEnum.Alcohol);
            Ingredient testRum = new Ingredient(testRumName, testAlcoholType);
            Assert.AreEqual(testRum.ToString(), testRumName);
        }

        [TestMethod]
        [TestCategory("BVT"), TestCategory("Ingredients")]
        public void Valid_Ingredient_List()
        {
            IngredientType testAlcoholType = new IngredientType(DrinkEnums.IngredientTypeEnum.Alcohol);
            IngredientType testSodaType = new IngredientType(DrinkEnums.IngredientTypeEnum.Soda);

            Ingredient testRum = new Ingredient(testRumName, testAlcoholType);
            Ingredient testCola = new Ingredient(testColaName, testSodaType);

            Dictionary<Ingredient, string> rumAndColaIngredientsDict = new Dictionary<Ingredient, string>() { { testRum, "1.5" }, { testCola, "4.5" }, };
            Dictionary<Ingredient, string> shuffledRumAndColaIngredientsDict = new Dictionary<Ingredient, string>() { { testRum, "1.5" }, { testCola, "4.5" }, };
        }
        #endregion

        #region Recipe Tests
        [TestMethod]
        [TestCategory("BVT"), TestCategory("Recipe")]
        public void Valid_Recipe()
        {
            IngredientType testAlcoholType = new IngredientType(DrinkEnums.IngredientTypeEnum.Alcohol);
            IngredientType testSodaType = new IngredientType(DrinkEnums.IngredientTypeEnum.Soda);

            Ingredient testRum = new Ingredient(testRumName, testAlcoholType);
            Ingredient testCola = new Ingredient(testColaName, testSodaType);

            Dictionary<Ingredient, string> rumAndColaIngredientsDict = new Dictionary<Ingredient, string>() { { testRum, "1.5" }, { testCola, "4.5" }, };
            Dictionary<Ingredient, string> shuffledRumAndColaIngredientsDict = new Dictionary<Ingredient, string>() { { testCola, "4.5" }, { testRum, "1.5" } };

            // Recipes
            Recipe rumCoke = new Recipe(testDrinkName, testLowercaseRocksGlass, rumAndColaIngredientsDict);

            Assert.AreEqual(testDrinkName, rumCoke.ToString());
        }

        /// <summary>
        /// Makes sure two identicle drinks cannot be added to the database.
        /// </summary>
        [TestMethod]
        [TestCategory("BVT"), TestCategory("Recipe")]
        public void Invalid_Recipe_Duplicate()
        {
            RecipeCollection testDrinkBook = new RecipeCollection();

            IngredientType testAlcoholType = new IngredientType(DrinkEnums.IngredientTypeEnum.Alcohol);
            IngredientType testSodaType = new IngredientType(DrinkEnums.IngredientTypeEnum.Soda);

            Ingredient testRum = new Ingredient(testRumName, testAlcoholType);
            Ingredient testCola = new Ingredient(testColaName, testSodaType);

            Dictionary<Ingredient, string> rumAndColaIngredientsDict = new Dictionary<Ingredient, string>() { { testRum, "1.5" }, { testCola, "4.5" }, };
            Dictionary<Ingredient, string> shuffledRumAndColaIngredientsDict = new Dictionary<Ingredient, string>() { { testCola, "4.5" }, { testRum, "1.5" } };

            Recipe rumCoke = new Recipe(testDrinkName, testLowercaseRocksGlass, rumAndColaIngredientsDict);
            Recipe shuffledRumCoke = new Recipe(testDrinkName, testLowercaseRocksGlass, shuffledRumAndColaIngredientsDict);

            List<Recipe> testRecipeList = new List<Recipe>(){ rumCoke, rumCoke };

            foreach (Recipe recipe in testRecipeList)
            {
                try
                {
                    testDrinkBook.Add(recipe);
                }
                catch (RecipeAlreadyExistsException ex)
                {
                    Console.Out.WriteLine(ex.Message);
                }
            }
            int expectedDrinks = 1;
            int actualDrinks = testDrinkBook.Count;

            Assert.AreEqual(expectedDrinks, actualDrinks, "FAIL: Expected drinks: {0}. Actual drinks: {1}.", expectedDrinks, actualDrinks);
        }

        /// <summary>
        /// Ensures two drinks with the same ingredients (but in a different order) don't get entered.
        /// </summary>
        [TestMethod]
        [TestCategory("BVT"), TestCategory("Recipe")]
        public void Invalid_Recipe_Shuffled_Duplicate_Recipe()
        {
            RecipeCollection testDrinkBook = new RecipeCollection();

            IngredientType testAlcoholType = new IngredientType(DrinkEnums.IngredientTypeEnum.Alcohol);
            IngredientType testSodaType = new IngredientType(DrinkEnums.IngredientTypeEnum.Soda);

            Ingredient testRum = new Ingredient(testRumName, testAlcoholType);
            Ingredient testCola = new Ingredient(testColaName, testSodaType);

            Dictionary<Ingredient, string> rumAndColaIngredientsDict = new Dictionary<Ingredient, string>() { { testRum, "1.5" }, { testCola, "4.5" }, };
            Dictionary<Ingredient, string> shuffledRumAndColaIngredientsDict = new Dictionary<Ingredient, string>() { { testCola, "4.5" }, { testRum, "1.5" } };

            Recipe rumCoke = new Recipe(testDrinkName, testLowercaseRocksGlass, rumAndColaIngredientsDict);
            Recipe shuffledRumCoke = new Recipe(testDrinkName, testLowercaseRocksGlass, shuffledRumAndColaIngredientsDict);

            List<Recipe> testRecipeList = new List<Recipe>() { rumCoke, shuffledRumCoke };

            foreach (Recipe recipe in testRecipeList)
            {
                try
                {
                    testDrinkBook.Add(recipe);
                }
                catch (RecipeAlreadyExistsException ex)
                {
                    Console.Out.WriteLine(ex.Message);
                }
            }
            int expectedDrinks = 1;
            int actualDrinks = testDrinkBook.Count;

            Assert.AreEqual(expectedDrinks, actualDrinks, "Expected drinks: {0}. Actual drinks: {1}.", expectedDrinks, actualDrinks);
        }

        #endregion

        #region Reciper Reader Tests
        /// <summary>
        /// Will test loading the 2 valid drinks from Resources\testValidRecipes.txt.
        /// </summary>
        [TestMethod]
        [TestCategory("BVT"), TestCategory("Input/Output")]
        public void Valid_LoadFromFile()
        {
            bool success = true;

            string testRecipes = @"BarTests\Resources\testValidRecipes.txt";

            RecipeCollection testDrinkBook = new RecipeCollection();
            RecipeReader newReader = new RecipeReader();

            testDrinkBook = newReader.loadRecipesFromFile(testDrinkBook, testRecipes);

            success &= (testDrinkBook.Count > 0);

            Assert.AreEqual(success, true, "Expected drinks: {0}. Actual drinks: {1}", 2, testDrinkBook.Count);
        }

        /// <summary>
        /// Will test the loading of 2 drinks where 1 is an exact duplicate of the first.
        /// Test will pass if 1 new drink is returned.
        /// uses Resources\testInvalidRecipesDuplicateDrinks.txt
        /// </summary>
        [TestMethod]
        [TestCategory("BVT"), TestCategory("Input/Output")]
        public void Invalid_LoadFromFile_Duplicates()
        {
            bool success = true;

            string testRecipes = @"BarTests\Resources\testInvalidRecipesDuplicateDrinks.txt";

            RecipeCollection testDrinkBook = new RecipeCollection();
            RecipeReader newReader = new RecipeReader();

            testDrinkBook = newReader.loadRecipesFromFile(testDrinkBook, testRecipes);

            success &= (testDrinkBook.Count == 1);

            Assert.AreEqual(success, true, "Expected drinks: {0}. Actual drinks: {1}", 2, testDrinkBook.Count);
        }
        #endregion
    }
}
