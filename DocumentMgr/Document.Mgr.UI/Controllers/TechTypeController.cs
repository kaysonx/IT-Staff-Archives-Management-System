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
    public class TechTypeController : BackAuthController
    {

        ITechService TechService = new TechService();
        ITechTypeService TechTypeService = new TechTypeService();


        public ActionResult Index()
        {
            var types = TechTypeService.LoadEntities(t => t.Id != 0).ToList();
            ViewData.Model = types;
            return View();
        }

        public ActionResult GetAllTechTypes()
        {
            int pageSize = Request["rows"] == null ? 10 : int.Parse(Request["rows"]);
            int pageIndex = Request["page"] == null ? 1 : int.Parse(Request["page"]);
            int total = 0;

            var data = TechTypeService.LoadPageEntities(pageSize, pageIndex, out total, t => t.Id != 0, false, t => t.Id);
            var result = new { total = total, rows = data };

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AddTechType()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddTechType(TechType type)
        {
            if (string.IsNullOrEmpty(type.TypeName))
            {
                return Content("error! typename  is required!");
            }
            int count = TechTypeService.LoadEntities(t => t.TypeName.Equals(type.TypeName)).Count();
            if (count > 0)
            {
                //已有此记录，不处理
                return Content("ok");
            }
            else
            {
                TechTypeService.Add(type);
                return Content("ok");
            }
        }

        public ActionResult EditTechType(int id)
        {
            ViewData.Model = TechTypeService.LoadEntities(t => t.Id == id).FirstOrDefault();
            return View();
        }
        [HttpPost]
        public ActionResult EditTechType(TechType type)
        {
            if (type != null && !string.IsNullOrEmpty(type.TypeName))
            {

                TechTypeService.Update(type);
                return Content("ok");

            }
            else
            {
                return Content("not a valid typeName!");
            }
        }

        public ActionResult Delete(int id)
        {
            //如果下面存在Tech则不能删除。
            int techCount = TechService.LoadEntities(t => t.TechTypeId == id).Count();
            if (techCount > 0)
            {
                return Content("必须先删除下面的所有Tech后方可删除!");
            }
            else
            {
                var entity = TechTypeService.LoadEntities(t => t.Id == id).FirstOrDefault();
                if (entity != null)
                {
                    TechTypeService.Delete(entity);
                }
                return Content("ok");
            }
        }

    }
}