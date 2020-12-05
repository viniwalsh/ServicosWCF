using System.Threading.Tasks;
using WEHerois.Model.Interfaces.Infrastructure;
using Azure.Storage.Queues;

namespace WEHerois.Infrastructure.Services.Queue
{
    public class QueueService : IQueueService
    {
        private readonly QueueServiceClient _queueServiceClient;
        private const string _queueName = "function-update-date-queue";

        public QueueService(string storageAccount)
        {
            _queueServiceClient = new QueueServiceClient(storageAccount);
        }

        public async Task SendAsync(string messageText)
        {
            var queueClient = _queueServiceClient.GetQueueClient(_queueName);
            await queueClient.CreateIfNotExistsAsync();
            await queueClient.SendMessageAsync(messageText);
        }
    }
}