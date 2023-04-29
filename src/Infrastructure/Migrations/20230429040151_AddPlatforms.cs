using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddPlatforms : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "platform",
                table: "announcements");

            migrationBuilder.AddColumn<string>(
                name: "platformid",
                table: "announcements",
                type: "text",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "platforms",
                columns: table => new
                {
                    id = table.Column<string>(type: "text", nullable: false),
                    name = table.Column<string>(type: "text", nullable: true),
                    icon_url = table.Column<string>(type: "text", nullable: true),
                    url = table.Column<string>(type: "text", nullable: true),
                    thumb_url = table.Column<string>(type: "text", nullable: true),
                    createdAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_platforms", x => x.id);
                });

            migrationBuilder.InsertData(
                table: "platforms",
                columns: new[] { "id", "createdAt", "icon_url", "name", "thumb_url", "updatedAt", "url" },
                values: new object[,]
                {
                    { "162b482f-a5c9-4ce4-8921-bc0b39eb1fe9", new DateTime(2023, 4, 29, 4, 1, 51, 564, DateTimeKind.Utc).AddTicks(6448), "https://icones.pro/wp-content/uploads/2021/03/symbole-de-plume-bleue.png", "freelancerbr", "https://freelancer.com.br/images/main-page/find-section-bg.jpg", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://freelancer.com.br" },
                    { "69f1f410-a320-417b-9d44-762ac5970810", new DateTime(2023, 4, 29, 4, 1, 51, 564, DateTimeKind.Utc).AddTicks(6467), "https://play-lh.googleusercontent.com/yNBbWLb1_Te7UELDjx4OjKpkYcW79nklxeB-xJThgaN0hiXpIDs2hHt3P9XISi4fEjwH=w240-h480-rw", "99freelas", "https://s3-sa-east-1.amazonaws.com/projetos-artes/fullsize%2F2014%2F04%2F07%2F16%2FWDL-Logo-39095_16236_061350792_1651536830.jpg", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://www.99freelas.com.br" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_announcements_platformid",
                table: "announcements",
                column: "platformid");

            migrationBuilder.CreateIndex(
                name: "IX_platforms_url",
                table: "platforms",
                column: "url",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_announcements_platforms_platformid",
                table: "announcements",
                column: "platformid",
                principalTable: "platforms",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_announcements_platforms_platformid",
                table: "announcements");

            migrationBuilder.DropTable(
                name: "platforms");

            migrationBuilder.DropIndex(
                name: "IX_announcements_platformid",
                table: "announcements");

            migrationBuilder.DropColumn(
                name: "platformid",
                table: "announcements");

            migrationBuilder.AddColumn<string>(
                name: "platform",
                table: "announcements",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
