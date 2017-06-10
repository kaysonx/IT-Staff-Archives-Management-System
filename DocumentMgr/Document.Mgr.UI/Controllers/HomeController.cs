using Document.Mgr.BLL;
using Document.Mgr.Common;
using Document.Mgr.IBLL;
using Document.Mgr.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Document.Mgr.UI.Controllers
{
    public class HomeController : BackAuthController
    {
        IUserInfoService UserInfoService = new UserInfoService();
        ICountryService CountryService = new CountryService();
        ICityService CityService = new CityService();
        IGroupService GroupService = new GroupService();
        IPositionService PositionService = new PositionService();
        IRoleService RoleService = new RoleService();

        #region 添加
        public ActionResult Add()
        {
            var positions = PositionService.LoadEntities(p => p.Id != 0);
            var roles = RoleService.LoadEntities(r => r.Id != 0);
            var countries = CountryService.LoadEntities(c => c.Id != 0);



            ViewData["positions"] = new SelectList(positions, "Id", "Name");
            ViewData["roles"] = new SelectList(roles, "Id", "Name");
            ViewData["countries"] = new SelectList(countries, "Id", "Name");
            return View();
        }

        [HttpPost]
        public ActionResult Add(UserInfo userInfo, string team)
        {
            //初始化
            Contact contact = new Contact();
            contact.BlogUrl = "";
            contact.PersonalEmail = "";
            contact.PhoneNumber = "";
            contact.WX = "";
            userInfo.Contact = contact;
            userInfo.IsAdmin = false;
            userInfo.IsAssigned = false;
            userInfo.IsDeleted = false;
            userInfo.IsEnabled = true;

            int groupId = int.Parse(team);
            userInfo.GroupId = groupId;

            //默认密码为邮箱名。

            string pwd = userInfo.Email.Substring(0, userInfo.Email.LastIndexOf('@'));
            string md5Pwd = MD5Helper.GetMd5Str32(pwd);
            userInfo.Pwd = md5Pwd;

            //默认头像
            userInfo.Picture = "/Upload/default.jpg";

            //生成员工ID
            int userCount = UserInfoService.LoadEntities(u => u.Id != 0 && u.HiredTime.Year == DateTime.Now.Year).Count();
            userInfo.UID = DateTime.Now.Year + "" + DateTime.Now.Month + "" + userCount;



            var add = UserInfoService.Add(userInfo);
            if (add.Id != 0)
            {
                return Content("ok");
            }
            else
            {
                return Content("error");
            }

        }

        public ActionResult GetCityData(int id)
        {
            var cities = CityService.LoadEntities(c => c.CountryId == id)
                    .Select(c => new { c.Id, c.Name });
            return Json(cities, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetTeamData(int id)
        {
            var teams = GroupService.LoadEntities(g => g.CityId == id)
                    .Select(g => new { g.Id, g.Name });
            return Json(teams, JsonRequestBehavior.AllowGet);
        }


        #endregion



        public ActionResult Index()
        {
            ViewData.Model = CurrentLoginUser;
            return View();
        }

        public ActionResult Logout()
        {
            Session["LoginUser"] = null;
            CurrentLoginUser = null;
            return Content("ok");
        }
    }
}