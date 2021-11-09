using Microsoft.AspNetCore.Http;
using Project_CCSB.Models;
using Project_CCSB.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Project_CCSB.Services
{
    public class ContractService : IContractService
    {
        private readonly ApplicationDbContext _db;
        private readonly IEmailSender _emailSender;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ContractService(ApplicationDbContext db, IEmailSender emailSender, IHttpContextAccessor httpContextAccessor)
        {
            _db = db;
            _emailSender = emailSender;
            _httpContextAccessor = httpContextAccessor;
        }

        public bool IsFirstAppointment(string licensePlate)
        {
            // First check if appointment exists
            bool isFirst = _db.Appointments.Any(x => x.LicensePlate == licensePlate);

            if (isFirst)
            {
                return false; // If appointment match is found it is not the first appointment
            } else
            {
                // Check if vehicle already has a contract
                bool hasContract = _db.Contracts.Any(x => x.Vehicle.LicensePlate == licensePlate);

                if (hasContract)
                    return false;

                return true;
            }
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

            // Calculate price for rest of year
            CalculatePriceRestYear(vehicle, invoice.Amount, contract.Start);

            // Add the contract to the database then save
            _db.Contracts.Add(contract);
            await _db.SaveChangesAsync();
        }

        public async Task CheckContract()
        {
            string userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            // Check if user has a vehicle and appointment
            bool vehicleExists = _db.Vehicles.Any(x => x.ApplicationUser.Id == userId);
            if (!vehicleExists)
                return;

            List<Vehicle> vehicles = _db.Vehicles.Where(x => x.ApplicationUser.Id == userId).ToList();

            foreach (Vehicle vehicle in vehicles)
            {
                if (IsFirstAppointment(vehicle.LicensePlate))
                    return;

                // Check for valid contract
                bool validContract = _db.Contracts.Any(x => x.Start.Year == DateTime.Now.Year);
            }

            // Create new contract
            Contract contract = _db.Contracts
                            .Where(x => (x.ApplicationUser.Id == userId) && (x.Start.AddYears(-1).Year == DateTime.Now.AddYears(-1).Year)).FirstOrDefault();

            AppointmentViewModel model = new AppointmentViewModel()
            {
                LicensePlate = contract.Vehicle.LicensePlate,
                Date = contract.End,
            };

            await MakeContract(model);
        }

        /// <summary>
        /// Calculates price to be paid until next year
        /// </summary>
        /// <param name="vehicle"></param>
        /// <param name="price"></param>
        /// <param name="contractStart"></param>
        private void CalculatePriceRestYear(Vehicle vehicle, decimal price, DateTime contractStart)
        {
            decimal priceRestOfYear = Math.Round(price / 12 * MonthsLeft(contractStart), 2);

            SendInvoiceMail(priceRestOfYear, vehicle);
        }

        /// <summary>
        /// Sends an email with the invoice data
        /// </summary>
        /// <param name="price"></param>
        /// <param name="vehicle"></param>
        private void SendInvoiceMail(decimal price, Vehicle vehicle)
        {
            string emailContent =
                "<div style=\"width: 250px; font-family: sans-serif; font-size: 14px; font-weight: normal; margin: 0; Margin-bottom: 50px;\">" +
                    $"<p>Voertuig: <span style=\"float: right; font-weight: bold; \">{vehicle.Type}</span></p>" +
                    $"<p>Kenteken: <span style=\"float: right; font-weight: bold; \">{vehicle.LicensePlate}</span></p>" +
                    $"<p>Prijs: <span style=\"float: right; font-weight: bold; \">€{price}</span></p>" +
                "</div>";

            var message = new Message(new string[] { "projectCCSB@gmail.com" }, "Factuur", emailContent, "addContract");
            _emailSender.SendEmail(message);
        }

        private static int MonthsLeft(DateTime contractStart)
        {
            DateTime newYears = DateTime.Parse($"01/01/{contractStart.AddYears(1).Year}");
            return (newYears.Year * 12 + newYears.Month) - (contractStart.Year * 12 + contractStart.Month);
        }

        /// <summary>
        /// Creates a new Contract.
        /// </summary>
        /// <param name="user"></param>
        /// <param name="vehicle"></param>
        /// <param name="appointment"></param>
        /// <param name="invoice"></param>
        /// <returns>Returns Contract</returns>
        private static Contract CreateNewContract(ApplicationUser user, Vehicle vehicle, AppointmentViewModel appointment, Invoice invoice)
        {
            DateTime nextYear = appointment.Date.AddYears(1);
            return new Contract()
            {
                ApplicationUser = user,
                Vehicle = vehicle,
                Start = appointment.Date,
                End = nextYear,

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

        public void RenewContracts()
        {
            Console.WriteLine("Renewing contracts");

            // Get all contracts
            List<ContractVehicleUser> contractsList = (from contracts in _db.Contracts
                                                       join users in _db.Users on contracts.ApplicationUser.Id equals users.Id
                                                       join vehicles in _db.Vehicles on contracts.Vehicle.LicensePlate equals vehicles.LicensePlate
                                                       where contracts.End.Year == DateTime.Now.Year
                                                       select new ContractVehicleUser
                                                       {
                                                           ApplicationUser = users,
                                                           Vehicle = vehicles,
                                                           Contract = contracts
                                                       }).ToList();

            // Loop through contracts and renew
            foreach (ContractVehicleUser contract in contractsList)
            {
                Contract newContract = new()
                {
                    ApplicationUser = contract.ApplicationUser,
                    Invoice = CreateNewInvoice(contract.Vehicle, DateTime.Now.AddDays(1).Date),
                    Vehicle = contract.Vehicle,
                    Start = DateTime.Now.AddDays(1).Date,
                    End = DateTime.Now.AddDays(1).AddYears(1).Date
                };
                _db.Contracts.Add(newContract);
            }

            // Save new contracts to database
            _db.SaveChanges();
        }
    }

    public class ContractVehicleUser
    {
        public Contract Contract { get; set; }
        public Vehicle Vehicle { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}
