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
      public int g { get; set; }
      public int ab { get; set; }
      public int r { get; set; }
      public int h { get; set; }
      public int b2 { get; set; }
      public int b3 { get; set; }
      public int hr { get; set; }
      public int rbi { get; set; }
      public int sb { get; set; }
      public int cs { get; set; }
      public int bb { get; set; }
      public int so { get; set; }
      public int ibb { get; set; }
      public int hbp { get; set; }
      public int sh { get; set; }
      public int sf { get; set; }
      public int? gidp { get; set; }

      public int PA { get => ab + bb + hbp + sh + sf; }

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
      public double stat_SB { get => Div(sb, sb + cs); }
      public double stat_SLUG { get => Div(h + b2 + 2 * b3 + 3 * hr, ab); }
      public double stat_OBP { get => Div(h + bb, ab + bb); }
      public double stat_HAve { get => Div(h + 0.5 * b2 + b3 + 1.5 * hr + 0.67 * bb, ab + bb); }
      public double stat_NRAve { get => Div(r - hr, ab + bb); }

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
