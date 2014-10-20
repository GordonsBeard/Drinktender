using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic.FileIO;
using System.Collections;

namespace DrinkLib
{
    /// <summary>
    /// Blueprint for all recipes.
    /// </summary>
    public class Recipe : IEquatable<Recipe>
    {
        // Properties
        private string name;
        public string Name
        {
            get
            {
                return this.name;
            }
            set
            {
                if (!(String.IsNullOrWhiteSpace(value)))
                {
                    this.name = value;
                }
            }
        }

        public Glass Glass { get; set; }

        public Dictionary<Ingredient, string> Ingredients { get; set; }

        // Constructors
        public Recipe(string name, Glass glass, Dictionary<Ingredient, string> ingredients)
        {
            this.Name = name;
            this.Glass = glass;
            this.Ingredients = ingredients;
        }

        // Overrides
        public override string ToString()
        {
            return this.Name;
        }

        public override int GetHashCode()
        {
            int hashCode = String.Join(", ", this.Ingredients.ToString()).GetHashCode();
            hashCode = (hashCode * 17) + this.Name.ToString().GetHashCode();

            return hashCode;
        }

        public bool Equals(Recipe other)
        {
            bool same = true;

            Dictionary<Ingredient, string> dict1 = this.Ingredients;
            Dictionary<Ingredient, string> dict2 = other.Ingredients;

            same &= dict1.Keys.SequenceEqual(dict2.Keys) ? true : false;

            same &= this.Glass == other.Glass ? true : false;

            same &= !(this.Ingredients.Except(other.Ingredients).Any());

            return same;
        }

        public int GetHashCode(Recipe obj)
        {
            throw new NotImplementedException();
        }

    }


    /// <summary>
    /// The RecipeCollection will be the master DrinkBook for consumers of the library to use.
    /// </summary>
    public class RecipeCollection : ICollection<Recipe>
    {
        // Interfaces
        public IEnumerator<Recipe> GetEnumerator()
        {
            return new RecipeEnumerator(this);
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return new RecipeEnumerator(this);
        }

        // Private variables
        private List<Recipe> innerCol;
        private bool isRO = false;

        // Constructors
        // When initialized creates the empty list for the innerCol.
        public RecipeCollection()
        {
            innerCol = new List<Recipe>();
        }

        // Indexing
        public Recipe this[int index]
        {
            get { return (Recipe)innerCol[index]; }
            set { innerCol[index] = value; }
        }

        // ICollection Functions
        /// <summary>
        /// Adds a Recipe to the collection, after checking if the Recipe does not already exist in the collection.
        /// </summary>
        /// <param name="newDrink">New Recipe object to be aded.</param>
        public void Add(Recipe newDrink)
        {
            #if DEBUG
            Console.WriteLine("Add {0}", newDrink.ToString());
            #endif

            // Before adding, make sure the Recipe does not exist in the RecipeCollection.
            if (!Contains(newDrink))
            {
                #if DEBUG
                Console.WriteLine("Adding drink to innerCol.\t\tinnerCol: {0}", innerCol.Count);
                #endif  
                
                // If false, the Recipe is brand new to the RecipeCollection (via innerCol).
                innerCol.Add(newDrink);
                
                #if DEBUG
                Console.WriteLine("Added.\t\t\t\t\tinnerCol: {0}", innerCol.Count);
                #endif
            }
        }

        public bool Contains(Recipe newDrink)
        {
            bool drinkExists = false;

            // Go through the list of current drinks (innerCol) and then compare
            // each of the Recipes to each other.
            foreach (Recipe drink in innerCol)
            {
                if (drinkExists = newDrink.GetHashCode() == drink.GetHashCode())
                {
#if DEBUG
                    Console.WriteLine("Hashes: {0} ({1}) and {2} ({3}) are same.", newDrink.GetHashCode().ToString(), newDrink.Name, drink.GetHashCode().ToString(), drink.Name);
#endif
                    drinkExists = true;
                }
            }
            return drinkExists;
        }

        public void Clear()
        {
            innerCol.Clear();
        }

        public void CopyTo(Recipe[] array, int arrayIndex)
        {
            for (int i = 0; i < innerCol.Count; i++)
            {
                array[i] = (Recipe)innerCol[i];
            }
        }

        public int Count
        {
            get { return innerCol.Count; }
        }

        public bool IsReadOnly
        {
            get { return isRO; }
        }

        public bool Remove(Recipe item)
        {
            bool result = false;

            // Iterate the innerCol to find the drink
            // to remove.
            for (int i = 0; i < innerCol.Count; i++)
            {
                Recipe curRecipe = (Recipe)innerCol[i];

                if (curRecipe.Equals(item))
                {
                    innerCol.RemoveAt(i);
                    result = true;
                    break;
                }
            }
            return result;
        }

        // Public Functions
        /// <summary>
        /// List all the drinks, with no ingredients as a string.
        /// </summary>
        /// <returns>String that contains all drink names.</returns>
        public string ListDrinks()
        {
            string drinkNames;
            drinkNames = "Drinks:";
            foreach (Recipe drink in innerCol)
            {
                drinkNames += String.Format("\n{0}", drink.Name);
            }
            return drinkNames;
        }

        /// <summary>
        /// List all the drinks with their ingredients as a string.
        /// </summary>
        /// <returns>String that contains all drinks and recipes.</returns>
        public string ListFullDrinks()
        {
            string drinkNames;
            drinkNames = "All Drinks:";
            foreach (Recipe drink in innerCol)
            {
                string ingredients = String.Join(", ", drink.Ingredients);
                drinkNames += String.Format("\n{0}:\n\t{1}\n", drink.Name, ingredients);
            }
            return drinkNames;
        }

        /// <summary>
        /// Returns a random drink from the innerCollection.
        /// </summary>
        /// <returns>A random Recipe.</returns>
        public Recipe RandomDrink()
        {
            Random random = new Random();

            Recipe randomRecipe = this[random.Next(0, innerCol.Count)];

            return randomRecipe;
        }
    }
    
    /// <summary>
    /// The Enumerator allows for indexing and manipulation of DrinkBook through indexing and such.
    /// </summary>
    public class RecipeEnumerator : IEnumerator<Recipe>
    {
        private RecipeCollection _collection;
        private int curIndex;
        private Recipe curRecipe;

        public RecipeEnumerator(RecipeCollection collection)
        {
            _collection = collection;
            curIndex = -1;
            curRecipe = default(Recipe);
        }

        public bool MoveNext()
        {
            if (++curIndex >= _collection.Count<Recipe>())
            {
                return false;
            }
            else
            {
                curRecipe = _collection[curIndex];
            }
            return true;
        }

        public Recipe Current
        {
            get { return curRecipe; }
        }

        public void Dispose()
        {
            
        }

        object System.Collections.IEnumerator.Current
        {
            get { return Current; }
        }

        public void Reset()
        {
            curIndex = -1;
        }
    }
}
