using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlyingDutchmanAirlines
{
    internal static class ExtensionMethods
    {
        internal static bool isPositive (this int input)
        {
            return input >= 0;
        }
    }
}
