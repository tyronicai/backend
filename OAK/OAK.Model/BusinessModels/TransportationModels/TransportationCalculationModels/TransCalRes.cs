using System;
using System.Collections.Generic;

namespace OAK.Model.BusinessModels.TransportationModels.TransportationCalculationModels
{
    public class TransCalRes
    {
        public List<Vehicle> vehicles = new List<Vehicle>();
        public int volume { get; set; }
        public int loadDay { get; set; }
        public int loadHour { get; set; }
        public int loadMinute { get; set; }
        public TimeSpan loadDuration { get; set; }

        public int unloadDay { get; set; }
        public int unloadHour { get; set; }
        public int unloadMinute { get; set; }
        public TimeSpan unloadDuration { get; set; }

        public int numOfLoadWorker { get; set; }
        public int numOfUnloadWorker { get; set; }

        public double distanceInKM { get; set; }
        public double laborLoadCost { get; set; }
        public double laborUnloadCost { get; set; }
        public double fareCost { get; set; }
        public double accomodationCost { get; set; }

    }
}