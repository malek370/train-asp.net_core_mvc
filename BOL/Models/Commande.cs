using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOL.Models
{
    public class Commande
    {
        [Required]
        [Key]
        public int Id { get; set; }
        
        public string? IdUser { get; set; }
        [Required]
        public int IdProduct { get; set; }
        [ForeignKey(nameof(IdProduct))]
        public Product? Product { get; set; }
        

    }
}
