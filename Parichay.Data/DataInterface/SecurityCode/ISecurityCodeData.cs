using Parichay.Bussiness.Model;
using Parichay.Data.Common;

namespace Parichay.Data
{
    public interface ISecurityCodeData : IBaseData<SecurePassword>
    {
        SecurePassword GetSinglepasskeyDetail(string purpose,string passkey);
    }
}
