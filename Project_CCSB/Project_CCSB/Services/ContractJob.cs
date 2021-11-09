using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Quartz;
using System;
using System.Threading.Tasks;

namespace Project_CCSB.Services
{
    [DisallowConcurrentExecution] // Prevents Quartz from trying to run the same job concurrently
    public class ContractJob : IJob
    {
        private readonly ILogger<ContractJob> _logger;
        private readonly IServiceProvider _serviceProvider;
        public ContractJob(ILogger<ContractJob> logger, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
        }

        public Task Execute(IJobExecutionContext context)
        {
            // Creating new scope so Dependency injection can be used
            using (var scope = _serviceProvider.CreateScope())
            {
                // Resolve the Scoped service
                var contractService = scope.ServiceProvider.GetService<IContractService>();
                contractService.RenewContracts();
                _logger.LogInformation("Hello world!");

            }
            return Task.CompletedTask;
        }
    }
}
