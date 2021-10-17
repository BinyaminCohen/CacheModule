using Logic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

[assembly: Parallelize(Workers = 4, Scope = ExecutionScope.MethodLevel)]

namespace Unit_tests_for_logic
{
    [TestClass]
    public class Tests
    {
        private CacheModule cm;
        
        
        [TestInitialize]
        public void Setup()
        {
            cm = CacheModule.GetInstance();
        }
       
        [TestMethod]
        public void CrateAndReadTest()
        {
            string key = cm.Create("{\"id\":12}");
            string key1 = cm.Create("aaa");
            string key2 = cm.Create("aaa");

            object data = cm.Read(key);
            object data1 = cm.Read(key1);
            object data2 = cm.Read(key2);
            
            
            Assert.AreEqual("{\"id\":12}",data);
            Assert.AreEqual("aaa",data1);
            Assert.AreEqual("aaa",data2);
            Assert.AreNotEqual(key1,key);
            Assert.AreNotEqual(key1,key2);
            
        }
        
        [TestMethod]
        public void CrateAndReadTest1()
        {
            string key = cm.Create("{\"id\":13}");
            string key1 = cm.Create("bbb");
            string key2 = cm.Create("ccc");
            string key3 = cm.Create(null);
            

            object data = cm.Read(key);
            object data1 = cm.Read(key1);
            object data2 = cm.Read(key2);
            object data3 = cm.Read("123");
            
            
            Assert.AreEqual("{\"id\":13}",data);
            Assert.AreEqual("bbb",data1);
            Assert.AreEqual("ccc",data2);
            Assert.AreNotEqual(key1,key);
            Assert.AreNotEqual(key1,key2);
            Assert.AreEqual(null,null);
            Assert.AreEqual(null,data3);
            
            
        }
        
        [TestMethod]
        public void PutTest()
        {
            
            string key = cm.Create("{Binyamin}");
            string key1 = cm.Create("Omer");
            string key2 = cm.Create("Lior");
            
             bool res = cm.Update(key,"Ben");
             bool res1 = cm.Update(key1,"Omry");
             bool res2 = cm.Update(key2,"Dan");
             
             object data = cm.Read(key);
             object data1 = cm.Read(key1);
             object data2 = cm.Read(key2);
             
             
             Assert.AreEqual("Ben", data);
             Assert.AreEqual("Omry", data1);
             Assert.AreEqual("Dan", data2);
            
        }
        [TestMethod]
        public void DeleteTest()
        {
            string key = cm.Create("{Binyamin}");
            string key1 = cm.Create("Omer");
            string key2 = cm.Create("Lior");
            
            bool res = cm.Delete(key);
            bool res1 = cm.Delete(key1);
            bool res2 = cm.Delete(key2);
             
            object data = cm.Read(key);
            object data1 = cm.Read(key1);
            object data2 = cm.Read(key2);
             
             
            Assert.AreEqual(null, data);
            Assert.AreEqual(null, data1);
            Assert.AreEqual(null, data2);
            
        }
    }
}