using Document.Mgr.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Document.Mgr.UI.Models
{
    public class ComparerForRTech : IEqualityComparer<R_UserInfo_Tech>
    {

        public bool Equals(R_UserInfo_Tech x, R_UserInfo_Tech y)
        {
            if (Object.ReferenceEquals(x, y)) return true;

            if (Object.ReferenceEquals(x, null) || Object.ReferenceEquals(y, null))
                return false;

            return x.TechId == y.TechId && x.LevelDescriptionId == y.LevelDescriptionId;
              
        }

        public int GetHashCode(R_UserInfo_Tech obj)
        {
            if (Object.ReferenceEquals(obj, null)) return 0;
            int hashTechId = obj.LevelDescriptionId.GetHashCode();
            int hashLevelId = obj.TechId.GetHashCode();
            return hashTechId ^ hashLevelId;
        }
    }
}