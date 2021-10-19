using API;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

[assembly: Parallelize(Workers = 4, Scope = ExecutionScope.MethodLevel)]


namespace Unit_test_for_API
{
    [TestClass]
    public class Tests
    {
        private CacheAPI ca;

        [TestInitialize]
        public void Setup()
        {
            ca = new CacheAPI();
        }

        [TestMethod]
        public void GetTestRainy()
        {
            var res = ca.Get("123");
            StatusCodeResult statusCode = (StatusCodeResult) res.Result;
            Assert.AreEqual(StatusCodes.Status404NotFound, statusCode.StatusCode);
        }

        [TestMethod]
        public void PostAndGetTestSunny()
        {
            var res = ca.Post(2021);
            ObjectResult postStatusCode = (ObjectResult) res.Result;
            Assert.AreEqual(StatusCodes.Status201Created, postStatusCode.StatusCode);
            object key = ((CreatedResult) res.Result).Value;

            var res1 = ca.Get((string) key);
            OkObjectResult okResult = (OkObjectResult) res1.Result;
            Assert.AreEqual(StatusCodes.Status200OK, okResult.StatusCode);
            int data = (int) okResult.Value;
            Assert.AreEqual(2021, data);
        }

        [TestMethod]
        public void PostAndPutTestSunny()
        {
            var res = ca.Post(2021.1017);
            ObjectResult postStatusCode = (ObjectResult) res.Result;
            Assert.AreEqual(StatusCodes.Status201Created, postStatusCode.StatusCode);
            object key = ((CreatedResult) res.Result).Value;

            var res1 = ca.Put((string) key, 2021.1018);
            NoContentResult statusCode = (NoContentResult) res1;
            Assert.AreEqual(StatusCodes.Status204NoContent, statusCode.StatusCode);


            var res2 = ca.Get((string) key);
            OkObjectResult okResult = (OkObjectResult) res2.Result;
            Assert.AreEqual(StatusCodes.Status200OK, okResult.StatusCode);
            double data = (double) okResult.Value;
            Assert.AreEqual(2021.1018, data);
        }

        [TestMethod]
        public void PutTestRainy()
        {
            //rainy
            var res = ca.Put("456", "ABC");
            StatusCodeResult statusCode = (StatusCodeResult) res;
            Assert.AreEqual(StatusCodes.Status404NotFound, statusCode.StatusCode);
        }

        [TestMethod]
        public void DeleteTestRainy()
        {
            //rainy
            var res = ca.Delete("999");
            StatusCodeResult statusCode = (StatusCodeResult) res;
            Assert.AreEqual(StatusCodes.Status404NotFound, statusCode.StatusCode);
        }

        [TestMethod]
        public void DeleteTestSunny()
        {
            var res = ca.Post("the king Shaul");
            ObjectResult postStatusCode = (ObjectResult) res.Result;
            Assert.AreEqual(StatusCodes.Status201Created, postStatusCode.StatusCode);

            object key = ((CreatedResult) res.Result).Value;
            var res1 = ca.Delete((string) key);
            StatusCodeResult statusCode = (StatusCodeResult) res1;
            Assert.AreEqual(StatusCodes.Status204NoContent, statusCode.StatusCode);
        }
    }
}