using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Mongos.Migrations
{
    public partial class pushnotification : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DeviceInfos",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeleterUserId = table.Column<long>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    RegID = table.Column<string>(nullable: false),
                    UserID = table.Column<long>(nullable: false),
                    HardwareModel = table.Column<string>(nullable: true),
                    PlatformType = table.Column<string>(nullable: false),
                    PlatformVersion = table.Column<string>(nullable: true),
                    UniqueDeviceId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceInfos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DeviceInfos_AbpUsers_UserID",
                        column: x => x.UserID,
                        principalTable: "AbpUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PushNotificationLogs",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeleterUserId = table.Column<long>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    UserId = table.Column<long>(nullable: false),
                    NotificationSentOn = table.Column<DateTime>(nullable: false),
                    NotificationContent = table.Column<string>(nullable: true),
                    IsRead = table.Column<bool>(nullable: false),
                    ReadOn = table.Column<DateTime>(nullable: true),
                    ReferenceId = table.Column<string>(nullable: true),
                    NotificationBody = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PushNotificationLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PushNotificationLogs_AbpUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DeviceInfos_UserID",
                table: "DeviceInfos",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_PushNotificationLogs_UserId",
                table: "PushNotificationLogs",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DeviceInfos");

            migrationBuilder.DropTable(
                name: "PushNotificationLogs");
        }
    }
}
