using CoreApi.Entities;

namespace CoreApi.Services.Services
{
    public interface IJwtService
    {
        string Generate(User user);
    }
}