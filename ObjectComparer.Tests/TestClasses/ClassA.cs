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

        public string stringProperty { get; set; }

        public DateTime DateTimeProperty { get; set; }
        public Dictionary<int, string> dictionaryProperty { get; set; }

        public double doubleProperty { get; }

        public bool boolProperty { get; }

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
            stringProperty = val;
        }
        public ClassA(DateTime val)
        {
            DateTimeProperty = val;
        }
        public ClassA(Dictionary<int, string> val)
        {
            dictionaryProperty = val;
        }
        public ClassA(double val)
        {
            doubleProperty = val;
        }
        public ClassA(bool val)
        {
            boolProperty = val;
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
