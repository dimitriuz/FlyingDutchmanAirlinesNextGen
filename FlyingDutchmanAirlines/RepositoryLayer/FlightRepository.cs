using FlyingDutchmanAirlines.DatabaseLayer;
using FlyingDutchmanAirlines.DatabaseLayer.Models;
using FlyingDutchmanAirlines.Exceptions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace FlyingDutchmanAirlines.RepositoryLayer
{
    public class FlightRepository
    {
        private readonly FlyingDutchmanAirlinesContext _context;

        public FlightRepository(FlyingDutchmanAirlinesContext context)
        {
            _context = context;
        }

        public async Task<Flight> GetFlightByFlightNumber(int flightNumber, int originAirportId, int destinationAirportId)
        {
            if (flightNumber < 0 || originAirportId < 0 || destinationAirportId < 0)
            {
                Console.WriteLine($"Argument Exception in GetFlightByFlightNumber! " +
                    $"flightNumber = {flightNumber}, originAirportId = {originAirportId}, destinationAirportId = {destinationAirportId}");
                throw new ArgumentException("Invalid argument provided");
            }

            return await _context.Flights.FirstOrDefaultAsync(f => (f.FlightNumber == flightNumber && f.Origin == originAirportId && f.Destination == destinationAirportId)) 
                ?? throw new FlightNotFoundException();

        }
    }
}
