using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Document.Mgr.UI.Models
{
    public class StatisticsBZModel
    {
        public string text { get; set; }
        public string subText { get; set; }
        public List<string> legendData { get; set; }
        public List<DataPair> data  { get; set; }


        
    }

   public  class DataPair
    {
        public string name { get; set; }
        public string value { get; set; }
    }
}