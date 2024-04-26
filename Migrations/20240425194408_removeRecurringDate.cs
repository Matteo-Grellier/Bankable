using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bankable.Migrations
{
    /// <inheritdoc />
    public partial class removeRecurringDate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsRecurrent",
                table: "Spendings");

            migrationBuilder.DropColumn(
                name: "RecurringDate",
                table: "Spendings");

            migrationBuilder.DropColumn(
                name: "RecurringDate",
                table: "Incomings");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsRecurrent",
                table: "Spendings",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "RecurringDate",
                table: "Spendings",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "RecurringDate",
                table: "Incomings",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
