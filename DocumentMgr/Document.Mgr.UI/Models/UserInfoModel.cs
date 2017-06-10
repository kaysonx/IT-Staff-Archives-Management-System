using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Document.Mgr.UI.Models
{
    //用于用户界面展示用
    public class UserInfoModel
    {
        public String UserName { get; set; }
        public String UID { get; set; }
        public String Gender { get; set; }
        public String RoleName { get; set; }
        public String PositionName { get; set; }
        public String HIredTime { get; set; }
        public String WrokExp { get; set; }
        public bool IsAssignable { get; set; }
        public String Pirture { get; set; }
        public String WorkCountry { get; set; }
        public String WorkCity { get; set; }
        public String WorkTeam { get; set; }

    }
}