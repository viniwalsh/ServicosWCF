using System.IO;
using System.Threading.Tasks;

namespace WEHerois.Model.Interfaces.Infrastructure
{
    public interface IBlobService
    {
        Task<string> UploadAsync(Stream stream);
        Task DeleteAsync(string BlobName);
    }
}
