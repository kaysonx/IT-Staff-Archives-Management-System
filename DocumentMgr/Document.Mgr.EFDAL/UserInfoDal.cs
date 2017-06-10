using Document.Mgr.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Document.Mgr.EFDAL
{
    public partial class UserInfoDal : BaseDal<UserInfo>, IDAL.IUserInfoDal
    {
        public bool UpdateBasicUserInfo(UserInfo userInfo)
        {
            db.Entry(userInfo).State = EntityState.Modified;
            
            //db.Entry(userInfo).Reference(u => u.Attachment).EntityEntry = false;
            //db.Entry(userInfo).Reference(u => u.Contact).IsModified = false;
            db.Entry(userInfo).Property(u => u.IsAdmin).IsModified = false;
            db.Entry(userInfo).Property(u => u.IsAssigned).IsModified = false;
            db.Entry(userInfo).Property(u => u.IsDeleted).IsModified = false;
            db.Entry(userInfo).Property(u => u.IsEnabled).IsModified = false;
            db.Entry(userInfo).Property(u => u.Picture).IsModified = false;
            //db.Entry(userInfo).Property(u => u.ProjectExp).IsModified = false;
            db.Entry(userInfo).Property(u => u.Pwd).IsModified = false;
            db.Entry(userInfo).Property(u => u.Remark).IsModified = false;
            db.Entry(userInfo).Property(u => u.UID).IsModified = false;
            
            return true;
        }
    } 
}
