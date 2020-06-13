using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectComparer.Comparers
{
    internal abstract class BaseComparer
    {        
        public abstract bool IsComparable(Type type);

        public bool AreEqual(object a, object b)
        {
            try
            {
                if (a == null || b == null) return a == b;
                else if (a == b) return true;
                else if (a.Equals(b)) return true;
                else
                {
                    bool result = this.Compare(a, b);
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        protected abstract bool Compare(object a, object b);
    }
}
