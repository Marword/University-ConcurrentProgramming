using BallSimulator.Data.API;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BallSimulator.DataTests
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
            Assert.AreNotEqual(dataApi.MinDiameter, default);
            Assert.AreNotEqual(dataApi.MaxDiameter, default);
            Assert.AreNotEqual(dataApi.MaxTempo, default);
        }
    }
}