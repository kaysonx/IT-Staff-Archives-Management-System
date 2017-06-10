using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Document.Mgr.Model
{
    //用于反序列化前端过来的单个skill
    public class SkillModel
    {
        public int TechId { get; set; }
        public int Level { get; set; }
    }
}