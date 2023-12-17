using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Studio.Models
{
	public class Material
	{
		public int Id { get; set; } // Столбец первичного ключа
        public string Name { get; set; } // Столбец имени материала
        public string Type { get; set; } // Столбец типа материала
		public string Color { get; set; } // Столбец цвета материала
		public ICollection<Order> Orders { get; set; } // Коллекция навигационных свойств для заказов
	}
}