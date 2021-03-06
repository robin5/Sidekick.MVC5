﻿// **********************************************************************************
// * Copyright (c) 2019 Robin Murray
// **********************************************************************************
// *
// * File: LaunchedSurveyCreateViewModel.cs
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
using System.Linq;
using System.Web.Mvc;
using PEClient.Validation;

namespace PEClient.Models
{
    public class LaunchedSurveyCreateViewModel
    {
        public IEnumerable<Survey> Surveys { get; set; }
        public IEnumerable<Team> Teams { get; set; }

        [NonNullEmptyOrWhiteSpace(ErrorMessage: "Please enter a Launch Name")]
        public string LaunchName { get; set; }

        [NonZero(ErrorMessage: "Please select a survey")]
        public int SurveyId { get; set; }

        [NonNullEmptyOrWhiteSpace(ErrorMessage: "Please enter a start date and time")]
        [DateTime]
        public string StartDateTime { get; set; }

        [NonNullEmptyOrWhiteSpace(ErrorMessage: "Please enter an end date and time")]
        [DateTime]
        public string EndDateTime { get; set; }
        
        [MinCount(1, ErrorMessage: "Please add one or more Peer Groups")]
        public IEnumerable<int> SelectedTeams { get; set; }
    }
}