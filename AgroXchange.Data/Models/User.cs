using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AgroXchange.Data.Models
{
    [Table("Users", Schema = "dbo")]
    public class User
    {
        [Key]
        [Display(Name = "User Id")]
        public Guid UserId { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(50)")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(50)")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(256)")]
        [Display(Name = "Email ID")]
        public string EmailId { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(256)")]
        [Display(Name = "Password Hash")]
        public string PasswordHash { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(256)")]
        [Display(Name = "Password Salt")]
        public string PasswordSalt { get; set; }

        [ForeignKey("UserRole")]
        [Required]
        public int RoleId { get; set; }

        public virtual Role UserRole { get; set; }

        [Required]
        [Column(TypeName = "datetime")]
        [Display(Name = "Date Created")]
        public DateTime DateCreated { get; set; }

        [Column(TypeName = "nvarchar(20)")]
        [Display(Name = "Activation Key")]
        public string ActivationKey { get; set; }

        [Required]
        [Column(TypeName = "bit")]
        [Display(Name = "Activated")]
        public bool Activated { get; set; }

        [Required]
        [Column(TypeName = "bit")]
        [Display(Name = "Locked Out")]
        public bool LockedOut { get; set; }

        [Column(TypeName = "nvarchar(20)")]
        [Display(Name = "Password Reset Key")]
        public string PasswordResetKey { get; set; }
    }
}
