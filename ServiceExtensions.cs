using MVCJob.Job;
using Quartz;

namespace MVCJob
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddJobHostedServices(this IServiceCollection services, IConfiguration configuration)
        {
            
            return services;
        }
    }
}
