using GeoPet.DataContract.Model;

namespace GeoPet.Interfaces.Services;

public interface IJwtService
{
    string GenerateToken(Owner user);
}