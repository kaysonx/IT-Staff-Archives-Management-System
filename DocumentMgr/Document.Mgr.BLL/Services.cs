 
using Document.Mgr.IBLL;
using Document.Mgr.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Document.Mgr.BLL
{
 
	public partial class CityService :BaseService<City>,ICityService
    {
        public override void SetCurrentDal()
        {
            CurrentDal = dbSession.CityDal;
        }
    }
 
	public partial class ContactService :BaseService<Contact>,IContactService
    {
        public override void SetCurrentDal()
        {
            CurrentDal = dbSession.ContactDal;
        }
    }
 
	public partial class CountryService :BaseService<Country>,ICountryService
    {
        public override void SetCurrentDal()
        {
            CurrentDal = dbSession.CountryDal;
        }
    }
 
	public partial class GroupService :BaseService<Group>,IGroupService
    {
        public override void SetCurrentDal()
        {
            CurrentDal = dbSession.GroupDal;
        }
    }
 
	public partial class LevelDescriptionService :BaseService<LevelDescription>,ILevelDescriptionService
    {
        public override void SetCurrentDal()
        {
            CurrentDal = dbSession.LevelDescriptionDal;
        }
    }
 
	public partial class PositionService :BaseService<Position>,IPositionService
    {
        public override void SetCurrentDal()
        {
            CurrentDal = dbSession.PositionDal;
        }
    }
 
	public partial class R_UserInfo_TechService :BaseService<R_UserInfo_Tech>,IR_UserInfo_TechService
    {
        public override void SetCurrentDal()
        {
            CurrentDal = dbSession.R_UserInfo_TechDal;
        }
    }
 
	public partial class RoleService :BaseService<Role>,IRoleService
    {
        public override void SetCurrentDal()
        {
            CurrentDal = dbSession.RoleDal;
        }
    }
 
	public partial class TechService :BaseService<Tech>,ITechService
    {
        public override void SetCurrentDal()
        {
            CurrentDal = dbSession.TechDal;
        }
    }
 
	public partial class TechTypeService :BaseService<TechType>,ITechTypeService
    {
        public override void SetCurrentDal()
        {
            CurrentDal = dbSession.TechTypeDal;
        }
    }
 
	public partial class UserInfoService :BaseService<UserInfo>,IUserInfoService
    {
        public override void SetCurrentDal()
        {
            CurrentDal = dbSession.UserInfoDal;
        }
    }

}