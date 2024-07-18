using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class Change_ChatEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChatUser_Chats_chatsId",
                table: "ChatUser");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Chats",
                table: "Chats");

            migrationBuilder.DropColumn(
                name: "Chat_Id",
                table: "Chats");

            migrationBuilder.RenameColumn(
                name: "chatsId",
                table: "ChatUser",
                newName: "chatsName");

            migrationBuilder.RenameIndex(
                name: "IX_ChatUser_chatsId",
                table: "ChatUser",
                newName: "IX_ChatUser_chatsName");

            migrationBuilder.AlterColumn<string>(
                name: "Chat_Name",
                table: "Chats",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Chats",
                table: "Chats",
                column: "Chat_Name");

            migrationBuilder.AddForeignKey(
                name: "FK_ChatUser_Chats_chatsName",
                table: "ChatUser",
                column: "chatsName",
                principalTable: "Chats",
                principalColumn: "Chat_Name",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChatUser_Chats_chatsName",
                table: "ChatUser");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Chats",
                table: "Chats");

            migrationBuilder.RenameColumn(
                name: "chatsName",
                table: "ChatUser",
                newName: "chatsId");

            migrationBuilder.RenameIndex(
                name: "IX_ChatUser_chatsName",
                table: "ChatUser",
                newName: "IX_ChatUser_chatsId");

            migrationBuilder.AlterColumn<string>(
                name: "Chat_Name",
                table: "Chats",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "Chat_Id",
                table: "Chats",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Chats",
                table: "Chats",
                column: "Chat_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ChatUser_Chats_chatsId",
                table: "ChatUser",
                column: "chatsId",
                principalTable: "Chats",
                principalColumn: "Chat_Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
