 

using Document.Mgr.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Document.Mgr.IBLL
{
 
	public partial interface ICityService :IBaseService<City>
    {
    }
 
	public partial interface IContactService :IBaseService<Contact>
    {
    }
 
	public partial interface ICountryService :IBaseService<Country>
    {
    }
 
	public partial interface IGroupService :IBaseService<Group>
    {
    }
 
	public partial interface ILevelDescriptionService :IBaseService<LevelDescription>
    {
    }
 
	public partial interface IPositionService :IBaseService<Position>
    {
    }
 
	public partial interface IR_UserInfo_TechService :IBaseService<R_UserInfo_Tech>
    {
    }
 
	public partial interface IRoleService :IBaseService<Role>
    {
    }
 
	public partial interface ITechService :IBaseService<Tech>
    {
    }
 
	public partial interface ITechTypeService :IBaseService<TechType>
    {
    }
 
	public partial interface IUserInfoService :IBaseService<UserInfo>
    {
    }

}