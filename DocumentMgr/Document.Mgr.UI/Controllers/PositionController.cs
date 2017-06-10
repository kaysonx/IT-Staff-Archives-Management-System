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
    public class PositionController : BackAuthController
    {

        IPositionService PositionService = new PositionService();
        IUserInfoService UserInfoService = new UserInfoService();

        public ActionResult Index()
        {
            var pos = PositionService.LoadEntities(t => t.Id != 0).ToList();
            ViewData.Model = pos;
            return View();
        }

        public ActionResult GetPos()
        {
            int pageSize = Request["rows"] == null ? 10 : int.Parse(Request["rows"]);
            int pageIndex = Request["page"] == null ? 1 : int.Parse(Request["page"]);
            int total = 0;

            var data = PositionService.LoadPageEntities(pageSize, pageIndex, out total, t => t.Id != 0, false, t => t.Id);
            var result = new { total = total, rows = data };

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(Position pos)
        {
            if (string.IsNullOrEmpty(pos.Name))
            {
                return Content("error! position name  is required!");
            }
            int count = PositionService.LoadEntities(t => t.Name.Equals(pos.Name)).Count();
            if (count > 0)
            {
                //已有此记录，不处理
                return Content("ok");
            }
            else
            {
                PositionService.Add(pos);
                return Content("ok");
            }
        }

        public ActionResult Edit(int id)
        {
            ViewData.Model = PositionService.LoadEntities(t => t.Id == id).FirstOrDefault();
            return View();
        }
        [HttpPost]
        public ActionResult Edit(Position pos)
        {
            if (pos != null && !string.IsNullOrEmpty(pos.Name))
            {

                PositionService.Update(pos);
                return Content("ok");

            }
            else
            {
                return Content("not a valid positionName!");
            }
        }

        public ActionResult Delete(int id)
        {
            //如果下面存在Tech则不能删除。
            int posCount = UserInfoService.LoadEntities(u => u.PositionId == id).Count();
            if (posCount > 0)
            {
                return Content("必须先删除下面的所有使用后方可删除!");
            }
            else
            {
                var entity = PositionService.LoadEntities(t => t.Id == id).FirstOrDefault();
                if (entity != null)
                {
                    PositionService.Delete(entity);
                }
                return Content("ok");
            }
        }
	}
}