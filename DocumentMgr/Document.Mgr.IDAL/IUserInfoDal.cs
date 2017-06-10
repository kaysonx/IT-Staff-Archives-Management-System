using Document.Mgr.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Document.Mgr.IDAL
{
    public partial interface IUserInfoDal 
    {
        bool UpdateBasicUserInfo(UserInfo userInfo);
    }
}
