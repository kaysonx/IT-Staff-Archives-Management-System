using Document.Mgr.BLL;
using Document.Mgr.IBLL;
using Document.Mgr.Model;
using Document.Mgr.UI.Models;
using Document.Mgr.UI.Search;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Document.Mgr.UI.Controllers
{
    //搜索专用
    public class SearchController : SearchAuthController
    {
        IPositionService PositionService = new PositionService();
        IRoleService RoleService = new RoleService();

        ITechService TechService = new TechService();
        ITechTypeService TechTypeService = new TechTypeService();

        ICountryService CountryService = new CountryService();
        ICityService CityService = new CityService();
        IGroupService GroupService = new GroupService();

        IUserInfoService UserInfoService = new UserInfoService();
        IR_UserInfo_TechService R_UserInfo_TechService = new R_UserInfo_TechService();


        public ActionResult Index()
        {
            var techTypes = TechTypeService.LoadEntities(t => t.Id != 0).ToList();
            var positions = PositionService.LoadEntities(p => p.Id != 0).ToList();
            var roles = RoleService.LoadEntities(r => r.Id != 0).ToList();
            var countries = CountryService.LoadEntities(c => c.Id != 0);

            ViewData["techTypes"] = new SelectList(techTypes, "Id", "TypeName");
            ViewData["countries"] = new SelectList(countries, "Id", "Name");


            ViewData["positions"] = positions;
            ViewData["roles"] = roles;

            return View();
        }

        [HttpPost]
        public ActionResult SearchPerson(string conditions, string keywords, int? page)
        {
            int pageSize = 10;
            int pageIndex = 1;
            if (page != null)
            {
                pageIndex = page.Value;
            }
            
            int total = 0;

            if (string.IsNullOrEmpty(conditions) && string.IsNullOrEmpty(keywords))
            {
                var result = new { data = "error, there is no search info!", total = 0 };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            //定义两个集合方便后面做交集。。。
            IQueryable<UserInfo> users;
            IEnumerable<UserInfo> newUsers = null;
            if (!string.IsNullOrEmpty(keywords))
            {
                try
                {
                    keywords = keywords.ToLower();
                    //先使用Lucene进行模糊搜索
                    List<int> searchedUid = SearchHelper.SeartchUser(keywords);
                    users = UserInfoService.LoadEntities(u => searchedUid.Contains(u.Id));
                }
                catch (Exception)
                {
                    users = UserInfoService.LoadEntities(u => u.Id != 0);
                }
            }
            else
            {
                users = UserInfoService.LoadEntities(u => u.Id != 0);
            }

            if (!string.IsNullOrEmpty(conditions))
            {
                //然后再进行精确搜索
                var skillModels = JsonConvert.DeserializeObject<SearchModel>(conditions);
                var positions = skillModels.positions;
                var roles = skillModels.roles;

                //使用lambda表达式来进行查询
                //先筛选大条件 然后再筛选小条件
                if (positions.Count > 0)
                {
                    users = users.Where(u => positions.Contains(u.PositionId));
                }

                if (roles.Count > 0)
                {
                    users = users.Where(u => roles.Contains(u.RoleId));
                }
                if (skillModels.workYear != null)
                {
                    users = users.Where(u => DateTime.Now.Year - u.HiredTime.Year >= skillModels.workYear);
                }

                if (skillModels.areas.Count > 0)
                {
                    List<int> countryIds = new List<int>();
                    List<int> cityIds = new List<int>();
                    List<int> teamIds = new List<int>();

                    //先将所有的查询信息转为数组。然后统一处理
                    foreach (var item in skillModels.areas)
                    {
                        if (item.countryId != null)
                        {
                            countryIds.Add(item.countryId.Value);
                        }
                        if (item.cityId != null)
                        {
                            cityIds.Add(item.cityId.Value);
                        }
                        if (item.teamId != null)
                        {
                            teamIds.Add(item.teamId.Value);
                        }
                    }

                    users = users.Where(u => cityIds.Contains(u.Group.CityId) && teamIds.Contains(u.GroupId));

                }

                //通过自定义比较器，对两个集合做交集即可确定是否存在该Tech
                if (skillModels.techs.Count > 0)
                {
                    List<R_UserInfo_Tech> r_techs = new List<R_UserInfo_Tech>();
                    List<SearchTechModel> paddingTech = new List<SearchTechModel>();
                    //先单独处理范围level
                    foreach (var item in skillModels.techs)
                    {
                        if (item.techLevel == 2)
                        {
                            item.techLevel = 1;
                            //2表示1-2级别，所以加上2
                            paddingTech.Add(new SearchTechModel() { techId = item.techId, techLevel = 2 });
                        }
                        if (item.techLevel == 6)
                        {
                            //6表示所有级别
                            item.techLevel = 1;
                            for (int i = 2; i <= 5; i++)
                            {
                                paddingTech.Add(new SearchTechModel() { techId = item.techId, techLevel = i });
                            }
                        }
                    }
                    skillModels.techs =  skillModels.techs.Concat(paddingTech).ToList();
                    foreach (var item in skillModels.techs)
                    {
                        R_UserInfo_Tech r = new R_UserInfo_Tech() { TechId = item.techId, LevelDescriptionId = item.techLevel };
                        r_techs.Add(r);
                    }
                    //先去数据库里查询一次。因为LINK TO OBJECT 不支持交集操作
                  newUsers =  users.ToList().Where(u => u.R_UserInfo_Tech.Intersect(r_techs, new ComparerForRTech()).Count() > 0);
                    
                }
            }

            try
            {
                //此时才会真正去数据库里做查询 使用匿名对象拿一些必须的属性。同时也防止出现循环序列化
                List<SearchUserInfoModel> sResult = new List<SearchUserInfoModel>();
                if (newUsers == null)
                {
                    total = users.Count();
                    var realUsers = users.OrderBy(u => u.Id).Skip(pageSize * (pageIndex - 1)).Take(pageSize).Select(u => new { u.Id, u.Name, u.Email, GroupName = u.Group.Name, RoleName = u.Role.Name, PositionName = u.Position.Name }).ToList();
                    foreach (var u in realUsers)
                    {
                        SearchUserInfoModel s = new SearchUserInfoModel(u.Id, u.Name, u.Email, u.GroupName, u.RoleName, u.PositionName);
                        s.Techs = getUserTechs(u.Id);
                        if (s.Techs.Length > 70)
                        {
                            s.Techs = s.Techs.Substring(0, 70) + "......";
                        }
                        sResult.Add(s);
                    }
                }
                else
                {
                    total = newUsers.Count();
                    var realUsers = newUsers.OrderBy(u => u.Id).Skip(pageSize * (pageIndex - 1)).Take(pageSize).Select(u => new { u.Id, u.Name, u.Email, GroupName = u.Group.Name, RoleName = u.Role.Name, PositionName = u.Position.Name }).ToList();
                    foreach (var u in realUsers)
                    {
                        SearchUserInfoModel s = new SearchUserInfoModel(u.Id, u.Name, u.Email, u.GroupName, u.RoleName, u.PositionName);
                        s.Techs = getUserTechs(u.Id);
                        if (s.Techs.Length > 70)
                        {
                            s.Techs = s.Techs.Substring(0, 70) + "......";
                        }
                        sResult.Add(s);
                    }
                }
                var res =new  {data = sResult, total};
                return Json(res, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                var result = new { data = "no matching data!!", total = 0 };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult CreateUserInfoIndex()
        {
            try
            {
                SearchHelper.CreateUserInfoIndex();
            }
            catch (Exception)
            {
                return Content("error, create index failed!");
            }
            return Content("ok");
        }

        public String getUserTechs(int uid)
        {
            var relations = R_UserInfo_TechService.LoadEntities(r => r.UserInfoId == uid);
            StringBuilder sb = new StringBuilder();
            foreach (var item in relations)
            {
                sb.Append(item.Tech.TechName +"|"+ item.LevelDescription.Level+"，");
            }
            return sb.ToString();
        }
     
    }
}