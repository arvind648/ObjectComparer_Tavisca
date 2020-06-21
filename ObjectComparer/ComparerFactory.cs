using Newtonsoft.Json.Serialization;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;

namespace ObjectComparer.Comparers
{
    internal class ComparerFactory
    {
        private readonly IComparer[] comparers =  {
                Singleton<ValueTypeComparer>.Instance,
                Singleton<EnumerableComparer>.Instance,
                Singleton<ClassComparer>.Instance
            };

        //private static IComparer[] _comparers = null;
        //public IEnumerable<IComparer> comparers
        //{
        //    get
        //    {
        //        if (_comparers == null)
        //        {
        //            _comparers = AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes())
        //                   .Where(x => typeof(IComparer).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
        //                   .Select(x => (IComparer)Activator.CreateInstance(x))
        //                   .Where(x=> x.GetType() != typeof(DefaultComparer)).ToArray();
        //        }
        //        return _comparers;
        //    }

        //}

        //private static readonly IEnumerable<IComparer> comparers = AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes())
        //           .Where(x => typeof(IComparer).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
        //           .Select(x => (IComparer)Activator.CreateInstance(x)).ToList();

        public IComparer GetComparer(Type type)
        {

            //IEnumerable<IComparer> comparers = AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes())
            //    .Where(x => typeof(IComparer).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
            //    .Select(x => (IComparer)Activator.CreateInstance(x)).ToList();

            //IEnumerable<IComparer> comparers = typeof(IComparer).Assembly.GetTypes()
            //         .Where(t => t.IsSubclassOf(typeof(IComparer)) && !t.IsAbstract && t != typeof(DefaultComparer))
            //     .Select(t => (IComparer)Activator.CreateInstance(t)).ToArray();

            return comparers.FirstOrDefault(c => c.IsComparable(type)) ?? Singleton<DefaultComparer>.Instance;
        }

    }


}
