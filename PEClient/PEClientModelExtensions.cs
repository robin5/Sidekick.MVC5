using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using PEClient.Models;

namespace PEClient
{
    public static class PEClientModelExtensions
    {
        private class QuestionsList
        {
            private List<SqlDataRecord> tblQuestions = new List<SqlDataRecord>();
            public readonly string TypeName = "[dbo].[QuestionList]";
            public List<SqlDataRecord> Questions
            {
                get
                {
                    return tblQuestions;
                }
            }
            public QuestionsList(IEnumerable<string> questions)
            {
                // Define the schema for a row in the QuestionList table-type in SQL Server
                var tableSchema = new List<SqlMetaData>(1) {
                        new SqlMetaData("QsIndex", SqlDbType.Int),
                        new SqlMetaData("QsText", SqlDbType.Text)
                    }.ToArray();

                // Insert rows into the table
                int i = 0; // This is where the zero based index for the questions is created
                foreach (string question in questions)
                {
                    var tableRow = new SqlDataRecord(tableSchema);
                    tableRow.SetSqlInt32(0, i++); // Set index value and increment index
                    tableRow.SetSqlString(1, question); // Add Question text
                    tblQuestions.Add(tableRow);
                }
            }
        }
        public static void spSurvey_Create(this PEClientContext db, string aspNetId, string surveyName, IEnumerable<string> questions)
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
                new SqlParameter // @AspNetId
                {
                    //SqlDbType = SqlDbType.UniqueIdentifier,
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 128,
                    Direction = ParameterDirection.Input,
                    ParameterName = "AspNetId",
                    Value = aspNetId
                },
                new SqlParameter // @SurveyName
                {
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 50,
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
            var result = db.Database.ExecuteSqlCommand(TransactionalBehavior.DoNotEnsureTransaction,
                "exec spSurvey_Create @AspNetId, @SurveyName, @Questions", parameters);

            // TODO: should return ID of new survey currently only returns -1
        }
        public static void spSurvey_Update(this PEClientContext db, string aspNetId, int surveyId, string surveyName, IEnumerable<string> questions)
        {
            QuestionsList questionsList = new QuestionsList(questions);

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
                    Value = aspNetId
                },
                new SqlParameter // @SurveyId
                {
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Input,
                    ParameterName = "SurveyId",
                    Value = surveyId
                },
                new SqlParameter // @SurveyName
                {
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 50,
                    Direction = ParameterDirection.Input,
                    ParameterName = "SurveyName",
                    Value = surveyName
                },
                new SqlParameter // @Questions
                {
                    SqlDbType = SqlDbType.Structured,
                    Direction = ParameterDirection.Input,
                    ParameterName = "Questions",
                    TypeName = questionsList.TypeName,
                    Value = questionsList.Questions
                }
            };

            // Do not forget to use "DoNotEnsureTransaction" because if you don't EF will start 
            // it's own transaction for your SP.  In that case you don't need internal transaction 
            // in DB or you must detect it with @@trancount and/or XACT_STATE() and change your logic
            db.Database.ExecuteSqlCommand(TransactionalBehavior.DoNotEnsureTransaction,
                "exec spSurvey_Update @AspNetId, @SurveyId, @SurveyName, @Questions", parameters);
        }
        public static void spTeam_Create(this PEClientContext db, string aspNetId, string teamName, IEnumerable<int> userIds)
        {
            // Assembling the table parameter requires the following 3 steps:

            // (1) Define the schema for a row in the QuestionList table-type in SQL Server
            var tableSchema = new List<SqlMetaData>(1) {
                        new SqlMetaData("UserId", SqlDbType.Int)
                    }.ToArray();

            // (2) Create table for holding the tables rows
            var tblUserIds = new List<SqlDataRecord>();

            // (3) Insert rows into the table
            foreach (int userId in userIds)
            {
                var row = new SqlDataRecord(tableSchema);
                row.SetSqlInt32(0, userId); // Add UserId
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
                    Value = aspNetId
                },
                new SqlParameter // @TeamName
                {
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 50,
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
        public static void spTeam_Update(this PEClientContext db, string aspNetId, int teamId, string teamName, IEnumerable<int> userIds)
        {
            // Assembling the table parameter requires the following 3 steps:

            // (1) Define the schema for a row in the QuestionList table-type in SQL Server
            var tableSchema = new List<SqlMetaData>(1) {
                        new SqlMetaData("UserId", SqlDbType.Int)
                    }.ToArray();

            // (2) Create table for holding the tables rows
            var tblUserIds = new List<SqlDataRecord>();

            // (3) Insert rows into the table
            foreach (int userId in userIds)
            {
                var row = new SqlDataRecord(tableSchema);
                row.SetSqlInt32(0, userId); // Add UserId
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
                    Value = aspNetId
                },
                new SqlParameter // @TeamId
                {
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Input,
                    ParameterName = "TeamId",
                    Value = teamId
                },
                new SqlParameter // @TeamName
                {
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 50,
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
                "exec spTeam_Update @AspNetId, @TeamId, @TeamName, @UserIds", parameters);
        }
        public static void spLaunch_Create(this PEClientContext db, string aspNetId, string launchName, int surveyId, DateTime startDate, DateTime endDate, IEnumerable<int> teamIds)
        {
            // Assembling the table parameter requires the following 3 steps:

            // (1) Define the schema for a row in the TeamIdList table-type in SQL Server
            var tableSchema = new List<SqlMetaData>(1) {
                        new SqlMetaData("TeamId", SqlDbType.Int)
                    }.ToArray();

            // (2) Create table for holding the tables rows
            var tblTeamIds = new List<SqlDataRecord>();

            // (3) Insert rows into the table
            foreach (int teamId in teamIds)
            {
                var row = new SqlDataRecord(tableSchema);
                row.SetSqlInt32(0, teamId); // Add UserId
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
                    Value = aspNetId
                },
                new SqlParameter // @LaunchName
                {
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 50,
                    Direction = ParameterDirection.Input,
                    ParameterName = "LaunchName",
                    Value = launchName
                },
                new SqlParameter // @SurveyId
                {
                    SqlDbType = SqlDbType.Int,
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
        public static void spResponses_Update(this PEClientContext db, string aspNetId, IEnumerable<ResponseUpdate> Responses, bool submit)
        {
            // Assembling the table parameter requires the following 3 steps:

            // (1) Define the schema for a row in the QuestionList table-type in SQL Server
            var tableSchema = new List<SqlMetaData>(1) {
                        new SqlMetaData("QuestionId", SqlDbType.Int),
                        new SqlMetaData("Reviewee", SqlDbType.Int),
                        new SqlMetaData("Reviewer", SqlDbType.Int),
                        new SqlMetaData("Text", SqlDbType.Text),
                        new SqlMetaData("GradeId", SqlDbType.TinyInt),
                        new SqlMetaData("TeamId", SqlDbType.Int)
                    }.ToArray();

            // (2) Create table for holding the tables rows
            var tblResponses = new List<SqlDataRecord>();

            // (3) Insert rows into the table
            foreach (ResponseUpdate response in Responses)
            {
                var tableRow = new SqlDataRecord(tableSchema);
                tableRow.SetSqlInt32(0, response.QuestionId); // Set index value and increment index
                tableRow.SetSqlInt32(1, response.Reviewee); // Set index value and increment index
                tableRow.SetSqlInt32(2, response.Reviewer); // Set index value and increment index
                tableRow.SetSqlString(3, response.Text); // Add Question text
                if (null == response.GradeId)
                {
                    tableRow.SetDBNull(4); // Set index value and increment index
                }
                else
                {
                    tableRow.SetSqlByte(4, (byte)response.GradeId); // Set index value and increment index
                }
                tableRow.SetSqlInt32(5, response.TeamId); // Set index value and increment index
                tblResponses.Add(tableRow);
            }

            // Define the Parameters for the stored procedure call
            SqlParameter[] parameters =
            {
                new SqlParameter // @AspNetId
                {
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 128,
                    Direction = ParameterDirection.Input,
                    ParameterName = "AspNetId",
                    Value = aspNetId
                },
                new SqlParameter // @Responses
                {
                    SqlDbType = SqlDbType.Structured,
                    Direction = ParameterDirection.Input,
                    ParameterName = "Responses",
                    TypeName = "[dbo].[ResponseList]",
                    Value = tblResponses
                },
                new SqlParameter // @TeamId
                {
                    SqlDbType = SqlDbType.Bit,
                    Direction = ParameterDirection.Input,
                    ParameterName = "Submit",
                    Value = submit
                }
            };

            // Do not forget to use "DoNotEnsureTransaction" because if you don't EF will start 
            // it's own transaction for your SP.  In that case you don't need internal transaction 
            // in DB or you must detect it with @@trancount and/or XACT_STATE() and change your logic
            db.Database.ExecuteSqlCommand(TransactionalBehavior.DoNotEnsureTransaction,
                "exec spResponses_Update @AspNetId, @Responses, @submit", parameters);
        }
    }
}