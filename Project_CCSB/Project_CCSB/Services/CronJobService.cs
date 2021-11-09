using Microsoft.Extensions.Hosting;
using NCrontab;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Project_CCSB.Services
{
    public class CronJobService : BackgroundService
    {
        private CrontabSchedule _schedule;
        private DateTime _nextRun;
        private readonly CheckContractService _checkContractService;

        private string Schedule => "*/10 * * * * *"; //Runs every 10 seconds
        // 0 0 31 12 * -> CRON for 31 december

        public CronJobService(CheckContractService checkContractService)
        {
            _schedule = CrontabSchedule.Parse(Schedule, new CrontabSchedule.ParseOptions { IncludingSeconds = true });
            _nextRun = _schedule.GetNextOccurrence(DateTime.Now);
            _checkContractService = checkContractService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            do
            {
                var now = DateTime.Now;
                var nextrun = _schedule.GetNextOccurrence(now);
                if (now > _nextRun)
                {
                    Process();
                    _nextRun = _schedule.GetNextOccurrence(DateTime.Now);
                }
                await Task.Delay(5000, stoppingToken); //5 seconds delay
            }
            while (!stoppingToken.IsCancellationRequested);
        }

        private void Process()
        {
            Console.WriteLine("Process");
            Console.WriteLine(_checkContractService.GetTest());
        }
    }
}
