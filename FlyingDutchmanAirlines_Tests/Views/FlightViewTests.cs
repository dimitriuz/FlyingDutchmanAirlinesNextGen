using FlyingDutchmanAirlines.Views;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlyingDutchmanAirlines_Tests.Views
{
    [TestClass]
    public class FlightViewTests
    {
        [TestMethod]
        public void Constructor_FlightView_Success()
        {
            string flightNumber = "0";
            string originCity = "Amsterdam";
            string originCityCode = "AMS";
            string destinationCity = "Moscow";
            string destinationCityCode = "SVO";

            FlightView view = new FlightView(flightNumber, (originCity, originCityCode), (destinationCity, destinationCityCode));
            
            Assert.IsNotNull(view);
            Assert.AreEqual(flightNumber, view.FlightNumber);
            Assert.AreEqual(originCity, view.Origin.City);
            Assert.AreEqual(originCityCode, view.Origin.Code);
            Assert.AreEqual(destinationCity, view.Destination.City);
            Assert.AreEqual (destinationCityCode, view.Destination.Code);
        }

        public void Constructor_FlightView_Success_FlightNumber_Null()
        {
            string originCity = "Athens";
            string originCityCode = "ATH";
            string destinationCity = "Dubai";
            string destinationCityCode = "DXB";

            FlightView view = new FlightView(null, (originCity, originCityCode), (destinationCity, destinationCityCode));

            Assert.IsNotNull(view);
            Assert.AreEqual(view.FlightNumber, "No flight number found");
            Assert.AreEqual(originCity, view.Origin.City);
            Assert.AreEqual(destinationCity, view.Destination.City);
        }

        [TestMethod]
        public void Constructor_AirportStruct_Success_City_EmptyString()
        {
            string destinationCity = string.Empty;
            string destinationCityCode = "SYD";

            AirportInfo airportStruct = new AirportInfo((destinationCity, destinationCityCode));
            Assert.IsNotNull(airportStruct);

            Assert.AreEqual(airportStruct.City, "No city found");
            Assert.AreEqual(airportStruct.Code, destinationCityCode);
        }

        [TestMethod]
        public void Constructor_AirportStruct_Success_Code_EmptyString()
        {
            string destinationCity = "Ushuaia";
            string destinationCityCode = string.Empty;

            AirportInfo airportStruct = new AirportInfo((destinationCity, destinationCityCode));
            Assert.IsNotNull(airportStruct);

            Assert.AreEqual(airportStruct.City, destinationCity);
            Assert.AreEqual(airportStruct.Code, "No code found");
        }

    }
}
