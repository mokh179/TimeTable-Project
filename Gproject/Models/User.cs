//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Gproject.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class User
    {
        public string userName { get; set; }
        public string password { get; set; }
        public string Name { get; set; }
        public int governateNumber { get; set; }
        public int typeNumber { get; set; }
        public int roleNumber { get; set; }
        public int levelNumber { get; set; }
    
        public virtual Governate Governate { get; set; }
        public virtual Level Level { get; set; }
        public virtual Role Role { get; set; }
        public virtual Type Type { get; set; }
    }
}
