using Parichay.Bussiness.Model;
using Parichay.Data.Common;
using System;
using System.Linq;

namespace Parichay.Data
{
    [Serializable]
    public class SecurityCodeData : BaseData<SecurePassword>, ISecurityCodeData
    {
        public SecurityCodeData(IContext context, ISPContext spContext) : base(context, spContext)
        {
            _context = context;
            _spContext = spContext;
            _dbset = context.Set<SecurePassword>();
        }

        public SecurePassword GetSinglepasskeyDetail(string purpose, string passkey)
        {

            return _context.SecurePassword
               .Where(x => x.IsActive == true & x.IsDeleted == false & x.Purpose == purpose & x.Password == passkey).SingleOrDefault();

            //.Where(x => x.IsActive == true & x.IsDeleted == false & x.Id == id).SingleOrDefault();
        }

    }
}
