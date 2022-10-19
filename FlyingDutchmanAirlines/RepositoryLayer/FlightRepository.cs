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
           
            if (!originAirportId.isPositive() || !destinationAirportId.isPositive())
            {
                Console.WriteLine($"Argument Exception in GetFlightByFlightNumber! " +
                    $"flightNumber = {flightNumber}, originAirportId = {originAirportId}, destinationAirportId = {destinationAirportId}");
                throw new ArgumentException("Invalid argument provided");
            }

            if (!flightNumber.isPositive())
            {
                Console.WriteLine($"Could not find flight in GetFlightByFlightNumber! flightNumber = {flightNumber}");
                throw new FlightNotFoundException();
            }

            return await _context.Flights.FirstOrDefaultAsync(f => (f.FlightNumber == flightNumber)) 
                ?? throw new FlightNotFoundException();

        }
    }
}
