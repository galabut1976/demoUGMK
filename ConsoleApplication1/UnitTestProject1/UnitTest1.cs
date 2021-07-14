using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ConsoleApplication1;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        MyList<int> list = null;

        [TestMethod]
        public void TestMethod1()
        {
            list = new MyList<int>(0, 1, 2, 3 );
            list.HeadReached += list_HeadReached;
            list.Add(4);
            Assert.AreEqual(5, list.Count); // проверка количества данных

            list.MoveBack(7);
            Assert.AreEqual(3, list.Current);
            list.MoveNext(11);
            Assert.AreEqual(4, list.Current);

            Assert.AreEqual(0, list.Head);
            Assert.AreEqual(3, list.Previous);
            Assert.AreEqual(0, list.Next);
        }

        void list_HeadReached(object sender, int e)
        {
            Assert.AreEqual(list.Current, e);
        }
    }
}
