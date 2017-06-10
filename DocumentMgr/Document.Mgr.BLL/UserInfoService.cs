using Document.Mgr.IBLL;
using Document.Mgr.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Document.Mgr.BLL
{
    public partial class UserInfoService : BaseService<UserInfo>, IUserInfoService
    {
        
        public int DeleteUsers(List<int> idList)
        {
            foreach (var id in idList)
            {
                var user = dbSession.UserInfoDal.LoadEntities(u => u.Id == id).FirstOrDefault();
                if (user != null)
                {
                    user.IsDeleted = true;
                    dbSession.UserInfoDal.Update(user);
                }
            }

            return dbSession.SaveChanges();
        }

        //更新部分字段
        public int UpdateBasicUserInfo(UserInfo userInfo)
        {
            dbSession.UserInfoDal.UpdateBasicUserInfo(userInfo);
            return dbSession.SaveChanges();
        }

        //处理Skill的update
        public int HandleSkill(int userId, SkillModel[] skillModels)
        {
            for (int i = 0; i < skillModels.Length; i++)
            {
                int techId = skillModels[i].TechId;
                int levelValue = skillModels[i].Level;
                var level = dbSession.LevelDescriptionDal.LoadEntities(l => l.Level ==  levelValue).FirstOrDefault();
               if (level == null)
	            {
		                level = dbSession.LevelDescriptionDal.LoadEntities(l => l.Level == 0).FirstOrDefault();
                        levelValue = 0;
	            }

               var relation = dbSession.R_UserInfo_TechDal.LoadEntities(r => r.UserInfoId == userId && r.TechId == techId).FirstOrDefault();
               if (relation == null)
               {
                   if (levelValue != 0)
                   {
                       //还未有此关系 新增一条记录
                       R_UserInfo_Tech r = new R_UserInfo_Tech()
                       {
                           UserInfoId = userId,
                           TechId = techId,
                           LevelDescriptionId = level.Id
                       };
                       dbSession.R_UserInfo_TechDal.Add(r);
                   }
             
               }
               else
               {
                   if (levelValue == 0)
                   {
                       //取消星级 删除记录
                       dbSession.R_UserInfo_TechDal.Delete(relation);
                   }
                   else
                   {
                       //只修改 
                      relation.LevelDescriptionId = level.Id;
                      dbSession.R_UserInfo_TechDal.Update(relation);
                   }
      
               }
            }
            return dbSession.SaveChanges();
        }


    }
}
