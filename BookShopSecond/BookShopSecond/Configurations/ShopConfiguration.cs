using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookShopSecond.Data.Configurations
{
    public class ShopConfiguration : IEntityTypeConfiguration<Shop>
	{
		public void Configure(EntityTypeBuilder<Shop> builder)
		{
			builder.ToTable(nameof(Shop), BSContext.DefaultSchemaName);
			builder.HasKey(x => x.Id);
			builder.Property(x => x.Id).ValueGeneratedOnAdd();
			builder.Property(x => x.Balance).IsRequired();
			builder.HasCheckConstraint("CK_BookShop.Balance", "[Balance] > 0");
			builder.Property(x => x.MaxBookQuantity).IsRequired();
			builder.HasCheckConstraint("CK_BookShop.Book_Quantity", "[MaxBookQuantity] > 0");
		}
	}
}
