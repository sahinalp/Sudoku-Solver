using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public enum OutputTypes
    {
        AllNumber = 1,
        NullOrEmpty = -1,
        NotNumber = -10,
        NotBetween1and9 = -100,
    }
}
