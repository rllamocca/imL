﻿using System;

namespace imL.Rest.Frotcom.Schema
{
#pragma warning disable IDE1006 // Estilos de nombres
    public class Vehicle200
    {
        public long id { set; get; }
        public string kind { set; get; }
        public bool hasCanbus { set; get; }
        public bool hasObd2 { set; get; }
        public bool hasSensor1 { set; get; }
        public bool hasSensor2 { set; get; }
        public bool hasSensor3 { set; get; }
        public int typeId { set; get; }
        public string typeName { set; get; }
        public int classId { set; get; }
        public string className { set; get; }
        public int assetId { set; get; }
        public int vehicleId { set; get; }
        public string coupledLicensePlate { set; get; }
        public string licensePlate { set; get; }
        public string field1 { set; get; }
        public string field2 { set; get; }
        public string field3 { set; get; }
        public string field4 { set; get; }
        public string field5 { set; get; }
        public int driverId { set; get; }
        public string driverName { set; get; }
        public string driverRef { set; get; }
        public int stopDuration { set; get; }
        public decimal latitude { set; get; }
        public decimal longitude { set; get; }
        public decimal direction { set; get; }
        public decimal speed { set; get; }
        public bool isOnPrivacy { set; get; }
        public int immobilizer { set; get; }
        public int icon { set; get; }
        public decimal odometerGps { set; get; }
        public decimal odometerCanbus { set; get; }
        public int tachoWorkState { set; get; }
        public string imei { set; get; }
        public DateTime? lastCommunication { set; get; }
        public int communicationStatus { set; get; }
        public int departmentId { set; get; }
        public string department { set; get; }
        public int segmentId { set; get; }
        public string segment { set; get; }
        public bool isOnTrip { set; get; }
        public int stopSpeedLimit { set; get; }
        public bool fuelManagement { set; get; }
        public string config { set; get; }
        public bool hasImmobilizer { set; get; }
        public bool hasBuzzer { set; get; }
        public string manufacturer { set; get; }
        public string model { set; get; }
        public int modelYear { set; get; }
        public decimal chronometer { set; get; }
        public decimal costKm { set; get; }
        public string driverMsisdn { set; get; }
        public int coDriverId { set; get; }
        public string coDriverName { set; get; }
        public string coDriverMsisdn { set; get; }
        public Alarm alarms { set; get; }
        public Message messages { set; get; }
        public Trip trips { set; get; }
        public Driverdrivingtime driverDrivingTimes { set; get; }
        public Codriverdrivingtime coDriverDrivingTimes { set; get; }
        public int placeId { set; get; }
        public string placeName { set; get; }
        public int routeId { set; get; }
        public string routeCode { set; get; }
        public Sensor sensors { set; get; }
        public int engineId { set; get; }
        public bool isStopped { set; get; }
        public bool useCanbusMileage { set; get; }
        public bool hasCruiseControl { set; get; }
        public int ecoProfileId { set; get; }
        public int fuelConsumption { set; get; }
        public decimal fuel100Km { set; get; }
        public decimal fuelHour { set; get; }
        public int vehicleStatusColor { set; get; }
        public string vehicleStatusName { set; get; }
        public int vehicleStatusId { set; get; }
        public string battery { set; get; }
        public bool canSendMessage { set; get; }
        public bool hasDrivingTimes { set; get; }
        public string protocols { set; get; }
        public string[] supportedProtocols { set; get; }
        public bool odometerOk { set; get; }
        public bool totalFuelOk { set; get; }
        public bool engineHoursOk { set; get; }
        public bool hasSeatBelt { set; get; }
        public bool hasGrossCombinationWeight { set; get; }
        public bool hasFirstAxleWeight { set; get; }
        public bool hasSecondAxleWeight { set; get; }
        public bool hasTrailerWeight { set; get; }
        public bool hasOilLowLevelIndicator { set; get; }
        public bool hasAdBlueLevel { set; get; }
        public bool hasBrakePedal { set; get; }
        public bool hasTotalFuelUsedHighRes { set; get; }
        public int canBusTotalFuelUsed { set; get; }
        public string vehicleAssetType { set; get; }
        public bool hasFatigueDetection { set; get; }
        public bool hasDistractionDetection { set; get; }
    }
#pragma warning restore IDE1006 // Estilos de nombres
}
