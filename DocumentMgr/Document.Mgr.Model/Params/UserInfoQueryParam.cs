using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Document.Mgr.Model.Params
{
    //封装查询条件
    class UserInfoQueryParam : BaseParam
    {
        public DateTime startHiredTime{get;set;}

        //默认为当前
        public DateTime endHiredTime = DateTime.Now;

        public string SearchName { get; set; }
        //登录邮箱
        public string SearchMail { get; set; }

        public int SearchPosition { get; set; }

        public int SearchOfffice { get; set; }

        public int SearchRole { get; set; }

        //工作时间
        public int startWrokTime { get; set; }

        public int endWrokTime { get; set; }
        //是否有项目经验
        public bool HasProjectExp { get; set; }

        //Techs
        public List<int> SearchTechs { get; set; }
    }
}
