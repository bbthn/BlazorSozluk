using BlazorSozluk.Common;
using BlazorSozluk.Common.Events.User;
using BlazorSozluk.Common.Infrastructure;
using BlazorSozluk.Projections.UserService.Services;

namespace BlazorSozluk.Projections.UserService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IConfiguration configuration;

        public Worker(ILogger<Worker> logger, IConfiguration configuration)
        {
            _logger = logger;
            this.configuration = configuration;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var userService = new UserServices(configuration);

            QueueFactory.CreateBasicConsumer()
                .EnsureExchange(SozlukConstants.UserExchangeName)
                .EnsureQueue(SozlukConstants.UserEmailChangedQueueName, SozlukConstants.UserExchangeName)
                .Receive<UserEmailChangedEvent>(user =>
                {
                    userService.EmailChangedEvent(user).GetAwaiter().GetResult();
                    _logger.LogInformation($"Received old email:{user.OldEmailAddress}, new email:{user.NewEmailAddress}");
                }).StartConsuming(SozlukConstants.UserEmailChangedQueueName);
        }
    }
}