using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrinkLib
{
    // Top-level Recipe exception, always look for these when defining a specific Recipe.
    public class InvalidRecipeException : Exception
    {

    }

    // Invalid Recipe (already exists) caught during adding. Will return Found/Entered drinks.
    public class DuplicateRecipeException : InvalidRecipeException { }

    // Invalid Recipe caught during RecipeReading. Will return the offending line.
    public class InvalidRecipeLineException : InvalidRecipeException
    {
        private string line;
        private InvalidRecipeException ex;

        public InvalidRecipeLineException(string attemptedLine)
        {
            this.line = attemptedLine;
        }
        public InvalidRecipeLineException(string[] attemptedLine)
        {
            this.line = String.Join(", ", attemptedLine);
            #if DEBUG
            Console.WriteLine("Bad drink: {0}", String.Join(", ", attemptedLine));
            #endif
        }

        public InvalidRecipeLineException(string[] attemptedLine, InvalidRecipeException ex)
        {
            #if DEBUG
            Console.WriteLine("Bad drink: {0}", String.Join(", ", attemptedLine));
            #endif

            this.ex = ex;
        }
    }

    // Invalid Recipe caught during Glass declaration. Will return bad glass line.
    public class InvalidGlassException : InvalidRecipeException
    {
        private string glass;

        public InvalidGlassException(string attemptedGlass)
        {
            this.glass = attemptedGlass;
        }
    }

}
