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
    
    public partial class PaperTopic
    {
        public int Id { get; set; }
        public int paperId { get; set; }
        public int keywrdId { get; set; }
    
        public virtual keyword keyword { get; set; }
        public virtual Paper Paper { get; set; }
    }
}
