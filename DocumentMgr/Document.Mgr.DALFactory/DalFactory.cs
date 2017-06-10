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
    /// 抽象工厂
    /// </summary>
    public partial class DalFactory
    {
        public static readonly string AssemblyName;

        static DalFactory()
        {
            AssemblyName = System.Configuration.ConfigurationManager.AppSettings["AssemblyName"];
        }
    }
}
