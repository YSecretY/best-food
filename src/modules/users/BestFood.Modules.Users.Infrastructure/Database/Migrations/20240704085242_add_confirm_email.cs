using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BestFood.Modules.Users.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class add_confirm_email : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "EmailConfirmed",
                schema: "users",
                table: "Users",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmailConfirmed",
                schema: "users",
                table: "Users");
        }
    }
}
