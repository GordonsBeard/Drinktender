using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DrinkLib
{
    public class RecipeAlreadyExistsException : Exception
    {
        public override string ToString()
        {
            return base.ToString();
        }
    }
}
