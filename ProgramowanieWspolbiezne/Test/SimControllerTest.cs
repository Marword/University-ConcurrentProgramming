﻿using BallSimulator.Data.API;
using BallSimulator.Logic.API;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;

namespace BallSimulator.Tests;

[TestClass]
public class LogicTest
{
    private readonly int _testWidth;
    private readonly int _testHeight;
    private readonly int _testMinDiameter;
    private readonly int _testMaxDiameter;
    private readonly DataAbstractApi _dataFixture;
    private readonly ILogger _loggerFixture;

    private LogicAbstractApi _controller;
    private IEnumerable<IBall>? _balls;

    public LogicTest()
    {

        _dataFixture = new DataFixture();
        _loggerFixture = new LoggerFixture();
        _controller = LogicAbstractApi.CreateLogicApi(_dataFixture, _loggerFixture);

        _testWidth = _dataFixture.GameWidth;
        _testHeight = _dataFixture.GameHeight;
        _testMinDiameter = _dataFixture.MinDiameter;
        _testMaxDiameter = _dataFixture.MaxDiameter;
    }

    [TestMethod]
    public void ConstructorTest()
    {
        Assert.IsNotNull(_controller);
    }

    [TestMethod]
    public void SimTest()
    {
        _controller = LogicAbstractApi.CreateLogicApi(_dataFixture, _loggerFixture);
        Assert.IsNotNull(_controller);

        _balls = _controller.MakeBalls(2);
        Assert.AreEqual(_balls.Count(), 2);

        float xPos = _balls.First().Coordinates.X;
        Thread.Sleep(100);
        _balls.First().Move(1f);
        Assert.AreNotEqual(_balls.First().Coordinates.X, xPos);
    }

    [TestMethod]
    public void RandomBallsCreationTest()
    {
        _controller = LogicAbstractApi.CreateLogicApi(_dataFixture, _loggerFixture);
        Assert.IsNotNull(_controller);

        int ballNumber = 10;

        var balls = _controller.MakeBalls(ballNumber);
        int counter = 0;

        foreach (var b in balls)
        {
            Assert.IsNotNull(b);
            Assert.IsTrue(IsBetween(b.Diameter, _testMinDiameter, _testMaxDiameter));
            Assert.IsTrue(IsBetween(b.Radius, _testMinDiameter / 2, _testMaxDiameter / 2));
            Assert.IsTrue(IsBetween(b.Coordinates.X, 0, _testWidth));
            Assert.IsTrue(IsBetween(b.Coordinates.Y, 0, _testHeight));
            counter++;
        }
        Assert.AreEqual(ballNumber, counter);
    }

    private static bool IsBetween(float value, float bottom, float top)
    {
        return value <= top && value >= bottom;
    }

    private class DataFixture : DataAbstractApi
    {
        public override int GameHeight => 100;
        public override int GameWidth => 100;
        public override float MaxTempo => 50;
        public override int MinDiameter => 20;
        public override int MaxDiameter => 50;
    }

    private class LoggerFixture : ILogger
    {
        public void LogError(string message, [CallerLineNumber] int lineNumber = -1) { return; }
        public void LogInfo(string message, [CallerLineNumber] int lineNumber = -1) { return; }
        public void LogWarning(string message, [CallerLineNumber] int lineNumber = -1) { return; }

        public void Dispose() { return; }
    }
}