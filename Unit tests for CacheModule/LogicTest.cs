using Logic;
using Microsoft.VisualBasic.FileIO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using Assert = NUnit.Framework.Assert;

[assembly: Parallelize(Workers = 4, Scope = ExecutionScope.MethodLevel)]

namespace Unit_tests_for_logic
{
    
    public class Tests
    {
        private CacheModule cm;
        
        
        [SetUp]
        public void Setup()
        {
            cm = CacheModule.GetInstance();
        }
       
        [Test]
        public void CrateAndReadTest()
        {
            string key = cm.Create("{\"id\":12}");
            string key1 = cm.Create("aaa");
            string key2 = cm.Create("aaa");

            string data = cm.Read(key);
            string data1 = cm.Read(key1);
            string data2 = cm.Read(key2);
            
            
            Assert.AreEqual("{\"id\":12}",data);
            Assert.AreEqual("aaa",data1);
            Assert.AreEqual("aaa",data2);
            Assert.AreNotEqual(key1,key);
            Assert.AreNotEqual(key1,key2);
            
        }
        
        [Test]
        public void CrateAndReadTest1()
        {
            string key = cm.Create("{\"id\":13}");
            string key1 = cm.Create("bbb");
            string key2 = cm.Create("ccc");
            string key3 = cm.Create(null);
            

            string data = cm.Read(key);
            string data1 = cm.Read(key1);
            string data2 = cm.Read(key2);
            
            
            Assert.AreEqual("{\"id\":13}",data);
            Assert.AreEqual("bbb",data1);
            Assert.AreEqual("ccc",data2);
            Assert.AreNotEqual(key1,key);
            Assert.AreNotEqual(key1,key2);
            Assert.AreEqual(null,null);
            
            
        }
        
        [Test]
        public void PutTest()
        {
            
            string key = cm.Create("{Binyamin}");
            string key1 = cm.Create("Omer");
            string key2 = cm.Create("Lior");
            
             bool res = cm.Update("Ben",key);
             bool res1 = cm.Update("Omry",key1);
             bool res2 = cm.Update("Dan",key2);
             
             string data = cm.Read(key);
             string data1 = cm.Read(key1);
             string data2 = cm.Read(key2);
             
             
             Assert.AreEqual("Ben", data);
             Assert.AreEqual("Omry", data1);
             Assert.AreEqual("Dan", data2);
            
        }
        [Test]
        public void DeleteTest()
        {
            string key = cm.Create("{Binyamin}");
            string key1 = cm.Create("Omer");
            string key2 = cm.Create("Lior");
            
            bool res = cm.Delete(key);
            bool res1 = cm.Delete(key1);
            bool res2 = cm.Delete(key2);
             
            string data = cm.Read(key);
            string data1 = cm.Read(key1);
            string data2 = cm.Read(key2);
             
             
            Assert.AreEqual(null, data);
            Assert.AreEqual(null, data1);
            Assert.AreEqual(null, data2);
            
        }
    }
}