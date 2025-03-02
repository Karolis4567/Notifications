using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Notifications.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CompanyMarkets",
                columns: table => new
                {
                    CompanyMarketId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyMarketCode = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: false),
                    CompanyMarketName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2025, 3, 2, 17, 11, 45, 275, DateTimeKind.Local).AddTicks(6534))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyMarkets", x => x.CompanyMarketId);
                });

            migrationBuilder.CreateTable(
                name: "CompanyTypes",
                columns: table => new
                {
                    CompanyTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyTypeCode = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false),
                    CompanyTypeName = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2025, 3, 2, 17, 11, 45, 288, DateTimeKind.Local).AddTicks(4755))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyTypes", x => x.CompanyTypeId);
                });

            migrationBuilder.CreateTable(
                name: "NotificationSchedules",
                columns: table => new
                {
                    NotificationScheduleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Days = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2025, 3, 2, 17, 11, 45, 303, DateTimeKind.Local).AddTicks(3833)),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2025, 3, 2, 17, 11, 45, 303, DateTimeKind.Local).AddTicks(4862))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationSchedules", x => x.NotificationScheduleId);
                });

            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompanyNumber = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    CompanyName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    CompanyTypeId = table.Column<int>(type: "int", nullable: false),
                    CompanyMarketId = table.Column<int>(type: "int", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2025, 3, 2, 17, 11, 45, 294, DateTimeKind.Local).AddTicks(319)),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2025, 3, 2, 17, 11, 45, 294, DateTimeKind.Local).AddTicks(1705))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.CompanyId);
                    table.ForeignKey(
                        name: "FK_Companies_CompanyMarkets_CompanyMarketId",
                        column: x => x.CompanyMarketId,
                        principalTable: "CompanyMarkets",
                        principalColumn: "CompanyMarketId");
                    table.ForeignKey(
                        name: "FK_Companies_CompanyTypes_CompanyTypeId",
                        column: x => x.CompanyTypeId,
                        principalTable: "CompanyTypes",
                        principalColumn: "CompanyTypeId");
                });

            migrationBuilder.CreateTable(
                name: "CompanyNotificationsScheduleAssignRules",
                columns: table => new
                {
                    CompanyNotificationScheduleAssignRulesId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyMarketId = table.Column<int>(type: "int", nullable: false),
                    CompanyTypeId = table.Column<int>(type: "int", nullable: false),
                    NotificationScheduleId = table.Column<int>(type: "int", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2025, 3, 2, 17, 11, 45, 307, DateTimeKind.Local).AddTicks(6217)),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2025, 3, 2, 17, 11, 45, 307, DateTimeKind.Local).AddTicks(7409))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyNotificationsScheduleAssignRules", x => x.CompanyNotificationScheduleAssignRulesId);
                    table.ForeignKey(
                        name: "FK_CompanyNotificationsScheduleAssignRules_CompanyMarkets_CompanyMarketId",
                        column: x => x.CompanyMarketId,
                        principalTable: "CompanyMarkets",
                        principalColumn: "CompanyMarketId");
                    table.ForeignKey(
                        name: "FK_CompanyNotificationsScheduleAssignRules_CompanyTypes_CompanyTypeId",
                        column: x => x.CompanyTypeId,
                        principalTable: "CompanyTypes",
                        principalColumn: "CompanyTypeId");
                    table.ForeignKey(
                        name: "FK_CompanyNotificationsScheduleAssignRules_NotificationSchedules_NotificationScheduleId",
                        column: x => x.NotificationScheduleId,
                        principalTable: "NotificationSchedules",
                        principalColumn: "NotificationScheduleId");
                });

            migrationBuilder.CreateTable(
                name: "CompanyNotificationSchedules",
                columns: table => new
                {
                    CompanyNotificationsScheduleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NotificationScheduleId = table.Column<int>(type: "int", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2025, 3, 2, 17, 11, 45, 311, DateTimeKind.Local).AddTicks(8464)),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2025, 3, 2, 17, 11, 45, 311, DateTimeKind.Local).AddTicks(9542))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyNotificationSchedules", x => x.CompanyNotificationsScheduleId);
                    table.ForeignKey(
                        name: "FK_CompanyNotificationSchedules_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "CompanyId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CompanyNotificationSchedules_NotificationSchedules_NotificationScheduleId",
                        column: x => x.NotificationScheduleId,
                        principalTable: "NotificationSchedules",
                        principalColumn: "NotificationScheduleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "CompanyMarkets",
                columns: new[] { "CompanyMarketId", "CompanyMarketCode", "CompanyMarketName" },
                values: new object[,]
                {
                    { 1, "DK", "Denmark" },
                    { 2, "NO", "Norway" },
                    { 3, "SE", "Sweden" },
                    { 4, "FI", "Finland" }
                });

            migrationBuilder.InsertData(
                table: "CompanyTypes",
                columns: new[] { "CompanyTypeId", "CompanyTypeCode", "CompanyTypeName" },
                values: new object[,]
                {
                    { 1, "s", "small" },
                    { 2, "m", "medium" },
                    { 3, "l", "large" }
                });

            migrationBuilder.InsertData(
                table: "NotificationSchedules",
                columns: new[] { "NotificationScheduleId", "CreationDate", "Days", "UpdateDate" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 3, 2, 17, 11, 45, 303, DateTimeKind.Local).AddTicks(7395), "1,5,10,15,20", new DateTime(2025, 3, 2, 17, 11, 45, 303, DateTimeKind.Local).AddTicks(7931) },
                    { 2, new DateTime(2025, 3, 2, 17, 11, 45, 303, DateTimeKind.Local).AddTicks(8344), "1,5,10,20", new DateTime(2025, 3, 2, 17, 11, 45, 303, DateTimeKind.Local).AddTicks(8354) },
                    { 3, new DateTime(2025, 3, 2, 17, 11, 45, 303, DateTimeKind.Local).AddTicks(8359), "1,7,14,28", new DateTime(2025, 3, 2, 17, 11, 45, 303, DateTimeKind.Local).AddTicks(8364) }
                });

            migrationBuilder.InsertData(
                table: "CompanyNotificationsScheduleAssignRules",
                columns: new[] { "CompanyNotificationScheduleAssignRulesId", "CompanyMarketId", "CompanyTypeId", "NotificationScheduleId" },
                values: new object[,]
                {
                    { 1, 1, 1, 1 },
                    { 2, 1, 2, 1 },
                    { 3, 1, 3, 1 },
                    { 4, 2, 1, 2 },
                    { 5, 2, 2, 2 },
                    { 6, 2, 3, 2 },
                    { 7, 3, 1, 3 },
                    { 8, 3, 2, 3 },
                    { 9, 4, 3, 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Companies_CompanyMarketId",
                table: "Companies",
                column: "CompanyMarketId");

            migrationBuilder.CreateIndex(
                name: "IX_Companies_CompanyTypeId",
                table: "Companies",
                column: "CompanyTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyNotificationSchedules_CompanyId",
                table: "CompanyNotificationSchedules",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyNotificationSchedules_NotificationScheduleId",
                table: "CompanyNotificationSchedules",
                column: "NotificationScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyNotificationsScheduleAssignRules_CompanyMarketId",
                table: "CompanyNotificationsScheduleAssignRules",
                column: "CompanyMarketId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyNotificationsScheduleAssignRules_CompanyTypeId",
                table: "CompanyNotificationsScheduleAssignRules",
                column: "CompanyTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyNotificationsScheduleAssignRules_NotificationScheduleId",
                table: "CompanyNotificationsScheduleAssignRules",
                column: "NotificationScheduleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CompanyNotificationSchedules");

            migrationBuilder.DropTable(
                name: "CompanyNotificationsScheduleAssignRules");

            migrationBuilder.DropTable(
                name: "Companies");

            migrationBuilder.DropTable(
                name: "NotificationSchedules");

            migrationBuilder.DropTable(
                name: "CompanyMarkets");

            migrationBuilder.DropTable(
                name: "CompanyTypes");
        }
    }
}
