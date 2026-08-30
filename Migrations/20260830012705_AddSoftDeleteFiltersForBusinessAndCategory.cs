using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mostra.Migrations
{
    /// <inheritdoc />
    public partial class AddSoftDeleteFiltersForBusinessAndCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BussinesId",
                table: "Products");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BussinesId",
                table: "Products",
                type: "integer",
                nullable: true);
        }
    }
}
