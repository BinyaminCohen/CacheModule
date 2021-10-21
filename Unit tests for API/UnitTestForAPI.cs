using RestAPI.controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;

[assembly: Parallelize(Workers = 4, Scope = ExecutionScope.MethodLevel)]


namespace Unit_test_for_API
{
    [TestClass]
    public class Tests
    {
        private CacheApi _ca;

        [TestInitialize]
        public void Setup()
        {
            _ca = new CacheApi();
        }

        [TestMethod]
        public void GetTestRainy()
        {
            var res = _ca.Get("123");
            StatusCodeResult statusCode = (StatusCodeResult) res.Result;
            Assert.AreEqual(StatusCodes.Status404NotFound, statusCode.StatusCode);
        }

        [TestMethod]
        public void PostAndGetTestSunny()
        {
            var res = _ca.Post("k1", 2021);
            ObjectResult postStatusCode = (ObjectResult) res.Result;
            Assert.AreEqual(StatusCodes.Status201Created, postStatusCode.StatusCode);
            object key = ((CreatedResult) res.Result).Value;
            Assert.AreEqual("k1", key);

            var res1 = _ca.Get((string) key);
            OkObjectResult okResult = (OkObjectResult) res1.Result;
            Assert.AreEqual(StatusCodes.Status200OK, okResult.StatusCode);
            int data = (int) okResult.Value;
            Assert.AreEqual(2021, data);
        }

        [TestMethod]
        public void PostAndPutTestSunny()
        {
            var res = _ca.Post("k2", 2021.1017);
            ObjectResult postStatusCode = (ObjectResult) res.Result;
            Assert.AreEqual(StatusCodes.Status201Created, postStatusCode.StatusCode);
            object key = ((CreatedResult) res.Result).Value;
            Assert.AreEqual("k2", key);

            var res1 = _ca.Put((string) key, 2021.1018);
            NoContentResult statusCode = (NoContentResult) res1;
            Assert.AreEqual(StatusCodes.Status204NoContent, statusCode.StatusCode);


            var res2 = _ca.Get((string) key);
            OkObjectResult okResult = (OkObjectResult) res2.Result;
            Assert.AreEqual(StatusCodes.Status200OK, okResult.StatusCode);
            double data = (double) okResult.Value;
            Assert.AreEqual(2021.1018, data);
        }

        [TestMethod]
        public void PutTestRainy()
        {
            //rainy
            var res = _ca.Put("456", "ABC");
            StatusCodeResult statusCode = (StatusCodeResult) res;
            Assert.AreEqual(StatusCodes.Status404NotFound, statusCode.StatusCode);
        }

        [TestMethod]
        public void DeleteTestRainy()
        {
            //rainy
            var res = _ca.Delete("999");
            StatusCodeResult statusCode = (StatusCodeResult) res;
            Assert.AreEqual(StatusCodes.Status404NotFound, statusCode.StatusCode);
        }

        [TestMethod]
        public void DeleteTestSunny()
        {
            var res = _ca.Post("k3", "the king Shaul");
            ObjectResult postStatusCode = (ObjectResult) res.Result;
            Assert.AreEqual(StatusCodes.Status201Created, postStatusCode.StatusCode);

            object key = ((CreatedResult) res.Result).Value;
            var res1 = _ca.Delete((string) key);
            var res2 = _ca.Get("k3");

            StatusCodeResult statusCode1 = (StatusCodeResult) res1;
            Assert.AreEqual(StatusCodes.Status204NoContent, statusCode1.StatusCode);

            StatusCodeResult statusCode2 = (StatusCodeResult) res2.Result;
            Assert.AreEqual(StatusCodes.Status404NotFound, statusCode2.StatusCode);
        }
    }
}