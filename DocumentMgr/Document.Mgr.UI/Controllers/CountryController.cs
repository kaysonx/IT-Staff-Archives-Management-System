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
    public class CountryController : BackAuthController
    {
        ICountryService CountryService = new CountryService();
        ICityService CityService = new CityService();
    
        public ActionResult Index()
        {
            var countries = CountryService.LoadEntities(c => c.Id != 0).ToList();
            ViewData.Model = countries;
            return View();
        }


        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(Country country)
        {
            if (string.IsNullOrEmpty(country.Name))
            {
                return Content("error! countryName  is required!");
            }
            int count = CountryService.LoadEntities(c => c.Name.Equals(country.Name)).Count();
            if (count > 0)
            {
                //已有此记录，不处理
                return Content("ok");
            }
            else
            {
                CountryService.Add(country);
                return Content("ok");
            }
        }

        public ActionResult Edit(int id)
        {
            ViewData.Model = CountryService.LoadEntities(c => c.Id == id).FirstOrDefault();
            return View();
        }
        [HttpPost]
        public ActionResult Edit(Country country)
        {
            if (country != null && !string.IsNullOrEmpty(country.Name))
            {

                CountryService.Update(country);
                return Content("ok");

            }
            else
            {
                return Content("not a valid typeName!");
            }
        }

        public ActionResult Delete(int id)
        {
            //如果下面存在City则不能删除。
            int techCount = CityService.LoadEntities(c => c.CountryId == id).Count();
            if (techCount > 0)
            {
                return Content("必须先删除下面的所有City后方可删除!");
            }
            else
            {
                var entity = CountryService.LoadEntities(c => c.Id == id).FirstOrDefault();
                if (entity != null)
                {
                    CountryService.Delete(entity);
                }
                return Content("ok");
            }
        }
	}
}