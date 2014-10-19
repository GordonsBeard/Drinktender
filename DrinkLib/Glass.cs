using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrinkLib
{
    public class Glass
    {
        // Properties
        private DrinkEnums.GlassTypeEnum glassType;

        public DrinkEnums.GlassTypeEnum Type
        {
            get { return this.glassType; }
            set
            {
                this.SetType(value);
            }
        }

        // Constructors
        public Glass()
        {
            this.Type = DrinkEnums.GlassTypeEnum.Undefined;
        }
        public Glass(DrinkEnums.GlassTypeEnum type)
        {
            this.Type = type;
        }

        public Glass(string impliedType)
        {
            this.Type = ((DrinkEnums.GlassTypeEnum)Enum.Parse(typeof(DrinkEnums.GlassTypeEnum), impliedType.Trim()));
        }

        // Functions
        public void SetType(string impliedType)
        {
            DrinkEnums.GlassTypeEnum glassEnum = (DrinkEnums.GlassTypeEnum)Enum.Parse(typeof(DrinkEnums.GlassTypeEnum), impliedType.ToString());
            this.glassType = glassEnum;
        }
        public void SetType(DrinkEnums.GlassTypeEnum glassEnum)
        {
            this.glassType = glassEnum;
        }

        // Overrides
        public override string ToString()
        {
            return String.Format("Glass: {0}", this.Type.ToString());
        }
    }
}
