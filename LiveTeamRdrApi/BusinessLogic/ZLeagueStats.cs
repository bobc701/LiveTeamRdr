using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LiveTeamRdrApi.BusinessLogic {

   public class ZLeagueStats {

      public int yearID { get; set; }
      public string lgID { get; set; }
      public int ab { get; set; }
      public int h { get; set; }
      public int b2 { get; set; }
      public int b3 { get; set; }
      public int hr { get; set; }
      public int bb { get; set; }
      public int ibb { get; set; }
      public int so { get; set; }
      public int sb { get; set; }
      public int cs { get; set; }
      public int hbp { get; set; }
      public int sh { get; set; }
      public int sf { get; set; }
      public int ipOuts { get; set; }

      public int pa { get => ab + bb + hbp + sh + sf; }

   }

}