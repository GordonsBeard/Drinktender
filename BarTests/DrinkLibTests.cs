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
        const string testRocksGlassName = "Glass: Rocks";          // Proper cased rocks glass.

        Glass testRocksGlass = new Glass(DrinkEnums.GlassTypeEnum.Rocks);

        // Ingredients
        const string testRumName = "Test White Rum";
        const string testColaName = "Test Cola";
        #endregion

        #region Glass Tests
        /// <summary>
        /// Tests the creation of a Rocks glass' ToString response.
        /// </summary>
        [TestMethod]
        [TestCategory("BVT"), TestCategory("Glass")]
        public void Valid_Glass()
        {
            string expectedString = "Glass: Rocks";
            string actualString;

            Glass testGlass = new Glass(DrinkEnums.GlassTypeEnum.Rocks);
            actualString = testGlass.ToString();

            Console.WriteLine("Expected:\t\"{0}\"\nActual:\t\t\"{1}\"", expectedString, actualString);
            Assert.AreEqual(expectedString, actualString);    
        }

        /// <summary>
        /// Test the SetType function of Glass.
        /// </summary>
        [TestMethod]
        [TestCategory("BVT"), TestCategory("Glass")]
        public void Valid_Glass_SetType()
        {
            string expectedString = "Glass: Rocks";
            string actualString;

            Glass testGlass = new Glass();
            Console.WriteLine("Test Glass initialized.");

            Console.WriteLine("SetType({0})", "Rocks");
            testGlass.SetType("Rocks");
            
            actualString = testGlass.ToString();

            Console.WriteLine("Expected:\t{0}\tActual:\t{1}", expectedString, actualString);
            Assert.AreEqual(expectedString, actualString, "Glass names.");

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
            string expectedString = String.Format("Ingredient: {0}", testRumName);
            string actualString;

            IngredientType testAlcoholType = new IngredientType(DrinkEnums.IngredientTypeEnum.Alcohol);
            Ingredient testRum = new Ingredient(testRumName, testAlcoholType);
            actualString = testRum.ToString();

            Console.WriteLine("Expected:\t\"{0}\"\nActual:\t\t\"{1}\"", expectedString, actualString);
            Assert.AreEqual(expectedString, actualString);
        }
        #endregion

        #region Recipe Tests
        /// <summary>
        /// Test a completely valid pre-built Recipe, and it's ToString override.
        /// </summary>
        [TestMethod]
        [TestCategory("BVT"), TestCategory("Recipe")]
        public void Valid_Recipe()
        {
            string expectedString = String.Format("Recipe: {0}", testDrinkName);
            string actualString;

            IngredientType testAlcoholType = new IngredientType(DrinkEnums.IngredientTypeEnum.Alcohol);
            IngredientType testSodaType = new IngredientType(DrinkEnums.IngredientTypeEnum.Soda);

            Ingredient testRum = new Ingredient(testRumName, testAlcoholType);
            Ingredient testCola = new Ingredient(testColaName, testSodaType);

            Dictionary<Ingredient, string> rumAndColaIngredientsDict = new Dictionary<Ingredient, string>() { { testRum, "1.5" }, { testCola, "4.5" }, };

            // Recipes
            Recipe testRumCoke = new Recipe(testDrinkName, testRocksGlass, rumAndColaIngredientsDict);
            actualString = testRumCoke.ToString();

            Console.WriteLine("Expected:\t\"{0}\"\nActual:\t\t\"{1}\"", expectedString, actualString);
            Assert.AreEqual(expectedString, actualString);
        }

        /// <summary>
        /// Ensures a drink can only have one type of ingredient.
        /// </summary>
        public void Invalid_Recipe_Duplicate_Ingredients()
        {

        }
        #endregion

        #region Recipie Collection Tests
        /// <summary>
        /// Makes sure two identicle drinks cannot be added to the database.
        /// </summary>
        [TestMethod]
        [TestCategory("BVT"), TestCategory("Recipe")]
        public void Invalid_RecipeCollection_Shuffled_Ingredients_Duplicate()
        {
            int expectedDrinks = 1;

            string expectedString = "Drinks:\nTest & Debug";
            string actualString;

            RecipeCollection testDrinkBook = new RecipeCollection();

            IngredientType testAlcoholType = new IngredientType(DrinkEnums.IngredientTypeEnum.Alcohol);
            IngredientType testSodaType = new IngredientType(DrinkEnums.IngredientTypeEnum.Soda);

            Ingredient testRum = new Ingredient(testRumName, testAlcoholType);
            Ingredient testCola = new Ingredient(testColaName, testSodaType);

            Dictionary<Ingredient, string> rumAndColaIngredientsDict = new Dictionary<Ingredient, string>() { { testRum, "1.5" }, { testCola, "4.5" }, };
            Dictionary<Ingredient, string> shuffledRumAndColaIngredientsDict = new Dictionary<Ingredient, string>() { { testCola, "4.5" }, { testRum, "1.5" } };

            Recipe rumCoke = new Recipe(testDrinkName, testRocksGlass, rumAndColaIngredientsDict);
            Recipe shuffledRumCoke = new Recipe(testDrinkName, testRocksGlass, shuffledRumAndColaIngredientsDict);

            List<Recipe> testRecipeList = new List<Recipe>(){ rumCoke, rumCoke };

            foreach (Recipe recipe in testRecipeList)
            {
                Console.WriteLine("Attempting to add {0}\tDrinkBook count = {1}", recipe.ToString(), testDrinkBook.Count);
                testDrinkBook.Add(recipe);
            }

            Console.WriteLine("All drinks added.");

            int actualDrinks = testDrinkBook.Count;
            Console.WriteLine("Expected Drinks:\t{0}\nActual Drinks:\t{1}", expectedDrinks, actualDrinks);
            Assert.AreEqual(expectedDrinks, actualDrinks);

            actualString = testDrinkBook.ListDrinks();
            Console.WriteLine("Expected:\n{0}\nActual:\n{1}", expectedString, actualString);

            Assert.AreEqual(expectedString, actualString, "FAIL");
        }

        /// <summary>
        /// Ensures two drinks with the same ingredients (but in a different order) don't get entered.
        /// </summary>
        [TestMethod]
        [TestCategory("BVT"), TestCategory("Recipe")]
        public void Invalid_RecipeCollection_Shuffled_Duplicate_Recipe()
        {
            int expectedDrinks = 1;

            string expectedString = "Drinks:\nTest & Debug";
            string actualString;

            RecipeCollection testDrinkBook = new RecipeCollection();

            IngredientType testAlcoholType = new IngredientType(DrinkEnums.IngredientTypeEnum.Alcohol);
            IngredientType testSodaType = new IngredientType(DrinkEnums.IngredientTypeEnum.Soda);

            Ingredient testRum = new Ingredient(testRumName, testAlcoholType);
            Ingredient testCola = new Ingredient(testColaName, testSodaType);

            Dictionary<Ingredient, string> rumAndColaIngredientsDict = new Dictionary<Ingredient, string>() { { testRum, "1.5" }, { testCola, "4.5" }, };
            Dictionary<Ingredient, string> shuffledRumAndColaIngredientsDict = new Dictionary<Ingredient, string>() { { testCola, "4.5" }, { testRum, "1.5" } };

            Recipe rumCoke = new Recipe(testDrinkName, testRocksGlass, rumAndColaIngredientsDict);
            Recipe shuffledRumCoke = new Recipe(testDrinkName, testRocksGlass, shuffledRumAndColaIngredientsDict);

            List<Recipe> testRecipeList = new List<Recipe>() { rumCoke, shuffledRumCoke };

            foreach (Recipe recipe in testRecipeList)
            {
                Console.WriteLine("Attempting to add {0}\tDrinkBook count = {1}", recipe.ToString(), testDrinkBook.Count);
                testDrinkBook.Add(recipe);
            }

            Console.WriteLine("All drinks added.");

            int actualDrinks = testDrinkBook.Count;
            Console.WriteLine("Expected Drinks:\t{0}\nActual Drinks:\t{1}", expectedDrinks, actualDrinks);
            Assert.AreEqual(expectedDrinks, actualDrinks);

            actualString = testDrinkBook.ListDrinks();
            Console.WriteLine("Expected:\n{0}\n\nActual:\n{1}", expectedString, actualString);

            Assert.AreEqual(expectedString, actualString, "FAIL");
        }

        /// <summary>
        /// Ensureus a drink can be removed from the Collection via .Remove()
        /// </summary>
        [TestMethod]
        [TestCategory("BVT"), TestCategory("Recipe Collection")]
        public void Valid_Remove_Recipe_From_Collection()
        {
            int expectedDrinks = 0;
            int actualDrinks;

            RecipeCollection testDrinkBook = new RecipeCollection();

            IngredientType testAlcoholType = new IngredientType(DrinkEnums.IngredientTypeEnum.Alcohol);
            IngredientType testSodaType = new IngredientType(DrinkEnums.IngredientTypeEnum.Soda);

            Ingredient testRum = new Ingredient(testRumName, testAlcoholType);
            Ingredient testCola = new Ingredient(testColaName, testSodaType);

            Dictionary<Ingredient, string> rumAndColaIngredientsDict = new Dictionary<Ingredient, string>() { { testRum, "1.5" }, { testCola, "4.5" }, };

            Recipe testRumCoke = new Recipe(testDrinkName, testRocksGlass, rumAndColaIngredientsDict);

            testDrinkBook.Add(testRumCoke);
            Console.WriteLine("Added {0} to Drink Book.\t Drink Book Count = {1}", testRumCoke.ToString(), testDrinkBook.Count);

            Console.WriteLine("Attempting to remove the drink we just added.");
            testDrinkBook.Remove(testRumCoke);
            actualDrinks = testDrinkBook.Count;

            Assert.AreEqual(expectedDrinks, actualDrinks, "FAIL");
            Console.WriteLine("Number of drinks remaining: {0} (Expected: {1})", actualDrinks, expectedDrinks);
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
            int expectedDrinks = 2;

            string testRecipes = @"BarTests\Resources\testValidRecipes.txt";

            RecipeCollection testDrinkBook = new RecipeCollection();
            RecipeReader newReader = new RecipeReader();

            testDrinkBook = newReader.loadRecipesFromFile(testDrinkBook, testRecipes);

            Console.WriteLine("Expected drinks: {0}. Actual drinks: {1}", 2, testDrinkBook.Count);
            Assert.AreEqual(expectedDrinks, testDrinkBook.Count, "FAIL");
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
            int expectedDrinks = 1;
            int actualDrinks;
            string expectedString = "Drinks:\nLong Island";
            string actualString;

            string testRecipeFile = @"BarTests\Resources\testInvalidRecipesDuplicateDrinks.txt";

            RecipeCollection testDrinkBook = new RecipeCollection();
            RecipeReader newReader = new RecipeReader();

            testDrinkBook = newReader.loadRecipesFromFile(testDrinkBook, testRecipeFile);
            actualDrinks = testDrinkBook.Count;
            Assert.AreEqual(expectedDrinks, actualDrinks);
            Console.WriteLine("Expected Drinks:\t{0}\tActual Drinks:\t{1}", expectedDrinks, actualDrinks);

            actualString = testDrinkBook.ListDrinks();

            Assert.AreEqual(expectedString, actualString, "Wrong drink list.");
            Console.WriteLine("\nExpected:\n{0}\n\nActual:\n{1}", expectedString, actualString);
        }

        /// <summary>
        /// Test that two drinks with identical ingredients (in a different order), result in only
        /// one drink being added. (No Duplicates)
        /// </summary>
        [TestMethod]
        [TestCategory("BVT"), TestCategory("Input/Output")]
        public void Invalid_LoadFromFile_Shuffled_Duplicates()
        {
            int expectedDrinks = 1;
            int actualDrinks;
            string expectedString = "Drinks:\nLong Island";
            string actualString;

            string testRecipeFile = @"BarTests\Resources\testInvalidRecipesShuffledIngredients.txt";
            
            RecipeCollection testDrinkBook = new RecipeCollection();
            RecipeReader newReader = new RecipeReader();

            testDrinkBook = newReader.loadRecipesFromFile(testDrinkBook, testRecipeFile);
            actualDrinks = testDrinkBook.Count;
            Assert.AreEqual(expectedDrinks, actualDrinks);
            Console.WriteLine("Expected Drinks:\t{0}\tActual Drinks:\t{1}", expectedDrinks, actualDrinks);

            actualString = testDrinkBook.ListDrinks();

            Assert.AreEqual(expectedString, actualString, "Wrong drink list.");
            Console.WriteLine("\nExpected:\n{0}\n\nActual:\n{1}", expectedString, actualString);
        }

        /// <summary>
        /// Test that no drinks are added from an empty file.
        /// </summary>
        [TestMethod]
        [TestCategory("BVT"), TestCategory("Input/Output")]
        public void Invalid_LoadFromFile_Test_Empty_File()
        {
            bool success = true;

            string testRecipes = @"BarTests\Resources\testInvalidRecipesEmptyFile.txt";

            RecipeCollection testDrinkBook = new RecipeCollection();
            RecipeReader newReader = new RecipeReader();

            testDrinkBook = newReader.loadRecipesFromFile(testDrinkBook, testRecipes);

            success &= (testDrinkBook.Count == 0);

            Assert.AreEqual(success, true, "Expected drinks: {0}. Actual drinks: {1}", 0, testDrinkBook.Count);
        }

        /// <summary>
        /// Ensures a file of gibberish will not cause any drinks to be added.
        /// </summary>
        [TestMethod]
        [TestCategory("Input/Output")]
        public void Invalid_LoadFromFile_Test_Garbage_File()
        {
            bool success = true;

            string testRecipes = @"BarTests\Resources\testInvalidRecipesGarbage.txt";

            RecipeCollection testDrinkBook = new RecipeCollection();
            RecipeReader newReader = new RecipeReader();

            testDrinkBook = newReader.loadRecipesFromFile(testDrinkBook, testRecipes);

            success &= (testDrinkBook.Count == 0);

            Assert.AreEqual(success, true, "Expected drinks: {0}. Actual drinks: {1}", 0, testDrinkBook.Count);
        }

        /// <summary>
        /// Ensures that the RecipeReader can handle extra spacing in the importing.
        /// </summary>
        [TestMethod]
        [TestCategory("Input/Output")]
        public void Invalid_LoadFromFile_Test_Extra_Spacing_File()
        {
            int expectedDrinks = 2;

            string testRecipes = @"BarTests\Resources\testInvalidRecipesStrangeFormatting.txt";

            RecipeCollection testDrinkBook = new RecipeCollection();
            RecipeReader newReader = new RecipeReader();

            testDrinkBook = newReader.loadRecipesFromFile(testDrinkBook, testRecipes);

            Assert.AreEqual(expectedDrinks, testDrinkBook.Count, "Wrong number of expected drinks returned.");
        }
        #endregion
    }
}
