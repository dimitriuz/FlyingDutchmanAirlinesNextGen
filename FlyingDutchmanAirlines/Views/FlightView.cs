﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlyingDutchmanAirlines.Views
{
    public struct AirportInfo
    {
        public string City { get; private set; }
        public string Code { get; private set; }

        public AirportInfo((string city, string code) airport)
        {
            City = string.IsNullOrEmpty(airport.city) ? "No city found" : airport.city;
            Code = string.IsNullOrEmpty(airport.code) ? "No code found" : airport.code;
        }
    }
    public class FlightView
    {
        public string FlightNumber { get; private set; }
        public AirportInfo Origin { get; private set; }
        public AirportInfo Destination { get; private set; }

        public FlightView(string flightNumber, (string city, string code) origin, (string city, string code) destination)
        {

            FlightNumber = string.IsNullOrEmpty(flightNumber) ? "No flight number found" : flightNumber;
            Origin = new AirportInfo(origin);
            Destination = new AirportInfo(destination);

        }
    }
}
