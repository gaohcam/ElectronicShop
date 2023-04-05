using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ElectronicShop_Data.Migrations
{
	public partial class ProductTable : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.CreateTable(
				name: "AppConfigs",
				columns: table => new
				{
					Key = table.Column<string>(type: "nvarchar(450)", nullable: false),
					Value = table.Column<string>(type: "nvarchar(max)", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_AppConfigs", x => x.Key);
				});

			migrationBuilder.CreateTable(
				name: "AppRoleClaims",
				columns: table => new
				{
					Id = table.Column<int>(type: "int", nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
					ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
					ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_AppRoleClaims", x => x.Id);
				});

			migrationBuilder.CreateTable(
				name: "AppRoles",
				columns: table => new
				{
					Id = table.Column<Guid>(type: "uniqueidentifier", maxLength: 200, nullable: false),
					Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
					Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
					NormalizedName = table.Column<string>(type: "nvarchar(max)", nullable: true),
					ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_AppRoles", x => x.Id);
				});

			migrationBuilder.CreateTable(
				name: "AppUserClaims",
				columns: table => new
				{
					Id = table.Column<int>(type: "int", nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
					ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
					ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_AppUserClaims", x => x.Id);
				});

			migrationBuilder.CreateTable(
				name: "AppUserLogins",
				columns: table => new
				{
					UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
					LoginProvider = table.Column<string>(type: "nvarchar(max)", nullable: true),
					ProviderKey = table.Column<string>(type: "nvarchar(max)", nullable: true),
					ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_AppUserLogins", x => x.UserId);
				});

			migrationBuilder.CreateTable(
				name: "AppUserRoles",
				columns: table => new
				{
					UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
					RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_AppUserRoles", x => new { x.UserId, x.RoleId });
				});

			migrationBuilder.CreateTable(
				name: "AppUsers",
				columns: table => new
				{
					Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
					Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
					Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
					UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
					NormalizedUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
					Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
					NormalizedEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
					EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
					PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
					SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
					ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
					PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
					PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
					TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
					LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
					LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
					AccessFailedCount = table.Column<int>(type: "int", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_AppUsers", x => x.Id);
				});

			migrationBuilder.CreateTable(
				name: "AppUserTokens",
				columns: table => new
				{
					UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
					LoginProvider = table.Column<string>(type: "nvarchar(max)", nullable: true),
					Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
					Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_AppUserTokens", x => x.UserId);
				});

			migrationBuilder.CreateTable(
				name: "Categories",
				columns: table => new
				{
					CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
					CategoryName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
					DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
					DateChanged = table.Column<DateTime>(type: "datetime2", nullable: false),
					UserChanged = table.Column<string>(type: "nvarchar(max)", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Categories", x => x.CategoryId)
						.Annotation("SqlServer:Clustered", true);
				});

			migrationBuilder.CreateTable(
				name: "Products",
				columns: table => new
				{
					ProductId = table.Column<string>(type: "nvarchar(450)", nullable: false),
					ProductName = table.Column<string>(type: "nvarchar(max)", nullable: false),
					ProductIntroduction = table.Column<string>(type: "nvarchar(max)", nullable: false),
					ProductDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
					CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
					ProductPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0m),
					ProductSalePrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0m),
					Origin = table.Column<string>(type: "nvarchar(max)", nullable: false),
					ProductImage = table.Column<string>(type: "nvarchar(max)", nullable: false),
					DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
					DateChanged = table.Column<DateTime>(type: "datetime2", nullable: false),
					UserChanged = table.Column<string>(type: "nvarchar(max)", nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Products", x => x.ProductId)
						.Annotation("SqlServer:Clustered", true);
					table.ForeignKey(
						name: "FK_Products_Categories_CategoryId",
						column: x => x.CategoryId,
						principalTable: "Categories",
						principalColumn: "CategoryId",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.InsertData(
				table: "AppConfigs",
				columns: new[] { "Key", "Value" },
				values: new object[,]
				{
					{ "HomeDescription", "This is description of Electronic Shop" },
					{ "HomeKeyword", "This is keyword of Electronic Shop" },
					{ "HomeTitle", "This is home page of Electronic Shop" }
				});

			migrationBuilder.InsertData(
				table: "AppRoles",
				columns: new[] { "Id", "ConcurrencyStamp", "Description", "Name", "NormalizedName" },
				values: new object[] { new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"), "389c7bd1-e384-4d4b-8da8-8322f32abe4c", "Administrator role", "admin", "admin" });

			migrationBuilder.InsertData(
				table: "AppUserRoles",
				columns: new[] { "RoleId", "UserId" },
				values: new object[] { new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"), new Guid("69bd714f-9576-45ba-b5b7-f00649be00de") });

			migrationBuilder.InsertData(
				table: "AppUsers",
				columns: new[] { "Id", "AccessFailedCount", "Address", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
				values: new object[] { new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"), 0, "123 Lien Ap 2-6 X.Vinh Loc A H. Binh Chanh", "4c1fadbd-81c5-4eca-85c8-e7236a6546d5", "lequocanh.qa@gmail.com", true, false, null, "Quoc Anh", "lequocanh.qa@gmail.com", "admin", "AQAAAAEAACcQAAAAEER4v0Ot9UeQZ7kmmuUc/psWTOqnsTDpIKEi4AIgIyCeVe6+FXZleMLXQpuan5Acmg==", "0774642207", false, "", false, "admin" });

			migrationBuilder.InsertData(
				table: "Categories",
				columns: new[] { "CategoryId", "CategoryName", "DateChanged", "DateCreated", "UserChanged" },
				values: new object[,]
				{
					{ new Guid("1c74ed19-9ff1-44ae-86e2-0998344057f7"), "Máy giặt", new DateTime(2023, 3, 18, 20, 1, 22, 782, DateTimeKind.Local).AddTicks(1139), new DateTime(2023, 3, 18, 20, 1, 22, 782, DateTimeKind.Local).AddTicks(1139), "admin" },
					{ new Guid("73aca840-b158-4734-a234-90f75b6d6192"), "Tủ lạnh", new DateTime(2023, 3, 18, 20, 1, 22, 782, DateTimeKind.Local).AddTicks(1135), new DateTime(2023, 3, 18, 20, 1, 22, 782, DateTimeKind.Local).AddTicks(1124), "admin" },
					{ new Guid("805ff97f-8450-4c87-b078-c851ab4c02a7"), "Máy lạnh", new DateTime(2023, 3, 18, 20, 1, 22, 782, DateTimeKind.Local).AddTicks(1137), new DateTime(2023, 3, 18, 20, 1, 22, 782, DateTimeKind.Local).AddTicks(1137), "admin" }
				});

			migrationBuilder.CreateIndex(
				name: "IX_Products_CategoryId",
				table: "Products",
				column: "CategoryId");
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(
				name: "AppConfigs");

			migrationBuilder.DropTable(
				name: "AppRoleClaims");

			migrationBuilder.DropTable(
				name: "AppRoles");

			migrationBuilder.DropTable(
				name: "AppUserClaims");

			migrationBuilder.DropTable(
				name: "AppUserLogins");

			migrationBuilder.DropTable(
				name: "AppUserRoles");

			migrationBuilder.DropTable(
				name: "AppUsers");

			migrationBuilder.DropTable(
				name: "AppUserTokens");

			migrationBuilder.DropTable(
				name: "Products");

			migrationBuilder.DropTable(
				name: "Categories");
		}
	}
}
