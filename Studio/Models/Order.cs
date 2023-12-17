using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Studio.Models
{
	public class Order
	{
		public int Id { get; set; } // Столбец первичного ключа
		[Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; } // Столбец даты заказа
		[Required, Range(100.0, double.MaxValue)]
		public decimal Price { get; set; } // Столбец цены заказа
		public int CustomerId { get; set; } // Столбец внешнего ключа для клиента
		public int ModelId { get; set; }
		public int PrinterId { get; set; }
		public Customer Customer { get; set; }
		public Model Model { get; set; }// Навигационное свойство для клиента
		public ICollection<Material> Materials { get; set; } // Коллекция навигационных свойств для материалов
		public Printer Printer { get; set; }

		public Order()
		{
			this.Materials = new HashSet<Material>();
		}
	}
}