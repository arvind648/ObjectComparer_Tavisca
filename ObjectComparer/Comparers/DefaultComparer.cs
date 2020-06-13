using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectComparer.Comparers
{
    internal class DefaultComparer : BaseComparer
    {
        public DefaultComparer() : base() { }

        public override bool IsComparable(Type type)
        {
            return true;
        }

        protected override bool Compare(object a, object b)
        {
            return false;
        }


    }
}
