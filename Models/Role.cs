
namespace CommonP.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Role
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "ID")]
        public int ID { get; set; }

        [MaxLength(50)]
        [Display(Name = "名称")]
        public string Name { get; set; }

        [MaxLength(500)]
        [Display(Name = "描述")]
        public string Description { get; set; }

        public virtual ICollection<Action> Action { get; set; }

        public virtual ICollection<Group> Group { get; set; }

    }
}