using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace ObjectComparer.Tests.TestClasses
{
    internal class ClassA
    {

        public ClassA()
        {
        }

        public int IntProperty { get; set; }      

        public string StringProperty { get; set; }

        public DateTime DateTimeProperty { get; set; }
        public Dictionary<int, string> DictionaryProperty { get; set; }

        public double DoubleProperty { get; }

        public bool BoolProperty { get; }

        public int[] IntArray { get; set; }

        public ClassB ClassB { get; set; }

        public IEnumerable<ClassB> EnumerableOfB { get; set; }

        #region Values_From_Constructure

        public ClassA(int val)
        {
            IntProperty = val;
        }
        public ClassA(string val)
        {
            StringProperty = val;
        }
        public ClassA(DateTime val)
        {
            DateTimeProperty = val;
        }
        public ClassA(Dictionary<int, string> val)
        {
            DictionaryProperty = val;
        }
        public ClassA(double val)
        {
            DoubleProperty = val;
        }
        public ClassA(bool val)
        {
            BoolProperty = val;
        }
        public ClassA(int[] val)
        {
            IntArray = val;
        }
        public ClassA(ClassB val)
        {
            ClassB = val;
        }
        public ClassA(IEnumerable<ClassB> val)
        {
            EnumerableOfB = val;
        }
        #endregion
    }

}
