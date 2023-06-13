using Microsoft.VisualStudio.TestTools.UnitTesting;
using BallSimulator.Model.API;
using BallSimulator.Model;

namespace BallSimulator.Tests
{
    [TestClass]
    public class ModelApiTest
    {
        [TestMethod]
        public void CreateModelApiTest()
        {
            ModelAbstractApi modelApi = ModelAbstractApi.CreateModelApi();
            Assert.IsNotNull(modelApi);
        }
    }
}
