using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Document.Mgr.Model;

namespace Document.Mgr.UI.Models
{
    //用于Skill的展示
    public class SkillViewModel
    {
        public string TechTypeName { get; set; }
        //Type下的所有Tech展示
        public List<TechViewModel> Techs { get; set; }
    }
}