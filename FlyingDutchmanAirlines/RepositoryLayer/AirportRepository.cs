using FlyingDutchmanAirlines.DatabaseLayer;
using FlyingDutchmanAirlines.DatabaseLayer.Models;
using FlyingDutchmanAirlines.Exceptions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FlyingDutchmanAirlines.RepositoryLayer
{
    public class AirportRepository
    {
        private readonly FlyingDutchmanAirlinesContext _context;

        [MethodImpl(MethodImplOptions.NoInlining)]
        public AirportRepository()
        {
            if (Assembly.GetExecutingAssembly().FullName == Assembly.GetCallingAssembly().FullName)
            {
                throw new Exception("This constructor should only be used for testing");
            }
        }
        public AirportRepository(FlyingDutchmanAirlinesContext context)
        {
            _context = context;
        }

        public virtual async Task<Airport> GetAirportByID(int airportID)
        {
            if (!airportID.isPositive())
            {
                Console.WriteLine($"Argument Exception in GetAirportByID! " +
                    $"AirportID = {airportID}");
                throw new ArgumentException("Invalid argument provided");
            }

            return await _context.Airports.FirstOrDefaultAsync(a => a.AirportId == airportID) ?? throw new AirportNotFoundException();
            
        }
    }
}
