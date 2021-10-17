using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Logic
{
    public class CacheModule
    {
        private ConcurrentDictionary<Guid, object> Cache = new ConcurrentDictionary<Guid, object>();
        private static CacheModule Instance = new CacheModule();
        
        public static CacheModule GetInstance()
        {
            return Instance;
        }

        ///  signal tone cache - only one instance of this
        private CacheModule()
        {
        }

        public string Create(object data)
        {
            if (data == null)
            {
                return null;
            }

            Guid key = Guid.NewGuid();

            Cache[key] = data;

            return key.ToString();
        }

        public object Read(string strKey)
        {
            Guid key = string2Guid(strKey);

            if (Cache.ContainsKey(key))
            {
                object data = Cache[key];

                return data;
            }

            return null;
        }

        public bool Update(string strKey, object newData)
        {
            Guid key = string2Guid(strKey);
            
            if (Cache.ContainsKey(key))
            {
                Cache[key] = newData;
                return true;
            }

            return false;
        }

        public bool Delete(string strKey)
        {
            Guid key = string2Guid(strKey);

            if (Cache.ContainsKey(key))
            {
                object removedItem;
                Cache.Remove(key, out removedItem);
                return true;
            }

            return false;
        }

        private Guid string2Guid(string strKey)
        {
            Guid key = Guid.Empty;
            try
            {
                key = Guid.Parse(strKey);
                return key;
            }
            catch (System.FormatException ex)
            {
                return key;
            }
        }
    }
}