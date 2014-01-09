using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChickenSoftware.RoadAlertServices.Models
{
    public class TrafficStop
    {
        public Int32 Id { get; set; }
        public double CadCallId { get; set; }
        public DateTime StopDateTime { get; set; }
        public Int32 DispositionId { get; set; }
        public String DispositionDesc { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}