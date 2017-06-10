using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Document.Mgr.UI.Models
{
    //new {Name = CurrentLoginUser.Name, Position = CurrentLoginUser.Position.Name, UID = CurrentLoginUser.UID, Team = CurrentLoginUser.Group.Name, WorkEmail = CurrentLoginUser.Email, PersonalEmail = CurrentLoginUser.Contact.PersonalEmail ?? "暂无", PhoneNumber = CurrentLoginUser.Contact.PhoneNumber ?? "暂无", WX = CurrentLoginUser.Contact.WX ?? "暂无", Blog = CurrentLoginUser.Contact.BlogUrl ?? "暂无", City = CurrentLoginUser.Group.City.Name, Country = country };
    //用于封装通讯录ViewModel
    public class ContactModel
    {
        public String Name { get; set; }
        public String Position { get; set; }
        public String UID { get; set; }
        public String Team { get; set; }
        public String WorkEmail { get; set; }
        public String PersonalEmail { get; set; }
        public String PhoneNumber { get; set; }
        public String WX { get; set; }
        public String Blog { get; set; }
        public String Country { get; set; }
        public String City { get; set; }
    }
}