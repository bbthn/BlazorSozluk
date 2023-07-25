using BlazorSozluk.Projections.UserService;
using BlazorSozluk.Projections.UserService.Services;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHostedService<Worker>();
        services.AddTransient<UserServices>();
        services.AddTransient<EmailService>();
    })
    .Build();

await host.RunAsync();
