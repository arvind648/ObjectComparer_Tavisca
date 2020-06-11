using System;
using System.Linq.Expressions;
using System.Reflection;
using ObjectComparer.Utils;

namespace ObjectComparer
{
    public abstract class BaseComparer : IBaseComparer
    {        

        public IValueComparer DefaultValueComparer { get; private set; }

        protected IComparersFactory Factory { get; }


        protected BaseComparer( BaseComparer parentComparer, IComparersFactory factory)
        {
            Factory = factory ?? new ComparersFactory();            
            DefaultValueComparer = new DefaultValueComparer();
            if (parentComparer != null)
            {
                DefaultValueComparer = parentComparer.DefaultValueComparer;               
            }
        }
        
    }
}