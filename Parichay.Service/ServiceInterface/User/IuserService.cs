using Parichay.Bussiness.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parichay.Service
{
    public interface IUserService
    {
        UserMst GetUserByName(string UserName);
        bool CheckValidUser(string UserName, string PassWord);
    }
}
