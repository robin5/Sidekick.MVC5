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
using System.Linq;
using System.Web;

namespace PEClient.Models
{
    public class SurveyDeleteViewModel
    {
        private string _aspNetId;
        private int _surveyId;
        public string SurveyName { get; set; }
        public string ErrorMessage { get; set; }
        public SurveyDeleteViewModel(string aspNetId, int surveyId)
        {
            this._aspNetId = aspNetId;
            this._surveyId = surveyId;
        }
        public bool Delete()
        {
            ErrorMessage = "";

            try
            {
                using (var db = new PEClientContext())
                {
                    db.spSurvey_Delete(_aspNetId, _surveyId);
                }
                return true;
            }
            catch (Exception ex)
            {
                ErrorMessage = ModelUtils.FormatExceptionMessage(ex);
                return false;
            }
        }
    }
}