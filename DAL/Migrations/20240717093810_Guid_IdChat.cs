using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class Guid_IdChat : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChatUser_Chats_chatsName",
                table: "ChatUser");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ChatUser",
                table: "ChatUser");

            migrationBuilder.DropIndex(
                name: "IX_ChatUser_chatsName",
                table: "ChatUser");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Chats",
                table: "Chats");

            migrationBuilder.DropColumn(
                name: "chatsName",
                table: "ChatUser");

            migrationBuilder.AddColumn<Guid>(
                name: "chatsChat_ID",
                table: "ChatUser",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<string>(
                name: "Chat_Name",
                table: "Chats",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<Guid>(
                name: "Chat_ID",
                table: "Chats",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_ChatUser",
                table: "ChatUser",
                columns: new[] { "UsersId", "chatsChat_ID" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Chats",
                table: "Chats",
                column: "Chat_ID");

            migrationBuilder.CreateIndex(
                name: "IX_ChatUser_chatsChat_ID",
                table: "ChatUser",
                column: "chatsChat_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_ChatUser_Chats_chatsChat_ID",
                table: "ChatUser",
                column: "chatsChat_ID",
                principalTable: "Chats",
                principalColumn: "Chat_ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChatUser_Chats_chatsChat_ID",
                table: "ChatUser");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ChatUser",
                table: "ChatUser");

            migrationBuilder.DropIndex(
                name: "IX_ChatUser_chatsChat_ID",
                table: "ChatUser");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Chats",
                table: "Chats");

            migrationBuilder.DropColumn(
                name: "chatsChat_ID",
                table: "ChatUser");

            migrationBuilder.DropColumn(
                name: "Chat_ID",
                table: "Chats");

            migrationBuilder.AddColumn<string>(
                name: "chatsName",
                table: "ChatUser",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Chat_Name",
                table: "Chats",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ChatUser",
                table: "ChatUser",
                columns: new[] { "UsersId", "chatsName" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Chats",
                table: "Chats",
                column: "Chat_Name");

            migrationBuilder.CreateIndex(
                name: "IX_ChatUser_chatsName",
                table: "ChatUser",
                column: "chatsName");

            migrationBuilder.AddForeignKey(
                name: "FK_ChatUser_Chats_chatsName",
                table: "ChatUser",
                column: "chatsName",
                principalTable: "Chats",
                principalColumn: "Chat_Name",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
