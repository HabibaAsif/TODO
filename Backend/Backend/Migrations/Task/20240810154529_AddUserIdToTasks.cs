using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Migrations.Task
{
    /// <inheritdoc />
    public partial class AddUserIdToTasks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserTasks_BackendUser_UserId",
                table: "UserTasks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BackendUser",
                table: "BackendUser");

            migrationBuilder.RenameTable(
                name: "BackendUser",
                newName: "Users");

            migrationBuilder.AddColumn<string>(
                name: "BackendUserId",
                table: "UserTasks",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_UserTasks_BackendUserId",
                table: "UserTasks",
                column: "BackendUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserTasks_Users_BackendUserId",
                table: "UserTasks",
                column: "BackendUserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserTasks_Users_UserId",
                table: "UserTasks",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserTasks_Users_BackendUserId",
                table: "UserTasks");

            migrationBuilder.DropForeignKey(
                name: "FK_UserTasks_Users_UserId",
                table: "UserTasks");

            migrationBuilder.DropIndex(
                name: "IX_UserTasks_BackendUserId",
                table: "UserTasks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "BackendUserId",
                table: "UserTasks");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "BackendUser");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BackendUser",
                table: "BackendUser",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserTasks_BackendUser_UserId",
                table: "UserTasks",
                column: "UserId",
                principalTable: "BackendUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
