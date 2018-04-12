using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace DAL.Migrations
{
    public partial class test : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_News_ItemId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Users_UserId",
                table: "Comments");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Comments",
                newName: "UserDboId");

            migrationBuilder.RenameColumn(
                name: "ItemId",
                table: "Comments",
                newName: "ItemDboId");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_UserId",
                table: "Comments",
                newName: "IX_Comments_UserDboId");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_ItemId",
                table: "Comments",
                newName: "IX_Comments_ItemDboId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_News_ItemDboId",
                table: "Comments",
                column: "ItemDboId",
                principalTable: "News",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Users_UserDboId",
                table: "Comments",
                column: "UserDboId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_News_ItemDboId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Users_UserDboId",
                table: "Comments");

            migrationBuilder.RenameColumn(
                name: "UserDboId",
                table: "Comments",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "ItemDboId",
                table: "Comments",
                newName: "ItemId");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_UserDboId",
                table: "Comments",
                newName: "IX_Comments_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_ItemDboId",
                table: "Comments",
                newName: "IX_Comments_ItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_News_ItemId",
                table: "Comments",
                column: "ItemId",
                principalTable: "News",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Users_UserId",
                table: "Comments",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
