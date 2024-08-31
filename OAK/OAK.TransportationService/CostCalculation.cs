using Geocoding;
using OAK.Model.BusinessModels.TransportationModels.TransportationCalculationModels;
using System;
using System.Collections.Generic;

namespace OAK.TansportationServices
{
    public class CostCalculation
    {
        private const int vehicleTypeIdxMax = 2; //vehicleTypeList.Count-1;

        private static readonly List<FloorVolume> floorVolumeList = new List<FloorVolume>()
        {
            new FloorVolume {minM2 = 1, maxM2 = 15, volume = 14, numberOfPerson = 1},
            new FloorVolume {minM2 = 16, maxM2 = 30, volume = 19, numberOfPerson = 1},
            new FloorVolume {minM2 = 31, maxM2 = 45, volume = 26, numberOfPerson = 1},
            new FloorVolume {minM2 = 46, maxM2 = 60, volume = 31, numberOfPerson = 2},
            new FloorVolume {minM2 = 61, maxM2 = 75, volume = 37, numberOfPerson = 2},
            new FloorVolume {minM2 = 76, maxM2 = 90, volume = 43, numberOfPerson = 2},
            new FloorVolume {minM2 = 91, maxM2 = 105, volume = 49, numberOfPerson = 3},
            new FloorVolume {minM2 = 106, maxM2 = 120, volume = 55, numberOfPerson = 3},
            new FloorVolume {minM2 = 121, maxM2 = 135, volume = 61, numberOfPerson = 4},
            new FloorVolume {minM2 = 136, maxM2 = 150, volume = 67, numberOfPerson = 4},
            new FloorVolume {minM2 = 151, maxM2 = 165, volume = 73, numberOfPerson = 4},
            new FloorVolume {minM2 = 166, maxM2 = 180, volume = 79, numberOfPerson = 4},
            new FloorVolume {minM2 = 181, maxM2 = 195, volume = 85, numberOfPerson = 4},
            new FloorVolume {minM2 = 196, maxM2 = 210, volume = 90, numberOfPerson = 5},
            new FloorVolume {minM2 = 211, maxM2 = 225, volume = 96, numberOfPerson = 5},
            new FloorVolume {minM2 = 226, maxM2 = 240, volume = 102, numberOfPerson = 5},
            new FloorVolume {minM2 = 241, maxM2 = 255, volume = 108, numberOfPerson = 6}
        };

        private static List<VehicleType> vehicleTypeList = new List<VehicleType>()
        {
            new VehicleType
            {
                id = 1, name = "LKW 3.5t", capacity = 21, dailyRent = 110, oilConsumptionPer100Km = 17,
                costPerKm = 17 * DistanceCostants.oilCost / 100.0, speed = 85, overDinstanceCostPerKm = 0.31
            }, //Real capacity 18
            new VehicleType
            {
                id = 2, name = "LKW 7.5t", capacity = 45, dailyRent = 150, oilConsumptionPer100Km = 22,
                costPerKm = 22 * DistanceCostants.oilCost / 100.0, speed = 75, overDinstanceCostPerKm = 0.31
            }, //Real capacity 35
            new VehicleType
            {
                id = 3, name = "LKW 12t", capacity = 60, dailyRent = 180, oilConsumptionPer100Km = 30,
                costPerKm = 30 * DistanceCostants.oilCost / 100.0, speed = 70, overDinstanceCostPerKm = 0.31
            } //Real capacity 45
        };

        private static double additionalPersonVolumeCoef = 2 / 3; // 2/3 of volume of per persons volume   
        private static double distanceMin = 10.0;
        private static double distanceStep = 10.0;
        private static double distancePenalty = 0.005;

        private static double loadLiftHelper = 0.50;
        private static double personalLiftHelper = 0.25;


        // 1 m3 per hour
        public static double loadingPerHour = 3 / 5;
        public static double unloadingPerHour = 2 / 5;

        public static readonly double loadingInMinutes = 30;
        public static readonly double unloadingInMinutes = 20;
        public static readonly double restPerHourInMinutes = 8;

        public void CalculateNumberOfVehicles(TransCalRes calculationResult)
        {
            int remainingVolume = calculationResult.volume;

            calculationResult.vehicles = new List<Vehicle>();
            // number of vehicles
            int vehicleTypeIdx = 0;

            while (remainingVolume > 0)
            {
                if (vehicleTypeIdx == vehicleTypeIdxMax)
                {
                    if (remainingVolume <= vehicleTypeList[vehicleTypeIdx].capacity)
                    {
                        calculationResult.vehicles.Add(new Vehicle
                        { vehicleType = vehicleTypeList[vehicleTypeIdx].id, volume = remainingVolume });
                        remainingVolume = 0;
                    }
                    else
                    {
                        calculationResult.vehicles.Add(new Vehicle
                        {
                            vehicleType = vehicleTypeList[vehicleTypeIdx].id,
                            volume = vehicleTypeList[vehicleTypeIdx].capacity
                        });
                        remainingVolume -= vehicleTypeList[vehicleTypeIdx].capacity;
                    }

                    vehicleTypeIdx = 0;
                    continue;
                }

                if (remainingVolume <= vehicleTypeList[vehicleTypeIdx].capacity)
                {
                    calculationResult.vehicles.Add(new Vehicle
                    { vehicleType = vehicleTypeList[vehicleTypeIdx].id, volume = remainingVolume });
                    remainingVolume = 0;
                    vehicleTypeIdx = 0;
                    continue;
                }


                if (vehicleTypeIdx == 0)
                {
                    vehicleTypeIdx++;
                    continue;
                }

                if (remainingVolume - vehicleTypeList[vehicleTypeIdx].capacity <= vehicleTypeList[0].capacity)
                {
                    calculationResult.vehicles.Add(new Vehicle
                    {
                        vehicleType = vehicleTypeList[vehicleTypeIdx].id,
                        volume = vehicleTypeList[vehicleTypeIdx].capacity
                    });
                    remainingVolume -= vehicleTypeList[vehicleTypeIdx].capacity;
                    vehicleTypeIdx = 0;
                }
                else
                {
                    vehicleTypeIdx++;
                }

            }
        }

        public double calculateDistance(double sLatitude, double sLongitude, double eLatitude, double eLongitude)
        {
            var sCoord = new Location(sLatitude, sLongitude);

            var eCoord = new Location(eLatitude, eLongitude);

            return sCoord.DistanceBetween(eCoord).ToKilometers();
        }

        private int FloorDifficultyPercent(int floor)
        {
            return floor * (floor + 1) / 2;
        }

        private double NumOfPersonCoefficient(int numberOfPerson)
        {
            return numberOfPerson * (100.0 - numberOfPerson * 3.0) / 100.0;
            return numberOfPerson * (100.0 - numberOfPerson * (numberOfPerson - 1.0) * (2.0 * numberOfPerson + 1.0)) /
                   100.0;
        }

        private double HourCoefficient(int minute, int elevatorType)
        {
            double liftHelper = 0.0;
            switch (elevatorType)
            {
                case 1:
                    liftHelper = 0.25;
                    break;
                case 2:
                    liftHelper = 0.50;
                    break;
                default:
                    liftHelper = 0.0;
                    break;
            }

            int hour = minute / 60;
            if (minute < 180)
                return 1.0;
            hour -= 2;
            double performanceLoss = hour * (hour + 2 * (1.0 - liftHelper));
            double ret = (100.0 - performanceLoss) / 100.0;
            return ret;

        }

        private int CalculateVolume(TransCalReq calculationVariables)
        {
            int floorVolIndex = 0;
            int volume = 0;
            try
            {
                floorVolIndex = floorVolumeList.FindIndex(p =>
                    p.minM2 == calculationVariables.minM2 && p.maxM2 == calculationVariables.maxM2);
            }
            catch (Exception ex)
            {

            }

            FloorVolume rFloorVolume = floorVolumeList[floorVolIndex];
            volume = rFloorVolume.volume;
            for (int numOfPeopleIdx = rFloorVolume.numberOfPerson;
                numOfPeopleIdx < calculationVariables.numberOfPerson;
                numOfPeopleIdx++)
            {
                volume += ((rFloorVolume.volume / rFloorVolume.numberOfPerson) * 2) / 3;
            }

            return volume;
        }

        private double CalculatePerformanceLoss(double durationInMinutes, int numOfCarrier)
        {
            double performanceLossInMinutes = 0;
            double durationPortion = 0;

            while (durationInMinutes >= 211)
            {
                if (durationInMinutes > 60 * 9)
                {
                    durationPortion = 60 * 9;
                    durationInMinutes -= durationPortion;
                }
                else
                {
                    durationPortion = durationInMinutes;
                    durationInMinutes = 0;
                }

                performanceLossInMinutes +=
                    durationPortion * Math.Pow(2.5, (1.2 * Math.Round(durationInMinutes / 60.0))) / 100.0;
            }

            return performanceLossInMinutes;
        }

        private double CalculateLaborCost(double durationInMinutes, int numOfCarrier, List<Vehicle> vehicles)
        {
            int numOfVehicle = vehicles.Count;
            double cost = 0;
            int durationInInt = Convert.ToInt32(Math.Round(durationInMinutes / numOfCarrier) / 60.0);
            if (durationInInt > 9 && durationInInt % 9 < 3)
            {
                durationInInt = (durationInInt / 9) * 9 + 3;
            }

            cost = (DistanceCostants.driverRate * numOfVehicle +
                    DistanceCostants.carrierRate * (numOfCarrier - numOfVehicle)) * durationInInt;

            return cost;
        }

        private double CalculateFareCost(double durationInMinutes, int numOfCarrier, double distanceInKm,
            List<Vehicle> vehicles)
        {
            int numOfVehicle = vehicles.Count;
            int daysOfLabor = Convert.ToInt32(durationInMinutes / (9 * 60));
            double cost = 0;
            VehicleType slowestVehicleType = vehicleTypeList[0];
            foreach (var vhc in vehicles)
            {
                if (slowestVehicleType.id < vehicleTypeList[vhc.vehicleType - 1].id)
                    slowestVehicleType = vehicleTypeList[vhc.vehicleType - 1];
            }

            double drivingDuration = (distanceInKm * 2.0 / slowestVehicleType.speed) * 60.0;
            double totalDuration = durationInMinutes + drivingDuration;

            int daysOfRent = Convert.ToInt32(0.5 + totalDuration / (16.0 * 60.0));
            if (daysOfRent < daysOfLabor)
                daysOfRent = daysOfLabor;

            foreach (var vehicle in vehicles)
            {
                vehicle.daysOfRent = daysOfRent;

                /* daily rent */
                cost += vehicleTypeList[vehicle.vehicleType - 1].dailyRent * vehicle.daysOfRent;
                /* oil */
                cost += distanceInKm * 2.0 * vehicleTypeList[vehicle.vehicleType - 1].costPerKm;

                if (distanceInKm * 2.0 > 200.0 * daysOfRent)
                {
                    cost += (distanceInKm * 2.0 - 200.0 * daysOfRent) *
                            vehicleTypeList[vehicle.vehicleType - 1].overDinstanceCostPerKm;
                }
            }

            cost += (DistanceCostants.driverRate * numOfVehicle +
                     DistanceCostants.carrierRate * (numOfCarrier - numOfVehicle)) * drivingDuration / 60.0;

            if (numOfCarrier > numOfVehicle * 3)
            {
                /* add cost of private car travelers */
                cost += (numOfCarrier - numOfVehicle * 3) * DistanceCostants.carrierRate * distanceInKm * 2 /
                        DistanceCostants.privateCarSpeed;
                /* add cost of oil consumption of car */
                cost += distanceInKm * 2 * DistanceCostants.privateCarOilConsumptionPerKm * DistanceCostants.oilCost;
            }

            return cost;
        }

        public TransCalRes CalculateJob(TransCalReq calculationVariables)
        {
            TransCalRes calculationResult = new TransCalRes();
            //add floor difficulty
            double loadingInMinutesCalc =
                loadingInMinutes * (100 + FloorDifficultyPercent(calculationVariables.fromFloor)) / 100;
            double unloadingInMinutesCalc =
                unloadingInMinutes * (100 + FloorDifficultyPercent(calculationVariables.toFloor)) / 100;


            // add distance difficulty

            if (calculationVariables.fromWalkingWay > distanceMin)
            {
                loadingInMinutesCalc = loadingInMinutesCalc *
                                       (1 + ((calculationVariables.fromWalkingWay - distanceMin) / distanceStep) *
                                           distancePenalty);
            }

            if (calculationVariables.toWalkingWay > distanceMin)
            {
                unloadingInMinutesCalc = unloadingInMinutesCalc *
                                         (1 + ((calculationVariables.toWalkingWay - distanceMin) / distanceStep) *
                                             distancePenalty);
            }

            calculationResult.volume = CalculateVolume(calculationVariables);

            int remainingVolume = calculationResult.volume;

            CalculateNumberOfVehicles(calculationResult);

            Console.Out.Write("# of Vehicles:" + calculationResult.vehicles.Count + " ");
            foreach (var rVehicle in calculationResult.vehicles)
            {
                Console.Out.Write("VehicleType: " + vehicleTypeList[rVehicle.vehicleType - 1].name + " Volume:" +
                                  rVehicle.volume + " ");
            }

            Console.Out.WriteLine("\n-----------------------");

            double carriableLoadingVolumeInHour;
            double remainingLoadingVolumeIndouble;

            double carriableUnloadingVolumeInHour;
            double remainingUnloadingVolumeIndouble;
            // carrier cost
            for (int numOfPerson = calculationResult.vehicles.Count * 2; numOfPerson <= 6; numOfPerson++)
            {
                remainingLoadingVolumeIndouble = calculationResult.volume;
                remainingUnloadingVolumeIndouble = calculationResult.volume;

                int loadingDay = 0;
                int loadingMinute = 0;

                int unloadingDay = 0;
                int unloadingMinute = 0;

                while (remainingLoadingVolumeIndouble > 0.01)
                {
                    carriableLoadingVolumeInHour =
                        60.0 * NumOfPersonCoefficient(numOfPerson) *
                        HourCoefficient(loadingMinute, calculationVariables.fromElevatorType) /
                        loadingInMinutesCalc;

                    if (remainingLoadingVolumeIndouble > carriableLoadingVolumeInHour)
                    {
                        loadingMinute += 60;
                        remainingLoadingVolumeIndouble -= carriableLoadingVolumeInHour;
                    }
                    else
                    {
                        loadingMinute +=
                            Convert.ToInt32(
                                Math.Truncate(60.0 * remainingLoadingVolumeIndouble / carriableLoadingVolumeInHour));
                        remainingLoadingVolumeIndouble = 0;
                    }

                    if (loadingMinute >= 600)
                    {
                        loadingDay++;
                        loadingMinute = loadingMinute - 600;
                    }
                }

                while (remainingUnloadingVolumeIndouble > 0.01)
                {
                    carriableUnloadingVolumeInHour =
                        60.0 * NumOfPersonCoefficient(numOfPerson) *
                        HourCoefficient(unloadingMinute, calculationVariables.toElevatorType) /
                        unloadingInMinutesCalc;

                    if (remainingUnloadingVolumeIndouble > carriableUnloadingVolumeInHour)
                    {
                        unloadingMinute += 60;
                        remainingUnloadingVolumeIndouble -= carriableUnloadingVolumeInHour;
                    }
                    else
                    {
                        unloadingMinute +=
                            Convert.ToInt32(Math.Truncate(60.0 * remainingUnloadingVolumeIndouble /
                                                          carriableUnloadingVolumeInHour));
                        remainingUnloadingVolumeIndouble = 0;
                    }

                    if (unloadingMinute >= 600)
                    {
                        unloadingDay++;
                        unloadingMinute = unloadingMinute - 600;
                    }
                }

                System.Console.Out.WriteLine("NumberOfCarrier:" + numOfPerson + " Volume:" + calculationResult.volume +
                                             " loadingDay:" + loadingDay + " loadingHour:" + loadingMinute / 60 +
                                             " loadingMinute:" + loadingMinute % 60 +
                                             " unloadingDay:" + unloadingDay + " unloadingHour:" +
                                             unloadingMinute / 60 + " unloadingMinute:" + unloadingMinute % 60);


            }

            return calculationResult;

        }


        private class CalculationOfWork
        {
            public int numberOfCarrier { get; set; }
            public double duration { get; set; }
            public double cost { get; set; }
        }

        public class FloorVolume
        {
            public int minM2 { get; set; }
            public int maxM2 { get; set; }
            public int volume { get; set; }
            public int numberOfPerson { get; set; }
        }

        public static class DistanceCostants
        {
            public static double driverRate = 17.5;
            public static double carrierRate = 14.95;
            public static double oilCost = 1.45;
            public static double privateCarOilConsumptionPerKm = 0.08;
            public static double privateCarSpeed = 50;
        }

        public double CalculateLoadDurationElevatorType_0(TransCalReq calculationVariables, TransCalRes calculationResult, int numberOfWorker)
        {
            double volumeDivider = 55;
            double floorDivider = 4;
            double workerDivider = 6;
            double walkingWayDivider = 15;
            double durationMultiplier = 360;

            double x0Coefficient = 0.1555;
            double volumeCoefficient = 0.9537;
            double floorCoefficient = 0.2596;
            double workerCoefficient = -0.3109;
            double walkingWayCoefficient = 0.0495;



            double loadDurationInMinutes = +x0Coefficient
                                           + volumeCoefficient * (calculationResult.volume / volumeDivider)
                                           + floorCoefficient * (calculationVariables.fromFloor / floorDivider)
                                           + workerCoefficient * (numberOfWorker / workerDivider)
                                           + walkingWayCoefficient * (calculationVariables.fromWalkingWay / walkingWayDivider);


            loadDurationInMinutes *= durationMultiplier;

            return loadDurationInMinutes;
        }

        public double CalculateLoadDurationElevatorType_1(TransCalReq calculationVariables, TransCalRes calculationResult, int numberOfWorker)
        {
            double loadDurationInMinutes = CalculateLoadDurationElevatorType_0(calculationVariables, calculationResult, numberOfWorker);

            return loadDurationInMinutes;
        }

        public double CalculateLoadDurationElevatorType_2(TransCalReq calculationVariables, TransCalRes calculationResult, int numberOfWorker)
        {
            double loadDurationInMinutes = CalculateLoadDurationElevatorType_0(calculationVariables, calculationResult, numberOfWorker);

            return loadDurationInMinutes;
        }

        public double CalculateUnloadDurationElevatorType_0(TransCalReq calculationVariables, TransCalRes calculationResult, int numberOfWorker)
        {
            double volumeDivider = 55;
            double floorDivider = 4;
            double workerDivider = 6;
            double walkingWayDivider = 15;
            double durationMultiplier = 360;

            double x0Coefficient = 0.1555;
            double volumeCoefficient = 0.9537;
            double floorCoefficient = 0.2596;
            double workerCoefficient = -0.3109;
            double walkingWayCoefficient = 0.0495;



            double unloadDurationInMinutes = +x0Coefficient
                                           + volumeCoefficient * (calculationResult.volume / volumeDivider)
                                           + floorCoefficient * (calculationVariables.toFloor / floorDivider)
                                           + workerCoefficient * (numberOfWorker / workerDivider)
                                           + walkingWayCoefficient * (calculationVariables.toWalkingWay / walkingWayDivider);


            unloadDurationInMinutes *= durationMultiplier;

            return unloadDurationInMinutes;
        }

        public double CalculateUnloadDurationElevatorType_1(TransCalReq calculationVariables, TransCalRes calculationResult, int numberOfWorker)
        {
            double unloadDurationInMinutes = CalculateUnloadDurationElevatorType_0(calculationVariables, calculationResult, numberOfWorker);

            return unloadDurationInMinutes;
        }

        public double CalculateUnloadDurationElevatorType_2(TransCalReq calculationVariables, TransCalRes calculationResult, int numberOfWorker)
        {
            double unloadDurationInMinutes = CalculateUnloadDurationElevatorType_0(calculationVariables, calculationResult, numberOfWorker);

            return unloadDurationInMinutes;
        }

        public TransCalRes CalculateTransportationML(TransCalReq calculationVariables)
        {
            TransCalRes calculationResult = new TransCalRes();

            calculationResult.distanceInKM = calculationVariables.dinstanceInKM;

            calculationResult.volume = CalculateVolume(calculationVariables);

            CalculateNumberOfVehicles(calculationResult);

            /*
             * Loading Model
             * Duration(minutes)/100 = −2.5127 + 0.3611 * (Vol/10) + 7.1025*(Kat/10) −1.792*(PA+FA*2) +4.8554*(Calisan/10) +0.534 * (Vol/10) * (PA+FA*2)
             */

            double optimumLoadDuration = 9999999999;
            int optimumLoadNumberOfCarrier = 0;
            double optimumLoadCost = 999999999999;

            double optimumUnloadDuration = 9999999999;
            int optimumUnloadNumberOfCarrier = 0;
            double optimumUnloadCost = 999999999999;

            double loadDurationInMinutes = 0;
            double unloadDurationInMinutes = 0;
            double loadCost = 0;
            double unloadCost = 0;
            double fareCost = 0;
            List<CalculationOfWork> loadCalculationList = new List<CalculationOfWork>();
            List<CalculationOfWork> unloadCalculationList = new List<CalculationOfWork>();

            for (int numOfCarrier = calculationResult.vehicles.Count * 2; numOfCarrier <= 6; numOfCarrier++)
            {

                if (calculationVariables.fromElevatorType == 0)
                {
                    loadDurationInMinutes =
                        CalculateLoadDurationElevatorType_0(calculationVariables, calculationResult, numOfCarrier);
                }
                else if (calculationVariables.fromElevatorType == 1)
                {
                    loadDurationInMinutes =
                        CalculateLoadDurationElevatorType_1(calculationVariables, calculationResult, numOfCarrier);
                }
                else //calculationVariables.fromElevatorType==2
                {
                    loadDurationInMinutes =
                        CalculateLoadDurationElevatorType_2(calculationVariables, calculationResult, numOfCarrier);
                }

                loadCost = CalculateLaborCost(loadDurationInMinutes, numOfCarrier, calculationResult.vehicles);

                loadCalculationList.Add(new CalculationOfWork()
                {
                    numberOfCarrier = numOfCarrier,
                    duration = loadDurationInMinutes,
                    cost = loadCost
                });

                if (loadCost + fareCost < optimumLoadCost)
                {
                    optimumLoadCost = loadCost;
                    optimumLoadNumberOfCarrier = numOfCarrier;
                    optimumLoadDuration = loadDurationInMinutes;

                }


                if (calculationVariables.toElevatorType == 0)
                {
                    unloadDurationInMinutes =
                        CalculateUnloadDurationElevatorType_0(calculationVariables, calculationResult, numOfCarrier);
                }
                else if (calculationVariables.toElevatorType == 1)
                {
                    unloadDurationInMinutes =
                        CalculateUnloadDurationElevatorType_1(calculationVariables, calculationResult, numOfCarrier);
                }
                else //calculationVariables.toElevatorType==2
                {
                    unloadDurationInMinutes =
                        CalculateUnloadDurationElevatorType_2(calculationVariables, calculationResult, numOfCarrier);
                }

                unloadCost = CalculateLaborCost(unloadDurationInMinutes, numOfCarrier,
                    calculationResult.vehicles);

                unloadCalculationList.Add(new CalculationOfWork()
                {
                    numberOfCarrier = numOfCarrier,
                    duration = unloadDurationInMinutes,
                    cost = unloadCost
                });
            }

            CalculationOfWork optimumLoadCalculationOfWork = loadCalculationList[0];
            CalculationOfWork optimumUnloadCalculationOfWork = unloadCalculationList[0];
            double optimumFareCost = 0;
            double optimumTotalCost = 99999999;
            double costOfCase = 0;
            foreach (var loadCalculation in loadCalculationList)
            {
                foreach (var unloadCalculation in unloadCalculationList)
                {
                    fareCost = CalculateFareCost(
                        loadCalculation.duration + unloadCalculation.duration,
                        unloadCalculation.numberOfCarrier, calculationVariables.dinstanceInKM,
                        calculationResult.vehicles);
                    costOfCase = fareCost + loadCalculation.cost + unloadCalculation.cost;

                    if (optimumTotalCost > costOfCase)
                    {

                        optimumTotalCost = costOfCase;
                        optimumFareCost = fareCost;
                        optimumLoadCalculationOfWork.cost = loadCalculation.cost;
                        optimumLoadCalculationOfWork.duration = loadCalculation.duration;
                        optimumLoadCalculationOfWork.numberOfCarrier = loadCalculation.numberOfCarrier;

                        optimumUnloadCalculationOfWork.cost = unloadCalculation.cost;
                        optimumUnloadCalculationOfWork.duration = unloadCalculation.duration;
                        optimumUnloadCalculationOfWork.numberOfCarrier = unloadCalculation.numberOfCarrier;

                    }

                }
            }

            /*load data */
            calculationResult.numOfLoadWorker = optimumLoadCalculationOfWork.numberOfCarrier;
            calculationResult.laborLoadCost = optimumLoadCalculationOfWork.cost;

            optimumLoadDuration = optimumLoadCalculationOfWork.duration;
            calculationResult.loadDay = 0;
            calculationResult.loadHour = 0;
            calculationResult.loadMinute = 0;

            if (Convert.ToInt32(optimumLoadDuration) >= (60 * 9))
            {
                calculationResult.loadDay = Convert.ToInt32(optimumLoadDuration / (60 * 9));
                optimumLoadDuration -= calculationResult.loadDay * (60 * 9);
            }

            if (Convert.ToInt32(optimumLoadDuration) != (60))
            {
                calculationResult.loadHour = Convert.ToInt32(optimumLoadDuration / 60);
                optimumLoadDuration -= calculationResult.loadHour * 60;
            }

            calculationResult.loadMinute = Convert.ToInt32(optimumLoadDuration);

            /* unload data */
            calculationResult.numOfUnloadWorker = optimumUnloadCalculationOfWork.numberOfCarrier;
            calculationResult.laborUnloadCost = optimumUnloadCalculationOfWork.cost;

            optimumUnloadDuration = optimumUnloadCalculationOfWork.duration;

            calculationResult.unloadDay = 0;
            calculationResult.unloadHour = 0;
            calculationResult.unloadMinute = 0;

            if (Convert.ToInt32(optimumUnloadDuration) >= (60 * 9))
            {
                calculationResult.unloadDay = Convert.ToInt32(optimumUnloadDuration / (60 * 9));
                optimumUnloadDuration -= calculationResult.unloadDay * (60 * 9);
            }

            if (Convert.ToInt32(optimumUnloadDuration) >= (60))
            {
                calculationResult.unloadHour = Convert.ToInt32(optimumUnloadDuration / 60);
                optimumUnloadDuration -= calculationResult.unloadHour * 60;
            }

            calculationResult.unloadMinute = Convert.ToInt32(optimumUnloadDuration);


            /* fare cost */
            calculationResult.fareCost = optimumFareCost;

            /* accomodation cost */

            return calculationResult;
        }
    }
}