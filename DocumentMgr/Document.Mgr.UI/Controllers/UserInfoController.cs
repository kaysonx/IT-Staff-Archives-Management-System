using Document.Mgr.BLL;
using Document.Mgr.Common;
using Document.Mgr.IBLL;
using Document.Mgr.Model;
using Document.Mgr.UI.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Document.Mgr.UI.Controllers
{
    public class UserInfoController : BackAuthController
    {

        IUserInfoService UserInfoService = new UserInfoService();
        ICountryService CountryService = new CountryService();
        ICityService CityService = new CityService();
        IGroupService GroupService = new GroupService();
        IPositionService PositionService = new PositionService();
        IRoleService RoleService = new RoleService();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetAllUserInfos(int? page, String condition)
        {
            int pageSize = Request["rows"] == null ? 10 : int.Parse(Request["rows"]);
            int pageIndex = page == null ? 1 : page.Value;
            int total = 0;
            SearchUserModel su = null;
            if (condition != null)
            {
                 su = JsonConvert.DeserializeObject<SearchUserModel>(condition);
            }
            

            var data = UserInfoService.LoadPageEntities(pageSize, pageIndex, out total, u => u.IsDeleted == false, true, u => u.Id)
                                .Select(u => new { u.Id, u.UID, u.Name, u.Email, u.Gender, u.HiredTime, u.IsEnabled, roleName = u.Role.Name,positionName =  u.Position.Name,groupName =  u.Group.Name});
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

        public ActionResult DeleteUserView()
        {
            return View();
        }

        public ActionResult DeletedUser(int? page, String condition)
        {
            int pageSize = Request["rows"] == null ? 10 : int.Parse(Request["rows"]);
            int pageIndex = page == null ? 1 : page.Value;
            int total = 0;
            SearchUserModel su = null;
            if (condition != null)
            {
                su = JsonConvert.DeserializeObject<SearchUserModel>(condition);
            }


            var data = UserInfoService.LoadPageEntities(pageSize, pageIndex, out total, u => u.IsDeleted == true, true, u => u.Id)
                                .Select(u => new { u.Id, u.UID, u.Name, u.Email, u.Gender, u.HiredTime, u.IsEnabled, roleName = u.Role.Name, positionName = u.Position.Name, groupName = u.Group.Name });
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

        //恢复用户 仅管理员有权限
        public ActionResult Reverse(string uid)
        {
            if (string.IsNullOrEmpty(uid))
            {
                return Content("参数错误！");
            }
            int uidNum = 0;
            try
            {
                uidNum = Convert.ToInt32(uid);
            }
            catch (Exception)
            {
                 return Content("参数错误！");
            }

            if (CurrentLoginUser.IsAdmin == false || CurrentLoginUser.Auth != 3)
            {
                return Content("您没有足够的权限！");
            }

            var user = UserInfoService.LoadEntities(u => u.Id == uidNum).FirstOrDefault();
            if (user == null)
            {
                return Content("用户不存在了！");
            }
            user.IsDeleted = false;
            UserInfoService.Update(user);
            return Content("ok");
        }
        //禁用用户
        public ActionResult DisableUser(string uid)
        {
            if (string.IsNullOrEmpty(uid))
            {
                return Content("参数错误！");
            }
            int uidNum = 0;
            try
            {
                uidNum = Convert.ToInt32(uid);
            }
            catch (Exception)
            {
                return Content("参数错误！");
            }


            var user = UserInfoService.LoadEntities(u => u.Id == uidNum).FirstOrDefault();
            if (user == null)
            {
                return Content("用户不存在了！");
            }
            user.IsEnabled = false;
            UserInfoService.Update(user);
            return Content("ok");
        }
        //启用用户
        public ActionResult EnableUser(string uid)
        {
            if (string.IsNullOrEmpty(uid))
            {
                return Content("参数错误！");
            }
            int uidNum = 0;
            try
            {
                uidNum = Convert.ToInt32(uid);
            }
            catch (Exception)
            {
                return Content("参数错误！");
            }

        
            var user = UserInfoService.LoadEntities(u => u.Id == uidNum).FirstOrDefault();
            if (user == null)
            {
                return Content("用户不存在了！");
            }
            user.IsEnabled = true;
            UserInfoService.Update(user);
            return Content("ok");
        }

        public ActionResult Delete(string ids)
        {
            if (string.IsNullOrEmpty(ids))
            {
                return Content("删除失败：未选中任何数据！");
            }

            string[] allIds = ids.Split(',');
            List<int> idList = new List<int>();

            foreach (var id in allIds)
            {
                idList.Add(int.Parse(id));
            }

            UserInfoService.DeleteUsers(idList);

            return Content("ok");
        }

        public ActionResult Edit(int id)
        {
            var positions = PositionService.LoadEntities(p => p.Id != 0);
            var roles = RoleService.LoadEntities(r => r.Id != 0);
            var countries = CountryService.LoadEntities(c => c.Id != 0);
            var cities = CityService.LoadEntities(c => c.Id != 0);
            var groups = GroupService.LoadEntities(g => true);

            var currentUser = UserInfoService.LoadEntities(u => u.Id == id).FirstOrDefault();

            var selectedCity = CityService.LoadEntities(c => c.Id == currentUser.Group.CityId).FirstOrDefault();
            var selectedCountry = CountryService.LoadEntities(c => c.Id == selectedCity.CountryId).FirstOrDefault();


            ViewData["positions"] = new SelectList(positions, "Id", "Name", currentUser.Position.Id);
            ViewData["roles"] = new SelectList(roles, "Id", "Name", currentUser.Role.Id); 
            ViewData["countries"] = new SelectList(countries, "Id", "Name", selectedCountry.Id);
            ViewData["cities"] = new SelectList(cities, "Id", "Name", selectedCity.Id);
            ViewData["groups"] = new SelectList(groups, "Id", "Name", currentUser.Group.Id);

            ViewData.Model = UserInfoService.LoadEntities(u => u.Id == id).FirstOrDefault();
            return View();
        }

        [HttpPost]
        public ActionResult Edit(UserInfo userInfo)
        {

            //Group group = GroupService.LoadEntities(g => g.Id == userInfo.Group.Id).FirstOrDefault();
            //Position position = PositionService.LoadEntities(p => p.Id == userInfo.Position.Id).FirstOrDefault();
            //Role role = RoleService.LoadEntities(r => r.Id == userInfo.Role.Id).FirstOrDefault();

            //userInfo.Position = position;
            //userInfo.Role = role;
            //userInfo.Group = group;

            ////前方小坑...一定要把关联实体设成Added。否则ef会失败...
            //GroupService.SetState(group);
            //PositionService.SetState(position);
            //RoleService.SetState(role);
            var updateUser = UserInfoService.LoadEntities(u => u.Id == userInfo.Id).FirstOrDefault();
            updateUser.Email = userInfo.Email;
            updateUser.Gender = userInfo.Gender;
            updateUser.GroupId = userInfo.GroupId;
            updateUser.HiredTime = userInfo.HiredTime;
            updateUser.Name = userInfo.Name;
            updateUser.PositionId = userInfo.PositionId;
            updateUser.RoleId = userInfo.RoleId;


            UserInfoService.Update(updateUser);
            return Content("ok");
        }

        public ActionResult ResetPwd(int id)
        {
            var user = UserInfoService.LoadEntities(u => u.Id == id).FirstOrDefault();
            ViewData.Model = user;
            return View();
        }

        [HttpPost]
        public ActionResult ResetPwd(int id, string newPwd1, string newPwd2)
        {
            var user = UserInfoService.LoadEntities(u => u.Id == id).FirstOrDefault();

            if (newPwd1 != newPwd2)
            {
                return Content("两次输入密码不匹配!");
            }
            string md5Pwd = MD5Helper.GetMd5Str32(newPwd1);
            user.Pwd = md5Pwd;
            UserInfoService.Update(user);
            return Content("ok");
        }

    }
}