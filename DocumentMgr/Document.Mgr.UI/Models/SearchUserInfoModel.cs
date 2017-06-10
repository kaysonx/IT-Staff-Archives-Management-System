using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Document.Mgr.UI.Models
{
    public class SearchUserInfoModel
    {
        // u.Id, u.Name, u.Email, GroupName = u.Group.Name, RoleName = u.Role.Name, PositionName = u.Position.Name, Techs = ""
        public int Id { get; set; }
        public String Email { get; set; }
        public String Name { get; set; }
        public String RoleName { get; set; }
        public String PositionName { get; set; }
        public String GroupName { get; set; }
        public String Techs { get; set; }
        //u.Id, u.Name, u.Email,  u.Group.Name, u.Role.Name, u.Position.Name
        public SearchUserInfoModel(int Id, String Name, String Email,  String GroupName, String RoleName, String PositionName)
        {
            this.Id = Id;
            this.Name = Name;
            this.Email = Email;
            this.GroupName = GroupName;
            this.RoleName = RoleName;
            this.PositionName = PositionName;
        }
    }
}