using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ObjectComparer.Utils;

namespace ObjectComparer
{
    public class EnumerablesComparer<T> : AbstractComparer
    {
        private readonly IComparer<T> _comparer;

        public EnumerablesComparer( BaseComparer parentComparer, ComparersFactory factory)
            : base( parentComparer, factory)
        {
            _comparer = Factory.GetObjectComparer<T>( this);
        }

        public override bool IsSimilar(Type type, object obj1, object obj2)
        {
            if (!type.InheritsFrom(typeof(IEnumerable<>)))
            {
                throw new ArgumentException("Invalid type");
            }


            obj1 = obj1 ?? Enumerable.Empty<T>();
            obj2 = obj2 ?? Enumerable.Empty<T>();

            if (!obj1.GetType().InheritsFrom(typeof(IEnumerable<T>)))
            {
                throw new ArgumentException(nameof(obj1));
            }

            if (!obj2.GetType().InheritsFrom(typeof(IEnumerable<T>)))
            {
                throw new ArgumentException(nameof(obj2));
            }

            var list1 = ((IEnumerable<T>)obj1).ToList();
            var list2 = ((IEnumerable<T>)obj2).ToList();
            try
            {
                list1.Sort();
                list2.Sort();
            }
            catch (Exception e) { }

            if (list1.Count != list2.Count)
            {
                return false;
                //if (!type.GetTypeInfo().IsArray)
                //{

                //    yield return new Difference("", list1.Count.ToString(), list2.Count.ToString(),
                //        DifferenceTypes.NumberOfElementsMismatch);
                //}

                //yield break;
            }

            for (var i = 0; i < list2.Count; i++)
            {
                if (!_comparer.IsSimilar(list1[i], list2[i])) return false;
                //foreach (var failure in _comparer.CalculateDifferences(list1[i], list2[i]))
                //{
                //    yield return failure.InsertPath($"[{i}]");
                //}
            }
            return true;
        }
    }
}
