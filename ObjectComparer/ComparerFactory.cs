using Newtonsoft.Json.Serialization;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;

namespace ObjectComparer.Comparers
{
    internal class ComparerFactory
    {
        private readonly BaseComparer[] comparers =  {
                Singleton<ValueTypeComparer>.Instance,
                Singleton<EnumerableComparer>.Instance,
                Singleton<ClassComparer>.Instance
            };

        public BaseComparer GetComparer(Type type)
        {
            //IEnumerable<BaseComparer> comparers = typeof(BaseComparer).Assembly.GetTypes()
            //         .Where(t => t.IsSubclassOf(typeof(BaseComparer)) && !t.IsAbstract && t != typeof(DefaultComparer))
            //     .Select(t => (BaseComparer)Activator.CreateInstance(t)).ToArray();

            return comparers.FirstOrDefault(c => c.IsComparable(type)) ?? Singleton<DefaultComparer>.Instance;
        }

    }


}
