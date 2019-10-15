using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AgroXchange.Data.Models
{
    [Table("Farms", Schema = "dbo")]
    public class Farm
    {
        [Key]
        [Display(Name = "Farm Id")]
        public Guid FarmId { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(9)")]
        [Display(Name = "Farm Type")]
        public string FarmType { get; set; }

        [Required]
        [Display(Name = "Latitude in Degrees")]
        public double Latitude { get; set; }

        [Required]
        [Display(Name = "Latitude in Radians")]
        public double LatitudeRad { get; set; }

        [Required]
        [Display(Name = "Longitude in Degrees")]
        public double Longitude { get; set; }

        [Required]
        [Display(Name = "Longitude in Radians")]
        public double LongitudeRad { get; set; }
    }

    public class FarmType
    {
        public const string Crop = "Crop";
        public const string Livestock = "Livestock";
    }
}
