using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TransactionManagement.Migrations
{
    /// <inheritdoc />
    public partial class YourMigrationName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "buyAmount",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "buyPrice",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "type",
                table: "Transactions");

            migrationBuilder.RenameColumn(
                name: "sellPrice",
                table: "Transactions",
                newName: "price");

            migrationBuilder.RenameColumn(
                name: "sellAmount",
                table: "Transactions",
                newName: "amount");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "price",
                table: "Transactions",
                newName: "sellPrice");

            migrationBuilder.RenameColumn(
                name: "amount",
                table: "Transactions",
                newName: "sellAmount");

            migrationBuilder.AddColumn<double>(
                name: "buyAmount",
                table: "Transactions",
                type: "double",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "buyPrice",
                table: "Transactions",
                type: "double",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "type",
                table: "Transactions",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }
    }
}
