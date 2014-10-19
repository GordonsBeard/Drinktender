using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrinkLib
{
    public class RecipeAlreadyExistsInCollectionException : Exception
    {
    }

    public class RecipeAlreadyExistsException : Exception
    {
    }

    public class InvalidGlassException : Exception
    {
    }

    public class InvalidRecipeLineException : Exception
    { 
    }
}
