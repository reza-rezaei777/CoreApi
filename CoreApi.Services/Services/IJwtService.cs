using CoreApi.Entities;

namespace CoreApi.Services.Services
{
    public interface IJwtService
    {
        Task<string> GenerateAsync(User user);
    }
}