using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebShop.Data.Migrations
{
    /// <inheritdoc />
    public partial class addedDiscountProperty2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "DiscountedPrice",
                table: "Discounts",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "TEXT",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "DiscountedPrice",
                table: "Discounts",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "TEXT");
        }
    }
}
