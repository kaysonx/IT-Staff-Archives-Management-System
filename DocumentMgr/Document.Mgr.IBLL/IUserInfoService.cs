using Document.Mgr.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Document.Mgr.IBLL
{
    public partial interface IUserInfoService
    {
        int DeleteUsers(List<int> idList);
        int UpdateBasicUserInfo(UserInfo userInfo);
        int HandleSkill(int userId, SkillModel[] skillModels);
    }
}
