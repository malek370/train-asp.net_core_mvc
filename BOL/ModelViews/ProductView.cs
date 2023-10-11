using BOL.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOL.ModelViews
{
	public class ProductView
	{
		public Product product;
        [ValidateNever]
		public Dictionary<int , string> categries { get; set; }
        
    }
}
