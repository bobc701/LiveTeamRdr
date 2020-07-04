using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LiveTeamRdrApi.BusinessLogic {

   public class ZLeagueStats {

      public int yearID { get; set; }
      public string lgID { get; set; }
      public Nullable<int> AB { get; set; }
      public Nullable<int> H { get; set; }
      public Nullable<int> B2 { get; set; }
      public Nullable<int> B3 { get; set; }
      public Nullable<int> HR { get; set; }
      public Nullable<int> BB { get; set; }
      public Nullable<int> IBB { get; set; }
      public Nullable<int> SO { get; set; }
      public Nullable<int> SB { get; set; }
      public Nullable<int> CS { get; set; }
      public Nullable<int> HBP { get; set; }
      public Nullable<int> SH { get; set; }
      public Nullable<int> SF { get; set; }
      public Nullable<int> IPouts { get; set; }


   }

}