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
        }
        public Glass(DrinkEnums.GlassTypeEnum impliedType)
        {
            this.SetType(impliedType);
        }

        public Glass(string impliedType)
        {
            this.SetType(impliedType);
        }

        // Functions
        public void SetType(string impliedType)
        {
            DrinkEnums.GlassTypeEnum glassEnum;
            try
            {
                glassEnum = (DrinkEnums.GlassTypeEnum)Enum.Parse(typeof(DrinkEnums.GlassTypeEnum), impliedType.ToString());
                this.glassType = glassEnum;
            }
            catch (ArgumentException)
            {
                throw new InvalidGlassException(impliedType.ToString());
            }
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
