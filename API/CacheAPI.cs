using Logic;
using Microsoft.AspNetCore.Mvc;

namespace API
{
    public class CacheAPI : Controller
    {
        private CacheModule cm = CacheModule.GetInstance();
        
        // GET
        [HttpGet("api/data/{key}")]
        public ActionResult<string> Get(int key)
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
        public ActionResult<int> Post(string data)
        {
            int key = cm.Create(data);
            return key;

        }
        
        //put
        [HttpPut("api/data/{key}")]
        public ActionResult<int> Put(int key, string data)
        {
            bool res = cm.Update(data, key);
            if (res == true)
            {
                return Accepted();  
            }

            return NotFound();

        }
        
        //delete
        [HttpPut("api/data/{key}")]
        public ActionResult<int> Delete(int key)
        {
            bool res = cm.Delete(key);
            if (res == true)
            {
                return Accepted();  
            }

            return NotFound();

        }
    }
}