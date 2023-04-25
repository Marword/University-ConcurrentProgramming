using Microsoft.VisualStudio.TestTools.UnitTesting;
using Presentation.ViewModel;
using System.ComponentModel;

namespace Presentation.ViewModelTests
{
    [TestClass]
    public class SimViewModelTest
    {
        private readonly SimViewModel simulationViewModel = new SimViewModel();

        [TestMethod]
        public void BallsCountPropertyChanged()
        {
            bool ballsCountChangedRaised = false;

            simulationViewModel.PropertyChanged += (object sender, PropertyChangedEventArgs e) => ballsCountChangedRaised = true;

            Assert.IsFalse(ballsCountChangedRaised);

            simulationViewModel.BallsCount = 15;
            Assert.IsTrue(ballsCountChangedRaised);
        }

        [TestMethod]
        public void StartStopSimTest()
        {
            bool isSimulationRunningChangedRaised = false;

            simulationViewModel.PropertyChanged += (object sender, PropertyChangedEventArgs e) => isSimulationRunningChangedRaised = true;

            Assert.IsFalse(simulationViewModel.IsSimulationOn);
            Assert.IsFalse(isSimulationRunningChangedRaised);

            simulationViewModel.StartSim();
            Assert.IsTrue(simulationViewModel.IsSimulationOn);
            Assert.IsTrue(isSimulationRunningChangedRaised);

            simulationViewModel.StopSim();
            Assert.IsFalse(simulationViewModel.IsSimulationOn);
        }

        [TestMethod]
        public void UpdateBallsTest()
        {
            bool ballsChangedRaised = false;

            simulationViewModel.PropertyChanged += (object sender, PropertyChangedEventArgs e) => ballsChangedRaised = true;

            Assert.IsFalse(ballsChangedRaised);
            var collectionBefore = simulationViewModel.Balls;

            simulationViewModel.UpdateBalls();

            Assert.IsTrue(ballsChangedRaised);
            var collectionAfter = simulationViewModel.Balls;

            Assert.AreNotSame(collectionBefore, collectionAfter);
        }
    }
}