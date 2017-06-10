using Document.Mgr.BLL;
using Document.Mgr.IBLL;
using Document.Mgr.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.ApplicationServices;
using System.Web.Mvc;

namespace Document.Mgr.UI.Controllers
{
    public class RoleController : BackAuthController
    {
        IRoleService RoleService = new BLL.RoleService();
        IUserInfoService UserInfoService = new UserInfoService();

        public ActionResult Index()
        {
            var role = RoleService.LoadEntities(t => t.Id != 0).ToList();
            ViewData.Model = role;
            return View();
        }

        public ActionResult GetRole()
        {
            int pageSize = Request["rows"] == null ? 10 : int.Parse(Request["rows"]);
            int pageIndex = Request["page"] == null ? 1 : int.Parse(Request["page"]);
            int total = 0;

            var data = RoleService.LoadPageEntities(pageSize, pageIndex, out total, t => t.Id != 0, false, t => t.Id);
            var result = new { total = total, rows = data };

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(Role role)
        {
            if (string.IsNullOrEmpty(role.Name))
            {
                return Content("error! role name  is required!");
            }
            int count = RoleService.LoadEntities(t => t.Name.Equals(role.Name)).Count();
            if (count > 0)
            {
                //已有此记录，不处理
                return Content("ok");
            }
            else
            {
                RoleService.Add(role);
                return Content("ok");
            }
        }

        public ActionResult Edit(int id)
        {
            ViewData.Model = RoleService.LoadEntities(t => t.Id == id).FirstOrDefault();
            return View();
        }
        [HttpPost]
        public ActionResult Edit(Role role)
        {
            if (role != null && !string.IsNullOrEmpty(role.Name))
            {

                RoleService.Update(role);
                return Content("ok");

            }
            else
            {
                return Content("not a valid role Name!");
            }
        }

        public ActionResult Delete(int id)
        {
            int roleCount = UserInfoService.LoadEntities(u => u.RoleId == id).Count();
            if (roleCount > 0)
            {
                return Content("必须先删除下面的所有使用后方可删除!");
            }
            else
            {
                var entity = RoleService.LoadEntities(t => t.Id == id).FirstOrDefault();
                if (entity != null)
                {
                    RoleService.Delete(entity);
                }
                return Content("ok");
            }
        }
    }
}