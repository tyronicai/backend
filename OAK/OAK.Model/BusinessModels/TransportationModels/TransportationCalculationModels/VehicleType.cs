namespace OAK.Model.BusinessModels.TransportationModels.TransportationCalculationModels
{
    public class VehicleType
    {
        public int id { get; set; }
        public string name { get; set; }
        public int capacity { get; set; }
        public double dailyRent { get; set; }
        public double costPerKm { get; set; }

        public double oilConsumptionPer100Km { get; set; }
        public double speed { get; set; }
        public double overDinstanceCostPerKm { get; set; }
    }
}