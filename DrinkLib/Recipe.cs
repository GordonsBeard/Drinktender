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

        public bool Equals(Recipe newRecipe)
        {
            if (new RecipeSameIngredients().Equals(this, newRecipe))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            int hashCode = this.Ingredients.ToString().GetHashCode();
            return hashCode.GetHashCode();
        }
    }

    /// <summary>
    /// Allows for Recipes to be compared.
    /// </summary>
    public class RecipeSameIngredients : EqualityComparer<Recipe>
    {
        /// <summary>
        /// Checks the name of the glass, and number/name of all Ingredients.
        /// </summary>
        /// <param name="d1">First Recipe</param>
        /// <param name="d2">Second Recipe</param>
        /// <returns>If true, the two drinks are effectively the same thing.</returns>
        public override bool Equals(Recipe d1, Recipe d2)
        {
            bool same = true;
            List<Ingredient> testc = d1.Ingredients.Keys.ToList();
            List<Ingredient> testd = d2.Ingredients.Keys.ToList();

            same &= d1.Glass == d2.Glass;
            same &= !(d1.Ingredients.Except(d2.Ingredients).Any());

            return same;
        }

        public override int GetHashCode(Recipe obj)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// The RecipeCollection will be the master DrinkBook for consumers of the library to use.
    /// </summary>
    public class RecipeCollection : ICollection<Recipe>
    {
        public IEnumerator<Recipe> GetEnumerator()
        {
            return new RecipeEnumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new RecipeEnumerator(this);
        }

        // Private inner collection to store the working set of Recipes.
        private List<Recipe> innerCol;

        // For IsReadOnly
        private bool isRO = false;

        // When initialized creates the empty list for the innerCol.
        public RecipeCollection()
        {
            innerCol = new List<Recipe>();
        }

        // Adds ability to index it like an array.
        public Recipe this[int index]
        {
            get { return (Recipe)innerCol[index]; }
            set { innerCol[index] = value; }
        }

        public void Add(Recipe newDrink)
        {
            // Before adding, make sure the Recipe does not exist in the RecipeCollection.
            if (!Contains(newDrink))
            {
                // If false, the Recipe is brand new to the RecipeCollection (via innerCol).
                innerCol.Add(newDrink);
            }
            else
            {
                throw new RecipeAlreadyExistsException();
            }
        }

        public bool Contains(Recipe newDrink)
        {
            bool found = false;
            // Go through the list of current drinks (innerCol) and then compare
            // each of the Recipes to each other.
            foreach (Recipe drink in innerCol)
            {
                if (drink.Equals(newDrink))
                {
                    found = true;
                    return found;
                }
            }
            return found;
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

                if (new RecipeSameIngredients().Equals(curRecipe, item))
                {
                    innerCol.RemoveAt(i);
                    result = true;
                    break;
                }
            }
            return result;
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
