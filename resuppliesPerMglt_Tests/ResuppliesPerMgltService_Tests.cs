using Microsoft.VisualStudio.TestTools.UnitTesting;
using ResuppliesPerMglt;

namespace ResuppliesPerMglt_Tests
{
    [TestClass]
    public class ResuppliesPerMgltService_Tests
    {
        [TestMethod]
        public void CalculateResupplyStops_Should_ReturnError_WhenNoMgltPerHour()
        {
            var actual = ResuppliesPerMgltService.CalculateResupplyStops(0, null, null);

            Assert.AreEqual((default, "Unknown Megalights per Hour"), actual);
        }

        [TestMethod]
        public void CalculateResupplyStops_Should_ReturnError_WhenUnknownConsumables()
        {
            var actual = ResuppliesPerMgltService.CalculateResupplyStops(0, 1, null);

            Assert.AreEqual((default, "Unknown max period consumables are available"), actual);
        }

        [TestMethod]
        [DataRow(1000000, 75, 1461.0, 9)]
        [DataRow(1000000, 80, 168.0, 74)]
        [DataRow(1000000, 20, 4383.0, 11)]
        public void CalculateResupplyStops_Should_ReturnCorrectValue(long distance, long mgltPerHour,
            double consumables, long expectedResult)
        {
            var (stopsCount, _) = ResuppliesPerMgltService.CalculateResupplyStops(distance, mgltPerHour, (decimal)consumables);

            Assert.AreEqual(expectedResult, stopsCount);
        }
    }
}
