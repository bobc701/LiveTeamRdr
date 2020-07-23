using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LiveTeamRdrApi.BusinessLogic {

   public class ZGamesByPosn {

      public string playerID { get; set; }
      public int g { get; set; }
      public int gs { get; set; }
      public int p { get; set; }
      public int c { get; set; }
      public int b1 { get; set; }
      public int b2 { get; set; }
      public int b3 { get; set; }
      public int ss { get; set; }
      public int lf { get; set; }
      public int cf { get; set; }
      public int rf { get; set; }
      public int of { get; set; }
      public int dh { get; set; }

      public string BBRefFielding { get; set; }

      public double r1 { get; set; }
      public double r2 { get; set; }
      public double r3 { get; set; }
      public double r4 { get; set; }
      public double r5 { get; set; }
      public double r6 { get; set; }
      public double r7 { get; set; }
      public double r8 { get; set; }
      public double r9 { get; set; }

      public string SkillStr { get; set; }


    // This allows us to say zgamesByPosn1["lf"] i/o zgamesByPosn1.lf
      public int this[string ix] {
      // ---------------------------------------------------
         get {
            return ix switch {
               "g" => (int)g,
               "gs" => (int)gs,
               "dh" => (int)dh,
               "p" => (int)p,
               "c" => (int)c,
               "b1" => (int)b1,
               "b2" => (int)b2,
               "b3" => (int)b3,
               "ss" => (int)ss,
               "lf" => (int)lf,
               "cf" => (int)cf,
               "rf" => (int)rf,
               "of" => (int)of,
               _ => 0
            };
         }

      }

   }
}