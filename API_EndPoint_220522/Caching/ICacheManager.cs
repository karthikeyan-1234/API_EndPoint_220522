using System.Threading.Tasks;

namespace API_EndPoint_220522.Caching
{
    public interface ICacheManager
    {
        Task<T> TryGetAsync<T>(string key);
        Task<bool> TrySetAsync<T>(string key, T value);
    }
}