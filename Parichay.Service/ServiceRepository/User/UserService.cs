using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Parichay.Bussiness.Model;
using Parichay.Data;

namespace Parichay.Service
{
    public class UserService : IUserService
    {
        private IUserData _IUserData;
        public UserService(IUserData UserData)
        {
            _IUserData = UserData;
        }
        public UserMst GetUserByName(string UserName)
        {
            return _IUserData.GetUserByName(UserName);
        }
        public bool CheckValidUser(string UserName,string PassWord)
        {
            return _IUserData.CheckValidUser(UserName, PassWord);
        }
    }
}
