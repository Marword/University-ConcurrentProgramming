using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;
using Presentation.Model;

namespace ModelTests
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
