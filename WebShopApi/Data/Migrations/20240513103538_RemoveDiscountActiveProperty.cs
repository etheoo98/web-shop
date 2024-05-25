using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebShop.Data.Migrations
{
    /// <inheritdoc />
    public partial class RemoveDiscountActiveProperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Discounts");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Discounts",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }
    }
}
