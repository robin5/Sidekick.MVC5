using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace PEClient
{
    public static class PEClientModelExtensions
    {
        public static void sp_CreateSurvey(this PEClientContext db, string identity, string surveyName, List<string> questions)
        {
            // Assembling the table parameter requires the following 3 steps:

            // (1) Define the schema for a row in the QuestionList table-type in SQL Server
            var tableSchema = new List<SqlMetaData>(1) {
                        new SqlMetaData("QsIndex", SqlDbType.Int),
                        new SqlMetaData("QsText", SqlDbType.Text)
                    }.ToArray();

            // (2) Create a list for holding the tables rows
            var tblQuestions = new List<SqlDataRecord>();

            // (3) Insert rows into the table
            int i = 0; // This is where the zero based index for the questions is created
            foreach (string question in questions)
            {
                var tableRow = new SqlDataRecord(tableSchema);
                tableRow.SetSqlInt32(0, i++); // Set index value and increment index
                tableRow.SetSqlString(1, question); // Add Question text
                tblQuestions.Add(tableRow);
            }

            // Define the Parameters for the stored procedure call
            SqlParameter[] parameters =
            {
                new SqlParameter // @Id
                {
                    //SqlDbType = SqlDbType.UniqueIdentifier,
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 128,
                    Direction = ParameterDirection.Input,
                    ParameterName = "Id",
                    Value = identity
                },
                new SqlParameter // @SurveyName
                {
                    SqlDbType = SqlDbType.VarChar,
                    Size = 40,
                    Direction = ParameterDirection.Input,
                    ParameterName = "SurveyName",
                    Value = surveyName
                },
                new SqlParameter // @Questions
                {
                    SqlDbType = SqlDbType.Structured,
                    Direction = ParameterDirection.Input,
                    ParameterName = "Questions",
                    TypeName = "[dbo].[QuestionList]", //Don't forget this one!
                    Value = tblQuestions
                }
            };

            // Do not forget to use "DoNotEnsureTransaction" because if you don't EF will start 
            // it's own transaction for your SP.  In that case you don't need internal transaction 
            // in DB or you must detect it with @@trancount and/or XACT_STATE() and change your logic
            db.Database.ExecuteSqlCommand(TransactionalBehavior.DoNotEnsureTransaction,
                "exec spSurvey_Create @Id, @SurveyName, @Questions", parameters);
        }
        public static void spTeam_Create(this PEClientContext db, string identity, string teamName, IEnumerable<int> userIds)
        {
            // Assembling the table parameter requires the following 3 steps:

            // (1) Define the schema for a row in the QuestionList table-type in SQL Server
            var tableSchema = new List<SqlMetaData>(1) {
                        new SqlMetaData("UserId", SqlDbType.Decimal)
                    }.ToArray();

            // (2) Create table for holding the tables rows
            var tblUserIds = new List<SqlDataRecord>();

            // (3) Insert rows into the table
            foreach (int userId in userIds)
            {
                var row = new SqlDataRecord(tableSchema);
                row.SetSqlDecimal(0, userId); // Add UserId
                tblUserIds.Add(row);
            }

            // Define the Parameters for the stored procedure call
            SqlParameter[] parameters =
            {
                new SqlParameter // @AspNetId
                {
                    //SqlDbType = SqlDbType.UniqueIdentifier,
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 128,
                    Direction = ParameterDirection.Input,
                    ParameterName = "AspNetId",
                    Value = identity
                },
                new SqlParameter // @TeamName
                {
                    SqlDbType = SqlDbType.VarChar,
                    Size = 40,
                    Direction = ParameterDirection.Input,
                    ParameterName = "TeamName",
                    Value = teamName
                },
                new SqlParameter // @UserIds
                {
                    SqlDbType = SqlDbType.Structured,
                    Direction = ParameterDirection.Input,
                    ParameterName = "UserIds",
                    TypeName = "[dbo].[UserIdList]", // As defined in database's User-Defined Table Types
                    Value = tblUserIds
                }
            };

            // Do not forget to use "DoNotEnsureTransaction" because if you don't EF will start 
            // it's own transaction for your SP.  In that case you don't need internal transaction 
            // in DB or you must detect it with @@trancount and/or XACT_STATE() and change your logic
            db.Database.ExecuteSqlCommand(TransactionalBehavior.DoNotEnsureTransaction,
                "exec spTeam_Create @AspNetId, @TeamName, @UserIds", parameters);
        }
        public static void spLaunch_Create(this PEClientContext db, string identity, string launchName, decimal surveyId, DateTime startDate, DateTime endDate, IEnumerable<int> teamIds)
        {
            // Assembling the table parameter requires the following 3 steps:

            // (1) Define the schema for a row in the TeamIdList table-type in SQL Server
            var tableSchema = new List<SqlMetaData>(1) {
                        new SqlMetaData("TeamId", SqlDbType.Decimal)
                    }.ToArray();

            // (2) Create table for holding the tables rows
            var tblTeamIds = new List<SqlDataRecord>();

            // (3) Insert rows into the table
            foreach (int teamId in teamIds)
            {
                var row = new SqlDataRecord(tableSchema);
                row.SetSqlDecimal(0, teamId); // Add UserId
                tblTeamIds.Add(row);
            }

            // Define the Parameters for the stored procedure call
            SqlParameter[] parameters =
            {
                new SqlParameter // @AspNetId
                {
                    //SqlDbType = SqlDbType.UniqueIdentifier,
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 128,
                    Direction = ParameterDirection.Input,
                    ParameterName = "AspNetId",
                    Value = identity
                },
                new SqlParameter // @LaunchName
                {
                    SqlDbType = SqlDbType.VarChar,
                    Size = 40,
                    Direction = ParameterDirection.Input,
                    ParameterName = "LaunchName",
                    Value = launchName
                },
                new SqlParameter // @SurveyId
                {
                    SqlDbType = SqlDbType.Decimal,
                    Precision = 10,
                    Scale = 2,
                    Direction = ParameterDirection.Input,
                    ParameterName = "SurveyId",
                    Value = surveyId
                },
                new SqlParameter // @StartDate
                {
                    SqlDbType = SqlDbType.DateTime,
                    Direction = ParameterDirection.Input,
                    ParameterName = "StartDate",
                    Value = startDate
                },
                new SqlParameter // @EndDate
                {
                    SqlDbType = SqlDbType.DateTime,
                    Direction = ParameterDirection.Input,
                    ParameterName = "EndDate",
                    Value = endDate
                },
                new SqlParameter // @TeamIds
                {
                    SqlDbType = SqlDbType.Structured,
                    Direction = ParameterDirection.Input,
                    ParameterName = "TeamIds",
                    TypeName = "[dbo].[TeamIdList]", // As defined in database's User-Defined Table Types
                    Value = tblTeamIds
                }
            };

            // Do not forget to use "DoNotEnsureTransaction" because if you don't EF will start 
            // it's own transaction for your SP.  In that case you don't need internal transaction 
            // in DB or you must detect it with @@trancount and/or XACT_STATE() and change your logic
            db.Database.ExecuteSqlCommand(TransactionalBehavior.DoNotEnsureTransaction,
                "exec spLaunch_Create @AspNetId, @LaunchName, @SurveyId, @StartDate, @EndDate, @TeamIds", parameters);
        }
    }
}