using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Studio.Models
{
	public class Customer
	{
		public int Id { get; set; } // Столбец первичного ключа
		public string Name { get; set; } // Столбец имени клиента
		public string Email { get; set; } // Столбец электронной почты клиента
		public string Phone { get; set; } // Столбец телефона клиента
		public ICollection<Order> Orders { get; set; } // Коллекция навигационных свойств для заказов
	}
}