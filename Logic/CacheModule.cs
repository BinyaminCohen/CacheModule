using System;
using System.Collections.Generic;

namespace Logic
{
    public class CacheModule
    {
        private Dictionary<int, string> Cache = new Dictionary<int, string>(); 
        private static CacheModule Instance = new CacheModule();
        private object WriteLock = new object();
        private int keyGen = 1; 
        
        
        public static CacheModule GetInstance()
        {
            return Instance;
        }
        
       ///  signaltone cache - only one instance of this
        private CacheModule()
        {

        }
        public int Create(string data)
        {
            lock(WriteLock)
            {
                int key = keyGen;
                
                keyGen++;
                Cache[key] = data;

                return key;
            }
        }

        public string Read(int key)
        {
            if (Cache.ContainsKey(key))
            {
                return Cache[key];
            }

            return null;
        }
        
        public bool Update(string newData, int key)
        {
            lock (WriteLock)
            {

                string oldData = Read(key);
                if (oldData != null)
                {
                    Cache[key] = newData;
                    return true;
                }

                return false;
            }

        }

        public bool Delete(int key)
        {
            lock (WriteLock)
            {
                string data = Read(key);
                if (data != null)
                {
                    Cache.Remove(key);
                    return true;
                }

                return false;
            }
        }

    }
}
