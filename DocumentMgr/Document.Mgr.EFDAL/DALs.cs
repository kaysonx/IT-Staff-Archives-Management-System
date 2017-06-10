 

using Document.Mgr.IDAL;
using Document.Mgr.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Document.Mgr.EFDAL
{
  
   
	public partial class CityDal :BaseDal<City>,IDAL.ICityDal
    {
    } 
	public partial class ContactDal :BaseDal<Contact>,IDAL.IContactDal
    {
    } 
	public partial class CountryDal :BaseDal<Country>,IDAL.ICountryDal
    {
    } 
	public partial class GroupDal :BaseDal<Group>,IDAL.IGroupDal
    {
    } 
	public partial class LevelDescriptionDal :BaseDal<LevelDescription>,IDAL.ILevelDescriptionDal
    {
    } 
	public partial class PositionDal :BaseDal<Position>,IDAL.IPositionDal
    {
    } 
	public partial class R_UserInfo_TechDal :BaseDal<R_UserInfo_Tech>,IDAL.IR_UserInfo_TechDal
    {
    } 
	public partial class RoleDal :BaseDal<Role>,IDAL.IRoleDal
    {
    } 
	public partial class TechDal :BaseDal<Tech>,IDAL.ITechDal
    {
    } 
	public partial class TechTypeDal :BaseDal<TechType>,IDAL.ITechTypeDal
    {
    } 
	public partial class UserInfoDal :BaseDal<UserInfo>,IDAL.IUserInfoDal
    {
    } 

}