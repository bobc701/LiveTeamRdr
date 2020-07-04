﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataAccess
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class DB_133455_mlbhistoryEntities1 : DbContext
    {
        public DB_133455_mlbhistoryEntities1()
            : base("name=DB_133455_mlbhistoryEntities1")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Appearance> Appearances { get; set; }
        public virtual DbSet<Batting> Battings { get; set; }
        public virtual DbSet<Pitching> Pitchings { get; set; }
        public virtual DbSet<UniqueTeam> UniqueTeams { get; set; }
        public virtual DbSet<ZTeam> ZTeams { get; set; }
        public virtual DbSet<Master> Masters { get; set; }
        public virtual DbSet<Team> Teams { get; set; }
        public virtual DbSet<ZFielding> ZFieldings { get; set; }
        public virtual DbSet<FieldingYear> FieldingYears { get; set; }
    
        public virtual ObjectResult<Batting1_app_Result> Batting1_app(string team, Nullable<int> year)
        {
            var teamParameter = team != null ?
                new ObjectParameter("team", team) :
                new ObjectParameter("team", typeof(string));
    
            var yearParameter = year.HasValue ?
                new ObjectParameter("year", year) :
                new ObjectParameter("year", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Batting1_app_Result>("Batting1_app", teamParameter, yearParameter);
        }
    
        public virtual ObjectResult<LeagueStats1_Result> LeagueStats1(Nullable<int> yearID, string lgID)
        {
            var yearIDParameter = yearID.HasValue ?
                new ObjectParameter("yearID", yearID) :
                new ObjectParameter("yearID", typeof(int));
    
            var lgIDParameter = lgID != null ?
                new ObjectParameter("lgID", lgID) :
                new ObjectParameter("lgID", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<LeagueStats1_Result>("LeagueStats1", yearIDParameter, lgIDParameter);
        }
    
        public virtual ObjectResult<Pitching1_app_Result> Pitching1_app(string team, Nullable<int> year)
        {
            var teamParameter = team != null ?
                new ObjectParameter("team", team) :
                new ObjectParameter("team", typeof(string));
    
            var yearParameter = year.HasValue ?
                new ObjectParameter("year", year) :
                new ObjectParameter("year", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Pitching1_app_Result>("Pitching1_app", teamParameter, yearParameter);
        }
    
        public virtual ObjectResult<FieldingYear1_app_Result> FieldingYear1_app(string team, Nullable<int> year)
        {
            var teamParameter = team != null ?
                new ObjectParameter("team", team) :
                new ObjectParameter("team", typeof(string));
    
            var yearParameter = year.HasValue ?
                new ObjectParameter("year", year) :
                new ObjectParameter("year", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<FieldingYear1_app_Result>("FieldingYear1_app", teamParameter, yearParameter);
        }
    
        public virtual ObjectResult<GamesByPosn1_app_Result> GamesByPosn1_app(string team, Nullable<int> bldYear)
        {
            var teamParameter = team != null ?
                new ObjectParameter("team", team) :
                new ObjectParameter("team", typeof(string));
    
            var bldYearParameter = bldYear.HasValue ?
                new ObjectParameter("BldYear", bldYear) :
                new ObjectParameter("BldYear", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GamesByPosn1_app_Result>("GamesByPosn1_app", teamParameter, bldYearParameter);
        }
    }
}
