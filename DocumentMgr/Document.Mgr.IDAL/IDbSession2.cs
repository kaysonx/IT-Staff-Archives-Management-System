 

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Document.Mgr.IDAL
{
  
    public partial interface IDbSession
    { 
	
		ICityDal CityDal { get; }
	
		IContactDal ContactDal { get; }
	
		ICountryDal CountryDal { get; }
	
		IGroupDal GroupDal { get; }
	
		ILevelDescriptionDal LevelDescriptionDal { get; }
	
		IPositionDal PositionDal { get; }
	
		IR_UserInfo_TechDal R_UserInfo_TechDal { get; }
	
		IRoleDal RoleDal { get; }
	
		ITechDal TechDal { get; }
	
		ITechTypeDal TechTypeDal { get; }
	
		IUserInfoDal UserInfoDal { get; }
	}

}