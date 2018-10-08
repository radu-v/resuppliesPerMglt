using Microsoft.VisualStudio.TestTools.UnitTesting;
using ResuppliesPerMglt.Helpers;

namespace ResuppliesPerMglt_Tests
{
    [TestClass]
    public class UrlUtils_Tests
    {
        [TestMethod]
        public void GetNextPageNumber_Should_ReturnNull_OnNullInput()
        {
            Assert.IsNull(UrlUtils.GetPageNumber(null));
        }

        [TestMethod]
        public void GetNextPageNumber_Should_ReturnNull_OnEmptyInput()
        {
            Assert.IsNull(UrlUtils.GetPageNumber(string.Empty));
            Assert.IsNull(UrlUtils.GetPageNumber(" \t"));
        }

        [TestMethod]
        public void GetQueryParams_ShouldReturn_EmptyNameValueCollection_onNull()
        {
            var collection = UrlUtils.GetQueryParams(null);

            Assert.AreEqual(0, collection.Count);
        }

        [TestMethod]
        public void GetQueryParams_ShouldReturn_EmptyNameValueCollection_onEmptyUrl()
        {
            var collection = UrlUtils.GetQueryParams(string.Empty);

            Assert.AreEqual(0, collection.Count);
        }

        [TestMethod]
        public void GetQueryParams_ShouldReturn_EmptyNameValueCollection_onInvalidUrl()
        {
            var collection = UrlUtils.GetQueryParams("some string");

            Assert.AreEqual(0, collection.Count);
        }

        [TestMethod]
        public void GetQueryParams_ShouldReturn_EmptyNameValueCollection_onValidUrlWithNoParams()
        {
            var collection = UrlUtils.GetQueryParams("http://some.valid.url/endpoint");

            Assert.AreEqual(0, collection.Count);
        }

        [TestMethod]
        public void GetQueryParams_ShouldReturn_NonEmptyNameValueCollection_OnValidUrlWithParams()
        {
            var collection = UrlUtils.GetQueryParams("http://some.valid.url/endpoint?param1=value1");

            Assert.AreNotEqual(0, collection.Count);
        }

        [TestMethod]
        public void GetQueryParams_ShouldReturn_NonEmptyNameValueCollection_OnRelativeUrlWithParams()
        {
            var collection = UrlUtils.GetQueryParams("endpoint?param1=value1");

            Assert.AreNotEqual(0, collection.Count);
        }
    }
}
