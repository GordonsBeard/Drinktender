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
                if (Enum.IsDefined(typeof(DrinkEnums.GlassTypeEnum), value))
                {
                    // Only set the glass type if it is indeed one of the glass types.
                    this.glassType = value;
                }
            }
        }

        // Constructors
        public Glass(DrinkEnums.GlassTypeEnum type)
        {
            this.Type = type;
        }

        // Overrides
        public override string ToString()
        {
            return this.Type.ToString();
        }
    }
}
