//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace Document.Mgr.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class LevelDescription
    {
        public LevelDescription()
        {
            this.Level = 0;
            this.R_UserInfo_Tech = new HashSet<R_UserInfo_Tech>();
        }
    
        public int Id { get; set; }
        public int Level { get; set; }
        public string Description { get; set; }
    
        public virtual ICollection<R_UserInfo_Tech> R_UserInfo_Tech { get; set; }
    }
}
