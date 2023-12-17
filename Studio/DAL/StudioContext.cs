using Studio.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Studio.DAL
{
	public class StudioContext : DbContext
	{
		// Конструктор с параметром строки подключения
		public StudioContext() : base("StudioContext")
		{
		}

		// Свойства для коллекций сущностей
		public DbSet<Printer> Printers { get; set; }
		public DbSet<Model> Models { get; set; }
		public DbSet<Order> Orders { get; set; }
		public DbSet<Customer> Customers { get; set; }
		public DbSet<Material> Materials { get; set; }

		// Метод для настройки модели
		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Order>()
			.HasRequired(o => o.Customer) // Указываем, что у заказа есть один обязательный клиент
			.WithMany(c => c.Orders) // Указываем, что у клиента есть много заказов
			.HasForeignKey(o => o.CustomerId) // Указываем, какое свойство является внешним ключом для этой связи
			.WillCascadeOnDelete(true); // Указываем, что при удалении клиента удаляются все связанные заказы

			// Определяем внешние ключи для связи Order-Model
			modelBuilder.Entity<Order>()
			.HasRequired(o => o.Model) // Указываем, что у заказа есть одна обязательная модель
			.WithMany(m => m.Orders) // Указываем, что у модели есть много заказов
			.HasForeignKey(o => o.ModelId) // Указываем, какое свойство является внешним ключом для этой связи
			.WillCascadeOnDelete(true); // Указываем, что при удалении модели удаляются все связанные заказы

			modelBuilder.Entity<Material>()
				.HasMany<Order>(s => s.Orders)
				.WithMany(s => s.Materials)
				.Map(cs =>
				{
					cs.MapLeftKey("MaterialId");
					cs.MapRightKey("OrderId");
					cs.ToTable("OrderMaterial");
				});
		}
	}
}