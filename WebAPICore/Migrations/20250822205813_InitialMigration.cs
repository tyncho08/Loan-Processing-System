using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LPSystemWebAPICore.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LoanTable",
                columns: table => new
                {
                    LoanId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    LoanType = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    LoanAmt = table.Column<decimal>(type: "decimal(19,4)", nullable: false),
                    LoanROI = table.Column<decimal>(type: "decimal(18,1)", nullable: false),
                    LoanPeriod = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoanTable", x => x.LoanId);
                });

            migrationBuilder.CreateTable(
                name: "UserTable",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserName = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    UserGender = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    UserEmail = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    UserPass = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    UserRole = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTable", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "ApplTable",
                columns: table => new
                {
                    AppId = table.Column<int>(type: "INTEGER", nullable: false),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false),
                    LoanId = table.Column<int>(type: "INTEGER", nullable: false),
                    UserMob = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    UserAdhaar = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    LoanAmt = table.Column<decimal>(type: "decimal(19,4)", nullable: false),
                    AppStatus = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplTable", x => new { x.AppId, x.UserId, x.LoanId });
                    table.ForeignKey(
                        name: "FK_ApplTable_LoanTable_LoanId",
                        column: x => x.LoanId,
                        principalTable: "LoanTable",
                        principalColumn: "LoanId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ApplTable_UserTable_UserId",
                        column: x => x.UserId,
                        principalTable: "UserTable",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "LoanTable",
                columns: new[] { "LoanId", "LoanAmt", "LoanPeriod", "LoanROI", "LoanType" },
                values: new object[,]
                {
                    { 1, 50000m, 12, 12.5m, "Personal Loan" },
                    { 2, 500000m, 240, 8.5m, "Home Loan" },
                    { 3, 300000m, 60, 9.5m, "Car Loan" },
                    { 4, 200000m, 48, 10.5m, "Education Loan" }
                });

            migrationBuilder.InsertData(
                table: "UserTable",
                columns: new[] { "UserId", "UserEmail", "UserGender", "UserName", "UserPass", "UserRole" },
                values: new object[,]
                {
                    { 1, "admin@lpsystem.com", "Male", "admin", "admin123", "Admin" },
                    { 2, "john@example.com", "Male", "john_doe", "password123", "Customer" },
                    { 3, "jane@example.com", "Female", "jane_smith", "password123", "Customer" },
                    { 4, "client1@lpsystem.com", "Male", "client1", "client123", "Client" }
                });

            migrationBuilder.InsertData(
                table: "ApplTable",
                columns: new[] { "AppId", "LoanId", "UserId", "AppStatus", "LoanAmt", "UserAdhaar", "UserMob" },
                values: new object[,]
                {
                    { 1, 1, 2, "Pending", 50000m, "123456789012", "9876543210" },
                    { 2, 2, 3, "Approved", 500000m, "123456789013", "9876543211" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApplTable_LoanId",
                table: "ApplTable",
                column: "LoanId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplTable_UserId",
                table: "ApplTable",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplTable");

            migrationBuilder.DropTable(
                name: "LoanTable");

            migrationBuilder.DropTable(
                name: "UserTable");
        }
    }
}
