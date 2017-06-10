using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Document.Mgr.UI.Models
{
    public class SearchModel
    {
        public int? workYear { get; set; }
        public List<int> positions { get; set; }
        public List<int> roles { get; set; }

        public List<SearchAreaModel> areas { get; set; }
        public List<SearchTechModel> techs { get; set; }

    }
}