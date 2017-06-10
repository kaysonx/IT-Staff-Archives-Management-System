using Document.Mgr.BLL;
using Document.Mgr.IBLL;
using Document.Mgr.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Document.Mgr.UI.Models;
using Newtonsoft.Json;
using System.IO;

namespace Document.Mgr.UI.Controllers
{
    public class ClientUserController : BaseController
    {
       //供前台用户使用

        IUserInfoService UserInfoService = new UserInfoService();
        IR_UserInfo_TechService R_UserInfo_TechService = new R_UserInfo_TechService();
        ITechService TechService = new TechService();
        ITechTypeService TechTypeService = new TechTypeService();
        ILevelDescriptionService LevelDescriptionService = new LevelDescriptionService();
        ICityService CityService = new CityService();

        public ActionResult Index()
        {
            ViewData.Model = CurrentLoginUser;
            return View();
        }
        //基本信息页面
        public ActionResult UserInfo()
        {
            UserInfoModel uModel = new UserInfoModel()
            {
                UID = CurrentLoginUser.UID,
                UserName = CurrentLoginUser.Name,
                RoleName = CurrentLoginUser.Role.Name,
                PositionName = CurrentLoginUser.Position.Name,
                IsAssignable = CurrentLoginUser.IsAssigned,
                Gender = CurrentLoginUser.Gender == 0 ? "Female" : "Male",
                HIredTime = CurrentLoginUser.HiredTime.ToString("yyyy-MM-dd"),
                Pirture = CurrentLoginUser.Picture,
                WrokExp = Math.Ceiling((DateTime.Now - CurrentLoginUser.HiredTime).TotalDays / 30.0/12.0).ToString(),
                WorkTeam = CurrentLoginUser.Group.Name,
                WorkCity = CurrentLoginUser.Group.City.Name
            };

           var city =  CityService.LoadEntities(c => c.Id == CurrentLoginUser.Group.CityId).FirstOrDefault();
           uModel.WorkCountry = city.Country.Name;

           ViewData.Model = uModel;
            return View();
        }
        //skill页面
        public ActionResult Skill()
        {
            var types = TechTypeService.LoadEntities(t => t.Id != 0);
            List<SkillViewModel> skills = new List<SkillViewModel>();
            //必须重新去取 不然用的是session里的缓存数据。不能及时显示更新情况
            var currentUser = UserInfoService.LoadEntities(c => c.Id == CurrentLoginUser.Id).FirstOrDefault();
            foreach (var type in types)
            {
                SkillViewModel skill = new SkillViewModel();
                skill.Techs = new List<TechViewModel>();
                skill.TechTypeName = type.TypeName;
                foreach (var tech in type.Tech)
                {
                    var level = (from r in currentUser.R_UserInfo_Tech
                                     where r.TechId == tech.Id
                                     select r.LevelDescription).FirstOrDefault();
                    if (level == null)
                    {
                        level = new LevelDescription() { Level = 0, Description = "Not Rate" };
                    }
                    TechViewModel techview = new TechViewModel(){
                        Tech = tech,
                        Level = level
                    };
                    skill.Techs.Add(techview);
                }
                skills.Add(skill);
            }
            
            ViewData.Model = skills;
            
            return View();
        }
        //团队成员skill查看
        public ActionResult GetSkillById(int uid)
        {
            var user = UserInfoService.LoadEntities(u => u.Id == uid).FirstOrDefault();
            if (user == null)
            {
                throw new Exception("操作非法！");
            }
            var types = TechTypeService.LoadEntities(t => t.Id != 0);
            List<SkillViewModel> skills = new List<SkillViewModel>();
            //必须重新去取 不然用的是session里的缓存数据。不能及时显示更新情况
            var currentUser = UserInfoService.LoadEntities(c => c.Id == user.Id).FirstOrDefault();
            foreach (var type in types)
            {
                SkillViewModel skill = new SkillViewModel();
                skill.Techs = new List<TechViewModel>();
                skill.TechTypeName = type.TypeName;
                foreach (var tech in type.Tech)
                {
                    var level = (from r in currentUser.R_UserInfo_Tech
                                 where r.TechId == tech.Id
                                 select r.LevelDescription).FirstOrDefault();
                    if (level == null)
                    {
                        level = new LevelDescription() { Level = 0, Description = "Not Rate" };
                    }
                    TechViewModel techview = new TechViewModel()
                    {
                        Tech = tech,
                        Level = level
                    };
                    skill.Techs.Add(techview);
                }
                skills.Add(skill);
            }

            ViewData.Model = skills;

            return View();
        }

        public ActionResult UpdateSkill(string skills)
        {

            if (string.IsNullOrEmpty(skills))
            {
                return Content("error, there is no skill info!");
            }

            var skillModels = JsonConvert.DeserializeObject<SkillModel[]>(skills);
            UserInfoService.HandleSkill(CurrentLoginUser.Id,skillModels);

            return Content("ok");
        }

        public ActionResult GetLevelInfo()
        {
            var levelInfos = LevelDescriptionService.LoadEntities(l => l.Id != 0)
                                            .Select(l => new { Level = l.Level.ToString(), Des = l.Description });
            Dictionary<string, string> dic = new Dictionary<string, string>();
            foreach (var item in levelInfos)
            {
                dic.Add(item.Level, item.Des);
            }

            return Json(dic, JsonRequestBehavior.AllowGet);
        }

        public ActionResult UpdateUserPwd()
        {
            ViewData.Model = CurrentLoginUser;
            return View();
        }
        //修改密码
        [HttpPost]
        public ActionResult UpdateUserPwd(string oldPwd, string newPwd1, string newPwd2)
        {
            var user = UserInfoService.LoadEntities(u => u.Id == CurrentLoginUser.Id).FirstOrDefault();

            if (user.Pwd != oldPwd)
            {
                return Content("密码错误!");
            }

            if (newPwd1 != newPwd2)
            {
                return Content("两次密码不匹配!");
            }

            user.Pwd = newPwd1;
            UserInfoService.Update(user);
            return Content("ok");
        }

        //登出
        public ActionResult LoginOut()
        {
            var loginUser = Session["LoginUser"];
            if (loginUser != null)
            {
                CurrentLoginUser = null;
                Session["LoginUser"] = null;
            }
            return Redirect("/Login/Index");
        }

        //头像上传
        public ActionResult UploadPic()
        {
            Response.ContentType = "text/plain";
            HttpPostedFileBase file = Request.Files["headImg"];//接收文件.
            string fileName = Path.GetFileName(file.FileName);//获取文件名.
            string fileExt = Path.GetExtension(fileName);
    

            string dir = "/UpLoad/" + CurrentLoginUser.UID + "/Pic/";
            Directory.CreateDirectory(Path.GetDirectoryName(Server.MapPath(dir)));//创建文件夹
            string fullDir = dir + fileName;

            file.SaveAs(Server.MapPath(fullDir));

    
            var user = UserInfoService.LoadEntities(u => u.Id == CurrentLoginUser.Id).FirstOrDefault();
            user.Picture = fullDir;
            UserInfoService.Update(user);
            return Content(fullDir);
        }

        //团队展示
        public ActionResult ShowTeam(string teamName)
        {
            var teamUsers = UserInfoService.LoadEntities(u => u.Group.Name == teamName);
            ViewData.Model = teamUsers;
            ViewData["teamName"] = teamName;
            return View();
        }
        //团队成员档案查看
        public ActionResult ViewDetail(int uid)
        {
            var user = UserInfoService.LoadEntities(u => u.Id == uid).FirstOrDefault();
            if (user == null)
            {
                throw new Exception("操作非法！");
            }
            UserInfoModel uModel = new UserInfoModel()
            {
                UID = user.UID,
                UserName = user.Name,
                RoleName = user.Role.Name,
                PositionName = user.Position.Name,
                IsAssignable = user.IsAssigned,
                Gender = user.Gender == 0 ? "Female" : "Male",
                HIredTime = user.HiredTime.ToString("yyyy-MM-dd"),
                Pirture = user.Picture,
                WrokExp = Math.Ceiling((DateTime.Now - user.HiredTime).TotalDays / 30.0 / 12.0).ToString(),
                WorkTeam = user.Group.Name,
                WorkCity = user.Group.City.Name
            };

            var city = CityService.LoadEntities(c => c.Id == user.Group.CityId).FirstOrDefault();
            uModel.WorkCountry = city.Country.Name;

            ViewData.Model = uModel;
            ViewData["id"] = user.Id;
            return View();
        }
	}
}