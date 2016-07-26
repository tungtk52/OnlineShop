namespace Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Contact")]
    public partial class Contact
    {
        public int Id { get; set; }

        [Column(TypeName = "ntext")]
        public string Content { get; set; }

        [StringLength(10)]
        public string Status { get; set; }
    }
}
