using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOL.Models
{
    public class Product
    {
        [Key] public int Id { get; set; }
        [Required]
        [Display(Name ="product Name")]
        public string Name { get; set; }
        public string? Description { get; set; }
        [Required]
        public string Author { get; set; }
        [Required]
        [Range(10,1000)]
        public float Price { set; get; }
    }
}
