//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CMS.DAL.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class ConferenceTopic
    {
        public int Id { get; set; }
        public int confId { get; set; }
        public int keywrdId { get; set; }
    
        public virtual Conference Conference { get; set; }
        public virtual Keyword Keyword { get; set; }
    }
}
