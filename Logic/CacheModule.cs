using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Logic
{
    public class CacheModule
    {
        /// <summary>
        /// Data structure of type ConcurrentDictionary for simulate cache memory,
        /// and solve Multi-trade problem. 
        /// </summary>
        private ConcurrentDictionary<string, object> Cache = new ConcurrentDictionary<string, object>();

        private static CacheModule Instance = new CacheModule();

        /// <summary>
        /// Single-tone instance of this class to provide only one cache memory.
        /// </summary>
        private CacheModule()
        {
        }

        public static CacheModule GetInstance()
        {
            return Instance;
        }

        /// <summary>
        /// The function receives key and data and checks whether the key or the data are not null,
        /// If not, the function checks if the key already exist.
        /// If not, the function puts in the dictionary (cache memory) the data in place of the specific key 
        /// and return the key.
        /// </summary>
        /// <param name="key">user key</param>
        /// <param name="data"></param>
        /// <returns></returns>
        public string Create(string key, object data)
        {
            if (key == null || data == null)
            {
                return null;
            }

            if (Cache.ContainsKey(key))
            {
                return null;
            }

            Cache[key] = data;

            return key;
        }

        /// <summary>
        /// The function read the data from the cache memory by his key.
        /// The function tries to find the specific key in the cache,
        /// Once it's found the function return the data.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public object Read(string key)
        {
            if (Cache.ContainsKey(key))
            {
                object data = Cache[key];

                return data;
            }

            return null;
        }

        /// <summary>
        /// The function update the data in the cache memory by his key and new data.
        /// The function tries to find the specific key in the cache,
        /// Once it's found the function replaces the old data with the new one.
        /// and return true if the action was succeeded or false if not. 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="newData"></param>
        /// <returns></returns>
        public bool Update(string key, object newData)
        {
            if (Cache.ContainsKey(key))
            {
                Cache[key] = newData;
                return true;
            }

            return false;
        }

        /// <summary>
        /// The function remove the data by his key from the cache memory.
        /// The function tries to find the specific key in the cache,
        /// Once it's found the function delete the specific data from the cache,
        /// and return true if the action was succeeded or false if not. 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool Delete(string key)
        {
            if (Cache.ContainsKey(key))
            {
                object removedItem;
                Cache.Remove(key, out removedItem);
                return true;
            }

            return false;
        }
    }
}