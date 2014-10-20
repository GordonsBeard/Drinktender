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
        private IngredientType type;

        public string Name { get; set; }
        public IngredientType Type
        {
            get
            {
                return this.type;
            }
        }

        // Constructors
        public Ingredient()
        {
            this.Name = String.Empty;
            this.SetType(DrinkEnums.IngredientTypeEnum.Undefined);
        }

        public Ingredient(string name)
        {
            this.Name = name;
            this.SetIngredientTypeByName(name);
        }

        public Ingredient(string name, IngredientType type)
        {
            this.Name = name;
            this.SetType(type);
        }

        // Functions
        private void SetType(DrinkEnums.IngredientTypeEnum value)
        {
            this.type = new IngredientType(value);
        }

        private void SetType(IngredientType value)
        {
            this.type = value;
        }

        private void SetType(string value)
        {
            DrinkEnums.IngredientTypeEnum ingType = (DrinkEnums.IngredientTypeEnum)Enum.Parse(typeof(DrinkEnums.IngredientTypeEnum), value);
            this.type = new IngredientType(ingType);
        }

        private void SetIngredientTypeByName(string ingredientName)
        {
            Dictionary<string, List<string>> typesOfIngDict = new Dictionary<string, List<string>>();

            typesOfIngDict.Add("Alcohol", new List<string>  { "Vodka", "Rum", "White Rum", "Spiced Rum", "Dark Rum", "Gin", "Tequila" });
            typesOfIngDict.Add("Soda", new List<string>     { "Tonic", "Soda", "Spiced Rum", "Cola", "Gin", "Sprite" });
            typesOfIngDict.Add("Liquer", new List<string>   { "Triple Sec", "Grand Mariner", "Kahlua", "Irish Creme" });
            typesOfIngDict.Add("Syrup", new List<string>    { "Simple Syrup", "Grenadine", "Sweetened Lime Juice" });
            typesOfIngDict.Add("Ice", new List<string>      { "Ice" });
            typesOfIngDict.Add("Juice", new List<string>    { "Olive Juice", "Lemon Juice", "Orange Juice", "Lime Juice" });
            typesOfIngDict.Add("Food", new List<string>     { "Olive", "Lemon Wedge", "Celery" });
            typesOfIngDict.Add("Bitters", new List<string>  { "Bitters" });

            foreach (KeyValuePair<string, List<string>> ingredientClassObj in typesOfIngDict)
            {
                // If the recipe's ingredients are known, automatically infer their type.
                if (ingredientClassObj.Value.Contains(ingredientName))
                {
                    SetType(ingredientClassObj.Key.ToString());
                }
                // Else give them a type of Undefined.
                else
                {
                    SetType(DrinkEnums.IngredientTypeEnum.Undefined);
                }
            }
        }

        // Overrides
        public override string ToString()
        {
            return this.Name;
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
