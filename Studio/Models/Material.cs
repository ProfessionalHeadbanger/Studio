using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Studio.Models
{
	public class Material
	{
		public int Id { get; set; } // Столбец первичного ключа
		[Required]
        public string MaterialName { get; set; } // Столбец имени материала
		[Required]
        public string Type { get; set; } // Столбец типа материала
		[Required]
		public string Color { get; set; } // Столбец цвета материала
		public ICollection<Order> Orders { get; set; } // Коллекция навигационных свойств для заказов
	}
}