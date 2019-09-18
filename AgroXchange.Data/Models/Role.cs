using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AgroXchange.Data.Models
{
    [Table("Roles", Schema = "dbo")]
    public class Role
    {
        [Key]
        [Display(Name = "Role Id")]
        public int RoleId { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(30)")]
        [Display(Name = "Role Name")]
        public string RoleName { get; set; }
    }
}
