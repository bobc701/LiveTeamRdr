//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataAccess
{
    using System;
    using System.Collections.Generic;
    
    public partial class UserTeam
    {
        public string UserName { get; set; }
        public int UserTeamID { get; set; }
        public string TeamName { get; set; }
        public Nullable<int> NumPos { get; set; }
        public Nullable<int> NumPit { get; set; }
        public bool UsesDh { get; set; }
        public bool IsComplete { get; set; }
        public string StatusMsg { get; set; }
    }
}
