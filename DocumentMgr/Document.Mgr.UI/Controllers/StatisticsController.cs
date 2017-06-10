using Document.Mgr.BLL;
using Document.Mgr.IBLL;
using Document.Mgr.UI.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Document.Mgr.UI.Controllers
{
    public class StatisticsController : BackAuthController
    {
        IPositionService PositionService = new PositionService();
        IRoleService RoleService = new RoleService();
        ITechTypeService TechTypeService = new TechTypeService();
        ITechService TechService = new TechService();
        IUserInfoService UserInfoService = new UserInfoService();
        IR_UserInfo_TechService R_UserInfo_TechService = new R_UserInfo_TechService(); 

       //统计专用
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetPositionDataZZ()
        {
            List<string> legendData = new List<string>();
            legendData.Add("职位");
            StatisticsZZModel zz = new StatisticsZZModel()
            {
                legendData =legendData,
                text="职位统计",
                subText = "统计时间：" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
            };

            var pos = PositionService.LoadEntities(p => p.Id != 0);
            Dictionary<String, string> dic = new Dictionary<string, string>();
            foreach (var item in pos)
            {
                int count = UserInfoService.LoadEntities(u => u.PositionId == item.Id).Count();
                dic.Add(item.Name, count.ToString());
            }
            List<string> keys = new List<string>();
            foreach (var key in dic.Keys)
            {
                keys.Add(key);
            }
            List<string> values = new List<string>();
            foreach (var value in dic.Values)
            {
                values.Add(value);
            }
            zz.yData = keys;
            zz.seriesData = values;

            return Json(zz, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetPositionDataBZ()
        {
            List<string> legendData = new List<string>();
            StatisticsBZModel bz = new StatisticsBZModel()
            {
                text = "职位统计",
                subText = "统计时间：" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
            };

            var pos = PositionService.LoadEntities(p => p.Id != 0);
            List<DataPair> data = new List<DataPair>();
            foreach (var item in pos)
            {
                int count = UserInfoService.LoadEntities(u => u.PositionId == item.Id).Count();
                DataPair dp = new DataPair()
                {
                    name = item.Name,
                    value = count.ToString()
                };
                data.Add(dp);
                legendData.Add(item.Name);
            }
            bz.data = data;
            bz.legendData = legendData;
            return Json(bz, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetRoleDataZZ()
        {
            List<string> legendData = new List<string>();
            legendData.Add("角色");
            StatisticsZZModel zz = new StatisticsZZModel()
            {
                legendData = legendData,
                text = "角色统计",
                subText = "统计时间：" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
            };

            var role = RoleService.LoadEntities(r => r.Id != 0);
            Dictionary<String, string> dic = new Dictionary<string, string>();
            foreach (var item in role)
            {
                int count = UserInfoService.LoadEntities(u => u.RoleId == item.Id).Count();
                dic.Add(item.Name, count.ToString());
            }
            List<string> keys = new List<string>();
            foreach (var key in dic.Keys)
            {
                keys.Add(key);
            }
            List<string> values = new List<string>();
            foreach (var value in dic.Values)
            {
                values.Add(value);
            }
            zz.yData = keys;
            zz.seriesData = values;

            return Json(zz, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetRoleDataBZ()
        {
            List<string> legendData = new List<string>();
            StatisticsBZModel bz = new StatisticsBZModel()
            {
                text = "角色统计",
                subText = "统计时间：" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
            };

            var role = RoleService.LoadEntities(p => p.Id != 0);
            List<DataPair> data = new List<DataPair>();
            foreach (var item in role)
            {
                int count = UserInfoService.LoadEntities(u => u.RoleId == item.Id).Count();
                DataPair dp = new DataPair()
                {
                    name = item.Name,
                    value = count.ToString()
                };
                data.Add(dp);
                legendData.Add(item.Name);
            }
            bz.data = data;
            bz.legendData = legendData;
            return Json(bz, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetTechDataZZ()
        {
            List<string> legendData = new List<string>();
            legendData.Add("技术");
            StatisticsZZModel zz = new StatisticsZZModel()
            {
                legendData = legendData,
                text = "技术统计",
                subText = "统计时间：" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
            };

            var tech = TechService.LoadEntities(r => r.Id != 0);
            Dictionary<string, string> dic = new Dictionary<string, string>();
            foreach (var item in tech)
            {
                int count = R_UserInfo_TechService.LoadEntities(r => r.TechId == item.Id).Count();
                //去重
                if (dic.ContainsKey(item.TechName))
                {
                    string value = dic[item.TechName];
                    dic[item.TechName] = (Convert.ToInt32(value) + count).ToString();
                }
                else
                {
                    dic.Add(item.TechName, count.ToString());
                }
            }
            List<string> keys = new List<string>();
            foreach (var key in dic.Keys)
            {
                keys.Add(key);
            }
            List<string> values = new List<string>();
            foreach (var value in dic.Values)
            {
                values.Add(value);
            }
            zz.yData = keys;
            zz.seriesData = values;

            return Json(zz, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetTechDataBZ()
        {
            List<string> legendData = new List<string>();
            StatisticsBZModel bz = new StatisticsBZModel()
            {
                text = "技术统计",
                subText = "统计时间：" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
            };

            var tech = TechService.LoadEntities(p => p.Id != 0);
            List<DataPair> data = new List<DataPair>();
            foreach (var item in tech)
            {
                int count = R_UserInfo_TechService.LoadEntities(r => r.TechId == item.Id).Count();
                DataPair dp = new DataPair()
                {
                    name = item.TechName,
                    value = count.ToString()
                };
                data.Add(dp);
                legendData.Add(item.TechName);
            }
            bz.data = data;
            bz.legendData = legendData;
            return Json(bz, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetYearDataZZ()
        {
            List<string> legendData = new List<string>();
            legendData.Add("在职年限");
            StatisticsZZModel zz = new StatisticsZZModel()
            {
                legendData = legendData,
                text = "在职员工统计",
                subText = "统计时间：" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
            };

            var users = UserInfoService.LoadEntities(u => u.IsDeleted == false);
            Dictionary<String, string> dic = new Dictionary<string, string>();
            int year1 = 0;
            int year3 = 0;
            int year5 = 0;
            int year10 = 0;
            int year = 0;
            foreach (var user in users)
            {
                int y = CountWorkYear(user.HiredTime);
                if (y == 0)
                {
                    year1++;
                }
                if (y >=1 && y < 3)
                {
                    year3++;
                }
                if (y >= 3 && y <5)
                {
                    year5++;
                }
                if (y >= 5 && y < 10)
                {
                    year10++;
                }
                if (y >= 10)
                {
                    year++;
                }
            }

            dic.Add("一年以下", year1.ToString());
            dic.Add("1-3", year3.ToString());
            dic.Add("3-5", year5.ToString());
            dic.Add("5-10", year10.ToString());
            dic.Add("10年以上", year.ToString());

            List<string> keys = new List<string>();
            foreach (var key in dic.Keys)
            {
                keys.Add(key);
            }
            List<string> values = new List<string>();
            foreach (var value in dic.Values)
            {
                values.Add(value);
            }
            zz.yData = keys;
            zz.seriesData = values;

            return Json(zz, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetYearDataBZ()
        {
            List<string> legendData = new List<string>();
            StatisticsBZModel bz = new StatisticsBZModel()
            {
                text = "在职员工统计",
                subText = "统计时间：" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
            };
            var users = UserInfoService.LoadEntities(u => u.IsDeleted == false);
            Dictionary<String, string> dic = new Dictionary<string, string>();
            int year1 = 0;
            int year3 = 0;
            int year5 = 0;
            int year10 = 0;
            int year = 0;
            foreach (var user in users)
            {
                int y = CountWorkYear(user.HiredTime);
                if (y == 0)
                {
                    year1++;
                }
                if (y >= 1 && y < 3)
                {
                    year3++;
                }
                if (y >= 3 && y < 5)
                {
                    year5++;
                }
                if (y >= 5 && y < 10)
                {
                    year10++;
                }
                if (y >= 10)
                {
                    year++;
                }
            }
            List<DataPair> data = new List<DataPair>();
            data.Add(new DataPair() { name = "一年以下", value = year1.ToString()});
            data.Add(new DataPair() { name = "1-3年", value = year3.ToString() });
            data.Add(new DataPair() { name = "3-5年", value = year5.ToString() });
            data.Add(new DataPair() { name = "5-10年", value = year10.ToString() });
            data.Add(new DataPair() { name = "10年以上", value = year.ToString() });

            legendData.Add("一年以下");
            legendData.Add("1-3年");
            legendData.Add("3-5年");
            legendData.Add("5-10年");
            legendData.Add("10年以上");
            
            bz.data = data;
            bz.legendData = legendData;
            return Json(bz, JsonRequestBehavior.AllowGet);
        }


        public ActionResult GetDataZZ(string category)
        {
            if (category == "position")
            {
                return GetPositionDataZZ();
            }
            if (category == "role")
            {
                return GetRoleDataZZ();
            }
            if (category == "tech")
            {
                return GetTechDataZZ();
            }
            if (category == "year")
            {
                return GetYearDataZZ();
            }
            return Content("error");
        }

        public ActionResult GetDataBZ(string category)
        {
            if (category == "position")
            {
                return GetPositionDataBZ();
            }
            if (category == "role")
            {
                return GetRoleDataBZ();
            }
            if (category == "tech")
            {
                return GetTechDataBZ();
            }
            if (category == "year")
            {
                return GetYearDataBZ();
            }
            return Content("error");
        }

        //计算入职年限
        public int CountWorkYear(DateTime hiredTime)
        {
           TimeSpan t  = DateTime.Now - hiredTime;
           double month = Math.Round(t.Days / 30.0,1);
           return (int)(month / 12);
        }

	}
}