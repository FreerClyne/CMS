//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CMS.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class Feedback
    {
        public int Id { get; set; }
        public int paperId { get; set; }
        public int userId { get; set; }
        public string fnlDecision { get; set; }
        public string feedback1 { get; set; }
    
        public virtual Paper Paper { get; set; }
        public virtual User User { get; set; }
    }
}
