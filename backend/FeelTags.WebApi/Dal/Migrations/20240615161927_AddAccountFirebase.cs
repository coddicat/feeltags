using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FeelTags.WebApi.Dal.Migrations
{
    /// <inheritdoc />
    public partial class AddAccountFirebase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AuthProvider",
                table: "Accounts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "FirebaseUid",
                table: "Accounts",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AuthProvider",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "FirebaseUid",
                table: "Accounts");
        }
    }
}
