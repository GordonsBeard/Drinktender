Drinktender
==========

A library for managing Drink Recipes. Not intended for public use but this is the MIT License so I don't really care what you plan on doing with it.

* * *

### DrinkLib
The actual recipe library.

#####Features:
+ Can import/add Recipes through text files. (See recipes.txt for examples)

Each Recipe identifies:
+ Name			(string)
+ Glass			(Glass object containing Name and eventually Size of glass.)
+ Ingredients	(List of Dictionary<Ingredient, string> representing the Ingredient and the amount for the drink.)

### BarTests
Drinktender is fully tested. Unit tests can be found in BarTests, ran via VS.