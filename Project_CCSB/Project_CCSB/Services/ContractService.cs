using Project_CCSB.Models;
using Project_CCSB.Models.ViewModels;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Project_CCSB.Services
{
    public class ContractService : IContractService
    {
        //TODO: Figure out correct dates for contracts

        private readonly ApplicationDbContext _db;

        public ContractService(ApplicationDbContext db)
        {
            _db = db;
        }

        public bool IsFirstAppointment(string licensePlate)
        {
            bool isFirst = _db.Appointments.Any(x => x.LicensePlate == licensePlate);

            if (isFirst) // If appointment match is found it is not the first appointment
                return false;
            
            return true;
        }

        public async Task MakeContract(AppointmentViewModel appointment)
        {
            // Get vehicle
            Vehicle vehicle = _db.Vehicles.Where(x => x.LicensePlate == appointment.LicensePlate).FirstOrDefault();

            // Get user Id using vehicle
            string userId = _db.Vehicles.Where(x => x.LicensePlate == appointment.LicensePlate)
                                        .Select(x => x.ApplicationUser.Id)
                                        .FirstOrDefault();

            // Get User using id
            ApplicationUser user = _db.Users.Where(x => x.Id == userId).FirstOrDefault();

            // Create new invoice
            Invoice invoice = CreateNewInvoice(vehicle, DateTime.Now);

            // Create new contract
            Contract contract = CreateNewContract(user, vehicle, appointment, invoice);

            // Add the contract to the database then save
            _db.Contracts.Add(contract);
            await _db.SaveChangesAsync();
        }

        /// <summary>
        /// Creates a new Contract.
        /// </summary>
        /// <param name="user"></param>
        /// <param name="vehicle"></param>
        /// <param name="appointment"></param>
        /// <param name="invoice"></param>
        /// <returns>Returns Contract</returns>
        private Contract CreateNewContract(ApplicationUser user, Vehicle vehicle, AppointmentViewModel appointment, Invoice invoice)
        {
            return new Contract()
            {
                ApplicationUser = user,
                Vehicle = vehicle,
                Start = appointment.Date,
                End = DateTime.Now,

                Invoice = invoice
            };
        }

        /// <summary>
        /// Create new Invoice based on vehicle length and date
        /// </summary>
        /// <param name="vehicle"></param>
        /// <param name="date"></param>
        /// <returns>returns Invoice</returns>
        private Invoice CreateNewInvoice(Vehicle vehicle, DateTime date)
        {
            return new Invoice()
            {
                Amount = CalculateInvoicePrice(vehicle.Length, vehicle.Type),
                InvoiceDate = date,
            };
        }

        /// <summary>
        /// Calculate the price of invoice based on length and vehicle type
        /// </summary>
        /// <param name="length"></param>
        /// <param name="vehicleType"></param>
        /// <returns>Price in decimal</returns>
        private decimal CalculateInvoicePrice(decimal length, string vehicleType)
        {
            // Round length to nearest 0.5
            decimal vehicleLength = Math.Round(length * 2, MidpointRounding.AwayFromZero) / 2;

            // Get price from database based on vehicle type
            decimal price = _db.Rate
                           .Where(x => x.VehicleType == vehicleType)
                           .Select(x => x.Price)
                           .FirstOrDefault();

            return vehicleLength * price;
        }

        public void SetNewPrice(Rate model)
        {
            // Remove old rate
            Rate rateToRemove = _db.Rate.Where(x => x.VehicleType == model.VehicleType).FirstOrDefault();
            _db.Rate.Remove(rateToRemove);

            // Add new rate
            _db.Rate.Add(model);

            _db.SaveChanges();
        }
    }
}
