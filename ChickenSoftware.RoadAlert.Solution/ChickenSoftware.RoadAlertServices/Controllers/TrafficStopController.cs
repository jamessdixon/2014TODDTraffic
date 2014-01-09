using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using System.Web.Http.OData;
using System.Web.Http.OData.Routing;
using ChickenSoftware.RoadAlertServices.Models;

namespace ChickenSoftware.RoadAlertServices.Controllers
{
    public class TrafficStopController : ODataController
    {
        private DataContext db = new DataContext();

        // GET odata/TrafficStop
        [Queryable]
        public IQueryable<TrafficStop> GetTrafficStop()
        {
            return db.TrafficStops;
        }

        // GET odata/TrafficStop(5)
        [Queryable]
        public SingleResult<TrafficStop> GetTrafficStop([FromODataUri] int key)
        {
            return SingleResult.Create(db.TrafficStops.Where(trafficstop => trafficstop.Id == key));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TrafficStopExists(int key)
        {
            return db.TrafficStops.Count(e => e.Id == key) > 0;
        }


    }
}
