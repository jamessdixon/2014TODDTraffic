using ChickenSoftware.RoadAlertServices.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ChickenSoftware.RoadAlertServices.Controllers
{
    public class TrafficStopSearchController : ApiController
    {
        public List<TrafficStop> Get()
        {
            DataContext context = new DataContext();
            return context.TrafficStops.ToList<TrafficStop>();
        }
        public TrafficStop Get(int id)
        {
            DataContext context = new DataContext();
            return context.TrafficStops.Where(ts => ts.Id == id).FirstOrDefault();
        }

        [HttpGet]
        [Route("api/TrafficStopSearch/Sample/")]
        public List<TrafficStop> Sample()
        {
            DataContext context = new DataContext();
            return context.TrafficStops.Where(ts => ts.Id < 100).ToList();
        }
    }
}
