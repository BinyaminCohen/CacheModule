using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Logic
{
    public class CacheModule
    {
        /// <summary>
        /// Data structure of type ConcurrentDictionary for simulate cache memory ,
        /// and solve Multi-trade problem. 
        /// </summary>
        private ConcurrentDictionary<Guid, object> Cache = new ConcurrentDictionary<Guid, object>();

        private static CacheModule Instance = new CacheModule();

        /// <summary>
        /// Signal tone instance of this class to provide only one cache memory.
        /// </summary>
        private CacheModule()
        {
        }

        public static CacheModule GetInstance()
        {
            return Instance;
        }

        /// <summary>
        /// The function receives any kind of data  and checks whether it is not null,
        /// If not the function generates a Guid key (to create a very large amount of keys for many objects),
        /// and puts in the dictionary (cache memory) the data in place of the specific key created,
        /// and return the key as a string.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
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

        /// <summary>
        /// The function read the data from the cache memory by his key.
        /// Converting the string key to Guid and try to find this specific key in the cache,
        /// Once it's found the function return the data.
        /// </summary>
        /// <param name="strKey"></param>
        /// <returns></returns>
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

        /// <summary>
        /// The function update the data in the cache memory by his key and new data.
        /// Converting the string key to Guid and try to find this specific key in the cache.
        /// Once it's found the function replaces the old data with the new one.
        /// and return true if the action was succeeded or false if not. 
        /// </summary>
        /// <param name="strKey"></param>
        /// <param name="newData"></param>
        /// <returns></returns>
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

        /// <summary>
        /// The function remove the data by his key from the cache memory.
        /// Converting the string key to Guid and try to find this specific key in the cache.
        /// Once it's found the function delete the specific data from the cache,
        /// and return true if the action was succeeded or false if not. 
        /// </summary>
        /// <param name="strKey"></param>
        /// <returns></returns>
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

        /// <summary>
        /// This function is responsible for converting the key from string to Guid.
        /// </summary>
        /// <param name="strKey"></param>
        /// <returns></returns>
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