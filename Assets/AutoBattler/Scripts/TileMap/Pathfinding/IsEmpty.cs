using System.Collections.Generic;
using System.Linq;

namespace AutoBattler
{
    public class Utilities
    {
        public static bool IsEmpty<T>(List<T> list)
        {
            if (list == null)
            {
                return true;
            }

            return !list.Any();
        }
    }
}
