using BlazorSozluk.Common.Events.Entry;
using BlazorSozluk.Common.Infrastructure;
using BlazorSozluk.Common;
using BlazorSozluk.Common.Events.EntryComment;

namespace BlazorSozluk.Projections.VoteService
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
            var voteService = new VoteService.Services.VoteService(configuration);

            QueueFactory.CreateBasicConsumer()
              .EnsureExchange(SozlukConstants.VoteExchangeName)
              .EnsureQueue(SozlukConstants.CreateEntryVoteQueueName, SozlukConstants.VoteExchangeName)
              .Receive<CreateEntryVoteEvent>(vote =>
              {
                  voteService.CreateEntryVote(vote).GetAwaiter().GetResult();
                  _logger.LogInformation($"Received EntryId: {vote.EntryId}, VoteType : {vote.VoteType}");

              })
              .StartConsuming(SozlukConstants.CreateEntryVoteQueueName);


            QueueFactory.CreateBasicConsumer()
           .EnsureExchange(SozlukConstants.VoteExchangeName)
           .EnsureQueue(SozlukConstants.DeleteEntryVoteQueueName, SozlukConstants.VoteExchangeName)
           .Receive<DeleteEntryVoteEvent>(vote =>
           {
               voteService.DeleteEntryVote(vote.EntryId,vote.CreatedBy).GetAwaiter().GetResult();
               _logger.LogInformation($"Received EntryCommentId: {vote.EntryId}");

           })
           .StartConsuming(SozlukConstants.CreateEntryCommentVoteQueueName);




          QueueFactory.CreateBasicConsumer()
         .EnsureExchange(SozlukConstants.VoteExchangeName)
         .EnsureQueue(SozlukConstants.CreateEntryCommentVoteQueueName, SozlukConstants.VoteExchangeName)
         .Receive<DeleteEntryCommentVoteEvent>(vote =>
         {
             voteService.DeleteEntryCommentVote(vote).GetAwaiter().GetResult();
             _logger.LogInformation($"Received EntryCommentId: {vote.EntryCommentId}");

         })
         .StartConsuming(SozlukConstants.DeleteEntryCommentVoteQueueName);



        QueueFactory.CreateBasicConsumer()
        .EnsureExchange(SozlukConstants.VoteExchangeName)
        .EnsureQueue(SozlukConstants.DeleteEntryVoteQueueName, SozlukConstants.VoteExchangeName)
        .Receive<DeleteEntryCommentVoteEvent>(vote =>
        {
            voteService.DeleteEntryCommentVote(vote).GetAwaiter().GetResult();
            _logger.LogInformation($"Received EntryCommentId: {vote.EntryCommentId}");

        })
        .StartConsuming(SozlukConstants.CreateEntryCommentVoteQueueName);
        }

    }
}