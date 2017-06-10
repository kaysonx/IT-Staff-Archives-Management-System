using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Document.Mgr.UI.Models
{
    //封装柱状图 数据
    public class StatisticsZZModel
    {
        public String text { get; set; }
        public String subText { get; set; }
        public List<String> legendData { get; set; }
        public List<String> yData { get; set; }
        public List<String> seriesData { get; set; }
    }
}