using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace BookShop.Data
{
	[UsedImplicitly]
	public sealed class BookShopContextContextDesignTimeFactory : IDesignTimeDbContextFactory<BSContext>
	{
		private const string DefaultConnectionString =
			"Server=localhost\\SQLEXPRESS;Database=BookShop;Trusted_Connection=True;";

		public static DbContextOptions<BSContext> GetSqlServerOptions([CanBeNull] string connectionString)
		{
			return new DbContextOptionsBuilder<BSContext>()
				.UseSqlServer(connectionString ?? DefaultConnectionString, x =>
				{
					x.MigrationsHistoryTable("__EFMigrationsHistory");
				})
				.Options;
		}
		public BSContext CreateDbContext(string[] args)
		{
			return new BSContext(GetSqlServerOptions(null));
		}
	}
}
