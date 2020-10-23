using PCLoan.Logic.Library.Models;

namespace PCLoan.Logic.Library.Services
{
    public interface IJsonWebTokenService
    {
        string GenerateJsonWebToken(UserModelDTO model);
    }
}