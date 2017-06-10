using Document.Mgr.BLL;
using Document.Mgr.IBLL;
using Document.Mgr.UI.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Document.Mgr.UI.Controllers
{
    //权限设置
    public class AuthController : BaseController
    {
        IUserInfoService UserInfoService = new UserInfoService();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AuthUser(string uid, string authType)
        {
            if(string.IsNullOrEmpty(uid) || string.IsNullOrEmpty(authType)){
                return Content("invalid operation！");
            }
            int uidNum = 0;
            int authTypeNum = 0;

            try 
	        {	        
		        uidNum = Convert.ToInt32(uid);
                authTypeNum = Convert.ToInt32(authType);
	        }
	        catch (Exception)
	        {
		        return Content("invalid arguments !");
	        }
            //权限分级：1-表示搜索权限，2-表示后台权限，3-表示管理员权限（提权上来的 没有资格动其他的管理员）
            var user = UserInfoService.LoadEntities(u => u.Id == uidNum).FirstOrDefault();
            user.Auth = (short)authTypeNum;
            UserInfoService.Update(user);
            return Content("ok");
        }

        public ActionResult UnAuthUser(string uid, string authType)
        {
            if (string.IsNullOrEmpty(uid) || string.IsNullOrEmpty(authType))
            {
                return Content("invalid operation！");
            }
            int uidNum = 0;
            int authTypeNum = 0;

            try
            {
                uidNum = Convert.ToInt32(uid);
                authTypeNum = Convert.ToInt32(authType);
            }
            catch (Exception)
            {
                return Content("invalid arguments !");
            }
            //权限分级：1-表示搜索权限，2-表示后台权限，3-表示管理员权限（提权上来的 没有资格动其他的管理员）
            var user = UserInfoService.LoadEntities(u => u.Id == uidNum).FirstOrDefault();
            if (authTypeNum == 3)
            {
                //此时应判断是否有权限取消管理员权限
                if (CurrentLoginUser.IsAdmin == false)
                {
                    return Content("你没有足够的权限 !");
                }
            }
            user.Auth = 0;
            UserInfoService.Update(user);
            return Content("ok");
        }

        public ActionResult SearchAuth()
        {
            return View();
        }
        public ActionResult BackAuth()
        {
            return View();
        }
        public ActionResult AdminList()
        {
            return View();
        }



        public ActionResult GetUnauthUserInfos(int? page, String condition)
        {
            int pageSize = Request["rows"] == null ? 10 : int.Parse(Request["rows"]);
            int pageIndex = page == null ? 1 : page.Value;
            int total = 0;
            SearchUserModel su = null;
            if (condition != null)
            {
                su = JsonConvert.DeserializeObject<SearchUserModel>(condition);
            }


            var data = UserInfoService.LoadPageEntities(pageSize, pageIndex, out total, u => u.IsDeleted == false && u.Auth == 0 && u.IsAdmin == false, true, u => u.Id)
                              .Select(u => new { u.Id, u.UID, u.Name, u.Email, u.HiredTime, u.IsEnabled, roleName = u.Role.Name, positionName = u.Position.Name, groupName = u.Group.Name });
            if (su != null)
            {
                if (su.startTime != null)
                {
                    data = data.Where(u => u.HiredTime >= su.startTime.Value);
                }
                if (su.endTime != null)
                {
                    data = data.Where(u => u.HiredTime <= su.endTime.Value);
                }
                if (su.searchInfo != null)
                {
                    data = data.Where(u => u.Name.Contains(su.searchInfo) || u.Email.Contains(su.searchInfo));
                }
            }
            var result = new { total = Math.Ceiling(Convert.ToDecimal(total / pageSize)), rows = data };
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetAdminList(int? page, String condition)
        {
            int pageSize = Request["rows"] == null ? 10 : int.Parse(Request["rows"]);
            int pageIndex = page == null ? 1 : page.Value;
            int total = 0;
            SearchUserModel su = null;
            if (condition != null)
            {
                su = JsonConvert.DeserializeObject<SearchUserModel>(condition);
            }


            var data = UserInfoService.LoadPageEntities(pageSize, pageIndex, out total, u => u.IsDeleted == false && (u.Auth == 3 || u.IsAdmin == true), true, u => u.Id)
                                 .Select(u => new { u.Id, u.UID, u.Name, u.Email, u.HiredTime, u.IsEnabled, roleName = u.Role.Name, positionName = u.Position.Name, groupName = u.Group.Name });
            if (su != null)
            {
                if (su.startTime != null)
                {
                    data = data.Where(u => u.HiredTime >= su.startTime.Value);
                }
                if (su.endTime != null)
                {
                    data = data.Where(u => u.HiredTime <= su.endTime.Value);
                }
                if (su.searchInfo != null)
                {
                    data = data.Where(u => u.Name.Contains(su.searchInfo) || u.Email.Contains(su.searchInfo));
                }
            }
            var result = new { total = Math.Ceiling(Convert.ToDecimal(total / pageSize)), rows = data };
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetAuthBackUserInfos(int? page, String condition)
        {
            int pageSize = Request["rows"] == null ? 10 : int.Parse(Request["rows"]);
            int pageIndex = page == null ? 1 : page.Value;
            int total = 0;
            SearchUserModel su = null;
            if (condition != null)
            {
                su = JsonConvert.DeserializeObject<SearchUserModel>(condition);
            }


            var data = UserInfoService.LoadPageEntities(pageSize, pageIndex, out total, u => u.IsDeleted == false && u.Auth == 2 && u.IsAdmin == false, true, u => u.Id)
                               .Select(u => new { u.Id, u.UID, u.Name, u.Email, u.HiredTime, u.IsEnabled, roleName = u.Role.Name, positionName = u.Position.Name, groupName = u.Group.Name });
            if (su != null)
            {
                if (su.startTime != null)
                {
                    data = data.Where(u => u.HiredTime >= su.startTime.Value);
                }
                if (su.endTime != null)
                {
                    data = data.Where(u => u.HiredTime <= su.endTime.Value);
                }
                if (su.searchInfo != null)
                {
                    data = data.Where(u => u.Name.Contains(su.searchInfo) || u.Email.Contains(su.searchInfo));
                }
            }
            var result = new { total = Math.Ceiling(Convert.ToDecimal(total / pageSize)), rows = data };
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetAuthSearchUserInfos(int? page, String condition)
        {
            int pageSize = Request["rows"] == null ? 10 : int.Parse(Request["rows"]);
            int pageIndex = page == null ? 1 : page.Value;
            int total = 0;
            SearchUserModel su = null;
            if (condition != null)
            {
                su = JsonConvert.DeserializeObject<SearchUserModel>(condition);
            }

            var data = UserInfoService.LoadPageEntities(pageSize, pageIndex, out total, u => u.IsDeleted == false && u.Auth == 1 && u.IsAdmin == false, true, u => u.Id)
                                         .Select(u => new { u.Id, u.UID, u.Name, u.Email, u.HiredTime, u.IsEnabled, roleName = u.Role.Name, positionName = u.Position.Name, groupName = u.Group.Name });
            if (su != null)
            {
                if (su.startTime != null)
                {
                    data = data.Where(u => u.HiredTime >= su.startTime.Value);
                }
                if (su.endTime != null)
                {
                    data = data.Where(u => u.HiredTime <= su.endTime.Value);
                }
                if (su.searchInfo != null)
                {
                    data = data.Where(u => u.Name.Contains(su.searchInfo) || u.Email.Contains(su.searchInfo));
                }
            }
            var result = new { total = Math.Ceiling(Convert.ToDecimal(total / pageSize)), rows = data };
            return Json(result, JsonRequestBehavior.AllowGet);
        }


	}
}