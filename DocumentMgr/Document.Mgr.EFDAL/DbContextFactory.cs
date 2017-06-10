using Document.Mgr.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Document.Mgr.EFDAL
{
    /// <summary>
    /// 保证EF的上下文。线程内实例唯一，一次请求一个实例。
    /// </summary>
    public class DbContextFactory
    {
        public static DbContext GetCurrentDbContext()
        {
            DbContext db = CallContext.GetData("DbContext") as DbContext;
            if (db == null)
            {
                db = new DocumentModelContainer();
                CallContext.SetData("DbContext", db);
            }
            return db;
        }

    }
}
