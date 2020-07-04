﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.UI.WebControls;
using DataAccess;
using LiveTeamRdrApi.BusinessLogic;

namespace LiveTeamRdrApi.Controllers
{
   public class TeamController : ApiController {

      // GET: api/Team/{teamTag}/{year:int}
      [Route("api/team/{teamTag}/{year:int}")]
      [HttpGet]
      public CTeamInfo GetTeam(string teamTag, int year) {
      // --------------------------------------------------
         var bldr = new CTeamBldr();
         CTeamInfo team1 = bldr.ConstructTeam(teamTag, year);

         return team1;
      }


      // Thisi s for testing.
      // Just returns zbatting1, so you can inspect it.
      [Route("api/test-batting/{teamTag}/{year:int}")]
      [HttpGet]
      public List<ZBatting> GetTeam2(string teamTag, int year) {
      // --------------------------------------------------
         var bldr = new CTeamBldr();
         CTeamInfo team1 = bldr.ConstructTeam(teamTag, year);

         return bldr.zbatting1;
      }


      [Route("api/fullteam/{teamTag}/{year:int}")]
      [HttpGet]
      public List<Batting1_app_Result> GetFullTeam(string teamTag, int year) {
         // --------------------------------------------------

         var ctx = new DataAccess.DB_133455_mlbhistoryEntities1();
         List<Batting1_app_Result> list1;
         list1 = ctx.Batting1_app(teamTag, year).ToList();

         return list1;
      }

   // ---------------------------------------------------
   // The following action 'GetTest', returns a complex data type,
   // CTeamInfo (actual class to be used in bcxb3, with
   // nested subtypes and a list of nested subtypes, and it can be tested
   // with 'HttpClientDemo' project, using HttpClient's GetAsync
   // method.
   // The object is hard coded, but the downstream process doesn't know that!
   // ---------------------------------------------------

      [Route("api/test/{teamTag}/{year:int}")]
      [HttpGet]
      public CTeamInfo GetTest(string teamTag, int year) {
      // --------------------------------------------------

         var team = new CTeamInfo {
            Team = teamTag + year.ToString(),
            City = "New York",
            NickName = "Mets",
            LineName = "NYM",
            leagueIP = 65000,
            leagueStats = new CBattingStats(),
            PlayerInfo = new List<CPlayerInfo> {
               new CPlayerInfo {
                  UseName = "Seaver",
                  UseName2 = "T.Seaver",
                  SkillStr = "4--------",
                  slot = 9,
                  posn = 1,
                  slotdh = 0,
                  posnDh = 9,
                  battingStats = new CBattingStats(),
                  Playercategory = 'P',
                  pitchingStats = new CPitchingStats()
               },
               new CPlayerInfo {
                  UseName = "Jones",
                  UseName2 = "C.Jones",
                  SkillStr = "-------4-",
                  slot = 0,
                  posn = 0,
                  slotdh = 0,
                  posnDh = 0,
                  battingStats = new CBattingStats(),
                  Playercategory = 'B',
                  pitchingStats = null
               }
            }
         };

         return team;
      }


      // POST: api/Team
      public void Post([FromBody]string value)
      {
      }

    }
}
