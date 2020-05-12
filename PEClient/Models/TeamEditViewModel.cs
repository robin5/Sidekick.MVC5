using PEClient.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace PEClient.Models
{
    public class TeamEditViewModel
    {
        /*****************************************************************
         *
         *****************************************************************/
        private string _saveErrorMessage;
        private string _teamName;
        private int _id;

        private List<Student> _students = new List<Student>();
        private List<SelectListItem> _candidates = new List<SelectListItem>();
        private List<SelectListItem> _peers = new List<SelectListItem>();
        private List<SelectListItem> _selectedStudents = new List<SelectListItem>();

        /*****************************************************************
         *
         *****************************************************************/
        public string SaveErrorMessage 
        { 
            get 
            { 
                return _saveErrorMessage; 
            } 
        }

        /*****************************************************************
         *
         *****************************************************************/
        public TeamEditViewModel()
        {
        }
        public TeamEditViewModel(string aspNetId, int id)
        {
            _id = id;
            LoadStudents(aspNetId);
            LoadTeam(aspNetId, _id);
        }

        /*****************************************************************
         *
         *****************************************************************/
        public void LoadStudents(string aspNetId)
        {
            using (var db = new PEClientContext())
            {
                // Get students from database
                var students = db.spAspNetUsers_GetAllStudents();

                // Cycle through result of database query and load data into the model
                foreach (var student in students)
                {
                    _students.Add(new Student { UserName = student.UserName, Name = student.FullName, id = student.UserId });
                }
            }

            foreach (var student in _students)
            {
                _candidates.Add(new SelectListItem { Text = student.Name + "  (" + student.UserName + ")", Value = (student.id).ToString() });
                _peers.Add(new SelectListItem { Text = student.Name + "  (" + student.UserName + ")", Value = (student.id).ToString() });
            }
        }
        private void LoadTeam(string aspNetId, int id)
        {
            using (var db = new PEClientContext())
            {
                List<int> memberUserIds = new List<int>();

                // Query database for surveys for the given identity
                var team = db.spTeam_GetById(aspNetId, id);

                // Cycle through result of database query and load data into the model
                foreach (var member in team)
                {
                    memberUserIds.Add((int)member.UserId);
                    if (null == _teamName)
                    {
                        _teamName = member.TeamName;
                    }
                }
                PeerSelection = memberUserIds;
            }
        }

        /*****************************************************************
         *                   Fields returned from the view
         *****************************************************************/
        public int Id
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
            }
        }
        [NonNullEmptyOrWhiteSpace(ErrorMessage: "A peer group's name cannot be blank.")]
        public string TeamName
        {
            get 
            { 
                return _teamName; 
            }
            set
            {
                // Remove white space from beginning and end of the template's name
                _teamName = value.Trim();
            }
        }
        public List<SelectListItem> Candidates { get { return _candidates; } }
        public IEnumerable<int> CandidateSelection { get; set; }
        public List<SelectListItem> Peers { get { return _peers; } }

        [MinCount(1, ErrorMessage: "Please add one or more users to the peer group.")]
        public IEnumerable<int> PeerSelection { get; set; }
        public IEnumerable<Student> PeerDetails { get { return _students; } }

        /*****************************************************************
         *
         *****************************************************************/
        public bool save(string aspNetId)
        {
            _saveErrorMessage = string.Empty;

            try
            {
                using (var db = new PEClientContext())
                {
                    //db.spTeam_Update(aspNetId, _id, _teamName, PeerSelection);
                }

                return true;
            }
            catch (Exception ex)
            {
                _saveErrorMessage = ModelUtils.FormatExceptionMessage(ex);
                return false;
            }
        }
    }
}