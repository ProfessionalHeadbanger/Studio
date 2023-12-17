using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Studio.Models
{
	public class Model
	{
		public int Id { get; set; } // Столбец первичного ключа
		[Required]
		public string Name { get; set; } // Столбец имени модели
		[Required]
		public string Description { get; set; } // Столбец описания модели
		public ICollection<Order> Orders { get; set; }
	}
}