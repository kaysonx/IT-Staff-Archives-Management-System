using Document.Mgr.BLL;
using Document.Mgr.IBLL;
using Document.Mgr.UI.Models;
using Document.Mgr.UI.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Document.Mgr.UI.Controllers
{
    public class ContactController : BaseController
    {
        IUserInfoService UserInfoService = new UserInfoService();
        ICityService CityService = new CityService();
        IContactService ContactService = new ContactService();
        public ActionResult Index()
        {
            string country = "暂无";
            var city = CityService.LoadEntities(c => c.Id == CurrentLoginUser.Group.CityId).FirstOrDefault();
            if (city != null)
            {
                country = city.Country.Name;
            }
            var contact = new  ContactModel(){Name = CurrentLoginUser.Name, Position = CurrentLoginUser.Position.Name, UID = CurrentLoginUser.UID, Team = CurrentLoginUser.Group.Name, WorkEmail = CurrentLoginUser.Email, PersonalEmail = CurrentLoginUser.Contact.PersonalEmail ?? "暂无", PhoneNumber = CurrentLoginUser.Contact.PhoneNumber ?? "暂无", WX = CurrentLoginUser.Contact.WX ?? "暂无", Blog = CurrentLoginUser.Contact.BlogUrl ?? "暂无", City = CurrentLoginUser.Group.City.Name, Country = country };
            ViewData.Model = contact;
            return View();
        }

        [HttpPost]
        public ActionResult UpdateContact(ContactModel cm)
        {
            if (cm != null)
            {
                var user = UserInfoService.LoadEntities(u => u.Id == CurrentLoginUser.Id).FirstOrDefault();
                user.Email = cm.WorkEmail;
                user.Contact.BlogUrl = cm.Blog;
                user.Contact.PersonalEmail = cm.PersonalEmail;
                user.Contact.PhoneNumber = cm.PhoneNumber;
                user.Contact.WX = cm.WX;
                UserInfoService.Update(user);
                return Content("ok");
            }
            return Content("输入信息有误!");
        }

        //查看别人的通讯录
        public ActionResult GetContact(int uid)
        {
            var user = UserInfoService.LoadEntities(u => u.Id == uid).FirstOrDefault();
            if (user == null)
            {
                return Content("error!invalid user id");
            }
            string country = "暂无";
            var city = CityService.LoadEntities(c => c.Id == user.Group.CityId).FirstOrDefault();
            if (city != null)
            {
                country = city.Country.Name;
            }
            var contact = new ContactModel() { Name = user.Name, Position = user.Position.Name, UID = user.UID, Team = user.Group.Name, WorkEmail = user.Email, PersonalEmail = user.Contact.PersonalEmail ?? "暂无", PhoneNumber = user.Contact.PhoneNumber ?? "暂无", WX = user.Contact.WX ?? "暂无", Blog = user.Contact.BlogUrl ?? "暂无", City = user.Group.City.Name, Country = country };
            ViewData.Model = contact;
            return View();
        }

        public ActionResult CreateIndex()
        {
            SearchHelper.CreateContactIndex();
            return Content("ok");
        }

        [HttpPost]
        public ActionResult Search(string keyword)
        {
            if (string.IsNullOrEmpty(keyword))
            {
                return Content("keyword 不能为空！");
            }

            int total = 0;
            var searchResult = SearchHelper.SeartchContact(keyword, 0, 10, out total);
            if (searchResult.Count <= 0)
            {
                //索引中无数据  去数据库中查找
                var users = UserInfoService.LoadEntities(u => u.Name.Contains(keyword) || u.Email.Contains(keyword) || u.Position.Name.Contains(keyword) || u.Contact.PhoneNumber.Contains(keyword));
                searchResult = users.Select(u => new SearchResult { UserId = u.Id.ToString(), Name = u.Name, Email = u.Email, PhoneNumber = u.Contact.PhoneNumber, Position = u.Position.Name }).ToList();
            }
            return Json(searchResult, JsonRequestBehavior.AllowGet);
        }
	}
}