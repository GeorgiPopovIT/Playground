using Quartz;
using Quartz.AspNetCore;

namespace UrlShortener;

public static class QuartzJobsInfrastructure
{
    public static void AddInfrastructure(this IServiceCollection services)
    {
        services.AddQuartz(q =>
        {

        });

        services.AddQuartzServer(options =>
        {
            options.WaitForJobsToComplete = true;
        });
    }
}
