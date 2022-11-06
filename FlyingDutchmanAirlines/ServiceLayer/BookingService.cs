﻿using FlyingDutchmanAirlines.DatabaseLayer.Models;
using FlyingDutchmanAirlines.Exceptions;
using FlyingDutchmanAirlines.RepositoryLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;

namespace FlyingDutchmanAirlines.ServiceLayer
{
    public class BookingService
    {
        private readonly BookingRepository _bookingRepository;
        private readonly CustomerRepository _customerRepository;
        private readonly FlightRepository _flightRepository;

        public BookingService(BookingRepository bookingRepository, CustomerRepository customerRepository, FlightRepository flightRepository)
        {
            _bookingRepository = bookingRepository;
            _customerRepository = customerRepository;
            _flightRepository = flightRepository;
        }

        public async Task<(bool, Exception?)> CreateBooking(string customerName, int flightNumber)
        {
            if (String.IsNullOrEmpty(customerName) || !flightNumber.isPositive())
            {
                return (false, new ArgumentException());
            }

            try
            {
                Customer customer = await GetCustomerFromDatabase(customerName) ?? await AddCustomerToDatabase(customerName);

                if (!await FlightExistsInDatabase(flightNumber))
                    return (false, new CouldNotAddBookingToDatabaseException());
                
                await _bookingRepository.CreateBooking(customer.CustomerId, flightNumber);
                return(true, null);
            } catch(Exception exception)
            {
                return (false, exception);
            }
        }

        private async Task<bool> FlightExistsInDatabase(int flightNumner)
        {
            try
            {
                return await _flightRepository.GetFlightByFlightNumber(flightNumner) != null;
            } catch (FlightNotFoundException)
            {
                return false;
            }
        }

        private async Task<Customer> GetCustomerFromDatabase(string name)
        {
            try
            {
                return await _customerRepository.GetCustomerByName(name);
            }
            catch (CustomerNotFoundException)
            {
                return null;
            }
            catch (Exception exception)
            {
                ExceptionDispatchInfo.Capture(exception.InnerException ?? new Exception()).Throw();
                return null;
            }
        }

        private async Task<Customer> AddCustomerToDatabase(string name)
        {
            await _customerRepository.CreateCustomer(name);
            return await _customerRepository.GetCustomerByName(name);
        }
    }
}
