// **********************************************************************************
// * Copyright (c) 2019 Robin Murray
// **********************************************************************************
// *
// * File: TemplateViewModel.cs
// *
// * Description: View model for the Template controller and view
// *
// * Author: Robin Murray
// *
// **********************************************************************************
// *
// * Granting License: The MIT License (MIT)
// * 
// *   Permission is hereby granted, free of charge, to any person obtaining a copy
// *   of this software and associated documentation files (the "Software"), to deal
// *   in the Software without restriction, including without limitation the rights
// *   to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// *   copies of the Software, and to permit persons to whom the Software is
// *   furnished to do so, subject to the following conditions:
// *   The above copyright notice and this permission notice shall be included in
// *   all copies or substantial portions of the Software.
// *   THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// *   IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// *   FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// *   AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// *   LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// *   OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// *   THE SOFTWARE.
// * 
// **********************************************************************************

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PEClient.Validation;
using System.Collections.Specialized;
using Microsoft.SqlServer.Server;
using System.Data;
using System.Data.SqlClient;
using System.Data.Entity;

namespace PEClient.Models
{
    public class TemplateViewModel
    {
        public string SaveErrorMessage { get; set; }
        private string _templateName;
        private List<string> _questions = new List<string>();

        public TemplateViewModel()
        {
        }
        public string Identity { get; set; }
        
        //[Required(ErrorMessage = "Surveys must have a name.")]
        [NonNullEmptyOrWhiteSpace(ErrorMessage: "A Survey's name cannot be blank.")]
        public string TemplateName 
        { 
            get { return _templateName; } 
            set
            {
                // Remove white space from beginning and end of the template's name
                _templateName = value.Trim();
            }
        }

        [Required(ErrorMessage = "Questions missing.")]
        [MinCount(1, "Peer Evaluations must have at least one question")]
        [NonNullEmptyOrWhiteSpace(ErrorMessage: "A Survey question cannot be blank.")]
        public List<string> Questions 
        {
            get
            {
                return _questions;
            }
            set
            {
                _questions = value;

                // Remove white space from beginning and end of each template question
                for (int i = 0; i < _questions.Count; ++i)
                {
                    _questions[i] = _questions[i].Trim();
                }
            }
        }

        public bool save()
        {
            try
            {
                SaveErrorMessage = "";

                using (var db = new PEClientContext())
                {
                    db.sp_CreateSurvey(Identity, _templateName, _questions);
                }

                //using (var db = new PEClientContext())
                //{
                //    // var user = db.tblUsers.Where(g => g.Identity == Identity).FirstOrDefault<tblUser>();


                //    //Build your record
                //    var tableSchema = new List<SqlMetaData>(1) {
                //        new SqlMetaData("QsIndex", SqlDbType.Int),
                //        new SqlMetaData("QsText", SqlDbType.Text)
                //    }.ToArray();

                //    //And a table as a list of those records
                //    var table = new List<SqlDataRecord>();

                //    // Add rows to table
                //    int i = 1;
                //    foreach (string question in _questions)
                //    {
                //        var tableRow = new SqlDataRecord(tableSchema);
                //        tableRow.SetSqlInt32(0, i++);
                //        tableRow.SetSqlString(1, question);
                //        table.Add(tableRow);
                //    }

                //    //Parameters for your query
                //    SqlParameter[] parameters =
                //    {
                //        // UserId
                //        new SqlParameter
                //        {
                //            //SqlDbType = SqlDbType.UniqueIdentifier,
                //            SqlDbType = SqlDbType.NVarChar,
                //            Size = 128,
                //            Direction = ParameterDirection.Input, // output!
                //            ParameterName = "Id",
                //            Value = Identity
                //        },
                //        // SurveyName
                //        new SqlParameter
                //        {
                //            SqlDbType = SqlDbType.VarChar,
                //            Size = 40,
                //            Direction = ParameterDirection.Input, // output!
                //            ParameterName = "SurveyName",
                //            Value = _templateName
                //        },
                //        // Questions List (Table)
                //        new SqlParameter
                //        {
                //            SqlDbType = SqlDbType.Structured,
                //            Direction = ParameterDirection.Input,
                //            ParameterName = "Questions",
                //            TypeName = "[dbo].[QuestionList]", //Don't forget this one!
                //            Value = table
                //        }
                //};


                //    // Do not forget to use "DoNotEnsureTransaction" because if you don't EF will start 
                //    // it's own transaction for your SP.  In that case you don't need internal transaction 
                //    // in DB or you must detect it with @@trancount and/or XACT_STATE() and change your logic
                //    db.Database.ExecuteSqlCommand(TransactionalBehavior.DoNotEnsureTransaction,
                //        "exec sp_CreateSurvey @Id, @SurveyName, @Questions", parameters);


                //    // db.sp_CreateSurvey(Identity, _templateName);
                //    // db.SaveChanges();
                //}
                return true;
            }
            catch (Exception ex)
            {
                SaveErrorMessage = ex.Message;

                Exception innerException = ex.InnerException;
                while (innerException != null)
                {
                    SaveErrorMessage += ("\n" + ex.InnerException.Message);
                    innerException = innerException.InnerException;
                }
                    return false;
            }
        }
    }
}