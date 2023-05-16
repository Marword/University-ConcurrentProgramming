using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.ComponentModel;
using System.Linq;
using ViewModel;

namespace ViewModelTests
{
    [TestClass]
    public class SimViewModelTest
    {
        private readonly SimViewModel simViewModel = new();

        [TestMethod]
        public void BallsCountPropertyChanged()
        {
            bool ballsCountChangedRaised = false;

            simViewModel.PropertyChanged += (object? sender, PropertyChangedEventArgs e) => ballsCountChangedRaised = true;

            Assert.IsFalse(ballsCountChangedRaised);

            simViewModel.BallsCount = 15;
            Assert.IsTrue(ballsCountChangedRaised);
        }

        [TestMethod]
        public void StartStopSimTest()
        {
            bool isSimRunningChangedRaised = false;

            simViewModel.PropertyChanged += (object? sender, PropertyChangedEventArgs e) => isSimRunningChangedRaised = true;

            Assert.IsFalse(simViewModel.IsSimRunning);
            Assert.IsFalse(isSimRunningChangedRaised);

            simViewModel.StartSim();
            Assert.IsTrue(simViewModel.IsSimRunning);
            Assert.IsTrue(isSimRunningChangedRaised);

            simViewModel.StopSim();
            Assert.IsFalse(simViewModel.IsSimRunning);
        }

        [TestMethod]
        public void UpdateBallsTest()
        {
            bool ballsChangedRaised = false;

            simViewModel.PropertyChanged += (object? sender, PropertyChangedEventArgs e) => ballsChangedRaised = true;

            Assert.IsFalse(ballsChangedRaised);
            var collectionBefore = simViewModel.Balls;

            simViewModel.StartSim();
            simViewModel.OnNext(collectionBefore.First());

            /*Assert.IsTrue(ballsChangedRaised);*/
            var collectionAfter = simViewModel.Balls;

            Assert.AreSame(collectionBefore, collectionAfter);
            simViewModel.StopSim();
        }
    }
}