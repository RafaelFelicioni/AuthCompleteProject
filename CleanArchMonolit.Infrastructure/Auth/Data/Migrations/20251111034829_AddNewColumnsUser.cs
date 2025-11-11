using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CleanArchMonolit.Infrastructure.Auth.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddNewColumnsUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TaxId",
                table: "Users",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TaxId",
                table: "Users");
        }
    }
}
