using FlyingDutchmanAirlines.DatabaseLayer;
using FlyingDutchmanAirlines.DatabaseLayer.Models;
using FlyingDutchmanAirlines.Exceptions;
using FlyingDutchmanAirlines.RepositoryLayer;
using FlyingDutchmanAirlines_Tests.Stubs;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;

namespace FlyingDutchmanAirlines_Tests.RepositoryLayer
{
    [TestClass]
    public class FlightRepositoryTests
    {
        private FlyingDutchmanAirlinesContext _context;
        private FlightRepository _repository;

        [TestInitialize]
        public async Task TestInitialize()
        {
            DbContextOptions<FlyingDutchmanAirlinesContext> options =
                new DbContextOptionsBuilder<FlyingDutchmanAirlinesContext>()
                .UseInMemoryDatabase("FlyingDutchman").Options;
            _context = new FlyingDutchmanAirlinesContext_Stub(options);
           


            _repository = new FlightRepository(_context);
            Assert.IsNotNull(_repository);
        }

        [TestMethod]
        public async Task GetFlightByNumber_Success(int flightNumber, int originId, int destinationId)
        {
            Flight flight = await _repository.GetFlightByFlightNumber(flightNumber, originId, destinationId);
            Flight dbFlight = _context.Flights.First(f => f.FlightNumber == flightNumber && f.Destination == destinationId && f.Origin == originId);
            Assert.IsNotNull(flight);
            Assert.AreEqual(dbFlight.FlightNumber, flight.FlightNumber);
            Assert.AreEqual(dbFlight.Destination, flight.Destination);
            Assert.AreEqual(dbFlight.Origin, flight.Origin);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public async Task GetFlightByID_Failure_InvelidInput()
        {
            StringWriter outputStream = new StringWriter();
            try
            {
                Console.SetOut(outputStream);
                await _repository.GetFlightByFlightNumber(-1, -1, -1);
            }
            catch (ArgumentException)
            {
                Assert.IsTrue(outputStream.ToString().Contains("Argument Exception in GetFlightByFlightNumber! flightNumber = -1"));
                throw new ArgumentException();
            }
            finally
            {
                outputStream.Dispose();
            }

        }

        [TestMethod]
        [ExpectedException(typeof(FlightNotFoundException))]
        public async Task GetFlightByID_Failure_DatabaseException()
        {
            await _repository.GetFlightByFlightNumber(10, 1, 1);
        }
    }
}
