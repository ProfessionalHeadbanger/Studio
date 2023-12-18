using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Studio.Models
{
	public class Customer
	{
		public int Id { get; set; } // Столбец первичного ключа
		[Required]
		public string Name { get; set; } // Столбец имени клиента
		[Required]
        [EmailAddress(ErrorMessage = "Некорректный адрес электронной почты")]
        public string Email { get; set; } // Столбец электронной почты клиента
		[Required]
        [RegularExpression(@"^\d{3}-\d{3}-\d{4}$", ErrorMessage = "Некорректный номер телефона. Используйте формат 123-456-7890")]
        public string Phone { get; set; } // Столбец телефона клиента
		public ICollection<Order> Orders { get; set; } // Коллекция навигационных свойств для заказов
	}
}