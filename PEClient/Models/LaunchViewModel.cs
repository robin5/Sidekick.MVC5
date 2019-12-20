using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PEClient.Models
{
    public class LaunchViewModel
    {
        private List<string> _templateNames = new List<string>();
        public string LaunchName { get; set; }
        public List<string> TemplateNames
        { 
            get { return _templateNames; }
        }
        public IEnumerable<SelectListItem> SelectedPeerGroups { get; set; }
        public IEnumerable<SelectListItem> PeerGroups { get; set; }

        public string StartDateTime { get; set; }
        public string EndDateTime { get; set; }

        public LaunchViewModel()
        {
            _templateNames.Add("Survey-1");
            _templateNames.Add("Survey-2");
            _templateNames.Add("Survey-3");
            _templateNames.Add("No Questions");
            _templateNames.Add("CTEC-227 Class Survey");
            _templateNames.Add("Final Test");

            List<SelectListItem> _peerGroups = new List<SelectListItem>();
            _peerGroups.Add(new SelectListItem { Text = "Team #1", Value = "1" });
            _peerGroups.Add(new SelectListItem { Text = "Team #2", Value = "2" });
            _peerGroups.Add(new SelectListItem { Text = "Team #3", Value = "3" });
            _peerGroups.Add(new SelectListItem { Text = "Team #4", Value = "4" });
            _peerGroups.Add(new SelectListItem { Text = "Team #5", Value = "5" });
            _peerGroups.Add(new SelectListItem { Text = "Team Celeb", Value = "6" });
            PeerGroups = _peerGroups;

            List<SelectListItem> _selectedPeerGroups = new List<SelectListItem>();
            //_selectedPeerGroups.Add(new SelectListItem { Text = "Team #2", Value = "2" });
            SelectedPeerGroups = _selectedPeerGroups;
        }
    }
}