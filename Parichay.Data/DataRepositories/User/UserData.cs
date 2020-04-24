using Parichay.Bussiness.Model;
using Parichay.Data.Common;
using Solution.Common;
using System;
using System.Linq;
using System.Transactions;
namespace Parichay.Data.DataRepositories.User
{
    [Serializable]
    public class UserData : BaseData<UserMst>, IUserData
    {
        public UserData(IContext context, ISPContext spContext) : base(context, spContext)
        {
            _context = context;
            _spContext = spContext;
            _dbset = context.Set<UserMst>();
        }
        public UserMst GetUserByName(string UserName)
        {
            try
            {
                using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted }))
                {
                    var users = _context.UserMst
                                           .Where(x => x.LoginID == UserName && x.IsActive==true && x.IsDeleted == false)
                                           .Select(x => x).ToList().FirstOrDefault();
                    scope.Complete();
                    return users;
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
            }
        }
        public bool CheckValidUser(string UserName,string PassWord)
        {
            try
            {
                using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted }))
                {
                    var users = _context.UserMst
                                           .Where(x => x.LoginID == UserName)
                                           .Select(x => x).ToList().FirstOrDefault();
                    if (users == null)
                    { 
                        scope.Complete();
                        return false;
                    }
                    // Verify Password Hash
                    string resultq = SecurePasswordHasher.Hash(PassWord);
                    bool result = SecurePasswordHasher.Verify(PassWord, users.Password);
                    if(result)
                    {
                        scope.Complete();
                        return true;
                    }
                    else
                    {
                        scope.Complete();
                        return false;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
           
        }
    }
}
