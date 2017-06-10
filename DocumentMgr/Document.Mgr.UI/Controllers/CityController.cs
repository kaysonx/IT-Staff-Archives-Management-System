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
    public class CityController : BackAuthController
    {

        ICityService CityService = new CityService();
        ICountryService CountryService = new CountryService();
        IGroupService GroupService = new GroupService();

        public ActionResult Index()
        {
            var citys = CityService.LoadEntities(c => c.Id != 0).ToList();
            ViewData.Model = citys;

            var countries = CountryService.LoadEntities(c => c.Id != 0);
            ViewData["countries"] = new SelectList(countries, "Id", "Name");
            return View();
        }

        //通过Country获取City
        public ActionResult GetCitiesByCountry(int countryId)
        {
            var data = CityService.LoadEntities(c => c.CountryId == countryId);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Add(string Name, int countryId)
        {
            if (string.IsNullOrEmpty(Name))
            {
                return Content("error, invalid Name!");
            }
            City city = new City()
            {
                Name = Name,
                CountryId = countryId
            };
            CityService.Add(city);
            return Content("ok");
        }


        public ActionResult Edit(int id)
        {
           var city = CityService.LoadEntities(c => c.Id == id).FirstOrDefault();
            var countries = CountryService.LoadEntities(c => c.Id != 0);

            ViewData["countries"] = new SelectList(countries, "Id", "Name", city.CountryId);
            ViewData.Model = city;
            return View();
        }

        [HttpPost]
        public ActionResult Edit(City city)
        {
            if (city != null && !string.IsNullOrEmpty(city.Name))
            {

                CityService.Update(city);
                return Content("ok");

            }
            else
            {
                return Content("not a valid Name!");
            }
        }

        public ActionResult Delete(int id)
        {
            //如果City下有Team则不能删除。
            int techCount = GroupService.LoadEntities(g => g.CityId == id).Count();
            if (techCount > 0)
            {
                return Content("必须删除所有City下的Team后方可删除!");
            }
            else
            {
                var entity = CityService.LoadEntities(t => t.Id == id).FirstOrDefault();
                if (entity != null)
                {
                    CityService.Delete(entity);
                }
                return Content("ok");
            }
        }
	}
}