using System;
using System.Collections.Generic;

namespace Logic
{
    public class CacheModule
    {
        private Dictionary<Guid, object> Cache = new Dictionary<Guid, object>(); 
        private static CacheModule Instance = new CacheModule();
        private object WriteLock = new object();


        public static CacheModule GetInstance()
        {
            return Instance;
        }
        
       ///  signal tone cache - only one instance of this
        private CacheModule()
        {

        }
        public string Create(string data)
        {
            if (data == null)
            {
                return null;
            }
            lock(WriteLock)
            {
                Guid key = Guid.NewGuid();
                
                Cache[key] = data;

                return key.ToString();
            }
        }

        public string Read(string strKey)
        {
            Guid key = Guid.Parse(strKey);
            
            if (Cache.ContainsKey(key))
            {
                object data = Cache[key];

                return data.ToString();
            }

            return null;
            
            ///return Read(key);
            
        }

        /*public string Read(Guid key)
        {
            if (Cache.ContainsKey(key))
            {
                object data = Cache[key];

                return data.ToString();
            }

            return null;
            
        }*/

        public bool Update(string newData, string strKey)
        {
            lock (WriteLock)
            {
                Guid key = Guid.Parse(strKey);
                
                if (Cache.ContainsKey(key))
                {
                    Cache[key] = newData;
                    return true;
                }

                return false;
            }

        }

        public bool Delete(string strKey)
        {
            lock (WriteLock)
            {
                Guid key = Guid.Parse(strKey);
               
                if (Cache.ContainsKey(key))
                {
                    Cache.Remove(key);
                    return true;
                }

                return false;
            }
        }

    }
}
