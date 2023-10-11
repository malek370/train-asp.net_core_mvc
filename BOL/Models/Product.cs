using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication2.BOL.Models;

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
        public int CategoryId { get; set; }
        [ForeignKey(nameof(CategoryId))]
        public Category? Category { get; set; }
        [Display(Name ="product image")]
        public string? imgURL { get; set; }
    }
}
