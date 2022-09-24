﻿using FlyingDutchmanAirlines.DatabaseLayer;
using FlyingDutchmanAirlines.DatabaseLayer.Models;
using FlyingDutchmanAirlines.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlyingDutchmanAirlines.RepositoryLayer
{
    public class BookingRepository
    {
        private readonly FlyingDutchmanAirlinesContext _context;
        public BookingRepository(FlyingDutchmanAirlinesContext _context)
        {
            this._context = _context;
        }

        public async Task CreateBooking(int customerID, int flightNumber)
        {
            if (customerID < 0 || flightNumber < 0)
            {
                Console.WriteLine($"Argument Exception in CreateBooking! " +
                    $"CustomerID = {customerID}, flightNumber = {flightNumber}");
                throw new ArgumentException("Invalid arguments provided");
            }

            Booking newBooking = new Booking { 
                CustomerId = customerID, 
                FlightNumber = flightNumber 
            };    

            try
            {
                _context.Bookings.Add(newBooking);
                await _context.SaveChangesAsync();
            } catch (Exception exception)
            {
                Console.WriteLine($"Exception during database query: {exception.Message}");
                throw new CouldNotAddBookingToDatabaseException();
            }
        }

    }
}
