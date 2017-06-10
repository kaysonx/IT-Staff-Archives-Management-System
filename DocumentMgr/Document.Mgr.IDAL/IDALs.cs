 

using Document.Mgr.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Document.Mgr.IDAL
{
	
	public partial interface ICityDal :IBaseDal<City>
    {
         
    }
	
	public partial interface IContactDal :IBaseDal<Contact>
    {
         
    }
	
	public partial interface ICountryDal :IBaseDal<Country>
    {
         
    }
	
	public partial interface IGroupDal :IBaseDal<Group>
    {
         
    }
	
	public partial interface ILevelDescriptionDal :IBaseDal<LevelDescription>
    {
         
    }
	
	public partial interface IPositionDal :IBaseDal<Position>
    {
         
    }
	
	public partial interface IR_UserInfo_TechDal :IBaseDal<R_UserInfo_Tech>
    {
         
    }
	
	public partial interface IRoleDal :IBaseDal<Role>
    {
         
    }
	
	public partial interface ITechDal :IBaseDal<Tech>
    {
         
    }
	
	public partial interface ITechTypeDal :IBaseDal<TechType>
    {
         
    }
	
	public partial interface IUserInfoDal :IBaseDal<UserInfo>
    {
         
    }

}