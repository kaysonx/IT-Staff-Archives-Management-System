 
using Document.Mgr.IDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Document.Mgr.DALFactory
{
    /// <summary>
    /// ³éÏó¹¤³§
    /// </summary>
    public partial class DalFactory
    {
   
	
		
		public static ICityDal GetCityDal()
        {
            
            object obj = Assembly.Load(AssemblyName).CreateInstance(AssemblyName + ".CityDal", true);

            return obj as ICityDal;
        }
	
		
		public static IContactDal GetContactDal()
        {
            
            object obj = Assembly.Load(AssemblyName).CreateInstance(AssemblyName + ".ContactDal", true);

            return obj as IContactDal;
        }
	
		
		public static ICountryDal GetCountryDal()
        {
            
            object obj = Assembly.Load(AssemblyName).CreateInstance(AssemblyName + ".CountryDal", true);

            return obj as ICountryDal;
        }
	
		
		public static IGroupDal GetGroupDal()
        {
            
            object obj = Assembly.Load(AssemblyName).CreateInstance(AssemblyName + ".GroupDal", true);

            return obj as IGroupDal;
        }
	
		
		public static ILevelDescriptionDal GetLevelDescriptionDal()
        {
            
            object obj = Assembly.Load(AssemblyName).CreateInstance(AssemblyName + ".LevelDescriptionDal", true);

            return obj as ILevelDescriptionDal;
        }
	
		
		public static IPositionDal GetPositionDal()
        {
            
            object obj = Assembly.Load(AssemblyName).CreateInstance(AssemblyName + ".PositionDal", true);

            return obj as IPositionDal;
        }
	
		
		public static IR_UserInfo_TechDal GetR_UserInfo_TechDal()
        {
            
            object obj = Assembly.Load(AssemblyName).CreateInstance(AssemblyName + ".R_UserInfo_TechDal", true);

            return obj as IR_UserInfo_TechDal;
        }
	
		
		public static IRoleDal GetRoleDal()
        {
            
            object obj = Assembly.Load(AssemblyName).CreateInstance(AssemblyName + ".RoleDal", true);

            return obj as IRoleDal;
        }
	
		
		public static ITechDal GetTechDal()
        {
            
            object obj = Assembly.Load(AssemblyName).CreateInstance(AssemblyName + ".TechDal", true);

            return obj as ITechDal;
        }
	
		
		public static ITechTypeDal GetTechTypeDal()
        {
            
            object obj = Assembly.Load(AssemblyName).CreateInstance(AssemblyName + ".TechTypeDal", true);

            return obj as ITechTypeDal;
        }
	
		
		public static IUserInfoDal GetUserInfoDal()
        {
            
            object obj = Assembly.Load(AssemblyName).CreateInstance(AssemblyName + ".UserInfoDal", true);

            return obj as IUserInfoDal;
        }
	}

}