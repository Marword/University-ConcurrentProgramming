using BallSimulator.Model.API;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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
