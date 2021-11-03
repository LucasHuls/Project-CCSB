using Project_CCSB.Models.ViewModels;
using System.Threading.Tasks;

namespace Project_CCSB.Services
{
    public interface IContractService
    {
        public bool IsFirstAppointment(string licensePlate);
        public Task MakeContract(AppointmentViewModel appointment);
    }
}
