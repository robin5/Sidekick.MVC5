using PEClient.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PEClient.Models
{
    public class SurveyIndexViewModel
    {
        private string name;
        private IEnumerable<string> questions = new List<string>();

        public int Id { get; set; }

        [NonNullEmptyOrWhiteSpace(ErrorMessage: "A Survey's name cannot be blank.")]
        public string Name
        {
            get { return name; }
            set
            {
                // Remove white space surrounding survey's name
                name = value.Trim();
            }
        }

        [Required(ErrorMessage = "Questions missing.")]
        [MinCount(1, "Peer Evaluations must have at least one question")]
        [NonNullEmptyOrWhiteSpace(ErrorMessage: "A Survey question cannot be blank.")]
        public IEnumerable<string> Questions
        {
            get { return questions; }
            set
            {
                // Remove white space surrounding each question
                questions = value.Select(x => x.Trim()).ToArray();
            }
        }
    }
}