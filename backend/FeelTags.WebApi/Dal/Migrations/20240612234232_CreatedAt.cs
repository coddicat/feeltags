using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FeelTags.WebApi.Dal.Migrations
{
    /// <inheritdoc />
    public partial class CreatedAt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedTime",
                table: "Questions");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Questions",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Accounts",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Accounts");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedTime",
                table: "Questions",
                type: "datetime(6)",
                nullable: true);
        }
    }
}
