using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bankable.Migrations
{
    /// <inheritdoc />
    public partial class userForeignKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_SavingProjects_userId",
                table: "SavingProjects",
                column: "userId");

            migrationBuilder.AddForeignKey(
                name: "FK_SavingProjects_Users_userId",
                table: "SavingProjects",
                column: "userId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SavingProjects_Users_userId",
                table: "SavingProjects");

            migrationBuilder.DropIndex(
                name: "IX_SavingProjects_userId",
                table: "SavingProjects");
        }
    }
}
