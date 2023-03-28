using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CalculatorTestsProject
{
    [TestClass]
    public class CalculatorTests
    {
        [TestMethod]
        public void AddTest()
        {
            CalculatorLibrary.Calculator x = new CalculatorLibrary.Calculator(2, 1);
            var result = x.Add();
            Assert.AreEqual(3, result);
        }

        [TestMethod]
        public void SubstractTest()
        {
            CalculatorLibrary.Calculator x = new CalculatorLibrary.Calculator(5, 3);
            var result = x.Sub();
            Assert.AreEqual(2, result);
        }

        [TestMethod]
        public void MultiplyTest()
        {
            CalculatorLibrary.Calculator x = new CalculatorLibrary.Calculator(2, 6);
            var result = x.Mul();
            Assert.AreEqual(12, result);
        }

        [TestMethod]
        public void DivideTest()
        {
            CalculatorLibrary.Calculator x = new CalculatorLibrary.Calculator(10, 4);
            var result = x.Div();
            Assert.AreEqual(2.5, result);
        }

        [TestMethod]
        public void ModuloTest()
        {
            CalculatorLibrary.Calculator x = new CalculatorLibrary.Calculator(7, 3);
            var result = x.Mod();
            Assert.AreEqual(1, result);
        }
    }
}