using DataAccess;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace LiveTeamRdrApi.BusinessLogic {

   public class CTeamBldr {

      public ZTeam zteam1 { get; set; }
      public List<ZBatting> zbatting1 { get; set; }
      public List<ZGamesByPosn> zgamesByPosn1 { get; set; }
      public List<ZPitching> zpitching1 { get; set; }
      public ZLeagueStats zleagueStats1 { get; set; }
      public List<ZFielding> zfielding1 { get; set; }
      public List<ZFieldingYear> zfieldingYear1 { get; set; }



      public CTeamInfo ConstructTeam(string teamTag, int year) {
      // -----------------------------------------------------------
         var ctx = new DB_133455_mlbhistoryEntities1();

         zteam1 = ctx.ZTeams.Where(t => t.ZTeam1 == teamTag && t.yearID == year).First();
         zbatting1 = ctx.Batting1_app(teamTag, year).ToZBatting(); 
         zpitching1 = ctx.Pitching1_app(teamTag, year).ToZPitching();
         zgamesByPosn1 = ctx.GamesByPosn1_app(teamTag, year).ToZGamesByPosn();
         zfielding1 = ctx.ZFieldings.ToList();
         zfieldingYear1 = ctx.FieldingYear1_app(teamTag, year).ToZFieldingYear();
         zleagueStats1 = ctx.LeagueStats1(year, zteam1.lgID).First().ToZLeagueStats(); 

         DupeUseNames();
         DupeUseNames2();

         FillSkillStr();     // This initializes with 3's based on GamesByPosn.
         ComputeSkillStr2(); // This This overlays with 'actual' skills using CF_FieldingYear.
         ComputeSkillStr();  // This will override with subjective skill's in CF_Fielding.

         ComputeDefense(dh: false);
         ComputeLineup(dh: false);
         ComputeDefense(dh: true);
         ComputeLineup(dh: true);

         var team = new CTeamInfo();
         WriteCDB(team);

         return team;

      }

      private const int MAX_BATTERS = 14;
      private const int MAX_STARTERS = 6;
      private const int MAX_CLOSERS = 2;
      private const int MAX_OTHERS = 3;
      private const int MAX_ROSTER = 25;




      private void WriteCDB(CTeamInfo team) {
         // ------------------------------------------
         var listB = new List<string>();
         var listP = new List<string>();
         IEnumerable<ZBatting> listb;
         IEnumerable<ZPitching> listp;

         void AddToList(List<string> list1, string playerId) {
            // -----------------------------------------------------------
            if (!list1.Contains(playerId)) {
               list1.Add(playerId);
            }
         }

         // --------------------------------------------
         // Build ListB and ListP with playerID's
         // --------------------------------------------

         // -- Starters (1 pitcher + batters w/ posn, slot, posndh, or slotdh (to ListP & ListB)

         listb = zbatting1.Where(bat => bat.slot != 0 || bat.slotDh != 0);
         foreach (ZBatting bat in listb) {
            if (bat.posn == 1 || bat.posnDh == 1) AddToList(listP, bat.playerID);
            else AddToList(listB, bat.playerID);
         }

         // -- Pitchers (to ListP)

         listp = zpitching1.OrderByDescending(pit => pit.GS);
         foreach (ZPitching pit in listp) {
            if (listP.Count >= MAX_STARTERS) break;
            AddToList(listP, pit.PlayerID);
         }

         // -- Closers (to ListP)

         listp = zpitching1.OrderByDescending(pit => pit.SV);
         foreach (ZPitching pit in listp) {
            if (listP.Count >= MAX_STARTERS + MAX_CLOSERS) break;
            AddToList(listP, pit.PlayerID);
         }

         // -- Other pitchers (to ListP)

         listp = zpitching1.OrderByDescending(pit => pit.IPouts);
         foreach (ZPitching pit in listp) {
            if (listP.Count >= MAX_STARTERS + MAX_CLOSERS + MAX_OTHERS) break;
            AddToList(listP, pit.PlayerID);
         }

         // -- More batters... (to listB)

         listb = zbatting1.OrderByDescending(bat => bat.AB);
         foreach (ZBatting bat in listb) {
            if (listB.Count + listP.Count >= MAX_ROSTER) break;
            AddToList(listB, bat.playerID);
         }


         // --------------------------------------
         // Construct the CTeamInfo object
         // --------------------------------------

         // new CTeamInfo: 'team' (See above)

         // Enter scalary data: name, cit, nickname, etc

         team.Team = zteam1.ZTeam1;
         team.YearID = zteam1.yearID;
         team.City = zteam1.City;
         team.NickName = zteam1.NickName;
         team.LineName = zteam1.LineName;

         // team.LeagueStats: new CBattingStats object, add scalars: pa, ab, etc

         team.leagueStats = new CBattingStats {
            pa = zleagueStats1.PA,
            ab = zleagueStats1.AB,
            h = zleagueStats1.H,
            b2 = zleagueStats1.B2,
            b3 = zleagueStats1.B3,
            hr = zleagueStats1.HR,
            so = zleagueStats1.SO,
            sh = zleagueStats1.SH,
            sf = zleagueStats1.SF,
            bb = zleagueStats1.BB,
            ibb = zleagueStats1.IBB,
            hbp = zleagueStats1.HBP,
            sb = zleagueStats1.SB,
            cs = zleagueStats1.CS,
            ipOuts = zleagueStats1.IPouts
         };


         // team.PlayerInfo: new List<CPlayerInfo>()

         team.PlayerInfo = new List<CPlayerInfo>();
         var listBoth = listB.Concat(listP);
         foreach (string id in listBoth) {
            var bat1 = zbatting1.First(b => b.playerID == id);
            var pit1 = zpitching1.FirstOrDefault(p => p.PlayerID == id); // Will be null for non-pitcher

            var player = new CPlayerInfo() {
               UseName = bat1.UseName,
               UseName2 = bat1.UseName2,
               SkillStr = bat1.SkillStr,
               Playercategory = pit1 == null ? 'B' : 'P',
               slot = bat1.slot,
               posn = bat1.posn,
               slotdh = bat1.slotDh,
               posnDh = bat1.posnDh,
               battingStats = new CBattingStats {
                  pa = bat1.PA,
                  ab = bat1.AB,
                  h = bat1.H,
                  b2 = bat1.B2,
                  b3 = bat1.B3,
                  hr = bat1.HR,
                  so = bat1.SO,
                  sh = bat1.SH,
                  sf = bat1.SF,
                  bb = bat1.BB,
                  ibb = bat1.IBB,
                  hbp = bat1.HBP,
                  sb = bat1.SB,
                  cs = bat1.CS,
                  ipOuts = null // Only for league stats
               },
               pitchingStats = pit1 == null ? null : new CPitchingStats {
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
               }

            };
            team.PlayerInfo.Add(player);
         }

      }

   }

}


private void ComputeLineup(bool dh) {
      // ------------------------------------------------------
      // We have already filled posn and posnDh, and so now we
      // will assign those players with slot and slotDh, respectively.
      // ------------------------------------------------------
         var sIDList = new List<string>();

         IEnumerable<ZBatting> list;
         if (dh)
            list = zbatting1.Where(bat => bat.posnDh >= 1 && bat.posnDh <= 10);
         else
            list = zbatting1.Where(bat => bat.posn >= 1 && bat.posn <= 9);


         string GetMaxStat(string stat) {
         // ---------------------------------------------------
            var list1 = list.Where(bat => !sIDList.Contains(bat.playerID));
            double max = list1.Max(bat => bat[stat]);
            //if (max == 0.0) return "";
            string id1 = list1.First(bat => bat[stat] == max).playerID;
            return id1;
         }


         void PostSlot(string id, int slot) {
         // ---------------------------------------------------
            if (dh) zbatting1.First(bat => bat.playerID == id).slotDh = slot;
            else zbatting1.First(bat => bat.playerID == id).slot = slot;
            sIDList.Add(id);
         }


      // Let's do the pitcher first...
         string id1;
         if (dh) {
            id1 = list.First(bat => bat.posnDh == 1).playerID;
            PostSlot(id1, 10); //Picher does not bat if DH rule.
         }
         else {
            id1 = list.First(bat => bat.posn == 1).playerID;
            PostSlot(id1, 9); // Pitcher bats 9th if no DH rule.
         }

         // Find player with highest slugging pct and bat him 4th...
         id1 = GetMaxStat("SLUG");
         PostSlot(id1, 4);

         // Find player with highest hitting ave and bat him 3th...
         id1 = GetMaxStat("HAve");
         PostSlot(id1, 3);

         // Find player with highest net run ave and bat him 1st...
         id1 = GetMaxStat("NRAve");
         PostSlot(id1, 1);

         // Find player with next highest net run ave and bat him 2th...
         id1 = GetMaxStat("NRAve");
         PostSlot(id1, 2);

         // Find player with next highest slugging pct and bat him 5th...
         id1 = GetMaxStat("SLUG");
         PostSlot(id1, 5);

         // Find player with next highest hitting ave and bat him 6th...
         id1 = GetMaxStat("HAve");
         PostSlot(id1, 6);

         // Find player with next highest hitting ave and bat him 7th...
         id1 = GetMaxStat("HAve");
         PostSlot(id1, 7);

         // Find player with next highest hitting ave and bat him 8th...
         id1 = GetMaxStat("HAve");
         PostSlot(id1, 8);

         // if DH rules, then find player with next highest hitting ave
         // and bat him 9th...
         if (dh) {
            id1 = GetMaxStat("HAve");
            PostSlot(id1, 9);
         }

      }


      private void ComputeDefense(bool dh) {
      // -------------------------------------------------
         var sIdList = new List<string>();
         string id = "";
         bool missingDH = false;

         string GetMaxGbp(string posn) {
            // ---------------------------------------------------
            var list = zgamesByPosn1.Where(g => !sIdList.Contains(g.playerID));
            int max = list.Max(g1 => g1[posn]);
            if (max == 0) return "";
            string id1 = list.First(g => g[posn] == max).playerID;
            return id1;

         }

         string GetMaxGS() {
            // ---------------------------------------------------
            var list = zpitching1.Where(pit => !sIdList.Contains(pit.PlayerID));
            int max = list.Max(pit1 => pit1.GS) ?? 0;
            if (max == 0) return "";
            string id1 = list.First(pit => pit.GS == max).PlayerID;
            return id1;
         }


         void PostPosn(string id1, int pos) {
            // -------------------------------------------------
            ZBatting bat = zbatting1.First(b => b.playerID == id1);
            if (dh)
               bat.posnDh = pos;
            else
               bat.posn = pos;
            sIdList.Add(id1);

         }

         // Before we start, reset posn fields to 0...
         foreach (ZBatting bat in zbatting1) {
            if (dh) bat.posnDh = 0;
            else bat.posn = 0;
         }

         // First assign the DH...
         if (dh) {
            id = GetMaxGbp("dh");
            missingDH = id == ""; // See below, we'll do this last.
            if (id != "") PostPosn(id, 10);  //posn = 10 mean non-fielder (DH)
         }

         // Get the starting pitcher, as one with most starts...
         //id = GetMax("gs");
         //if (id == "") id = GetMax("p");
         //PostPosn(id, 1);

         // GS is not in Lahman's Appearances before 1973, so use Pitching...
         id = GetMaxGS();
         PostPosn(id, 1);

         id = GetMaxGbp("ss");
         PostPosn(id, 6);

         id = GetMaxGbp("b2");
         PostPosn(id, 4);

         id = GetMaxGbp("b3");
         PostPosn (id, 5);

         id = GetMaxGbp("b1");
         PostPosn (id, 3);

         id = GetMaxGbp("c");
         PostPosn(id, 2);

         // Outfield...
         // Unfortunately, Lahman's Fielding table does not
         // split out RF, CF,  LF except in last couple of years.
         // Before that it is all "OF"

         id = GetMaxGbp("cf");
         if (id == "") id = GetMaxGbp("OF");
         PostPosn(id, 8);

         id = GetMaxGbp("rf");
         if (id == "") id = GetMaxGbp("OF");
         PostPosn(id, 9);

         id = GetMaxGbp("lf");
         if (id == "") id = GetMaxGbp("OF");
         PostPosn(id, 7);

         if (dh && missingDH) {
         // This will happen in years before dh was used.
         // We need to identify one anyway!
            id = GetMaxGbp("of"); // Use 'of' for this purpose.
            PostPosn(id, 10);
         }

      }


      private int GetSkill(int posn, double? inn, int? RtotYr) {
         // -------------------------------------------------------------
         // This is algorism to convert Rtot/Yr into fielding skill 0..6, centered
         var bracket = new int[6];
         double r;
         int res;

         // Credibility: Consider 400 inn as fully credible.
         // If < 400 inn, weight with 0.
         // So if very few inn, most likely r will be 0, which will translate to '3'.
         if (RtotYr == null || inn == null) return 3;
         if (RtotYr == 0) return 3;
         r = (double)(inn >= 400 ? RtotYr : inn / 400.0 * RtotYr);
         switch (posn) {
            case 1: bracket = new int[] { -5, -4, -3, 3, 4, 5 }; break;
            case 2: bracket = new int[] { -25, -20, -10, 10, 15, 20 }; break;
            case 3: bracket = new int[] { -10, -8, -5, 7, 10, 15 }; break;
            case 4: bracket = new int[] { -15, -12, -6, 6, 12, 20 }; break;
            case 5: bracket = new int[] { -15, -12, -6, 6, 12, 20 }; break;
            case 6: bracket = new int[] { -15, -12, -6, 6, 12, 20 }; break;
            case 7: bracket = new int[] { -25, -18, -10, 10, 15, 20 }; break;
            case 8: bracket = new int[] { -25, -18, -10, 10, 15, 20 }; break;
            case 9: bracket = new int[] { 25, -18, -10, 10, 15, 20 }; break;
         }

         res = 6;
         for (int i = 0; i <= 5; i++) {
            if (r < bracket[i]) {
               res = i;
               break;
            }
         }

      // Due to questionable reliability, lets just constrain this to
      // 2, 3, 4... (-bc 1907)
         switch (res) {
            case 0: return 2;
            case 1: return 2; 
            case 2: return 3; 
            case 3: return 3; 
            case 4: return 3; 
            case 5: return 4; 
            case 6: return 4;
            default: return 3;
         }

      }


      private void ComputeSkillStr() {
         // --------------------------------------------------------
         // TASK: Update SkillStr in zbatting1 from data in ZFieldings,
         // which is 1 rec for each player, with a field for each posn, p1, ... ,p9.
         // Data is manually (and subjectively) maintained in PackageWriter.mdb
         // and must be imported into SQL.
         // --------------------------------------------------------
         StringBuilder s = null;

         foreach (ZBatting bat in zbatting1) {
            var fld = zfielding1.FirstOrDefault(f => f.playerID == bat.playerID);
            if (fld == null) continue;
            s = new StringBuilder(bat.SkillStr);
            if (fld.p1 != null) s[0] = fld.p1.ToString()[0];
            if (fld.p2 != null) s[1] = fld.p2.ToString()[0];
            if (fld.p3 != null) s[2] = fld.p3.ToString()[0];
            if (fld.p4 != null) s[3] = fld.p4.ToString()[0];
            if (fld.p5 != null) s[4] = fld.p5.ToString()[0];
            if (fld.p6 != null) s[5] = fld.p6.ToString()[0];
            if (fld.p7 != null) s[6] = fld.p7.ToString()[0];
            if (fld.p8 != null) s[7] = fld.p8.ToString()[0];
            if (fld.p9 != null) s[8] = fld.p9.ToString()[0];

            bat.SkillStr = s.ToString();
         }


      }


      private void ComputeSkillStr2() {
      // ------------------------------------------------------
      // TASK: Update SkillStr in zbatting1 from data in ZFieldingYears,
      // which is 1 rec for each player, team, year, and pos.
      // This table is maintained outside of Lahman, currently have
      // used BBRef fielding text files from (I think) 2015 to 2019.
      // ------------------------------------------------------
         StringBuilder s;
         foreach (ZBatting bat in zbatting1) {
            s = new StringBuilder(bat.SkillStr);
            var fld = zfieldingYear1.Where(f =>
               f.playerID == bat.playerID && f.ZTeam == bat.ZTeam && f.year == bat.yearID);
            foreach (ZFieldingYear f in fld) {
               f.Skill = GetSkill(f.Posn, f.inn, f.RtotYr);
               s[f.Posn - 1] = f.Skill.ToString()[0];
            }
            bat.SkillStr = s.ToString();

         }
      }



         private void FillSkillStr() {
         // ------------------------------------------------------
         // TASK:
         // Build 's' using GBP, then post it to ZBatter, matching 
         // on playerID.
         // ------------------------------------------------------

         StringBuilder s;
         foreach (var bat in zbatting1) {

            var gbp = zgamesByPosn1.FirstOrDefault(g => g.playerID == bat.playerID);
            if (gbp == null)
               bat.SkillStr = "---------";
            else {
               s = new StringBuilder("---------");
               if (gbp.p > 0 || bat.PlayerCategory == "P") s[0] = '3';
               if (gbp.c > 0) s[1] = '3';
               if (gbp.b1 > 0) s[2] = '3';
               if (gbp.b2 > 0) s[3] = '3';
               if (gbp.b3 > 0) s[4] = '3';
               if (gbp.ss > 0) s[5] = '3';
               if (gbp.lf > 0) s[6] = '3';
               if (gbp.cf > 0) s[7] = '3';
               if (gbp.rf > 0) s[8] = '3';

               // In absence of specific OF position, we'll use the
               // OF position and apply it to all 3...
               if (gbp.lf == 0 && gbp.cf == 0 && gbp.rf == 0 && gbp.of != 0) {
                  s[6] = '3';
                  s[7] = '3';
                  s[8] = '3';
               }
            }
         }

      }


      private void DupeUseNames() {
      // ------------------------------------------
      // TASK: Eliminate dupes in UseName.

      // Go through this 3 times...
         for (int i = 1; i <= 3; i++) {

            var x =
               (from bat in zbatting1
                group bat by bat.UseName into grouping
                where grouping.Count() >= 2
                select grouping);

            if (x.Count() == 0) break;
            foreach (var g in x) {
               zbatting1
                  .Where(bat => bat.UseName == g.Key).ToList()
                  .ForEach(bat => bat.UseName = bat.nameFirst.Substring(0, i) + "." + bat.nameLast);
            }

         }

      }


      private void DupeUseNames2() {
         // ------------------------------------------
         // TASK: Eliminate dupes in UseName2, which is already initialized as A.Judge...

         // Go through this 2 times...
         for (int i = 2; i <= 3; i++) {

            var x =
               (from bat in zbatting1
                group bat by bat.UseName2 into grouping
                where grouping.Count() >= 2
                select grouping);

            if (x.Count() == 0) break;
            foreach (var g in x) {
               zbatting1
                  .Where(bat => bat.UseName2 == g.Key).ToList()
                  .ForEach(bat => bat.UseName2 = bat.nameFirst.Substring(0, i) + "." + bat.nameLast);
            }

         }

      }


   }




}