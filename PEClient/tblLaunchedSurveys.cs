//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PEClient
{
    using System;
    using System.Collections.Generic;
    
    public partial class tblLaunchedSurveys
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tblLaunchedSurveys()
        {
            this.tblLaunchedSurveyQuestions = new HashSet<tblLaunchedSurveyQuestions>();
            this.tblLaunchedTeams = new HashSet<tblLaunchedTeams>();
        }
    
        public int SurveyId { get; set; }
        public string Name { get; set; }
        public int OwnerId { get; set; }
        public System.DateTime StartDate { get; set; }
        public System.DateTime EndDate { get; set; }
        public bool Released { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblLaunchedSurveyQuestions> tblLaunchedSurveyQuestions { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblLaunchedTeams> tblLaunchedTeams { get; set; }
    }
}
