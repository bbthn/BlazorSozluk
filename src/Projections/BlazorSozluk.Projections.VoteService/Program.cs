using BlazorSozluk.Projections.VoteService;
using BlazorSozluk.Projections.VoteService.Services;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHostedService<Worker>();
    })
    .Build();

await host.RunAsync();
