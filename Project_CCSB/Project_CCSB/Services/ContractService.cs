using Project_CCSB.Models;
using Project_CCSB.Models.ViewModels;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Project_CCSB.Services
{
    public class ContractService : IContractService
    {
        private readonly ApplicationDbContext _db;

        public ContractService(ApplicationDbContext db)
        {
            _db = db;
        }

        /// <summary>
        /// Check if an appointment is the first appointment
        /// </summary>
        /// <param name="licensePlate"></param>
        /// <returns>True or False</returns>
        public bool IsFirstAppointment(string licensePlate)
        {
            bool isFirst = _db.Appointments.Any(x => x.LicensePlate == licensePlate);

            if (isFirst) // If appointment match is found it is not the first appointment
                return false;
            
            return true;
        }

        public async Task MakeContract(AppointmentViewModel appointment)
        {
            // Get length of vehicle
            Vehicle vehicle = _db.Vehicles.Where(x => x.LicensePlate == appointment.LicensePlate).FirstOrDefault();

            // Create new incoive
            Invoice invoice = CreateNewInvoice(vehicle.Length, appointment.Date);
            _db.Invoices.Add(invoice);

            // Create new contract
            Contract contract = CreateNewContract(vehicle, invoice);
            _db.Contracts.Add(contract);
            
            // Save new changes to the database
            await _db.SaveChangesAsync();
        }

        /// <summary>
        /// Makes new Contract based on vehicle and invoice
        /// </summary>
        /// <param name="vehicle"></param>
        /// <param name="invoice"></param>
        /// <returns>Returns Contract</returns>
        private Contract CreateNewContract(Vehicle vehicle, Invoice invoice)
        {
            // Get user by vehicle
            ApplicationUser user = _db.Users.Where(x => x.Id == vehicle.ApplicationUser.Id).FirstOrDefault();

            Contract contract = new Contract()
            {
                ApplicationUser = user,
                Invoice = invoice,
                Vehicle = vehicle,
                Start = DateTime.Now,
                End = DateTime.Now
            };

            return contract;
        }

        /// <summary>
        /// Create new Invoice based on vehicle length and date
        /// </summary>
        /// <param name="vehicleLength"></param>
        /// <param name="date"></param>
        /// <returns>returns Invoice</returns>
        private Invoice CreateNewInvoice(decimal vehicleLength, DateTime date)
        {
            return new Invoice()
            {
                Amount = CalculateInvoicePrice(vehicleLength),
                InvoiceDate = date
            };
        }

        /// <summary>
        /// Calculate the price of invoice based on length
        /// </summary>
        /// <param name="length"></param>
        /// <returns>Price in decimal</returns>
        private decimal CalculateInvoicePrice(decimal length)
        {
            // Round length to nearest 0.5
            decimal vehicleLength = Math.Round(length * 2, MidpointRounding.AwayFromZero) / 2;

            decimal price = 45;

            return vehicleLength * price;
        }
    }
}
