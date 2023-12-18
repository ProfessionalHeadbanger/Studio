using Studio.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Status = Studio.Models.Status;

namespace Studio.DAL
{
	public class StudioInitializer : DropCreateDatabaseIfModelChanges<StudioContext>
	{
		// В этом методе можно заполнить таблицы начальными данными
		protected override void Seed(StudioContext context)
		{
			// Создаем список принтеров
			var printers = new List<Printer>
			{
				new Printer { Manufacturer = "MakerBot", Name = "Replicator", Status = Status.Working },
				new Printer { Manufacturer = "Ultimaker", Name = "2+", Status = Status.Working },
				new Printer { Manufacturer = "Formlabs", Name = "Form 3", Status = Status.Working }
			};

			// Добавляем принтеры в контекст
			printers.ForEach(s => context.Printers.Add(s));
			context.SaveChanges();

			// Создаем список моделей
			var models = new List<Model>
			{
				new Model { Name = "Chess Piece", Description = "A pawn for chess game" },
				new Model { Name = "Vase", Description = "A decorative vase with flower pattern" },
				new Model { Name = "Keychain", Description = "A personalized keychain with name" }
			};

			// Добавляем модели в контекст
			models.ForEach(s => context.Models.Add(s));
			context.SaveChanges();

			// Создаем список клиентов
			var customers = new List<Customer>
			{
				new Customer { Name = "Alice", Email = "alice@example.com", Phone = "123-456-7890" },
				new Customer { Name = "Bob", Email = "bob@example.com", Phone = "234-567-8900" },
				new Customer { Name = "Charlie", Email = "charlie@example.com", Phone = "345-678-9012" }
			};

			// Добавляем клиентов в контекст
			customers.ForEach(s => context.Customers.Add(s));
			context.SaveChanges();

			// Создаем список материалов
			var materials = new List<Material>
			{
				new Material { Name = "SUNLU", Type = "ABS", Color = "White" },
				new Material { Name = "MAKO", Type = "PETG", Color = "Black" },
				new Material { Name = "ERYONE", Type = "PLA", Color = "Clear" }
			};

			// Добавляем материалы в контекст
			materials.ForEach(s => context.Materials.Add(s));
			context.SaveChanges();

			// Создаем список заказов
			var orders = new List<Order>
			{
				new Order { Date = DateTime.Now, Price = 100, CustomerId = 1, ModelId = 1, PrinterId = 1 },
				new Order { Date = DateTime.Now.AddDays(-1), Price = 150, CustomerId = 2, ModelId = 2, PrinterId = 2 },
				new Order { Date = DateTime.Now.AddDays(-2), Price = 200, CustomerId = 3, ModelId = 3, PrinterId = 3 }
			};

			orders[0].Materials.Add(materials[2]);
			orders[1].Materials.Add(materials[0]);
			orders[2].Materials.Add(materials[1]);
			// Добавляем заказы в контекст
			orders.ForEach(s => context.Orders.Add(s));
			context.SaveChanges();

			// Вызываем базовый метод
			base.Seed(context);
		}
	}
}