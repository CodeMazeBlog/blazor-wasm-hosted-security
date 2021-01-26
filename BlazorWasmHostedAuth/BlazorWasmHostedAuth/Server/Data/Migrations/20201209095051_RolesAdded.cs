using Microsoft.EntityFrameworkCore.Migrations;

namespace BlazorWasmHostedAuth.Server.Data.Migrations
{
    public partial class RolesAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "b5f291df-59d6-468c-8721-c0d05bf073d9", "70cd447b-0fa5-4474-85a0-647ff8a057d0", "Visitor", "Visitor" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "5a4118a2-4180-4a7f-b516-303f938e83e0", "9e264f84-0588-4356-98db-e37cccf25f29", "Administrator", "ADMINISTRATOR" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5a4118a2-4180-4a7f-b516-303f938e83e0");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b5f291df-59d6-468c-8721-c0d05bf073d9");
        }
    }
}
