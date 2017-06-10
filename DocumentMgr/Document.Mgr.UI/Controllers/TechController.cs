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
    public class TechController : BackAuthController
    {
        ITechService TechService = new TechService();
        ITechTypeService TechTypeService = new TechTypeService();
        IR_UserInfo_TechService R_UserInfo_TechService = new R_UserInfo_TechService(); 

        public ActionResult Index()
        {
            var types = TechService.LoadEntities(t => t.Id != 0).ToList();
            ViewData.Model = types;

            var techTypes = TechTypeService.LoadEntities(t => t.Id != 0);
            ViewData["techTypes"] = new SelectList(techTypes, "Id", "TypeName");
            return View();
        }

        //通过类型获取Tech
        public ActionResult GetTechsByTechType(int typeId)
        {
            var data = TechService.LoadEntities(t => t.TechTypeId == typeId).Select(t => new {t.Id, t.TechName});
            return Json(data, JsonRequestBehavior.AllowGet);
        }

         [HttpPost]
        public ActionResult AddTech(string TechName, int typeId)
        {
            if (string.IsNullOrEmpty(TechName))
            {
                return Content("TechName不能为空!");
            }
            if (TechService.LoadEntities(t => t.TechName == TechName).Count() > 0)
            {
                return Content("该Tech已经存在!");
            }
            Tech tech = new Tech()
            {
                TechName = TechName,
                TechTypeId = typeId
            };
            TechService.Add(tech);
            return Content("ok");
        }


         public ActionResult Edit(int id)
         {
             var tech = TechService.LoadEntities(t => t.Id == id).FirstOrDefault();
             var techTypes = TechTypeService.LoadEntities(t => t.Id != 0);

             ViewData["techTypes"] = new SelectList(techTypes, "Id", "TypeName", tech.TechTypeId);
             ViewData.Model = tech;
             return View();
         }

        [HttpPost]
         public ActionResult Edit(Tech tech)
         {
             if (tech != null && !string.IsNullOrEmpty(tech.TechName))
             {

                 TechService.Update(tech);
                 return Content("ok");

             }
             else
             {
                 return Content("TechName非法!");
             }
         }

         public ActionResult Delete(int id)
         {
             //如果已有人使用此Tech则不能删除。
             int techCount = R_UserInfo_TechService.LoadEntities(r => r.TechId == id).Count();
             if (techCount > 0)
             {
                 return Content("必须删除所有此Tech的引用后方可删除!");
             }
             else
             {
                 var entity = TechService.LoadEntities(t => t.Id == id).FirstOrDefault();
                 if (entity != null)
                 {
                     TechService.Delete(entity);
                 }
                 return Content("ok");
             }
         }

	}
}