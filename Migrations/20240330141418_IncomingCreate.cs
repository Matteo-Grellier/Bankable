using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bankable.Migrations
{
    /// <inheritdoc />
    public partial class IncomingCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Incomings_BankAccounts_BankAccountId1",
                table: "Incomings");

            migrationBuilder.DropIndex(
                name: "IX_Incomings_BankAccountId1",
                table: "Incomings");

            migrationBuilder.DropColumn(
                name: "BankAccountId1",
                table: "Incomings");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "BankAccountId1",
                table: "Incomings",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Incomings_BankAccountId1",
                table: "Incomings",
                column: "BankAccountId1",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Incomings_BankAccounts_BankAccountId1",
                table: "Incomings",
                column: "BankAccountId1",
                principalTable: "BankAccounts",
                principalColumn: "Id");
        }
    }
}
