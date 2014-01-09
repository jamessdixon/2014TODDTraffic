using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ChickenSoftware.RoadAlertServices.Controllers;

namespace ChickenSoftware.RoadAlertServices.Tests
{
    [TestClass]
    public class TrafficStopSearchControllerIntegrationTests
    {
        [TestMethod]
        public void GetSample_Returns99Records()
        {
            TrafficStopSearchController controller = new TrafficStopSearchController();
            var sample = controller.Sample();

            Int32 expected = 99;
            Int32 actual = sample.Count;

            Assert.AreEqual(expected, actual);
        }
    }
}
