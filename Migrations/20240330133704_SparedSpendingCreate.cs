using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bankable.Migrations
{
    /// <inheritdoc />
    public partial class SparedSpendingCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SparedSpendings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    SavingProjectId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Amount = table.Column<float>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SparedSpendings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SparedSpendings_SavingProjects_SavingProjectId",
                        column: x => x.SavingProjectId,
                        principalTable: "SavingProjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SparedSpendings_SavingProjectId",
                table: "SparedSpendings",
                column: "SavingProjectId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SparedSpendings");
        }
    }
}
