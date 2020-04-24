using Parichay.Bussiness.Model;
using Parichay.Data.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parichay.Data
{
    public interface IUserData : IBaseData<UserMst>
    {
        UserMst GetUserByName(string UserName);
        bool CheckValidUser(string UserName, string PassWord);
    }
}
