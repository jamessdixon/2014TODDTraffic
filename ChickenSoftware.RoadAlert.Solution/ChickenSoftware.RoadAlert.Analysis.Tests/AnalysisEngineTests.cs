using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ChickenSoftware.RoadAlert.Analysis.Tests
{
    [TestClass]
    public class AnalysisEngineTests
    {

        [TestMethod]
        public void NumberOfRecords_ReturnsExpected()
        {
            Int32 notEpected = 0;
            Int32 actual = AnalysisEngine.NumberOfRecords;
            Assert.AreNotEqual(notEpected, actual);
        }

        [TestMethod]
        public void NumberOfRecordsFor2012DataSet_ReturnsExpected()
        {
            Int32 expected = 27778;
            Int32 actual = AnalysisEngine.NumberOfRecords;
            Assert.AreEqual(expected, actual);
        }
        
        [TestMethod]
        public void ActualTrafficStopsByMonth_ReturnsExpected()
        {
            Int32 notExpected = 0;
            var stops = AnalysisEngine.ActualTrafficStopsByMonth;
            Assert.AreNotEqual(notExpected, stops.Length);

        }

        [TestMethod]
        public void Months_ReturnsExpected()
        {
            Int32 notExpected = 0;
            var months = AnalysisEngine.Months;
            Assert.AreNotEqual(notExpected, months.Length);
        }

        [TestMethod]
        public void ExpectedTrafficStopsByMonth_ReturnsExpected()
        {
            var stops = AnalysisEngine.ExpectedTrafficStopsByMonth(27778);
            double expected = 2359;
            double actual =stops[0].Item2;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TrafficStopsByMonth_ReturnsExpected()
        {
            var output = AnalysisEngine.TrafficStopsByMonth;
            Assert.IsNotNull(output);

        }

        [TestMethod]
        public void Days_ReturnsExpected()
        {
            Int32 notExpected = 0;
            var days = AnalysisEngine.Days;
            Assert.AreNotEqual(notExpected, days.Length);

        }

        [TestMethod]
        public void ExpectedTrafficStopsByDay_ReturnsExpected()
        {
            var stops = AnalysisEngine.ExpectedTrafficStopsByDay(27778);
            double expected = 913;
            double actual = stops[0].Item2;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TrafficStopsByDay_ReturnsExpected()
        {
            var output = AnalysisEngine.TrafficStopsByDay;
            Assert.IsNotNull(output);
        }

        [TestMethod]
        public void TrafficStopsByHour_ReturnsExpected()
        {
            var output = AnalysisEngine.TrafficStopsByHour;
            Assert.IsNotNull(output);
        }

        [TestMethod]
        public void ActualTrafficStopsByGPS_ReturnsExpected()
        {
            var output = AnalysisEngine.ActualTrafficStopsByGPS;
            Assert.IsNotNull(output);
        }

        [TestMethod]
        public void GetVarianceOfTrafficStopsByGPS_ReturnsExpected()
        {
            var output = AnalysisEngine.GetVarianceOfTrafficStopsByGPS;
            Assert.IsNotNull(output);
        }

        [TestMethod]
        public void GetAverageOfTrafficStopsByGPS_ReturnsExpected()
        {
            var output = AnalysisEngine.GetAverageOfTrafficStopsByGPS;
            Assert.IsNotNull(output);
        }

    }
}
