﻿using System;

namespace imL.Rest.Frotcom.Schema
{
    public class Vehicle
    {
        public long id { get; set; }
        public string kind { get; set; }
        public bool hasCanbus { get; set; }
        public bool hasObd2 { get; set; }
        public bool hasSensor1 { get; set; }
        public bool hasSensor2 { get; set; }
        public bool hasSensor3 { get; set; }
        public int typeId { get; set; }
        public string typeName { get; set; }
        public int classId { get; set; }
        public string className { get; set; }
        public int assetId { get; set; }
        public int vehicleId { get; set; }
        public string coupledLicensePlate { get; set; }
        public string licensePlate { get; set; }
        public string field1 { get; set; }
        public string field2 { get; set; }
        public string field3 { get; set; }
        public string field4 { get; set; }
        public string field5 { get; set; }
        public int driverId { get; set; }
        public string driverName { get; set; }
        public string driverRef { get; set; }
        public int stopDuration { get; set; }
        public decimal latitude { get; set; }
        public decimal longitude { get; set; }
        public decimal direction { get; set; }
        public decimal speed { get; set; }
        public bool isOnPrivacy { get; set; }
        public int immobilizer { get; set; }
        public int icon { get; set; }
        public decimal odometerGps { get; set; }
        public decimal odometerCanbus { get; set; }
        public int tachoWorkState { get; set; }
        public string imei { get; set; }
        public DateTime? lastCommunication { get; set; }
        public int communicationStatus { get; set; }
        public int departmentId { get; set; }
        public string department { get; set; }
        public int segmentId { get; set; }
        public string segment { get; set; }
        public bool isOnTrip { get; set; }
        public int stopSpeedLimit { get; set; }
        public bool fuelManagement { get; set; }
        public string config { get; set; }
        public bool hasImmobilizer { get; set; }
        public bool hasBuzzer { get; set; }
        public string manufacturer { get; set; }
        public string model { get; set; }
        public int modelYear { get; set; }
        public decimal chronometer { get; set; }
        public decimal costKm { get; set; }
        public string driverMsisdn { get; set; }
        public int coDriverId { get; set; }
        public string coDriverName { get; set; }
        public string coDriverMsisdn { get; set; }
        public Alarm alarms { get; set; }
        public Message messages { get; set; }
        public Trip trips { get; set; }
        public Driverdrivingtime driverDrivingTimes { get; set; }
        public Codriverdrivingtime coDriverDrivingTimes { get; set; }
        public int placeId { get; set; }
        public string placeName { get; set; }
        public int routeId { get; set; }
        public string routeCode { get; set; }
        public Sensor sensors { get; set; }
        public int engineId { get; set; }
        public bool isStopped { get; set; }
        public bool useCanbusMileage { get; set; }
        public bool hasCruiseControl { get; set; }
        public int ecoProfileId { get; set; }
        public int fuelConsumption { get; set; }
        public decimal fuel100Km { get; set; }
        public decimal fuelHour { get; set; }
        public int vehicleStatusColor { get; set; }
        public string vehicleStatusName { get; set; }
        public int vehicleStatusId { get; set; }
        public string battery { get; set; }
        public bool canSendMessage { get; set; }
        public bool hasDrivingTimes { get; set; }
        public string protocols { get; set; }
        public string[] supportedProtocols { get; set; }
        public bool odometerOk { get; set; }
        public bool totalFuelOk { get; set; }
        public bool engineHoursOk { get; set; }
        public bool hasSeatBelt { get; set; }
        public bool hasGrossCombinationWeight { get; set; }
        public bool hasFirstAxleWeight { get; set; }
        public bool hasSecondAxleWeight { get; set; }
        public bool hasTrailerWeight { get; set; }
        public bool hasOilLowLevelIndicator { get; set; }
        public bool hasAdBlueLevel { get; set; }
        public bool hasBrakePedal { get; set; }
        public bool hasTotalFuelUsedHighRes { get; set; }
        public int canBusTotalFuelUsed { get; set; }
        public string vehicleAssetType { get; set; }
        public bool hasFatigueDetection { get; set; }
        public bool hasDistractionDetection { get; set; }
    }
}