//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LMS.DATA.EF
{
    using System;
    using System.Collections.Generic;
    
    public partial class CourseCompletion
    {
        public int CourseCompletionID { get; set; }
        public string UserID { get; set; }
        public int CourseID { get; set; }
        public System.DateTime DateCompleted { get; set; }
    
        public virtual Cours Cours { get; set; }
        public virtual UserDetail UserDetail { get; set; }
    }
}