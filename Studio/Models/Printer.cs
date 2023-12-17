using System;
using System.Collections.Generic;
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
		public string Manufacturer { get; set; }
		public string Name { get; set; } // Столбец имени принтера
		public Status Status { get; set; } // Столбец статуса принтера
	}
}