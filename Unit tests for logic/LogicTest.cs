using Logic;
using Microsoft.VisualBasic.FileIO;
using NUnit.Framework;

namespace Unit_tests_for_logic
{
    public class Tests
    {
        private CacheModule cm;

        private int[] keys = new int[3];
        
        [SetUp]
        public void Setup()
        {
            cm = CacheModule.GetInstance();
        }

        [Test, Order(0)]
        public void AddTest()
        {
            keys[0] = cm.Create("{\"id\":12}");
            keys[1] = cm.Create("aaa");
            keys[2] = cm.Create("aaa");
                
            Assert.AreEqual(keys[0],1);
            Assert.AreEqual(keys[1],2);
            Assert.AreNotEqual(keys[0],-1);
            Assert.AreNotEqual(keys[1],1);
            Assert.AreNotEqual(keys[1],keys[2]);
            
        }
        [Test, Order(1)]
        public void GetTest()
        {
            string data0 = cm.Read(keys[0]);
            string data00 = cm.Read(keys[0]);
            string data1 = cm.Read(keys[1]);
            string other = cm.Read(4);
           
            Assert.AreEqual("{\"id\":12}",data0);
            Assert.AreEqual("{\"id\":12}",data00);
            Assert.AreEqual("aaa",data1);
            Assert.AreEqual(null,other);
            
            
        }

        [Test, Order(2)]
        public void PutTest()
        {
             bool res = cm.Update("bbb",keys[1]);
             bool res1 = cm.Update("bbb",4);
             string data = cm.Read(keys[1]);
             
             
             Assert.AreEqual(true, res);
             Assert.AreEqual("bbb", data);
             Assert.AreEqual(false, res1);
            
        }
        [Test,Order(3)]
        public void DeleteTest()
        {
            bool res = cm.Delete(keys[0]);
            bool res1 = cm.Delete(keys[0]);
            bool res2 = cm.Delete(4);
            
            Assert.AreEqual(true, res);
            Assert.AreEqual(false, res1);
            Assert.AreEqual(false, res2);
            
        }
    }
}