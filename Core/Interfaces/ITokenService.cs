using Core.Entities;

namespace Core.Interfaces;

public interface ITokenService
{
    string CreateUserToken(AppUser userData);
}
