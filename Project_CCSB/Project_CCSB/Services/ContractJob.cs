using Microsoft.Extensions.DependencyInjection;
using Quartz;
using System;
using System.Threading.Tasks;

namespace Project_CCSB.Services
{
    [DisallowConcurrentExecution] // Prevents Quartz from trying to run the same job concurrently
    public class ContractJob : IJob
    {
        private readonly IServiceProvider _serviceProvider;
        public ContractJob(IServiceProvider serviceProvider)
        {
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
            }
            return Task.CompletedTask;
        }
    }
}
