using Project_CCSB.Models;

namespace Project_CCSB.Services
{
    public interface IEmailSender
    {
        /// <summary>
        /// Sends email with message
        /// </summary>
        /// <param name="message"></param>
        public void SendEmail(Message message);
    }
}
