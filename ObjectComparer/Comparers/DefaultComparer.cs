using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectComparer.Comparers
{
    internal class DefaultComparer : IComparer
    {
        public DefaultComparer() { }// : base() { }

        public bool IsComparable(Type type)
        {
            return true;
        }

        public  bool Compare(object a, object b)
        {
            return false;
        }


    }
}
