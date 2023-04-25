using Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DataTests
{
    [TestClass]
    public class DataApiTest
    {
        [TestMethod]
        public void DataApiCreateTest()
        {
            DataAbstractApi dataApi = DataAbstractApi.CreateDataApi();
            Assert.IsNotNull(dataApi);
        }

        [TestMethod]
        public void PropertiesTest()
        {
            DataAbstractApi dataApi = DataAbstractApi.CreateDataApi();
            Assert.AreNotEqual(dataApi.GameHeight, default);
            Assert.AreNotEqual(dataApi.GameWidth, default);
            Assert.AreNotEqual(dataApi.BallRadius, default);
        }
    }
}