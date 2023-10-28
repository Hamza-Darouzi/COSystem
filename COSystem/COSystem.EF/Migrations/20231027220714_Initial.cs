using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace COSystem.EF.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Company",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "VARCHAR(100)", maxLength: 100, nullable: false),
                    Activity = table.Column<string>(type: "VARCHAR(100)", maxLength: 100, nullable: true),
                    FoundingDate = table.Column<DateTime>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Company", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DistributionBranch",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "VARCHAR(100)", maxLength: 100, nullable: false),
                    Address = table.Column<string>(type: "VARCHAR(100)", maxLength: 100, nullable: true),
                    CompanyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DistributionBranch", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DistributionBranch_Company_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Company",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "VARCHAR(100)", maxLength: 100, nullable: false),
                    Type = table.Column<string>(type: "VARCHAR(100)", maxLength: 100, nullable: true),
                    CompanyId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Product_Company_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Company",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProductionBranch",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "VARCHAR(100)", maxLength: 100, nullable: false),
                    Address = table.Column<string>(type: "VARCHAR(100)", maxLength: 100, nullable: true),
                    CompanyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductionBranch", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductionBranch_Company_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Company",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Production",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductionBranchId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "INT", nullable: false),
                    ProductionDate = table.Column<DateTime>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Production", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Production_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Production_ProductionBranch_ProductionBranchId",
                        column: x => x.ProductionBranchId,
                        principalTable: "ProductionBranch",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Transaction",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DistributionBranchId = table.Column<int>(type: "int", nullable: false),
                    ProductionBranchId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "INT", nullable: false),
                    TransactionDate = table.Column<DateTime>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transaction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transaction_DistributionBranch_DistributionBranchId",
                        column: x => x.DistributionBranchId,
                        principalTable: "DistributionBranch",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Transaction_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Transaction_ProductionBranch_ProductionBranchId",
                        column: x => x.ProductionBranchId,
                        principalTable: "ProductionBranch",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_DistributionBranch_CompanyId",
                table: "DistributionBranch",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_CompanyId",
                table: "Product",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Production_ProductId",
                table: "Production",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Production_ProductionBranchId",
                table: "Production",
                column: "ProductionBranchId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductionBranch_CompanyId",
                table: "ProductionBranch",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_DistributionBranchId",
                table: "Transaction",
                column: "DistributionBranchId");

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_ProductId",
                table: "Transaction",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_ProductionBranchId",
                table: "Transaction",
                column: "ProductionBranchId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Production");

            migrationBuilder.DropTable(
                name: "Transaction");

            migrationBuilder.DropTable(
                name: "DistributionBranch");

            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "ProductionBranch");

            migrationBuilder.DropTable(
                name: "Company");
        }
    }
}
