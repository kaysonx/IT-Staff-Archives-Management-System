using Document.Mgr.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Document.Mgr.UI.Controllers
{
    //用作验证用户登录的公共Controller.
    public class BaseController : Controller
    {

        public UserInfo CurrentLoginUser { get; set; }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
          
            if (filterContext.HttpContext.Session["LoginUser"] == null)
            {
                filterContext.Result = new RedirectResult("/Login/Index");
            }
            else
            {
                CurrentLoginUser = (UserInfo)filterContext.HttpContext.Session["LoginUser"];
                base.OnActionExecuting(filterContext);
            }
        }

    }
}