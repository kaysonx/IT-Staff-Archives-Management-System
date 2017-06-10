using Document.Mgr.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Document.Mgr.UI.Models
{

     //单个Tech和Level
        public class TechViewModel
        {
            public Tech Tech { get; set; }
            public LevelDescription Level { get; set; }
        }
    
}