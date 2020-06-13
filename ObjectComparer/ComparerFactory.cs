using Newtonsoft.Json.Serialization;
using System;
using System.CodeDom;
using System.Linq;

namespace ObjectComparer.Comparers
{
    internal class ComparerFactory
    {              
        private readonly BaseComparer[] Comparers =  {              
                Singleton<ValueTypeComparer>.Instance,
                Singleton<EnumerableComparer>.Instance,
                Singleton<ClassComparer>.Instance
            };

        public BaseComparer GetComparer(Type type)
        {
            return Comparers.FirstOrDefault(c => c.IsComparable(type)) ?? Singleton<DefaultComparer>.Instance;
        }

    }

    
}
