using Project_CCSB.Models;

namespace Project_CCSB.Services
{
    public class CheckContractService
    {
        private readonly ApplicationDbContext _db;
        private string _test;

        public CheckContractService(ApplicationDbContext db)
        {
            _db = db;
            _test = "hallo";
        }

        public string GetTest()
        {
            return _test;
        }
    }
}
