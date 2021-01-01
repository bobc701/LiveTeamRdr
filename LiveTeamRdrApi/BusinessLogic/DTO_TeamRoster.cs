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
      public DTO_BattingStats leagueStats { get; set; }    //new for b12, custom teams


      public DTO_PlayerInfo() {
      // -------------------------------------------------
      // Class members must be assigned through properties.

      }


      public DTO_PlayerInfo(ZBatting bat1, ZPitching pit1, DataAccess.LeagueStat lg1) {
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
            ab = bat1.ab,
            h = bat1.h,
            b2 = bat1.b2,
            b3 = bat1.b3,
            hr = bat1.hr,
            rbi = bat1.rbi,
            so = bat1.so,
            sh = bat1.sh,
            sf = bat1.sf,
            bb = bat1.bb,
            ibb = bat1.ibb,
            hbp = bat1.hbp,
            sb = bat1.sb,
            cs = bat1.cs,
            ipOuts = -1 // Only for league stats
         };
         if (pit1 != null)
            pitchingStats = new DTO_PitchingStats {
               g = pit1.g,
               gs = pit1.gs,
               w = pit1.w,
               l = pit1.l,
               bfp = pit1.bfp,
               ipOuts = pit1.ipOuts,
               h = pit1.h,
               er = pit1.er,
               hr = pit1.hr,
               so = pit1.so,
               bb = pit1.bb,
               ibb = pit1.ibb,
               sv = pit1.sv
            };
         leagueStats = new DTO_BattingStats {
            //pa = lg1.PA,
            ab = lg1.AB,
            h = lg1.H,
            b2 = lg1.C2B,
            b3 = lg1.C3B,
            hr = lg1.HR,
            //rbi = lg1.rbi,
            so = (int)lg1.SO,
            sh = (int)lg1.SH,
            sf = (int)lg1.SF,
            bb = (int)lg1.BB,
            ibb = (int)lg1.IBB,
            hbp = (int)lg1.HBP,
            sb = (int)lg1.SB,
            cs = (int)lg1.CS,
            ipOuts = (int)lg1.IPouts

         };
      }

   }


   public class DTO_BattingStats {
      public int pa { get; set; }
      public int ab { get; set; }
      public int h { get; set; }
      public int b2 { get; set; }
      public int b3 { get; set; }
      public int hr { get; set; }
      public int rbi { get; set; }
      public int so { get; set; }
      public int sh { get; set; }
      public int sf { get; set; }
      public int bb { get; set; }
      public int ibb { get; set; }
      public int hbp { get; set; }
      public int sb { get; set; }
      public int cs { get; set; }
      public int ipOuts { get; set; } // For league stats
   }

   public class DTO_PitchingStats {
      public int g { get; set; }
      public int gs { get; set; }
      public int w { get; set; }
      public int l { get; set; }
      public int bfp { get; set; }
      public int ipOuts { get; set; }
      public int h { get; set; }
      public int er { get; set; }
      public int hr { get; set; }
      public int so { get; set; }
      public int bb { get; set; }
      public int ibb { get; set; }
      public int sv { get; set; }
   }

}

