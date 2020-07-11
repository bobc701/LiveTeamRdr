using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace LiveTeamRdrApi.BusinessLogic {

   public class DTO_TeamRoster {
      public string DataVersion { get; set; } = "V3.0";
      public string Team { get; set; }
      public int YearID { get; set; }
      public string LgID { get; set; }
      public string LineName { get; set; }
      public string City { get; set; }
      public string NickName { get; set; }
      public bool UsesDhDefault { get; set; }
      public int ComplPct { get; set; }
      public DTO_BattingStats leagueStats { get; set; } // Includes ipOuts
      public List<DTO_PlayerInfo> PlayerInfo { get; set; }

   }


   public class DTO_PlayerInfo {

      public string UseName { get; set; }
      public string UseName2 { get; set; }
      public string SkillStr { get; set; }
      public char Playercategory { get; set; } //(1/2) or(B/P) : char
      public int slot { get; set; }
      public int posn { get; set; }
      public int slotdh { get; set; }
      public int posnDh { get; set; }
      public DTO_BattingStats battingStats { get; set; }
      public DTO_PitchingStats pitchingStats { get; set; } //(if 2, null if 1)


      public DTO_PlayerInfo() {
      // -------------------------------------------------
      // Class members must be assigned through properties.

      }


      public DTO_PlayerInfo(ZBatting bat1, ZPitching pit1) {
         // ---------------------------------------------------------
         UseName = bat1.UseName;
         UseName2 = bat1.UseName2;
         SkillStr = bat1.SkillStr;
         Playercategory = pit1 == null ? 'B' : 'P';
         slot = bat1.slot;
         posn = bat1.posn;
         slotdh = bat1.slotDh;
         posnDh = bat1.posnDh;
         battingStats = new DTO_BattingStats {
            pa = bat1.PA,
            ab = bat1.AB,
            h = bat1.H,
            b2 = bat1.B2,
            b3 = bat1.B3,
            hr = bat1.R,
            rbi = bat1.RBI,
            so = bat1.SO,
            sh = bat1.SH,
            sf = bat1.SF,
            bb = bat1.BB,
            ibb = bat1.IBB,
            hbp = bat1.HBP,
            sb = bat1.SB,
            cs = bat1.CS,
            ipOuts = null // Only for league stats
         };
         if (pit1 != null)
            pitchingStats = new DTO_PitchingStats {
               g = pit1.G,
               gs = pit1.GS,
               w = pit1.W,
               l = pit1.L,
               bfp = pit1.BFP,
               ipOuts = pit1.IPouts,
               h = pit1.H,
               er = pit1.ER,
               hr = pit1.HR,
               so = pit1.SO,
               bb = pit1.BB,
               ibb = pit1.IBB,
               sv = pit1.SV
            };
      }

   }


   public class DTO_BattingStats {
      public int? pa { get; set; }
      public int? ab { get; set; }
      public int? h { get; set; }
      public int? b2 { get; set; }
      public int? b3 { get; set; }
      public int? hr { get; set; }
      public int? rbi { get; set; }
      public int? so { get; set; }
      public int? sh { get; set; }
      public int? sf { get; set; }
      public int? bb { get; set; }
      public int? ibb { get; set; }
      public int? hbp { get; set; }
      public int? sb { get; set; }
      public int? cs { get; set; }
      public int? ipOuts { get; set; } // For league stats
   }

   public class DTO_PitchingStats {
      public int? g { get; set; }
      public int? gs { get; set; }
      public int? w { get; set; }
      public int? l { get; set; }
      public int? bfp { get; set; }
      public int? ipOuts { get; set; }
      public int? h { get; set; }
      public int? er { get; set; }
      public int? hr { get; set; }
      public int? so { get; set; }
      public int? bb { get; set; }
      public int? ibb { get; set; }
      public int? sv { get; set; }
   }

}

