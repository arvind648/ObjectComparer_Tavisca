using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectComparer.Comparers
{
    internal class ValueTypeComparer : BaseComparer
    {
        public ValueTypeComparer() : base() { }

        private static readonly Type[] ValueTypes = new Type[]
            {
                typeof(int),
                typeof(uint),
                typeof(long),
                typeof(ulong),
                typeof(float),
                typeof(double),
                typeof(decimal),
                typeof(byte),
                typeof(string),
                typeof(DateTime),
                typeof(bool),
                typeof(char)
            };

        public override bool IsComparable(Type type)
        {
            return ValueTypes.Contains(type) || type.IsEnum;
        }

        protected override bool Compare(object a, object b)
        {            
            return a.Equals(b);
        }



    }
}
