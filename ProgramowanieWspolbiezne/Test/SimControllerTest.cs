using Logic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;

namespace Test
{
    public class SimControllerTest
    {
        private readonly LogicAbstractApi controller;

        public SimControllerTest()
        {
            controller = LogicAbstractApi.CreateLogicApi();
        }

        [TestMethod]
        public void BallCreationTest()
        {
            controller.MakeBalls(2);
            Assert.AreEqual(controller.Balls.Length, 2);
        }

        [TestMethod]
        public void SimulationTest()
        {
            controller.MakeBalls(2);
            float coordX = controller.Balls[0].Coordinates.X;
            controller.StartSim();
            Thread.Sleep(100);
            controller.StopSim();
            Assert.AreNotEqual(controller.Balls[0].Coordinates.X, coordX);

        }
    }
}
