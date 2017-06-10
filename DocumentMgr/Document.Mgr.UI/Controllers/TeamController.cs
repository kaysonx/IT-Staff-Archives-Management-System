using Document.Mgr.BLL;
using Document.Mgr.IBLL;
using Document.Mgr.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Document.Mgr.UI.Controllers
{

    public class TeamController : BackAuthController
    {

        IGroupService GroupService = new GroupService();
        ICityService CityService = new CityService();
        ICountryService CountryService = new CountryService();
        IUserInfoService UserInfoService = new UserInfoService();

        public ActionResult Index()
         {
             var groups = GroupService.LoadEntities(t => t.Id != 0).ToList();
             ViewData.Model = groups;

             var countries = CountryService.LoadEntities(t => t.Id != 0);
             var firstCountry = CountryService.LoadEntities(c => c.Id != 0).OrderBy(t => t.Id).FirstOrDefault();
             ViewData["countries"] = new SelectList(countries, "Id", "Name",firstCountry.Id);
             var cities = CityService.LoadEntities(t => t.CountryId == firstCountry.Id);
             ViewData["cities"] = new SelectList(cities, "Id", "Name");


             return View();
         }

        //通过country拿city
        public ActionResult GetCitiesByCountry(int countryId)
        {
            var data = CityService.LoadEntities(t => t.CountryId == countryId);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        //通过city拿team
        public ActionResult GetTeamsByCity(int cityId)
        {
            var data = GroupService.LoadEntities(t => t.CityId == cityId);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        //团队展示
        //public ActionResult ShowTeam(string teamName)
        //{
        //    var teamUsers = UserInfoService.LoadEntities(u => u.Group.Name == teamName);
        //    ViewData.Model = teamUsers;
        //    ViewData["teamName"] = teamName;
        //    return View();
        //}

        [HttpPost]
        public ActionResult Add(string teamName, int city, string description)
        {
            if (string.IsNullOrEmpty(teamName))
            {
                return Content("error, invalid Name!");
            }
            Group group = new Group()
            {
                CityId = city,
                Name = teamName,
            };
            if (string.IsNullOrEmpty(description))
            {
                group.Description = "";
            }
            else
            {
                group.Description = description;
            }
            GroupService.Add(group);
            return Content("ok");
        }


        public ActionResult Edit(int id)
        {            
            var team =  GroupService.LoadEntities(t => t.Id == id).FirstOrDefault();
            var cities = CityService.LoadEntities(t => t.CountryId == team.City.CountryId);
            var countries = CountryService.LoadEntities(t => t.Id != 0);

            ViewData["countries"] = new SelectList(countries, "Id", "Name", team.City.CountryId);
            ViewData["cities"] = new SelectList(cities, "Id", "Name", team.CityId);

            ViewData.Model = team;
            return View();
        }

        [HttpPost]
        public ActionResult Edit(Group group)
        {
            if (group != null && !string.IsNullOrEmpty(group.Name))
            {
                if (!string.IsNullOrEmpty(group.Description))
                {
                    group.Description = group.Description.Trim();
                }
                GroupService.Update(group);
                return Content("ok");

            }
            else
            {
                return Content("not a valid Name!");
            }
        }

        public ActionResult Delete(int id)
        {
            //如果已有人存在此Group则不能删除。
            int count = UserInfoService.LoadEntities(u => u.GroupId == id).Count();
            if (count > 0)
            {
                return Content("必须删除所有此Group的引用后方可删除!");
            }
            else
            {
                var entity = GroupService.LoadEntities(t => t.Id == id).FirstOrDefault();
                if (entity != null)
                {
                    GroupService.Delete(entity);
                }
                return Content("ok");
            }
        }
    }
}