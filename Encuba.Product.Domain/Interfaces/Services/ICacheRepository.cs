namespace Encuba.Product.Domain.Interfaces.Services;

public interface ICacheRepository
{
    Task AddItemAsync<T>(string key, T value); 
    Task<T?> GetItemAsync<T>(string key);
    Task DeleteItemAsync(string key); 
    Task<IEnumerable<string>> ListAllKeysAsync();
}