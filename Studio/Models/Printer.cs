using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Studio.Models
{
	public enum Status
	{
		Ready, Working, Repairing
	}

	public class Printer
	{
		public int Id { get; set; } // Столбец первичного ключа
		[Required]
		public string Manufacturer { get; set; }
		[Required]
		public string Name { get; set; } // Столбец имени принтера
		[Required]
		public Status Status { get; set; } // Столбец статуса принтера
	}
}