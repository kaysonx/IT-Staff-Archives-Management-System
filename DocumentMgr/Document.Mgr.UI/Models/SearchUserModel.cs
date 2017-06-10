using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Document.Mgr.UI.Models
{
    public class SearchUserModel
    {
        //搜索用户Model
        public DateTime? startTime { get; set; }

        public DateTime? endTime { get; set; }

        public String searchInfo { get; set; }
    }
}