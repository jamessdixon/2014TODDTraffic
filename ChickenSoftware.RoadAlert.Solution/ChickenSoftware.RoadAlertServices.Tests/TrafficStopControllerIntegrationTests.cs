using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ChickenSoftware.RoadAlertServices.Controllers;

namespace ChickenSoftware.RoadAlertServices.Tests
{
    [TestClass]
    public class TrafficStopControllerIntegrationTests
    {
        [TestMethod]
        public void GetTrafficStopUsingKey_ReturnsExpected()
        {
            TrafficStopController controller = new TrafficStopController();
            var trafficStop = controller.GetTrafficStop(1);
            Assert.IsNotNull(trafficStop);
        }


    }
}
