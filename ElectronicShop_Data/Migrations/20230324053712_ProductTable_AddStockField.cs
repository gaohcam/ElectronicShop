using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ElectronicShop_Data.Migrations
{
    public partial class ProductTable_AddStockField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Stock",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "7ce04ac6-f739-40dd-acc0-f82ff531e3ff");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "4e75024b-4e6a-4973-99f0-ae6f3236b72b", "AQAAAAEAACcQAAAAEEc015qrAlqsMb9PtB1/YNuTgRM4aCJNWjbPMlNMzHAFGvKBX0XFy+yOtwtLDCxAxw==" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: new Guid("1c74ed19-9ff1-44ae-86e2-0998344057f7"),
                columns: new[] { "DateChanged", "DateCreated" },
                values: new object[] { new DateTime(2023, 3, 24, 12, 37, 11, 752, DateTimeKind.Local).AddTicks(5948), new DateTime(2023, 3, 24, 12, 37, 11, 752, DateTimeKind.Local).AddTicks(5948) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: new Guid("73aca840-b158-4734-a234-90f75b6d6192"),
                columns: new[] { "DateChanged", "DateCreated" },
                values: new object[] { new DateTime(2023, 3, 24, 12, 37, 11, 752, DateTimeKind.Local).AddTicks(5943), new DateTime(2023, 3, 24, 12, 37, 11, 752, DateTimeKind.Local).AddTicks(5934) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: new Guid("805ff97f-8450-4c87-b078-c851ab4c02a7"),
                columns: new[] { "DateChanged", "DateCreated" },
                values: new object[] { new DateTime(2023, 3, 24, 12, 37, 11, 752, DateTimeKind.Local).AddTicks(5946), new DateTime(2023, 3, 24, 12, 37, 11, 752, DateTimeKind.Local).AddTicks(5946) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Stock",
                table: "Products");

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "389c7bd1-e384-4d4b-8da8-8322f32abe4c");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "4c1fadbd-81c5-4eca-85c8-e7236a6546d5", "AQAAAAEAACcQAAAAEER4v0Ot9UeQZ7kmmuUc/psWTOqnsTDpIKEi4AIgIyCeVe6+FXZleMLXQpuan5Acmg==" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: new Guid("1c74ed19-9ff1-44ae-86e2-0998344057f7"),
                columns: new[] { "DateChanged", "DateCreated" },
                values: new object[] { new DateTime(2023, 3, 18, 20, 1, 22, 782, DateTimeKind.Local).AddTicks(1139), new DateTime(2023, 3, 18, 20, 1, 22, 782, DateTimeKind.Local).AddTicks(1139) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: new Guid("73aca840-b158-4734-a234-90f75b6d6192"),
                columns: new[] { "DateChanged", "DateCreated" },
                values: new object[] { new DateTime(2023, 3, 18, 20, 1, 22, 782, DateTimeKind.Local).AddTicks(1135), new DateTime(2023, 3, 18, 20, 1, 22, 782, DateTimeKind.Local).AddTicks(1124) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: new Guid("805ff97f-8450-4c87-b078-c851ab4c02a7"),
                columns: new[] { "DateChanged", "DateCreated" },
                values: new object[] { new DateTime(2023, 3, 18, 20, 1, 22, 782, DateTimeKind.Local).AddTicks(1137), new DateTime(2023, 3, 18, 20, 1, 22, 782, DateTimeKind.Local).AddTicks(1137) });
        }
    }
}
