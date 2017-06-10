 
using Document.Mgr.EFDAL;
using Document.Mgr.IDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Document.Mgr.DALFactory
{
    public partial class DbSession :IDbSession
    {
   
	
		private ICityDal _CityDal;
		public ICityDal CityDal
		{
			get
			{
				if (_CityDal == null)
				{
					_CityDal = DalFactory.GetCityDal();
				}
				return _CityDal;
			}
		}
	
		private IContactDal _ContactDal;
		public IContactDal ContactDal
		{
			get
			{
				if (_ContactDal == null)
				{
					_ContactDal = DalFactory.GetContactDal();
				}
				return _ContactDal;
			}
		}
	
		private ICountryDal _CountryDal;
		public ICountryDal CountryDal
		{
			get
			{
				if (_CountryDal == null)
				{
					_CountryDal = DalFactory.GetCountryDal();
				}
				return _CountryDal;
			}
		}
	
		private IGroupDal _GroupDal;
		public IGroupDal GroupDal
		{
			get
			{
				if (_GroupDal == null)
				{
					_GroupDal = DalFactory.GetGroupDal();
				}
				return _GroupDal;
			}
		}
	
		private ILevelDescriptionDal _LevelDescriptionDal;
		public ILevelDescriptionDal LevelDescriptionDal
		{
			get
			{
				if (_LevelDescriptionDal == null)
				{
					_LevelDescriptionDal = DalFactory.GetLevelDescriptionDal();
				}
				return _LevelDescriptionDal;
			}
		}
	
		private IPositionDal _PositionDal;
		public IPositionDal PositionDal
		{
			get
			{
				if (_PositionDal == null)
				{
					_PositionDal = DalFactory.GetPositionDal();
				}
				return _PositionDal;
			}
		}
	
		private IR_UserInfo_TechDal _R_UserInfo_TechDal;
		public IR_UserInfo_TechDal R_UserInfo_TechDal
		{
			get
			{
				if (_R_UserInfo_TechDal == null)
				{
					_R_UserInfo_TechDal = DalFactory.GetR_UserInfo_TechDal();
				}
				return _R_UserInfo_TechDal;
			}
		}
	
		private IRoleDal _RoleDal;
		public IRoleDal RoleDal
		{
			get
			{
				if (_RoleDal == null)
				{
					_RoleDal = DalFactory.GetRoleDal();
				}
				return _RoleDal;
			}
		}
	
		private ITechDal _TechDal;
		public ITechDal TechDal
		{
			get
			{
				if (_TechDal == null)
				{
					_TechDal = DalFactory.GetTechDal();
				}
				return _TechDal;
			}
		}
	
		private ITechTypeDal _TechTypeDal;
		public ITechTypeDal TechTypeDal
		{
			get
			{
				if (_TechTypeDal == null)
				{
					_TechTypeDal = DalFactory.GetTechTypeDal();
				}
				return _TechTypeDal;
			}
		}
	
		private IUserInfoDal _UserInfoDal;
		public IUserInfoDal UserInfoDal
		{
			get
			{
				if (_UserInfoDal == null)
				{
					_UserInfoDal = DalFactory.GetUserInfoDal();
				}
				return _UserInfoDal;
			}
		}
	}

}