﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PEClient
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class PEClientContext : DbContext
    {
        public PEClientContext()
            : base("name=PEClientContext")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<AspNetUsers> AspNetUsers { get; set; }
        public virtual DbSet<tblGrades> tblGrades { get; set; }
        public virtual DbSet<tblLaunchedQuestions> tblLaunchedQuestions { get; set; }
        public virtual DbSet<tblLaunchedSurveyQuestions> tblLaunchedSurveyQuestions { get; set; }
        public virtual DbSet<tblLaunchedSurveys> tblLaunchedSurveys { get; set; }
        public virtual DbSet<tblQuestions> tblQuestions { get; set; }
        public virtual DbSet<tblRoles> tblRoles { get; set; }
        public virtual DbSet<tblSubmissionStatus> tblSubmissionStatus { get; set; }
        public virtual DbSet<tblSurveyCompletes> tblSurveyCompletes { get; set; }
        public virtual DbSet<tblSurveyInstances> tblSurveyInstances { get; set; }
        public virtual DbSet<tblSurveyQuestions> tblSurveyQuestions { get; set; }
        public virtual DbSet<tblSurveyResponses> tblSurveyResponses { get; set; }
        public virtual DbSet<tblSurveys> tblSurveys { get; set; }
        public virtual DbSet<tblTeams> tblTeams { get; set; }
        public virtual DbSet<tblTeamUsers> tblTeamUsers { get; set; }
        public virtual DbSet<tblUserRoles> tblUserRoles { get; set; }
        public virtual DbSet<tblUsers> tblUsers { get; set; }
        public virtual DbSet<vwAspNetUsers_GetStudents> vwAspNetUsers_GetStudents { get; set; }
    
        public virtual ObjectResult<spAspNetUsers_GetAllStudents_Result> spAspNetUsers_GetAllStudents()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<spAspNetUsers_GetAllStudents_Result>("spAspNetUsers_GetAllStudents");
        }
    
        public virtual ObjectResult<spLaunchedSurveys_GetAll_Result> spLaunchedSurveys_GetAll(string id)
        {
            var idParameter = id != null ?
                new ObjectParameter("Id", id) :
                new ObjectParameter("Id", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<spLaunchedSurveys_GetAll_Result>("spLaunchedSurveys_GetAll", idParameter);
        }
    
        public virtual int spSurvey_Create(string id, string surveyName)
        {
            var idParameter = id != null ?
                new ObjectParameter("Id", id) :
                new ObjectParameter("Id", typeof(string));
    
            var surveyNameParameter = surveyName != null ?
                new ObjectParameter("SurveyName", surveyName) :
                new ObjectParameter("SurveyName", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("spSurvey_Create", idParameter, surveyNameParameter);
        }
    
        public virtual ObjectResult<spSurvey_GetAll_Result> spSurvey_GetAll(string id)
        {
            var idParameter = id != null ?
                new ObjectParameter("Id", id) :
                new ObjectParameter("Id", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<spSurvey_GetAll_Result>("spSurvey_GetAll", idParameter);
        }
    
        public virtual int spTeam_Create(string aspNetId, string teamName)
        {
            var aspNetIdParameter = aspNetId != null ?
                new ObjectParameter("AspNetId", aspNetId) :
                new ObjectParameter("AspNetId", typeof(string));
    
            var teamNameParameter = teamName != null ?
                new ObjectParameter("TeamName", teamName) :
                new ObjectParameter("TeamName", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("spTeam_Create", aspNetIdParameter, teamNameParameter);
        }
    
        public virtual ObjectResult<spTeam_GetAll_Result> spTeam_GetAll(string id)
        {
            var idParameter = id != null ?
                new ObjectParameter("Id", id) :
                new ObjectParameter("Id", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<spTeam_GetAll_Result>("spTeam_GetAll", idParameter);
        }
    }
}
