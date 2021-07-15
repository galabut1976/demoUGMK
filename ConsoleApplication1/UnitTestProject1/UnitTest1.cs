using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ConsoleApplication1;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        MyList2<int> list = null;

        [TestMethod]
        public void TestMethod1()
        {
            list = new MyList2<int>();
            for (int i = 0; i < 4; i++) list.Add(i);
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
