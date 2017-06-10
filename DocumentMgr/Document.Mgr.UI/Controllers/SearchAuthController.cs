using Document.Mgr.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Document.Mgr.UI.Controllers
{
    public class SearchAuthController : Controller
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
                if (CurrentLoginUser.Auth < 1 && !CurrentLoginUser.IsAdmin)
                {
                    filterContext.Result = new RedirectResult("/Error.html");
                }

                base.OnActionExecuting(filterContext);
            }
        }
	}
}