using Logic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

[assembly: Parallelize(Workers = 4, Scope = ExecutionScope.MethodLevel)]

namespace Unit_tests_for_logic
{
    [TestClass]
    public class Tests
    {
        private CacheModule _cm;


        [TestInitialize]
        public void Setup()
        {
            _cm = CacheModule.GetInstance();
        }

        [TestMethod]
        public void CreateAndReadTest()
        {
            string key = _cm.Create("a", "{\"id\":12}");
            string key1 = _cm.Create("b", "aaa");
            string key2 = _cm.Create("c", "aaa");

            object data = _cm.Read(key);
            object data1 = _cm.Read(key1);
            object data2 = _cm.Read(key2);


            Assert.AreEqual("{\"id\":12}", data);
            Assert.AreEqual("aaa", data1);
            Assert.AreEqual("aaa", data2);
            Assert.AreEqual("a", key);
            Assert.AreEqual("b", key1);
            Assert.AreEqual("c", key2);
            Assert.AreNotEqual(key1, key);
            Assert.AreNotEqual(key1, key2);
        }

        [TestMethod]
        public void CreateAndReadTest1()
        {
            string key = _cm.Create("a1", "{\"id\":13}");
            string key1 = _cm.Create("b1", "bbb");
            string key2 = _cm.Create("c1", "ccc");
            string key3 = _cm.Create("d1", null);


            object data = _cm.Read(key);
            object data1 = _cm.Read(key1);
            object data2 = _cm.Read(key2);
            object data3 = _cm.Read("123");


            Assert.AreEqual("{\"id\":13}", data);
            Assert.AreEqual("bbb", data1);
            Assert.AreEqual("ccc", data2);
            Assert.AreNotEqual(key1, key);
            Assert.AreNotEqual(key1, key2);
            Assert.AreEqual(null, key3);
            Assert.AreEqual(null, data3);
        }

        [TestMethod]
        public void PutTest()
        {
            string key = _cm.Create("a2", "{Binyamin}");
            string key1 = _cm.Create("b2", "Omer");
            string key2 = _cm.Create("c2", "Lior");

            bool res = _cm.Update(key, "Ben");
            bool res1 = _cm.Update(key1, "Omry");
            bool res2 = _cm.Update(key2, "Dan");
            bool res3 = _cm.Update("123", "abc");

            Assert.AreEqual(true, res);
            Assert.AreEqual(true, res1);
            Assert.AreEqual(true, res2);
            Assert.AreEqual(false, res3);


            object data = _cm.Read(key);
            object data1 = _cm.Read(key1);
            object data2 = _cm.Read(key2);


            Assert.AreEqual("Ben", data);
            Assert.AreEqual("Omry", data1);
            Assert.AreEqual("Dan", data2);
        }

        [TestMethod]
        public void DeleteTest()
        {
            string key = _cm.Create("a3", "{Binyamin}");
            string key1 = _cm.Create("b3", "Omer");
            string key2 = _cm.Create("c3", "Lior");

            bool res = _cm.Delete(key);
            bool res1 = _cm.Delete(key1);
            bool res2 = _cm.Delete(key2);
            bool res3 = _cm.Delete("456");

            Assert.AreEqual(true, res);
            Assert.AreEqual(true, res1);
            Assert.AreEqual(true, res2);
            Assert.AreEqual(false, res3);

            object data = _cm.Read(key);
            object data1 = _cm.Read(key1);
            object data2 = _cm.Read(key2);


            Assert.AreEqual(null, data);
            Assert.AreEqual(null, data1);
            Assert.AreEqual(null, data2);
        }
    }
}