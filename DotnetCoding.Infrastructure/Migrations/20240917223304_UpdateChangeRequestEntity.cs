using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DotnetCoding.Infrastructure.Migrations
{
    public partial class UpdateChangeRequestEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ChangeRequest_ProductQueueId",
                table: "ChangeRequest");

            migrationBuilder.CreateIndex(
                name: "IX_ChangeRequest_ProductQueueId",
                table: "ChangeRequest",
                column: "ProductQueueId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ChangeRequest_ProductQueueId",
                table: "ChangeRequest");

            migrationBuilder.CreateIndex(
                name: "IX_ChangeRequest_ProductQueueId",
                table: "ChangeRequest",
                column: "ProductQueueId");
        }
    }
}
