using Parichay.Bussiness.Model;

namespace Parichay.Service
{
    public interface ISecurityCodeService
    {
        SecurePassword GetSinglepasskeyDetail(string purpose, string passkey);

    }
}
