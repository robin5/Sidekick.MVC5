// **********************************************************************************
// * Copyright (c) 2019 Robin Murray
// **********************************************************************************
// *
// * File: SurveyCreateViewModel.cs
// *
// * Description: View model for the survey controller for creating a survey
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
    public class SurveyCreateViewModel
    {
        private string _surveyName;
        private List<string> _questions = new List<string>();

        public string SaveErrorMessage { get; set; }

        public SurveyCreateViewModel()
        {
        }

        [NonNullEmptyOrWhiteSpace(ErrorMessage: "A Survey's name cannot be blank.")]
        public string SurveyName 
        { 
            get { return _surveyName; } 
            set
            {
                // Remove white space from beginning and end of the template's name
                _surveyName = value.Trim();
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

        public bool save(string identity)
        {
            SaveErrorMessage = "";

            try
            {
                using (var db = new PEClientContext())
                {
                    db.spSurvey_Create(identity, _surveyName, _questions);
                }

                return true;
            }
            catch (Exception ex)
            {
                SaveErrorMessage = ModelUtils.FormatExceptionMessage(ex);
                return false;
            }
        }
    }
}