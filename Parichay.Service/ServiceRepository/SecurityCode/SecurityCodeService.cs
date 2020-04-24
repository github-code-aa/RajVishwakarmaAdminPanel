using Parichay.Bussiness.Model;
using Parichay.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parichay.Service
{
    public class SecurityCodeService : ISecurityCodeService
    {
        private ISecurityCodeData _ISecurityCodeData;
        public SecurityCodeService(ISecurityCodeData ISecurityCodeData)
        {
            _ISecurityCodeData = ISecurityCodeData;
        }

        public SecurePassword GetSinglepasskeyDetail(string purpose, string passkey)
        {
            return _ISecurityCodeData.GetSinglepasskeyDetail(purpose, passkey);
        }
    }
}
