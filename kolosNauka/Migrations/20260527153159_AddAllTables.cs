using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace kolosNauka.Migrations
{
    /// <inheritdoc />
    public partial class AddAllTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "test-example");

            migrationBuilder.CreateTable(
                name: "ComponentManufacturers",
                schema: "test-example",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Abbreviation = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    FoundationDate = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComponentManufacturers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ComponentTypes",
                schema: "test-example",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Abbreviation = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComponentTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PCs",
                schema: "test-example",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Weight = table.Column<float>(type: "real", nullable: false),
                    Warranty = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Stock = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PCs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Components",
                schema: "test-example",
                columns: table => new
                {
                    Code = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ComponentManufacturerId = table.Column<int>(type: "int", nullable: false),
                    ComponentTypesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Components", x => x.Code);
                    table.ForeignKey(
                        name: "FK_Components_ComponentManufacturers_ComponentManufacturerId",
                        column: x => x.ComponentManufacturerId,
                        principalSchema: "test-example",
                        principalTable: "ComponentManufacturers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Components_ComponentTypes_ComponentTypesId",
                        column: x => x.ComponentTypesId,
                        principalSchema: "test-example",
                        principalTable: "ComponentTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PCComponents",
                schema: "test-example",
                columns: table => new
                {
                    PCId = table.Column<int>(type: "int", nullable: false),
                    ComponentCode = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Amount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PCComponents", x => new { x.PCId, x.ComponentCode });
                    table.ForeignKey(
                        name: "FK_PCComponents_Components_ComponentCode",
                        column: x => x.ComponentCode,
                        principalSchema: "test-example",
                        principalTable: "Components",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PCComponents_PCs_PCId",
                        column: x => x.PCId,
                        principalSchema: "test-example",
                        principalTable: "PCs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Components_ComponentManufacturerId",
                schema: "test-example",
                table: "Components",
                column: "ComponentManufacturerId");

            migrationBuilder.CreateIndex(
                name: "IX_Components_ComponentTypesId",
                schema: "test-example",
                table: "Components",
                column: "ComponentTypesId");

            migrationBuilder.CreateIndex(
                name: "IX_PCComponents_ComponentCode",
                schema: "test-example",
                table: "PCComponents",
                column: "ComponentCode");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PCComponents",
                schema: "test-example");

            migrationBuilder.DropTable(
                name: "Components",
                schema: "test-example");

            migrationBuilder.DropTable(
                name: "PCs",
                schema: "test-example");

            migrationBuilder.DropTable(
                name: "ComponentManufacturers",
                schema: "test-example");

            migrationBuilder.DropTable(
                name: "ComponentTypes",
                schema: "test-example");
        }
    }
}
