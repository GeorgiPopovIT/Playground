using AngleSharp;
using Quartz;

namespace UrlShortener;

public class GetNewsJob : IJob
{
    private const int id = 2;
    public async Task Execute(IJobExecutionContext jobContext)
    {
        var config = Configuration.Default.WithDefaultLoader();

        var context = BrowsingContext.New(config);


        var document = await context.OpenAsync("https://sportal.bg/news-{id}");


    }

}
