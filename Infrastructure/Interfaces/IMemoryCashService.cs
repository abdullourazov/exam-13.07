namespace Infrastructure.Interfaces;

public interface IMemoryCashService
{
    Task SetData<T>(string key, T data, int expirationMinutes);
    Task<T?> GetData<T>(string key);
    Task DeleteData(string key);
}
