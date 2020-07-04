using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataAccess;
using System.Data.Entity.Core.Objects;
using System.Web.UI.WebControls.WebParts;

namespace LiveTeamRdrApi.BusinessLogic {

   public static class Mapping {

      public static List<ZBatting> ToZBatting(this ObjectResult<Batting1_app_Result> listIn) {
         // ---------------------------------------------------------------------
         var result = new List<ZBatting>();
         ZBatting statsOut;
         foreach (var statsIn in listIn) {
            statsOut = new ZBatting() {

               playerID = statsIn.playerID,
               lgID = statsIn.lgID,
               yearID = statsIn.yearID,
               ZTeam = statsIn.ZTeam,
               nameLast = statsIn.nameLast,
               nameFirst = statsIn.nameFirst,
               UseName = statsIn.UseName,  // UseName is for play-by-play, 'Judge'
               UseName2 = statsIn.nameFirst.Substring(0, 1) + "." + statsIn.nameLast, // UseName2 is for box scores, 'A.Judge'
               bats = statsIn.bats,
               throws = statsIn.throws,
               PlayerCategory = statsIn.PlayerCategory,
               G = statsIn.G,
               AB = statsIn.AB,
               R = statsIn.R,
               H = statsIn.H,
               B2 = statsIn.C2B,
               B3 = statsIn.C3B,
               HR = statsIn.HR,
               RBI = statsIn.RBI,
               SB = statsIn.SB,
               CS = statsIn.CS,
               BB = statsIn.BB,
               SO = statsIn.SO,
               IBB = statsIn.IBB,
               HBP = statsIn.HBP,
               SH = statsIn.SH,
               SF = statsIn.SF,
               GIDP = statsIn.GIDP,

               slot = 0,
               posn = 0,
               slotDh = 0,
               posnDh = 0,

               SkillStr = "---------",

               p_h = 0.0,
               p_2b = 0.0,
               p_3b = 0.0,
               p_hr = 0.0,
               p_bb = 0.0,
               p_so = 0.0,
               p_sb = 0.0

            };
            result.Add(statsOut);

         }
         return result;
      }


      public static List<ZGamesByPosn> ToZGamesByPosn(this ObjectResult<GamesByPosn1_app_Result> listIn) {
         // ---------------------------------------------------------------------
         var result = new List<ZGamesByPosn>();
         ZGamesByPosn statsOut;
         foreach (var statsIn in listIn) {
            statsOut = new ZGamesByPosn() {

               playerID = statsIn.playerID,
               g = statsIn.G_all,
               gs = statsIn.GS,
               p = statsIn.G_p,
               c = statsIn.G_c,
               b1 = statsIn.G_1b,
               b2 = statsIn.G_2b,
               b3 = statsIn.G_3b,
               ss = statsIn.G_ss,
               lf = statsIn.G_lf,
               cf = statsIn.G_cf,
               rf = statsIn.G_rf,
               of = statsIn.G_of,
               dh = statsIn.G_dh,

               BBRefFielding = "",

               r1 = 0.0,
               r2 = 0.0,
               r3 = 0.0,
               r4 = 0.0,
               r5 = 0.0,
               r6 = 0.0,
               r7 = 0.0,
               r8 = 0.0,
               r9 = 0.0,

               SkillStr = ""
            };
            result.Add(statsOut);

         }
         return result;
      }


      public static List<ZPitching> ToZPitching(this ObjectResult<Pitching1_app_Result> listIn) {
         // ---------------------------------------------------------------------
         var result = new List<ZPitching>();
         ZPitching statsOut;
         foreach (var statsIn in listIn) {
            statsOut = new ZPitching() {

               PlayerID = statsIn.playerID,
               BFP = statsIn.BFP,
               G = statsIn.G,
               GS = statsIn.GS,
               IPouts = statsIn.IPouts, // IPx3
               H = statsIn.H,
               HR = statsIn.HR,
               BB = statsIn.BB,
               IBB = statsIn.IBB,
               SO = statsIn.SO,
               HBP = statsIn.HBP,
               SV = statsIn.SV,
               W = statsIn.W,
               L = statsIn.L,
               R = null,
               ER = statsIn.ER,
               ERA = statsIn.ERA,
               WP = statsIn.WP,
               BK = null,
               p_h = 0.0,
               p_hr = 0.0,
               p_bb = 0.0,
               p_so = 0,
               rotation = null


            };
            result.Add(statsOut);

         }
         return result;

      }


      public static List<ZFieldingYear> ToZFieldingYear(this ObjectResult<FieldingYear1_app_Result> listIn) {
         // ---------------------------------------------------------------------
         var result = new List<ZFieldingYear>();
         ZFieldingYear statsOut;
         foreach (var statsIn in listIn) {
            statsOut = new ZFieldingYear() {

               playerID = statsIn.playerID,
               year = statsIn.year,
               ZTeam = statsIn.ZTeam,
               Posn = statsIn.Posn,
               inn = statsIn.inn,
               po = statsIn.po,
               a = statsIn.a,
               e = statsIn.e,
               dp = statsIn.dp,
               Rtot = (int)statsIn.Rtot,
               RtotYr = (int)statsIn.Rtot,
               Skill = (int)statsIn.Skill

            };
            result.Add(statsOut);

         }
         return result;

      }


      public static ZLeagueStats ToZLeagueStats(this LeagueStats1_Result statsIn) {
         // ---------------------------------------------------------------------
         var result = new ZLeagueStats();
         ZLeagueStats statsOut;
         statsOut = new ZLeagueStats() {

            yearID = statsIn.yearID,
            lgID = statsIn.lgID,
            AB = statsIn.AB,
            H = statsIn.H,
            B2 = statsIn.C2B,
            B3 = statsIn.C3B,
            HR = statsIn.HR,
            BB = statsIn.BB,
            IBB = statsIn.IBB,
            SO = statsIn.SO,
            SB = statsIn.SB,
            CS = statsIn.CS,
            HBP = statsIn.HBP,
            SH = statsIn.SH,
            SF = statsIn.SF,
            IPouts = statsIn.IPouts
         };
         return statsOut;

      }

   }

}
