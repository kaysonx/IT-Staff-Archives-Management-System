using Document.Mgr.BLL;
using Document.Mgr.Common;
using Document.Mgr.IBLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Document.Mgr.UI.Controllers
{
    public class LoginController : Controller
    {

        IUserInfoService UserInfoService = new UserInfoService();

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string LoginMail, string LoginPwd, string vCode)
        {
            //1.验证验证码
            string strCode = Session["vCode"] == null ? string.Empty : Session["vCode"].ToString();
            Session["vCode"] = null; //验证码只用一次， 以防暴力破解。

            if (string.IsNullOrEmpty(vCode))
            {
                return Content("请输入验证码!");
            }
            if (strCode.ToLower() != vCode.ToLower())
            {
                return Content("验证码验证失败!");
            }

            //2.验证用户
            string md5Pwd = MD5Helper.GetMd5Str32(LoginPwd);
            var loginUser = UserInfoService.LoadEntities(u => u.Email == LoginMail && u.Pwd == md5Pwd).FirstOrDefault();

            if (loginUser == null)
            {
                return  Content("用户名或密码错误!");
            }
            
            if (loginUser.IsDeleted || !loginUser.IsEnabled)
            {
                return Content("账户异常，请联系管理员!");
            }


            //3.保存用户信息到Session
            Session["LoginUser"] = loginUser;
            return Content("ok");
        }

        public ActionResult ShowVCode()
        {
            Common.ValidateCodeHelper codeHelper = new Common.ValidateCodeHelper();
            string strCode = codeHelper.CreateValidateCode(4);

            Session["vCode"] = strCode;

            byte[] data = codeHelper.CreateValidateGraphic(strCode);
            return File(data, "image/jpeg");
        }
	}
}