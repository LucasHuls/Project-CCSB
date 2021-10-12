using Project_CCSB.Models;

namespace Project_CCSB.Services
{
    public interface IEmailSender
    {
        void SendEmail(Message message);
    }
}
