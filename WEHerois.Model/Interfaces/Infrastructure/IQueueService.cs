using System.Threading.Tasks;

namespace WEHerois.Model.Interfaces.Infrastructure
{
    public interface IQueueService
    {
        Task SendAsync(string messageText);
    }
}
