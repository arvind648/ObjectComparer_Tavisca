using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectComparer
{
    interface IComparer
    {
        bool IsComparable(Type type);

        bool Compare(object a, object b);
    }
}
