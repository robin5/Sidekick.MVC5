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
    
    public partial class tblSurveyQuestions
    {
        public decimal SurveyQuestionId { get; set; }
        public decimal SurveyId { get; set; }
        public decimal QuestionId { get; set; }
        public decimal QsIndex { get; set; }
    
        public virtual tblQuestions tblQuestions { get; set; }
        public virtual tblSurveys tblSurveys { get; set; }
        public virtual tblSurveys tblSurveys1 { get; set; }
    }
}