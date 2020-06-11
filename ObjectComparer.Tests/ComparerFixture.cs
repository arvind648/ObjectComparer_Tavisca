using System;
using System.Collections.Generic;
using System.Linq;
using NSubstitute;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ObjectComparer.Tests.TestClasses;
using ObjectComparer.Utils;
using Castle.Core.Resource;

namespace ObjectComparer.Tests
{
    [TestClass]
    public class ComparerFixture
    {
        #region Null_Value
        [TestMethod]
        public void NullValue_InEquality()
        {
            string first = null, second = "arvind";
            var comparer = new Comparer();
            var isEqual = comparer.Compare(first, second, out IEnumerable<Difference> diff);
            Assert.IsFalse(isEqual);
            Assert.IsFalse(diff.IsNullOrEmpty());
        }
        public void NullValue_Equality()
        {
            string first = null, second = null;
            var comparer = new Comparer();
            var isEqual = comparer.Compare(first, second, out IEnumerable<Difference> diff);
            Assert.IsTrue(isEqual);
            Assert.IsTrue(diff.IsNullOrEmpty());
        }
        #endregion

        #region Int
        [TestMethod]
        public void Int_Equality()
        {
            var a1 = new ClassA(9);
            var a2 = new ClassA(9);
            var comparer = new Comparer();

            var isEqual = comparer.Compare(a1, a2, out IEnumerable<Difference> diff);

            Assert.IsTrue(isEqual);
            Assert.IsTrue(diff.IsNullOrEmpty());
        }

        [TestMethod]
        public void Int_Inequality()
        {
            var a1 = new ClassA(10);
            var a2 = new ClassA(8);
            var comparer = new Comparer<ClassA>();

            var isEqual = comparer.Compare(a1, a2, out IEnumerable<Difference> diff);

            Assert.IsFalse(diff.IsNullOrEmpty());
            Assert.IsFalse(isEqual);
            Assert.AreEqual("IntProperty", diff.First().MemberPath);
        }
        #endregion

        #region Datetime
        [TestMethod]
        public void DateTime_Equality()
        {
            DateTime dtNow = DateTime.Now;
            var a1 = new ClassA(dtNow);
            var a2 = new ClassA(dtNow);
            var comparer = new Comparer<ClassA>();

            var isEqual = comparer.Compare(a1, a2, out IEnumerable<Difference> diff);

            Assert.IsTrue(diff.IsNullOrEmpty());
            Assert.IsTrue(isEqual);
        }
        [TestMethod]
        public void DateTime_InEquality()
        {
            DateTime dtNow = DateTime.Now;
            var a1 = new ClassA(dtNow);
            var a2 = new ClassA(dtNow.AddDays(2));
            var comparer = new Comparer<ClassA>();

            var isEqual = comparer.Compare(a1, a2, out IEnumerable<Difference> diff);
            Assert.IsFalse(diff.IsNullOrEmpty());
            Assert.IsFalse(isEqual);
            Assert.AreEqual("DateTimeProperty", diff.First().MemberPath);
        }
        #endregion

        #region Double
        [TestMethod]
        public void Double_Equality()
        {
            var a1 = new ClassA(2.0);
            var a2 = new ClassA(2.0);
            var comparer = new Comparer<ClassA>();

            var isEqual = comparer.Compare(a1, a2, out IEnumerable<Difference> diff);

            Assert.IsTrue(diff.IsNullOrEmpty());
            Assert.IsTrue(isEqual);
        }
        [TestMethod]
        public void Double_InEquality()
        {
            var a1 = new ClassA(2.0);
            var a2 = new ClassA(5.5);
            var comparer = new Comparer<ClassA>();

            var isEqual = comparer.Compare(a1, a2, out IEnumerable<Difference> diff);
            Assert.IsFalse(diff.IsNullOrEmpty());
            Assert.IsFalse(isEqual);
        }
        #endregion

        #region Double
        [TestMethod]
        public void Bool_Equality()
        {
            var a1 = new ClassA(true);
            var a2 = new ClassA(true);
            var comparer = new Comparer<ClassA>();

            var isEqual = comparer.Compare(a1, a2, out IEnumerable<Difference> diff);

            Assert.IsTrue(diff.IsNullOrEmpty());
            Assert.IsTrue(isEqual);
        }
        [TestMethod]
        public void Bool_InEquality()
        {
            var a1 = new ClassA(true);
            var a2 = new ClassA(false);
            var comparer = new Comparer<ClassA>();

            var isEqual = comparer.Compare(a1, a2, out IEnumerable<Difference> diff);
            Assert.IsFalse(diff.IsNullOrEmpty());
            Assert.IsFalse(isEqual);
        }
        #endregion

        #region Int_Array
        [TestMethod]
        public void IntArray_MacthingSequence_Equality()
        {
            var a1 = new ClassA(new int[] { 1, 4, 3 });
            var a2 = new ClassA(new int[] { 1, 4, 3 });
            var comparer = new Comparer<ClassA>();

            var isEqual = comparer.Compare(a1, a2, out IEnumerable<Difference> diff);

            Assert.IsTrue(diff.IsNullOrEmpty());
            Assert.IsTrue(isEqual);
        }
        [TestMethod]
        public void IntArray_MisMacthingSequence_Equality()
        {
            var a1 = new ClassA(new int[] { 1, 4, 3 });
            var a2 = new ClassA(new int[] { 4, 3, 1 });
            var comparer = new Comparer<ClassA>();

            var isEqual = comparer.Compare(a1, a2, out IEnumerable<Difference> diff);

            Assert.IsTrue(diff.IsNullOrEmpty());
            Assert.IsTrue(isEqual);
        }
        [TestMethod]
        public void IntArray_InEquality()
        {
            var a1 = new ClassA(new int[] { 1, 4, 3 });
            var a2 = new ClassA(new int[] { 1, 3, 5, 7 });
            var comparer = new Comparer<ClassA>();

            var isEqual = comparer.Compare(a1, a2, out IEnumerable<Difference> diff);
            Assert.IsFalse(diff.IsNullOrEmpty());
            Assert.IsFalse(isEqual);
        }
        #endregion

        #region Nested_Class
        [TestMethod]
        public void NestedClass_Equality()
        {
            var a1 = new ClassA
            {
                ClassB = new ClassB
                {
                    Property1 = "A"
                }
            };
            var a2 = new ClassA
            {
                ClassB = new ClassB
                {
                    Property1 = "A"
                }
            };
            var comparer = new Comparer<ClassA>();

            var isEqual = comparer.Compare(a1, a2, out IEnumerable<Difference> diff);

            Assert.IsTrue(diff.IsNullOrEmpty());
            Assert.IsTrue(isEqual);
        }
        [TestMethod]
        public void NestedClass_InEquality()
        {
            var a1 = new ClassA
            {
                ClassB = new ClassB
                {
                    Property1 = "A"
                }
            };
            var a2 = new ClassA
            {
                ClassB = new ClassB
                {
                    Property1 = "BBB"
                }
            };
            var comparer = new Comparer<ClassA>();

            var isEqual = comparer.Compare(a1, a2, out IEnumerable<Difference> diff);
            Assert.IsFalse(diff.IsNullOrEmpty());
            Assert.IsFalse(isEqual);
        }
        #endregion

        #region Nested_IEnumerable_Of_Class
        [TestMethod]
        public void NestedEnumerableOfClass_Equality()
        {
            var a1 = new ClassA
            {
                EnumerableOfB = new List<ClassB>
                {
                    new ClassB
                    {
                        Property1 = "A"
                    }
                }
            };
            var a2 = new ClassA
            {
                EnumerableOfB = new List<ClassB>
                {
                    new ClassB
                    {
                        Property1 = "A"
                    }
                }
            };
            var comparer = new Comparer<ClassA>();

            var isEqual = comparer.Compare(a1, a2, out IEnumerable<Difference> diff);

            Assert.IsTrue(diff.IsNullOrEmpty());
            Assert.IsTrue(isEqual);
        }
        [TestMethod]
        public void NestedEnumerableOfClass_InEquality()
        {
            var a1 = new ClassA
            {
                EnumerableOfB = new List<ClassB>
                {
                    new ClassB
                    {
                        Property1 = "A"
                    },
                    new ClassB
                    {
                        Property1 = "C"
                    }
                }
            };
            var a2 = new ClassA
            {
                EnumerableOfB = new List<ClassB>
                {
                    new ClassB
                    {
                        Property1 = "A"
                    }
                }
            };
            var comparer = new Comparer<ClassA>();

            var isEqual = comparer.Compare(a1, a2, out IEnumerable<Difference> diff);
            Assert.IsFalse(diff.IsNullOrEmpty());
            Assert.IsFalse(isEqual);
        }
        #endregion

        #region IEnumerable
        [TestMethod]
        public void MatchingSequenceList_Equality()
        {
            var comparer = new Comparer<List<string>>();
            var obj1 = new List<string> { "1", "2", "0" };
            var obj2 = new List<string> { "1", "2", "0" };
            bool IsEqual = comparer.Compare(obj1, obj2, out IEnumerable<Difference> diff);
            Assert.IsTrue(IsEqual);
            Assert.IsTrue(diff.IsNullOrEmpty());
        }

        [TestMethod]
        public void NonMatchingSequenceList_Equality()
        {
            var comparer = new Comparer<List<string>>();

            var obj1 = new List<string> { "1", "2", "0" };
            var obj2 = new List<string> { "2", "0", "1" };
            bool IsEqual = comparer.Compare(obj1, obj2, out IEnumerable<Difference> diff);
            Assert.IsTrue(IsEqual);
            Assert.IsTrue(diff.IsNullOrEmpty());
        }

        [TestMethod]
        public void List_Inequality()
        {
            var comparer = new Comparer<List<string>>();

            var obj1 = new List<string> { "1", "2", "3" };
            var obj2 = new List<string> { "1", "2", "5", "6" };
            bool IsEqual = comparer.Compare(obj1, obj2, out IEnumerable<Difference> diff);
            Assert.IsFalse(IsEqual);
            Assert.IsFalse(diff.IsNullOrEmpty());
        }
        #endregion 

        #region Dictionary
        [TestMethod]
        public void Dictionary_Equality()
        {
            var comparer = new Comparer<Dictionary<int, string>>();
            Dictionary<int, string> obj1 = new Dictionary<int, string> { { 3, "4" } };
            Dictionary<int, string> obj2 = new Dictionary<int, string> { { 3, "4" } };

            bool IsEqual = comparer.Compare(obj1, obj2, out IEnumerable<Difference> diff);
            Assert.IsTrue(IsEqual);
            Assert.IsTrue(diff.IsNullOrEmpty());
        }

        [TestMethod]
        public void Dictionary_InEquality()
        {
            var comparer = new Comparer<Dictionary<int, string>>();
            Dictionary<int, string> obj1 = new Dictionary<int, string> { { 5, "4" }, { 9, "2" } };
            Dictionary<int, string> obj2 = new Dictionary<int, string> { { 3, "4" } };

            bool IsEqual = comparer.Compare(obj1, obj2, out IEnumerable<Difference> diff);
            Assert.IsFalse(IsEqual);
            Assert.IsFalse(diff.IsNullOrEmpty());
        }
        #endregion

    }
}
