using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
  /// <inheritdoc />
  public partial class AddJobs : Migration
  {
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.CreateTable(
          name: "authors",
          columns: table => new
          {
            id = table.Column<string>(type: "text", nullable: false),
            name = table.Column<string>(type: "text", nullable: true),
            avatar_url = table.Column<string>(type: "text", nullable: true),
            createdAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
            updatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_authors", x => x.id);
          });

      migrationBuilder.CreateTable(
          name: "announcements",
          columns: table => new
          {
            id = table.Column<string>(type: "text", nullable: false),
            title = table.Column<string>(type: "text", nullable: true),
            description = table.Column<string>(type: "text", nullable: true),
            tags = table.Column<string>(type: "text", nullable: true),
            url = table.Column<string>(type: "text", nullable: true),
            authorid = table.Column<string>(type: "text", nullable: true),
            createdAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
            updatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_announcements", x => x.id);
            table.ForeignKey(
                      name: "FK_announcements_authors_authorid",
                      column: x => x.authorid,
                      principalTable: "authors",
                      principalColumn: "id");
          });

      migrationBuilder.CreateTable(
          name: "proposals",
          columns: table => new
          {
            id = table.Column<string>(type: "text", nullable: false),
            price = table.Column<int>(type: "integer", nullable: false),
            description = table.Column<string>(type: "text", nullable: true),
            announcementid = table.Column<string>(type: "text", nullable: true),
            createdAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
            updatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_proposals", x => x.id);
            table.ForeignKey(
                      name: "FK_proposals_announcements_announcementid",
                      column: x => x.announcementid,
                      principalTable: "announcements",
                      principalColumn: "id");
          });

      migrationBuilder.CreateTable(
          name: "jobs",
          columns: table => new
          {
            id = table.Column<string>(type: "text", nullable: false),
            status = table.Column<int>(type: "integer", nullable: false),
            proposalid = table.Column<string>(type: "text", nullable: true),
            createdAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
            updatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_jobs", x => x.id);
            table.ForeignKey(
                      name: "FK_jobs_proposals_proposalid",
                      column: x => x.proposalid,
                      principalTable: "proposals",
                      principalColumn: "id");
          });

      migrationBuilder.CreateIndex(
          name: "IX_announcements_authorid",
          table: "announcements",
          column: "authorid");

      migrationBuilder.CreateIndex(
          name: "IX_announcements_url",
          table: "announcements",
          column: "url",
          unique: true);

      migrationBuilder.CreateIndex(
          name: "IX_jobs_proposalid",
          table: "jobs",
          column: "proposalid");

      migrationBuilder.CreateIndex(
          name: "IX_proposals_announcementid",
          table: "proposals",
          column: "announcementid");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.DropTable(
          name: "jobs");

      migrationBuilder.DropTable(
          name: "proposals");

      migrationBuilder.DropTable(
          name: "announcements");

      migrationBuilder.DropTable(
          name: "authors");
    }
  }
}
