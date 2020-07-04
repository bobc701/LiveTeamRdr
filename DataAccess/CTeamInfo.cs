using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DataAccess {

   public class CTeamInfo {

      public string Team { get; set; }
      public int YearID { get; set; }
      public string LgID { get; set; }
      public string LineName { get; set; }
      public string City { get; set; }
      public string NickName { get; set; }
      public CBattingStats leagueStats { get; set; } // Includes ipOuts
      public List<CPlayerInfo> PlayerInfo { get; set; }

   }


   public class CPlayerInfo {
      public string UseName { get; set; }
      public string UseName2 { get; set; }
      public string SkillStr { get; set; }
      public char Playercategory { get; set; } //(1/2) or(B/P) : char
      public int slot { get; set; }
      public int posn { get; set; }
      public int slotdh { get; set; }
      public int posnDh { get; set; }
      public CBattingStats battingStats { get; set; }
      public CPitchingStats pitchingStats { get; set; } //(if 2, null if 1)
   }


   public class CBattingStats {
      public int? pa { get; set; }
      public int? ab { get; set; }
      public int? h { get; set; }
      public int? b2 { get; set; }
      public int? b3 { get; set; }
      public int? hr { get; set; }
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

   public class CPitchingStats {
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

