﻿// **********************************************************************************
// * Copyright (c) 2019 Robin Murray
// **********************************************************************************
// *
// * File: ViewTeamViewModel.cs
// *
// * Description: View model for the ViewTeam controller and view
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
    public class TeamIndexViewModel
    {
        List<spTeam_GetById_Result> _teamMembers = new List<spTeam_GetById_Result>();
        public int? Id { get; set; }
        public TeamIndexViewModel()
        {
        }
        public TeamIndexViewModel(string aspNetId, int? teamId)
        {
            this.Id = teamId;
            LoadTeam(aspNetId, teamId);
        }

        public void LoadTeam(string aspNetId, int? teamId)
        {
            if (null != teamId)
            {
                using (var db = new PEClientContext())
                {
                    // Query database for surveys for the given identity
                    var members = db.spTeam_GetById(aspNetId, teamId);

                    // Cycle through result of database query and load data into the model
                    foreach (var member in members)
                    {
                        _teamMembers.Add(member);
                    }
                }
            }
        }

        public List<spTeam_GetById_Result> TeamMembers
        {
            get
            {
                return _teamMembers;
            }
        }
    }
}