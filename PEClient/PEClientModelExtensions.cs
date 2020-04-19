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
    }
}