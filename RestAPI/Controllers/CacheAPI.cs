using Logic;
using Microsoft.AspNetCore.Mvc;

namespace API
{
    [ApiController]
    [Route("[controller]")]
    public class CacheAPI : Controller
    {
        /// <summary>
        /// Create one instance of the cache module class.
        /// </summary>
        private CacheModule cm = CacheModule.GetInstance();

        /// <summary>
        /// GET - This function responsible to bringing data by his key.
        /// When the key is not null, this function calls to Read function with the key at cache module,
        /// and return HTTP code 200 (ok) and the data if the key exist.
        /// If not the function return HTTP code 400 (Bad Request) if the key is null or
        /// HTTP code 404 (Not Found) if the cache memory don't have this key. 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        [HttpGet("cacheModuleRestApi/data/{key}")]
        public ActionResult<object> Get(string key)
        {
            if (key == null)
            {
                return BadRequest(); // HTTP code 400 bad request.
            }

            object res = cm.Read(key);

            if (res != null)
            {
                return Ok(res); // HTTP code 200 ok.
            }

            return NotFound(); // HTTP code 404 not found. 
        }

        /// <summary>
        /// POST - this function responsible to save data in the cache memory.
        /// This function get any type of data and calls to Create function with the data at cache module,
        /// Create function save the data in the cache and return the new key for this data.
        /// The function Post return HTTP code 201 (Created) and the URL + key.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost("cacheModuleRestApi/data")]
        public ActionResult<object> Post(object data)
        {
            string key = cm.Create(data);

            return Created("cacheModuleRestApi/data/" + key, (object) key); // HTTP code 201 created.
        }

        /// <summary>
        /// PUT - this function responsible to update the data in the cache memory by his specific key and new data.
        /// If the key is null this function return HTTP code 400 (Bad Request). 
        /// This function calls to Update function with the key and the new data at cache module.
        /// The Update function looking for the old data by his key and update him by new data,
        /// and return ture or false if the action was succeeded.
        /// When we get ture this function return HTTP code 204 (No Content).
        /// When we get false this function return HTTP code 404 (Not Found).
        /// </summary>
        /// <param name="key"></param>
        /// <param name="newData"></param>
        /// <returns></returns>
        [HttpPut("cacheModuleRestApi/data/{key}")]
        public ActionResult Put(string key, object newData)
        {
            if (key == null)
            {
                return BadRequest(); // HTTP code 400 bad request.
            }

            bool res = cm.Update(key, newData);

            if (res)
            {
                return NoContent(); // HTTP code 204 no content.  
            }

            return NotFound(); // HTTP code 404 not found. 
        }

        /// <summary>
        /// DELETE - this function responsible to remove data from the cache memory by his specific key.
        /// If the key is null this function return HTTP code 400 (Bad Request).
        /// This function calls to Delete function with the key at cache module.
        /// The function Delete at the cache remove the data if it finds it's key and return ture.
        /// When we get ture this function return HTTP code 204 (No Content).
        /// When we get false this function return HTTP code 404 (Not Found).
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        [HttpDelete("cacheModuleRestApi/data/{key}")]
        public ActionResult Delete(string key)
        {
            if (key == null)
            {
                return BadRequest(); // HTTP code 400 bad request.
            }

            bool res = cm.Delete(key);

            if (res)
            {
                return NoContent(); // HTTP code 204 no content. 
            }

            return NotFound(); // HTTP code 404 not found.
        }
    }
}