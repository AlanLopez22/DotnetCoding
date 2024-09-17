using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DotnetCoding.Infrastructure.Migrations
{
    public partial class UpdateModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ProductQueue_Status",
                table: "ProductQueue");

            migrationBuilder.DropIndex(
                name: "IX_ProductDetails_IsActive_PostedDate",
                table: "ProductDetails");

            migrationBuilder.DropColumn(
                name: "InactivatedDate",
                table: "ProductQueue");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "ProductQueue");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "ProductDetails");

            migrationBuilder.RenameColumn(
                name: "RequestDate",
                table: "ProductQueue",
                newName: "RequestedDate");

            migrationBuilder.RenameIndex(
                name: "IX_ProductQueue_RequestDate",
                table: "ProductQueue",
                newName: "IX_ProductQueue_RequestedDate");

            migrationBuilder.AddColumn<DateTime>(
                name: "ApprovedDate",
                table: "ProductQueue",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RejectedDate",
                table: "ProductQueue",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "State",
                table: "ProductQueue",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "ProductDetails",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "ProductDetails",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<DateTime>(
                name: "PostedDate",
                table: "ProductDetails",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DeletedDate",
                table: "ProductDetails",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<int>(
                name: "State",
                table: "ProductDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ProductQueue_State",
                table: "ProductQueue",
                column: "State");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ProductQueue_State",
                table: "ProductQueue");

            migrationBuilder.DropColumn(
                name: "ApprovedDate",
                table: "ProductQueue");

            migrationBuilder.DropColumn(
                name: "RejectedDate",
                table: "ProductQueue");

            migrationBuilder.DropColumn(
                name: "State",
                table: "ProductQueue");

            migrationBuilder.DropColumn(
                name: "State",
                table: "ProductDetails");

            migrationBuilder.RenameColumn(
                name: "RequestedDate",
                table: "ProductQueue",
                newName: "RequestDate");

            migrationBuilder.RenameIndex(
                name: "IX_ProductQueue_RequestedDate",
                table: "ProductQueue",
                newName: "IX_ProductQueue_RequestDate");

            migrationBuilder.AddColumn<DateTime>(
                name: "InactivatedDate",
                table: "ProductQueue",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "ProductQueue",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "ProductDetails",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "ProductDetails",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<DateTime>(
                name: "PostedDate",
                table: "ProductDetails",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DeletedDate",
                table: "ProductDetails",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "ProductDetails",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_ProductQueue_Status",
                table: "ProductQueue",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_ProductDetails_IsActive_PostedDate",
                table: "ProductDetails",
                columns: new[] { "IsActive", "PostedDate" });
        }
    }
}
