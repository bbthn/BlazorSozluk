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
        private readonly UserService.Services.EmailService emailService;
        private readonly UserService.Services.UserServices userService;

        public Worker(ILogger<Worker> logger, IConfiguration configuration, EmailService emailService, UserServices userService)
        {
            _logger = logger;
            this.configuration = configuration;
            this.emailService = emailService;
            this.userService = userService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {

            QueueFactory.CreateBasicConsumer()
                .EnsureExchange(SozlukConstants.UserExchangeName)
                .EnsureQueue(SozlukConstants.UserEmailChangedQueueName, SozlukConstants.UserExchangeName)
                .Receive<UserEmailChangedEvent>(async user =>
                {
                    //DB INSERT 
                    var confirmationId =  await userService.CreateEmailConfirmation(user);

                    // Generate confirmation Link 
                    var link= emailService.GenerateConfirmationLink(confirmationId);

                    _logger.LogInformation($"Received old email:{user.OldEmailAddress}, new email:{user.NewEmailAddress}");

                    emailService.SendEmail(link).GetAwaiter().GetResult();

                }).StartConsuming(SozlukConstants.UserEmailChangedQueueName);
        }
    }
}