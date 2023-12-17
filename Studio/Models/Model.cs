using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Studio.Models
{
	public class Model
	{
		public int Id { get; set; } // Столбец первичного ключа
		public string Name { get; set; } // Столбец имени модели
		public string Description { get; set; } // Столбец описания модели
		public ICollection<Order> Orders { get; set; }
	}
}