﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrinkLib
{
    public class DrinkEnums
    {
        public enum GlassTypeEnum
        {
            Bottle,
            Margarita,
            Highball,
            Shotglass,
            Martini,
            Collins,
            Rocks,
            Cocktail,
            Wine,
            Champagne,
            Hurricane,
            Mug,
            Pint,
            Undefined,
        }
        public enum IngredientTypeEnum
        {
            Alcohol,
            Liqueur,
            Soda,
            Juice,
            Bitters,
            Syrup,
            Food,
            Ice,
            Undefined,
        }

        public enum IngredientTypeUnits
        {
            oz,
            piece,
            dash,
            scoop,
            Undefined,
        }
    }
}
