namespace CMS.DAL.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ConferenceMember")]
    public partial class ConferenceMember
    {
        public int Id { get; set; }

        public int confId { get; set; }

        public int userId { get; set; }

        public virtual Conference Conference { get; set; }

        public virtual User User { get; set; }
    }
}
