using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DotnetCoding.Infrastructure.Migrations
{
    public partial class CreateModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProductDetails",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    PostedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductDetails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductQueue",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    RequestReason = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    RequestDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    InactivatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductQueue", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductQueue_ProductDetails_ProductId",
                        column: x => x.ProductId,
                        principalTable: "ProductDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductDetails_IsActive_PostedDate",
                table: "ProductDetails",
                columns: new[] { "IsActive", "PostedDate" });

            migrationBuilder.CreateIndex(
                name: "IX_ProductDetails_Name",
                table: "ProductDetails",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_ProductDetails_PostedDate",
                table: "ProductDetails",
                column: "PostedDate");

            migrationBuilder.CreateIndex(
                name: "IX_ProductDetails_Price",
                table: "ProductDetails",
                column: "Price");

            migrationBuilder.CreateIndex(
                name: "IX_ProductDetails_Status",
                table: "ProductDetails",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_ProductQueue_ProductId",
                table: "ProductQueue",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductQueue_RequestDate",
                table: "ProductQueue",
                column: "RequestDate");

            migrationBuilder.CreateIndex(
                name: "IX_ProductQueue_Status",
                table: "ProductQueue",
                column: "Status");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductQueue");

            migrationBuilder.DropTable(
                name: "ProductDetails");
        }
    }
}
