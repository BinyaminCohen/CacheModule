using Logic;
using Microsoft.AspNetCore.Mvc;

namespace API
{
    public class CacheAPI : Controller
    {
        private CacheModule cm = CacheModule.GetInstance();
        
        // GET
        [HttpGet("api/data/{key}")]
        public ActionResult<string> Get(string key)
        {
            string res = cm.Read(key);
            if (res != null)
            {
                return res;
            }
            return NotFound();
        }
        
        //Post
        [HttpPost("api/data")]
        public ActionResult<string> Post(string data)
        {
            string key = cm.Create(data);
            return key;

        }
        
        //Put
        [HttpPut("api/data/{key}")]
        public ActionResult<string> Put(string key, string data)
        {
            bool res = cm.Update(data, key);
            
            if (res)
            {
                return Accepted();  
            }

            return NotFound();

        }
        
        //Delete
        [HttpPut("api/data/{key}")]
        public ActionResult<int> Delete(string key)
        {
            bool res = cm.Delete(key);
            
            if (res)
            {
                return Accepted();  
            }

            return NotFound();

        }
    }
}