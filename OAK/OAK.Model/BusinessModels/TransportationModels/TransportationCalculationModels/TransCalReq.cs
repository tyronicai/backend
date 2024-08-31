namespace OAK.Model.BusinessModels.TransportationModels.TransportationCalculationModels
{
    public class TransCalReq
    {
        public int minM2 { get; set; }
        public int maxM2 { get; set; }
        public int fromFloor { get; set; }
        public int toFloor { get; set; }

        public int fromElevatorType { get; set; }
        public int toElevatorType { get; set; }

        public int fromWalkingWay { get; set; }
        public int toWalkingWay { get; set; }
        public int numberOfPerson { get; set; }
        public double fromLatitude { get; set; }
        public double fromLongitude { get; set; }
        public double toLatitude { get; set; }
        public double toLongitude { get; set; }

        public double dinstanceInKM { get; set; }
    }
}