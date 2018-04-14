using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreApp.Models
{
    public class Event
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name = "From")]
        public DateTime Datefrom { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name = "To")]
        public DateTime DateTo { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public int TotalSpace { get; set; }

        [Required]
        public int RemainingSpace { get; set; }

        [Required]
        public string Takesplace { get; set; }

        [Required]
        public int Price { get; set; }

        [ForeignKey("Administrator")]
        public int AdminId { get; set; }
        public virtual Administrator Administrator { get; set; }
    }
}
