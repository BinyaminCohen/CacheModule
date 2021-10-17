using Logic;
using Microsoft.AspNetCore.Mvc;

namespace API
{
    public class CacheAPI : Controller
    {
        private CacheModule cm = CacheModule.GetInstance();
        
        // GET
        [HttpGet("api/data/{key}")]
        public ActionResult<object> Get(string key)
        {
            if (key == null)
            {
                return BadRequest();
            }
            object res = cm.Read(key);
            
            if (res != null)
            {
                return Ok(res);
            }
            return NotFound();
        }
        
        //Post
        [HttpPost("api/data")]
        public ActionResult<object> Post(object data)
        {
            string key = cm.Create(data);
            
            return Created("api/data/" + key, (object)key);

        }
        
        //Put
        [HttpPut("api/data/{key}")]
        public ActionResult Put(string key, object data)
        {
            if (key == null)
            {
                return BadRequest();
            }
            
            bool res = cm.Update(key, data);
            
            if (res)
            {
                return NoContent();  
            }

            return NotFound();

        }
        
        //Delete
        [HttpPut("api/data/{key}")]
        public ActionResult Delete(string key)
        {
            
            if (key == null)
            {
                return BadRequest();
            }
            
            bool res = cm.Delete(key);
            
            if (res)
            {
                return NoContent();  
            }

            return NotFound();

        }
    }
}