using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ChickenSoftware.RoadAlertServices.Models
{
    public class DataContext : DbContext
    {    
        public DataContext() : base("name=DataContext")
        {
        }

        public DbSet<TrafficStop> TrafficStops { get; set; }
    
    }
}
