using Document.Mgr.EFDAL;
using Document.Mgr.IDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Document.Mgr.DALFactory
{
    public partial class DbSession : IDbSession
    {
        public int SaveChanges()
        {
            return DbContextFactory.GetCurrentDbContext().SaveChanges();
        }
    }
}
