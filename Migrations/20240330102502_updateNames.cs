using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bankable.Migrations
{
    /// <inheritdoc />
    public partial class updateNames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SavingProjects_Users_userId",
                table: "SavingProjects");

            migrationBuilder.RenameColumn(
                name: "updateAt",
                table: "Users",
                newName: "UpdateAt");

            migrationBuilder.RenameColumn(
                name: "lastName",
                table: "Users",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "firstName",
                table: "Users",
                newName: "FirstName");

            migrationBuilder.RenameColumn(
                name: "createdAt",
                table: "Users",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "willEndAt",
                table: "SavingProjects",
                newName: "WillEndAt");

            migrationBuilder.RenameColumn(
                name: "userId",
                table: "SavingProjects",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "updateAt",
                table: "SavingProjects",
                newName: "UpdateAt");

            migrationBuilder.RenameColumn(
                name: "lastName",
                table: "SavingProjects",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "firstName",
                table: "SavingProjects",
                newName: "FirstName");

            migrationBuilder.RenameColumn(
                name: "finalAmount",
                table: "SavingProjects",
                newName: "FinalAmount");

            migrationBuilder.RenameColumn(
                name: "currentAmountSaved",
                table: "SavingProjects",
                newName: "CurrentAmountSaved");

            migrationBuilder.RenameColumn(
                name: "createdAt",
                table: "SavingProjects",
                newName: "CreatedAt");

            migrationBuilder.RenameIndex(
                name: "IX_SavingProjects_userId",
                table: "SavingProjects",
                newName: "IX_SavingProjects_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_SavingProjects_Users_UserId",
                table: "SavingProjects",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SavingProjects_Users_UserId",
                table: "SavingProjects");

            migrationBuilder.RenameColumn(
                name: "UpdateAt",
                table: "Users",
                newName: "updateAt");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "Users",
                newName: "lastName");

            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "Users",
                newName: "firstName");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Users",
                newName: "createdAt");

            migrationBuilder.RenameColumn(
                name: "WillEndAt",
                table: "SavingProjects",
                newName: "willEndAt");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "SavingProjects",
                newName: "userId");

            migrationBuilder.RenameColumn(
                name: "UpdateAt",
                table: "SavingProjects",
                newName: "updateAt");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "SavingProjects",
                newName: "lastName");

            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "SavingProjects",
                newName: "firstName");

            migrationBuilder.RenameColumn(
                name: "FinalAmount",
                table: "SavingProjects",
                newName: "finalAmount");

            migrationBuilder.RenameColumn(
                name: "CurrentAmountSaved",
                table: "SavingProjects",
                newName: "currentAmountSaved");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "SavingProjects",
                newName: "createdAt");

            migrationBuilder.RenameIndex(
                name: "IX_SavingProjects_UserId",
                table: "SavingProjects",
                newName: "IX_SavingProjects_userId");

            migrationBuilder.AddForeignKey(
                name: "FK_SavingProjects_Users_userId",
                table: "SavingProjects",
                column: "userId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
