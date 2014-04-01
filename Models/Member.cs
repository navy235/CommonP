namespace CommonP.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Member
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "ID")]
        public int ID { get; set; }

        [MaxLength(50)]
        [Display(Name = "名称")]
        public string Name { get; set; }

        [MaxLength(50)]
        [Display(Name = "密码")]
        public string Password { get; set; }

        public int GroupID { get; set; }

        public virtual Group Group { get; set; }

    }
}