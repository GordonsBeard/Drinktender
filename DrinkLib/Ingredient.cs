using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DrinkLib
{
    /// <summary>
    /// The Ingredients contain information on their name and type.
    /// Type will be Unknown/Undefined if added just by name.
    /// </summary>
    public class Ingredient
    {
        // Properties
        public string Name { get; set; }
        public IngredientType Type { get; set; }

        // Constructors
        public Ingredient()
        {
            this.Name = String.Empty;
            this.Type = new IngredientType(DrinkEnums.IngredientTypeEnum.Undefined);
        }

        public Ingredient(string name)
        {
            this.Name = name;
            this.Type = new IngredientType(DrinkEnums.IngredientTypeEnum.Undefined);
        }
        public Ingredient(string name, IngredientType type)
        {
            this.Name = name;
            this.Type = type;
        }

        // Overrides
        public override string ToString()
        {
            return String.Format("Ingredient: {0}", this.Name);
        }
    }

    /// <summary>
    /// IngredientType will define the units, and eventually, color and weight
    /// and glassEnum have you.
    /// </summary>
    public class IngredientType
    {
        public DrinkEnums.IngredientTypeEnum Type { get; set; }
        public DrinkEnums.IngredientTypeUnits Units { get; set; }

        public IngredientType(DrinkEnums.IngredientTypeEnum typeEnum)
        {
            this.Type = typeEnum;

            switch (typeEnum)
            {
                case DrinkEnums.IngredientTypeEnum.Alcohol:
                case DrinkEnums.IngredientTypeEnum.Juice:
                case DrinkEnums.IngredientTypeEnum.Soda:
                case DrinkEnums.IngredientTypeEnum.Syrup:
                case DrinkEnums.IngredientTypeEnum.Liqueur:
                    this.Units = DrinkEnums.IngredientTypeUnits.oz;
                    break;

                case DrinkEnums.IngredientTypeEnum.Bitters:
                    this.Units = DrinkEnums.IngredientTypeUnits.dash;
                    break;

                case DrinkEnums.IngredientTypeEnum.Food:
                    this.Units = DrinkEnums.IngredientTypeUnits.piece;
                    break;

                case DrinkEnums.IngredientTypeEnum.Ice:
                    this.Units = DrinkEnums.IngredientTypeUnits.scoop;
                    break;

                case DrinkEnums.IngredientTypeEnum.Undefined:
                default:
                    this.Units = DrinkEnums.IngredientTypeUnits.Undefined;
                    break;
            }
        }

        public IngredientType(string name)
        {

        }

        public override string ToString()
        {
            return this.Type.ToString();
        }
    }
}
