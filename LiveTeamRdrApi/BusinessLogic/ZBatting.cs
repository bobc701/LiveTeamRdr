using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataAccess;

namespace LiveTeamRdrApi.BusinessLogic {

   public class ZBatting {
      // --------------------------------------------------


      public string playerID { get; set; }
      public string lgID { get; set; }
      public int yearID { get; set; }
      public string ZTeam { get; set; }
      public string nameLast { get; set; }
      public string nameFirst { get; set; }
      public string UseName { get; set; }
      public string UseName2 { get; set; }
      public string bats { get; set; }
      public string throws { get; set; }
      public string PlayerCategory { get; set; }
      public int? G { get; set; }
      public int? AB { get; set; }
      public int? R { get; set; }
      public int? H { get; set; }
      public int? B2 { get; set; }
      public int? B3 { get; set; }
      public int? HR { get; set; }
      public Nullable<int> RBI { get; set; }
      public Nullable<int> SB { get; set; }
      public Nullable<int> CS { get; set; }
      public Nullable<int> BB { get; set; }
      public Nullable<int> SO { get; set; }
      public Nullable<int> IBB { get; set; }
      public Nullable<int> HBP { get; set; }
      public Nullable<int> SH { get; set; }
      public Nullable<int> SF { get; set; }
      public Nullable<int> GIDP { get; set; }

      public int? PA { get => AB + BB + HBP + SH + SF; }

      public int slot { get; set; }
      public int posn { get; set; }
      public int slotDh { get; set; }
      public int posnDh { get; set; }

      public string SkillStr { get; set; } = "---------";

      double Div(double? n, double? m) {
         // ---------------------------------------------
         if (!n.HasValue || !m.HasValue) return 0.0;
         if (m == 0.0) return 0.0;
         return Math.Round((double)n / (double)m, 3);
      }

      // These stats are used in the deveopment of the default lineups...
      public double stat_SB { get => Div(SB, SB + CS); }
      public double stat_SLUG { get => Div(H + B2 + 2 * B3 + 3 * HR, AB); }
      public double stat_OBP { get => Div(H + BB, AB + BB); }
      public double stat_HAve { get => Div(H + 0.5 * B2 + B3 + 1.5 * HR + 0.67 * BB, AB + BB); }
      public double stat_NRAve { get => Div(R - HR, AB + BB); }

      public double p_h { get; set; }
      public double p_2b { get; set; }
      public double p_3b { get; set; }
      public double p_hr { get; set; }
      public double p_bb { get; set; }
      public double p_so { get; set; }
      public double p_sb { get; set; }


      // This allows us to say bat["SLUG"] i/o bat.stat_SLUG
      public double this[string ix] {
         // ---------------------------------------------------
         get {
            return ix switch
            {
               "SBPct" => stat_SB,
               "SLUG" => stat_SLUG,
               "OBP" => stat_OBP,
               "HAve" => stat_HAve,
               "NRAve" => stat_NRAve,
               _ => 0.0
            };
         }


      }

   }

}
