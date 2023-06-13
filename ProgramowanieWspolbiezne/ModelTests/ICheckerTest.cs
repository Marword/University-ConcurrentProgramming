using Microsoft.VisualStudio.TestTools.UnitTesting;
using BallSimulator.Model.API;
using BallSimulator.Model;

namespace BallSimulator.Tests
{
    [TestClass]
    public class ICheckerTest
    {
        [TestMethod]
        public void BallCountCheckerTest()
        {
            const int min = 1;
            const int max = 20;

            IChecker<int> validator = new BallsCountChecker(min, max);

            Assert.IsTrue(validator.Check(max - 1));
            Assert.IsTrue(validator.Check(min + 1));

            Assert.IsFalse(validator.Check(max + 1));
            Assert.IsFalse(validator.Check(min - 1));
        }
    }
}
