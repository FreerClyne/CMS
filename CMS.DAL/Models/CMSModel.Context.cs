﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class CMSDBEntities : DbContext
    {
        public CMSDBEntities()
            : base("name=CMSDBEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Conference> Conferences { get; set; }
        public virtual DbSet<ConferenceMember> ConferenceMembers { get; set; }
        public virtual DbSet<ConferenceTopic> ConferenceTopics { get; set; }
        public virtual DbSet<Expertise> Expertises { get; set; }
        public virtual DbSet<Feedback> Feedbacks { get; set; }
        public virtual DbSet<keyword> keywords { get; set; }
        public virtual DbSet<Paper> Papers { get; set; }
        public virtual DbSet<PaperReview> PaperReviews { get; set; }
        public virtual DbSet<PaperTopic> PaperTopics { get; set; }
        public virtual DbSet<RegisterRequest> RegisterRequests { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<User> Users { get; set; }
    }
}